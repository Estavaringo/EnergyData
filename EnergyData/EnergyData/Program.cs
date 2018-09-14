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
            while (true) { 
                Console.WriteLine("\t\t SUBESTAÇÃO 88");

                List<MedidorDeEnergia> medidores = new List<MedidorDeEnergia>();
            
                medidores.Add(new MedidorDeEnergia(152,"10.179.33.89",186,"PM710","Cubículo 1 - SE3",true));
                medidores.Add(new MedidorDeEnergia(154,"10.179.33.89",187,"PM710","Cubículo 2 - SE1 SE5",true));
                medidores.Add(new MedidorDeEnergia(155,"10.179.33.89",188,"PM710","Cubículo 3 - SE6",true));
                medidores.Add(new MedidorDeEnergia(156,"10.179.33.89",189,"PM710","Cubículo 4 - Geral Trafo 2",true));
                medidores.Add(new MedidorDeEnergia(157,"10.179.33.89",190,"PM710","Cubículo 7 - SE4",true));
                medidores.Add(new MedidorDeEnergia(158,"10.179.33.89",191,"PM710","Cubículo 8 - SE1",true));
                medidores.Add(new MedidorDeEnergia(159,"10.179.33.89",192,"PM710","Cubículo 9 - SE5",true));
                medidores.Add(new MedidorDeEnergia(160,"10.179.33.89",193,"PM710","Cubículo 10 - Geral Trafo 1",false));

                foreach (MedidorDeEnergia medidor in medidores) {
                    if (medidor.ativo) {
                        Console.WriteLine(medidor.descricao);
                        try {
                            List<Medicao> medicoes = new List<Medicao>();
                            medicoes.Add(medidor.getTensao());
                            medicoes.Add(medidor.getConsumo());
                            medicoes.Add(medidor.getCorrente());
                            medicoes.Add(medidor.getFatorPot());
                            foreach (Medicao medicao in medicoes) {
                                Console.WriteLine(medicao);
                            }
                        } catch (Exception e) {
                            Console.WriteLine("FALHA DE COMUNICAÇÃO COM O MEDIDOR" + e.Message);
                        }
                    }
                    Console.WriteLine("");
                }
                Console.Read();
                Console.Clear();
            }

        }
    }
}
