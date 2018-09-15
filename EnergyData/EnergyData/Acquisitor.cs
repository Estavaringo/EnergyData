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
    class Acquisitor {

        private EnergyMeter medidorDeEnergia;
        private List<RegisterType> registers;

        public Acquisitor(EnergyMeter medidorDeEnergia, List<RegisterType> registers) {
            this.medidorDeEnergia = medidorDeEnergia;
            this.registers = registers;
        }


        //executa a conexão com o equipamento e retorna o valor dos registradores buscados
        //NECESSARIO LANÇAR EXCESSÃO QUANDO FALHAR A CONEXÃO
        public List<Medicao> Executa() {
            List<Medicao> medicoes = new List<Medicao>();
            try{
                using (TcpClient client = new TcpClient(this.medidorDeEnergia.ip, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                    foreach (RegisterType registerType in this.registers) {

                        if (registerType.meterModel == this.medidorDeEnergia.modelo) {

                            Medicao medicao = new Medicao(registerType.description, this.medidorDeEnergia.codigo);

                            ushort startAddress = registerType.register;
                            ushort[] registers = master.ReadHoldingRegisters(this.medidorDeEnergia.endereco, startAddress, registerType.numInputs);

                            //throw new Exception("Medidor Offline");

                            medicao.valor = this.ToFloat(registers);
                            medicao.dataHora = DateTime.Now;
                            medicoes.Add(medicao);
                        }
                    }
                }
            }catch (Exception e){
                throw e;
            }
            return medicoes;
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
