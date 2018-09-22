using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyData {

    //define the address and description of the register to be read
    class Register {

        public String description { get; set; }
        public ushort register { get; set; }    //modbus address
        public ushort numInputs { get; set; }   //register size
        public String meterModel { get; set; }  //manufacturer model
        
        public Register(String description, ushort register, ushort numInputs, String meterModel) {
            this.description = description;
            this.register = register;
            this.numInputs = numInputs;
            this.meterModel = meterModel;
        }

    }
}
