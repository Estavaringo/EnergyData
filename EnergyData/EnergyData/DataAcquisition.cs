using EnergyData.DAO;
using Modbus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace EnergyData {

    //Acquires the data and inserts into DB
    class DataAcquisition {

        public List<EnergyMeter> meters { get; set; }
        public List<Register> registers { get; set; }
        public List<Measurement> measurements { get; set; }

        public DataAcquisition(List<EnergyMeter> meters, List<Register> registers){
            this.meters = meters;
            this.registers = registers;
        }

        public void Run() {
            while (true) {
                measurements = new List<Measurement>();
                int tries = 0;
                foreach (EnergyMeter meter in this.meters) {
                    if (meter.active) {
                        while (tries < 2) {
                            try { 
                                //insere as medições no BD
                                //foreach (Measurement measurement in meter.getValueOfRegisters(registers))
                                //measurements.Add(measurement);
                                measurements = meter.getValueOfRegisters(registers);
                                MedicaoDAO.insert(measurements);
                                break;
                            } catch (IOException e) {
                                Console.WriteLine(e.Message + e.InnerException.Message + DateTime.Now);
                                //create a log with error messages
                                tries++;
                            } catch (SlaveException e) { //falha de comunicação modbus
                                Console.WriteLine(e.Message + e.InnerException.Message + DateTime.Now);
                                //create a log with error messages
                                tries++;
                            } catch (SocketException e) { //falha conexão com o IP
                                Console.WriteLine(e.Message + e.InnerException.Message + DateTime.Now);
                                //create a log with error messages
                                tries++;
                            }
                        }
                    }
                }
            }
        }
    }
}
