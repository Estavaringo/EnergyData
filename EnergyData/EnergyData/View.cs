using System;
using System.Collections.Generic;

namespace EnergyData {

    //show the readings in real time, just for test only
    class View {
        List<EnergyMeter> meters; String local; List<Register> registers;

        public View(List<EnergyMeter> meters, String local, List<Register> registers) {
            this.meters = meters;
            this.local = local;
            this.registers = registers;
        }

        public void Run() {
            while (true) {
                List<Measurement> measurements = new List<Measurement>();
                foreach (EnergyMeter meter in meters) {
                    if (meter.active) {
                        try {
                            foreach (Measurement measurement in meter.getValueOfRegisters(registers))
                                measurements.Add(measurement);
                        } catch (Exception e) {
                            Console.Write(e.Message + e.InnerException.Message + DateTime.Now);
                            //create a log with error messages
                            continue;
                        }
                    }
                }
                String outp = "\t\t" + local + "\t" + "\n\n";
                foreach (EnergyMeter meter in meters) {
                    if (meter.active) {
                        outp = outp + "\t" + meter.description + "\n";
                        if (meter.status) {
                            foreach (Measurement medicao in measurements) {
                                if (medicao.meterID == meter.id) outp = outp + "\t\t" + medicao.ToString() + "\n";
                            }
                        } else {
                            outp = outp + "\t FALHA DE COMUNICAÇÃO COM O MEDIDOR \n";
                        }
                    }
                    outp = outp + "\n";
                }
                Console.Clear();
                Console.WriteLine(outp);
            }
        }
    }
}
