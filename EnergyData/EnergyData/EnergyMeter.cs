using Modbus;
using Modbus.Device;
using Modbus.Utility;
using System;
using System.Collections.Generic;
using System.IO;
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
                foreach (Register register in registers) {
                    if (register.meterModel == this.model) {
                        using (TcpClient client = new TcpClient(this.ip, 502)) {
                            using (ModbusIpMaster master = ModbusIpMaster.CreateIp(client)) {
                                //master.Transport.ReadTimeout = 300;
                                master.Transport.WaitToRetryMilliseconds = 0;
                                master.Transport.Retries = 1;

                                Measurement medicao = new Measurement(register.description, this.id);

                                ushort startAddress = register.register;
                                ushort[] values = master.ReadHoldingRegisters(this.address, startAddress, register.numInputs);

                                medicao.value = ModbusUtility.GetSingle(values[1], values[0]);

                                if (this.model.Equals("CCK4100S") && register.description.Equals("V"))
                                    medicao.value = medicao.value * (88000 / 63.5);

                                medicao.dateTime = DateTime.Now;
                                measurements.Add(medicao);
                                if (!this.status) this.status = true;
                            }
                        }
                    }
                }
            } catch (IOException e) {
                this.status = false;
                throw new IOException(this.id + " " + this.description + " " + this.model, e);
            } catch (SlaveException e) {
                this.status = false;
                throw new SlaveException(this.id + " " + this.description + " " + this.model, e);
            } catch (SocketException e) { //falha conexão com o IP
                this.status = false;
                throw e;
            }
            return measurements;
        }

    }
}
