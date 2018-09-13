using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {

    class MedidorDeEnergia {

        int codigo { get; set; }
        String ip { get; set; }
        int endereco { get; set; }
        String modelo { get; set; }
        String status { get; set; }

        public bool checkCon() {
            return false;
        }

        public Medicao getTensao() {
            Medicao med = new Medicao("V", this.codigo);
            //TO DO CODE HERE
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
            //TO DO CODE HERE
            return med;
        }

        public Medicao getCorrente() {
            Medicao med = new Medicao("A", this.codigo);
            //TO DO CODE HERE
            return med;
        }

        public Medicao getFatorPot() {
            Medicao med = new Medicao("FP", this.codigo);
            //TO DO CODE HERE
            return med;
        }

    }
}
