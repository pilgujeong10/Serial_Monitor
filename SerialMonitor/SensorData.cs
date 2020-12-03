using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialMonitor
{
    class SensorData
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Value { get; set; }
        public SensorData(string date, string time, string value)
        {
            this.Date = date;
            this.Time = time;
            this.Value = value;
        }
    }
}
