using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {
    class Medicao {

        public DateTime dataHora { get; set; }
        public int codigoMedidor { get; set; }
        public String grandezaEletrica { get; set; }
        public float valor { get; set; }

        public Medicao(String grandezaEletrica, int codigoMedidor) {
            this.codigoMedidor = codigoMedidor;
            this.grandezaEletrica = grandezaEletrica;
        }

    }
}
