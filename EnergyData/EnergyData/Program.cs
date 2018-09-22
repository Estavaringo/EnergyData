
using System.Collections.Generic;
using System.Threading;

namespace EnergyData
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates the list of registers that you can retrieve from the meter
            List<Register> registers = new List<Register>();
            registers.Add(new Register("V", 1012, 2, "PM710"));
            registers.Add(new Register("KW", 1004, 2, "PM710"));
            registers.Add(new Register("A", 1016, 2, "PM710"));
            registers.Add(new Register("FP", 1010, 2, "PM710"));
            registers.Add(new Register("V", 1034, 2, "CCK4100S"));
            registers.Add(new Register("Hz", 1082, 2, "CCK4100S"));

            //List of meters 
            List<EnergyMeter> metersSE88 = new List<EnergyMeter>();
            metersSE88.Add(new EnergyMeter(1, "10.179.33.88", 11, "CCK4100S", "Linha 1 - Geral", true));
            metersSE88.Add(new EnergyMeter(152, "10.179.33.89", 186, "PM710", "Cubículo 1 - SE3", true));
            metersSE88.Add(new EnergyMeter(154, "10.179.33.89", 187, "PM710", "Cubículo 2 - SE1 SE5", true));
            metersSE88.Add(new EnergyMeter(155, "10.179.33.89", 188, "PM710", "Cubículo 3 - SE6", true));
            metersSE88.Add(new EnergyMeter(156, "10.179.33.89", 189, "PM710", "Cubículo 4 - Geral Trafo 2", false));
            metersSE88.Add(new EnergyMeter(157, "10.179.33.89", 190, "PM710", "Cubículo 7 - SE4", true));
            metersSE88.Add(new EnergyMeter(158, "10.179.33.89", 191, "PM710", "Cubículo 8 - SE1", true));
            metersSE88.Add(new EnergyMeter(159, "10.179.33.89", 192, "PM710", "Cubículo 9 - SE5", true));
            metersSE88.Add(new EnergyMeter(160, "10.179.33.89", 193, "PM710", "Cubículo 10 - Geral Trafo 1", false));


            List<EnergyMeter> metersMainLine = new List<EnergyMeter>();
            metersMainLine.Add(new EnergyMeter(1, "10.179.33.88", 11, "CCK4100S", "Linha 1 - Geral", true));

            //creates thread thats visualize the data
            View view = new View(metersSE88, "SUBESTAÇÃO 88", registers);
            Thread threadView = new Thread(view.Run);
            threadView.Start();


            Alarm alarm = new Alarm(metersMainLine, registers);
            Thread threadAlarm = new Thread(alarm.Run);
            threadAlarm.Priority = ThreadPriority.Highest;
            threadAlarm.Start();

        }      
    }
}
