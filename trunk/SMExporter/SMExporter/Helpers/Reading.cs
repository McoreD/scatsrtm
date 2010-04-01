using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMExporter
{
    public class Reading
    {

        public List<Approach> Approaches { get; set; }
        public double PlansVoted { get; set; }
        public int CycleLengthNominal { get; set; }
        public int CycleLengthActual { get; set; }
        public int CycleLengthRequired { get; set; }
        public int CycleLengthSA { get; set; }
        public string PlansActive { get; set; }
        public string Time { get; set; }
        public string DayRead { get; set; }
        public string DateRead { get; set; }
        public string PhaseA { get; set; }
        public string PhaseB { get; set; }
        public string PhaseC { get; set; }
        public string PhaseD { get; set; }
        public string PhaseE { get; set; }
        public string PhaseF { get; set; }
        public string PhaseG { get; set; }

        public Reading()
        {
            Approaches = new List<Approach>();
        }
    }
}
