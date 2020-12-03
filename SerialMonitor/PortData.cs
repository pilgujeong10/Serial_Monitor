using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialMonitor
{
    class PortData
    {
        public string Port { get; set; }
        public string Baudrate { get; set; }
        public PortData(string port, string baudrate)
        {
            this.Port = port;
            this.Baudrate = baudrate;
        }
    }
}
