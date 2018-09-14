using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {

    class MedidorDeEnergia {

        public int codigo { get; set; }
        public String ip { get; set; }
        public byte endereco { get; set; }
        public String modelo { get; set; }
        public String descricao { get; set; }
        public bool status { get; set; }    //flag medidor OK ou não
        public bool ativo { get; set; }

        public MedidorDeEnergia(int codigo, String ip, byte endereco, String modelo, String descricao, bool ativo){
            this.codigo = codigo;
            this.ip = ip;
            this.endereco = endereco;
            this.modelo = modelo;
            this.descricao = descricao;
            this.ativo = ativo;
        }

        public bool checkCon() {
            return false;
        }

        //Retorna o valor de tensão do medidor 
        public Medicao getTensao() {
            Medicao med = new Medicao("V", this.codigo);
            Aquisitor aquisitor = null;

            //constroi o aquisitor com o endereço correspondente ao modelo do medidor
            switch (modelo) {
                case "PM710":
                    aquisitor = new Aquisitor(this, 1012, 2);
                    break;
                default:
                    Console.WriteLine("Data: " + DateTime.Now + "Modelo de medidor não suportado " + this.codigo + " " + this.modelo);
                    throw new Exception("Modelo de medidor não suportado");
                    break;
            }

            try {
                med.valor = aquisitor.Executa();
            }catch(Exception e) {
                this.status = false; 
                throw e;
            }

            this.status = true;
            med.dataHora = DateTime.Now;

            return med;
        }

        /*
        public float getDemanda() {
            Medicao med = new Medicao("instantKW", this.codigo);
            return med;
        }
        */


        public Medicao getConsumo() {
            Medicao med = new Medicao("KW", this.codigo);
            Aquisitor aquisitor = null;

            //constroi o aquisitor com o endereço correspondente ao modelo do medidor
            switch (modelo) {
                case "PM710":
                    aquisitor = new Aquisitor(this, 1006, 2);
                    break;
                default:
                    Console.WriteLine("Data: " + DateTime.Now + "Modelo de medidor não suportado " + this.codigo + " " + this.modelo);
                    throw new Exception("Modelo de medidor não suportado");
                    break;
            }

            try {
                med.valor = aquisitor.Executa();
            } catch (Exception e) {
                this.status = false; 
                throw e;
            }

            this.status = true;
            med.dataHora = DateTime.Now;

            return med;
        }

        public Medicao getCorrente() {
            Medicao med = new Medicao("A", this.codigo);
            Aquisitor aquisitor = null;

            //constroi o aquisitor com o endereço correspondente ao modelo do medidor
            switch (modelo) {
                case "PM710":
                    aquisitor = new Aquisitor(this, 1018, 2);
                    break;
                default:
                    Console.WriteLine("Data: " + DateTime.Now + "Modelo de medidor não suportado " + this.codigo + " " + this.modelo);
                    throw new Exception("Modelo de medidor não suportado");
                    break;
            }

            try {
                med.valor = aquisitor.Executa();
            } catch (Exception e) {
                this.status = false; 
                throw e;
            }

            this.status = true;
            med.dataHora = DateTime.Now;

            return med;
        }

        public Medicao getFatorPot() {
            Medicao med = new Medicao("FP", this.codigo);
            Aquisitor aquisitor = null;

            //constroi o aquisitor com o endereço correspondente ao modelo do medidor
            switch (modelo) {
                case "PM710":
                    aquisitor = new Aquisitor(this, 1010, 2);
                    break;
                default:
                    Console.WriteLine("Data: " + DateTime.Now + "Modelo de medidor não suportado " + this.codigo + " " + this.modelo);
                    throw new Exception("Modelo de medidor não suportado");
                    break;
            }

            try {
                med.valor = aquisitor.Executa();
            } catch (Exception e) {
                this.status = false; 
                throw e;
            }

            this.status = true;
            med.dataHora = DateTime.Now;

            return med;
        }

    }
}
