using Modbus.Device;
using Modbus.Utility;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace EnergyData {

    class EnergyMeter {

        public int codigo { get; set; }
        public String ip { get; set; }
        public byte endereco { get; set; }
        public String modelo { get; set; }
        public String description { get; set; }
        public bool status { get; set; }    //flag medidor OK ou não
        public bool active { get; set; }

        public EnergyMeter(int codigo, String ip, byte endereco, String modelo, String descricao, bool ativo){
            this.codigo = codigo;
            this.ip = ip;
            this.endereco = endereco;
            this.modelo = modelo;
            this.description = descricao;
            this.active = ativo;
            this.status = true;
        }

        public bool checkCon() {
            return false;
        }

        //retorna o valor do registrador especificado por type
        public List<Medicao> getValueOfRegisters(List<RegisterType> types) {
            

            List<Medicao> medicoes = new List<Medicao>();
            try {
                using (TcpClient client = new TcpClient(this.ip, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                    foreach (RegisterType registerType in types) {

                        if (registerType.meterModel == this.modelo) {

                            Medicao medicao = new Medicao(registerType.description, this.codigo);

                            ushort startAddress = registerType.register;
                            ushort[] registers = master.ReadHoldingRegisters(this.endereco, startAddress, registerType.numInputs);

                            //throw new Exception("Medidor Offline");
                            
                            medicao.valor = ModbusUtility.GetSingle(registers[1], registers[0]);
                            medicao.dataHora = DateTime.Now;
                            medicoes.Add(medicao);
                            if (!this.status) this.status = true;
                        }
                    }
                }
            } catch (Exception e) {
                this.status = false;
                throw e;
            }
            return medicoes;
        }

    }
}
