using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData
{
    class Program
    {
        static void Main(string[] args)
        {

            List<RegisterType> registers = new List<RegisterType>();

            registers.Add(new RegisterType("V", 1012, 2, "PM710"));
            registers.Add(new RegisterType("KW", 1004, 2, "PM710"));
            registers.Add(new RegisterType("A", 1016, 2, "PM710"));
            registers.Add(new RegisterType("FP", 1010, 2, "PM710"));

            List<EnergyMeter> meters = new List<EnergyMeter>();

            meters.Add(new EnergyMeter(152, "10.179.33.89", 186, "PM710", "Cubículo 1 - SE3", true));
            meters.Add(new EnergyMeter(154, "10.179.33.89", 187, "PM710", "Cubículo 2 - SE1 SE5", true));
            meters.Add(new EnergyMeter(155, "10.179.33.89", 188, "PM710", "Cubículo 3 - SE6", true));
            meters.Add(new EnergyMeter(156, "10.179.33.89", 189, "PM710", "Cubículo 4 - Geral Trafo 2", false));
            meters.Add(new EnergyMeter(157, "10.179.33.89", 190, "PM710", "Cubículo 7 - SE4", true));
            meters.Add(new EnergyMeter(158, "10.179.33.89", 191, "PM710", "Cubículo 8 - SE1", true));
            meters.Add(new EnergyMeter(159, "10.179.33.89", 192, "PM710", "Cubículo 9 - SE5", true));
            meters.Add(new EnergyMeter(160, "10.179.33.89", 193, "PM710", "Cubículo 10 - Geral Trafo 1", false));

            while (true) { 
                Console.WriteLine("\t\t SUBESTAÇÃO 88");

                foreach (EnergyMeter meter in meters) {

                    if (meter.active) {
                        Console.WriteLine(meter.description);

                        try {
                            List<Medicao> medicoes = new List<Medicao>();

                            foreach (RegisterType register in registers) {
                                if (register.meterModel.Equals(meter.modelo)) medicoes.Add(meter.getValueOfRegister(register));
                            }

                            foreach (Medicao medicao in medicoes) {
                                Console.WriteLine(medicao);
                            }

                        } catch (Exception e) {
                            Console.WriteLine("FALHA DE COMUNICAÇÃO COM O MEDIDOR ");
                        }
                    }


                    Console.WriteLine("");
                }
                Console.ReadLine();
                Console.Clear();
            }

        }
    }
}
