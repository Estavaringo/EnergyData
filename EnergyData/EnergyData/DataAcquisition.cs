using System;
using System.Collections.Generic;

namespace EnergyData {

    //Acquires the data and insets into DB
    class DataAcquisition {

        public List<EnergyMeter> meters { get; set; }
        public List<RegisterType> registers { get; set; }
        public List<Medicao> medicoes { get; set; }

        public DataAcquisition(List<EnergyMeter> meters, List<RegisterType> registers){
            this.meters = meters;
            this.registers = registers;
        }

        public void Run() {
            while (true) {
                medicoes = new List<Medicao>();
                foreach (EnergyMeter meter in this.meters) {
                    if (meter.active) {
                        try {
                            foreach(Medicao medicao in meter.getValueOfRegisters(registers))
                            medicoes.Add(medicao);
                            
                        } catch (Exception e) {
                            continue;
                        }
                    }
                }
            }
        }
    }
}
