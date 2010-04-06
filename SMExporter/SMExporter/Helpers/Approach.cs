using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMExporter
{
    public class Approach
    {
        public string PhaseId { get; set; }
        public int PhaseTime { get; set; }
        public int Intersection { get; set; }
        public List<Lane> Lanes { get; set; }
        public int ADS { get; set; }
        public string SAorLK { get; set; }

        public Approach()
        {
            Lanes = new List<Lane>();
        }

        public double GetVehPerLanePerHourPT(Lane lane)
        {
            double ans = 0.0;
            if (PhaseTime > 0)
            {
                ans = 3600 * lane.VO / PhaseTime;
            }
            return Math.Round(ans, 2);
        }

        public double GetCongestionIndex(Reading reading, Lane lane)
        {
            double ans = 0.0;
            if (lane.Capacity > 0)
            {
                ans = GetVehPerLanePerHourPT(lane) / lane.Capacity;
            }
            return Math.Round(ans, 4);
        }
    }
}
