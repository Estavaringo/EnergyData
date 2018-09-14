using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EnergyData {
    class Aquisitor {

        private EnergyMeter medidorDeEnergia;
        private ushort registrador;    
        private ushort quantidade;

        public Aquisitor(EnergyMeter medidorDeEnergia, ushort registrador, ushort quantidade) {
            this.medidorDeEnergia = medidorDeEnergia;
            this.registrador = registrador;
            this.quantidade = quantidade;
        }


        //executa a conexão com o equipamento e retorna o valor do registrador buscado
        //NECESSARIO LANÇAR EXCESSÃO QUANDO FALHAR A CONEXÃO
        public float Executa() {
            float res;
            try{
                using (TcpClient client = new TcpClient(this.medidorDeEnergia.ip, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);


                    ushort startAddress = this.registrador--;
                    ushort[] registers = master.ReadHoldingRegisters(this.medidorDeEnergia.endereco, startAddress, quantidade);

                    //throw new Exception("Medidor Offline");
            
                res = this.ToFloat(registers);
                }
            }catch (Exception e){
                throw e;
            }
            return res;
        }

        //converte o valor do registrador(32 bit) para float
        public float ToFloat(ushort[] registers) {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(registers[0] & 0xFF);
            bytes[1] = (byte)(registers[0] >> 8);
            bytes[2] = (byte)(registers[1] & 0xFF);
            bytes[3] = (byte)(registers[1] >> 8);
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
