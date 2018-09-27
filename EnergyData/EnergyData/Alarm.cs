using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//monitor value and perform some action
namespace EnergyData {
    class Alarm {
        List<EnergyMeter> meters; List<Register> registers;
        float limitMin = 87000;

        public Alarm(List<EnergyMeter> meters, List<Register> registers) {
            this.meters = meters;
            this.registers = registers;
        }
        public void Run() {
            while (true) {
                List<Measurement> medicoes = new List<Measurement>();
                foreach (EnergyMeter meter in meters) {
                    if (meter.active) {
                      /*  try {
                            //foreach (Measurement medicao in meter.getValueOfRegisters(registers)) {
                             //   if (medicao.magnitude.Equals("V") && medicao.value < limitMin) {
                                    
                            //    }
                            }
                        } catch (Exception e) {
                            Console.Write(e.Message + e.InnerException.Message);
                            continue;
                        }*/
                    }
                }
            }
        }
    }
}
