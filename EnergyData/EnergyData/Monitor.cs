using System;
using System.Collections.Generic;

namespace EnergyData {

    //show the readings in real time, just for test only
    class Monitor {

        public static void Run(List<EnergyMeter> meters, String local, List<RegisterType> registers) {
            while (true) {
                List<Medicao> medicoes = new List<Medicao>();
                foreach (EnergyMeter meter in meters) {
                    if (meter.active) {
                        try {
                            foreach (Medicao medicao in meter.getValueOfRegisters(registers))
                                medicoes.Add(medicao);
                        } catch (Exception e) {
                              continue;
                        }
                    }
                }
                String outp = "\t\t" + local + "\t" + "\n\n";
                foreach (EnergyMeter meter in meters) {
                    if (meter.active) {
                        outp = outp + "\t" + meter.description + "\n";
                        if (meter.status) {
                            foreach (Medicao medicao in medicoes) {
                                if (medicao.codigoMedidor == meter.codigo) outp = outp + "\t\t" + medicao.ToString() + "\n";
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
