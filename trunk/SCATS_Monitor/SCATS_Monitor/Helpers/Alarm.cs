using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCATS_Monitor
{
    public class Alarm
    {
        public string VMSName { get; set; }
        public string Timestamp { get; set; }
        public string AlarmType { get; set; }
        public string AlarmDescription { get; set; }
    }
}
