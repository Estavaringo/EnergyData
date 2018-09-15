using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {

    class EnergyMeter {

        public int codigo { get; set; }
        public String ip { get; set; }
        public byte endereco { get; set; }
        public String modelo { get; set; }
        public String description { get; set; }
        public bool status { get; set; }    //flag medidor OK ou não
        public bool active { get; set; }

        public EnergyMeter(int codigo, String ip, byte endereco, String modelo, String descricao, bool ativo){
            this.codigo = codigo;
            this.ip = ip;
            this.endereco = endereco;
            this.modelo = modelo;
            this.description = descricao;
            this.active = ativo;
            this.status = true;
        }

        public bool checkCon() {
            return false;
        }

        //retorna o valor do registrador especificado por type
        public List<Medicao> getValueOfRegisters(List<RegisterType> types) {
            
            Acquisitor acquisitor = null;
            List<Medicao> medicoes = null;

            //constroi o aquisitor com o endereço e tamanho do registrador
            acquisitor = new Acquisitor(this, types);


            try {
                medicoes = acquisitor.Executa();
            } catch (Exception e) {
                this.status = false;
                throw e;
            }
            

            return medicoes;
        }

    }
}
