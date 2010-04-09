using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCATS_Monitor
{
    public class Controller
    {
        public string LM { get; set; }
        public string IntersectionName { get; set; }
        public string ControllerType { get; set; }
        public string SCATSFault { get; set; }
        public DateTime TimeOfFault { get; set; }
        public string IPAddress { get; set; }
        public DateTime CommsTimeChk { get; set; }
        public string PossibleFault { get; set; }
    }
}
