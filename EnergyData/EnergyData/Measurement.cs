using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {
    class Measurement {

        public DateTime dateTime { get; set; }
        public int meterID { get; set; }
        public String magnitude { get; set; }
        public double value { get; set; }

        public Measurement(String grandezaEletrica, int codigoMedidor) {
            this.meterID = codigoMedidor;
            this.magnitude = grandezaEletrica;
        }

        public Measurement() {
        }

        public override string ToString() {
            return this.value + " " + magnitude + " - "+ this.dateTime.TimeOfDay;
        }
    }
}
