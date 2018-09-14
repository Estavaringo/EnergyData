using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {

    //define qual o numero e a description do registrador que vai ser lido
    class RegisterType {

        public String description { get; set; }
        public ushort register { get; set; }
        public ushort numInputs { get; set; }
        public String meterModel { get; set; }
        
        public RegisterType(String description, ushort register, ushort numInputs, String meterModel) {
            this.description = description;
            this.register = register;
            this.numInputs = numInputs;
            this.meterModel = meterModel;
        }

    }
}
