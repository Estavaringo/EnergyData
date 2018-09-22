using System;
using System.Collections.Generic;

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
                foreach (EnergyMeter meter in this.meters) {
                    if (meter.active) {
                        try {
                            foreach(Measurement medicao in meter.getValueOfRegisters(registers))
                            measurements.Add(medicao);
                            
                        } catch (Exception e) {
                            continue;
                        }
                    }
                }
            }
        }
    }
}
