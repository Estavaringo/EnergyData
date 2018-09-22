using Modbus.Device;
using Modbus.Utility;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace EnergyData {

    class EnergyMeter {

        public int id { get; set; }         
        public String ip { get; set; }      //IP address (modbus TCP protocol)
        public byte address { get; set; }   //modbus address
        public String model { get; set; }   //manufacturer model
        public String description { get; set; }
        public bool status { get; set; }    //flag OK
        public bool active { get; set; }

        public EnergyMeter(int codigo, String ip, byte endereco, String modelo, String descricao, bool ativo){
            this.id = codigo;
            this.ip = ip;
            this.address = endereco;
            this.model = modelo;
            this.description = descricao;
            this.active = ativo;
            this.status = true;
        }


        public bool checkCon() {
            throw new Exception("Not implemented yet");
        }

        //return register values specified by types
        public List<Measurement> getValueOfRegisters(List<Register> registers) {
            
            List<Measurement> measurements = new List<Measurement>();
            try {
                using (TcpClient client = new TcpClient(this.ip, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                    foreach (Register register in registers) {

                        if (register.meterModel == this.model) {

                            Measurement medicao = new Measurement(register.description, this.id);

                            ushort startAddress = register.register;
                            ushort[] values = master.ReadHoldingRegisters(this.address, startAddress, register.numInputs);
                            
                            medicao.value = ModbusUtility.GetSingle(values[1], values[0]);

                            if (this.model.Equals("CCK4100S") && register.description.Equals("V"))
                                medicao.value = medicao.value * (92000 / 66);
                            
                            medicao.dateTime = DateTime.Now;
                            measurements.Add(medicao);
                            if (!this.status) this.status = true;
                        }
                    }
                }
            } catch (Exception e) {
                this.status = false;
                
                throw new Exception(this.id + " " + this.description + " " + this.model, e);
            }
            return measurements;
        }

    }
}
