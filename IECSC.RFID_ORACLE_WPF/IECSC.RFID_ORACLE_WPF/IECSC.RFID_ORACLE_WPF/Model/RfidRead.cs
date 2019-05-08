using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IECSC.RFID_ORACLE_WPF
{
    public class RfidRead
    {
        public string IPAddress { get; set; }
        public string LocNo { get; set; }
        public string PortNo { get; set; }
        public string ReadInfo { get; set; }
        public string ReadTime { get; set; }
        public bool IsConnected { get; set; } = false;
    }
}
