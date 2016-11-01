using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquitous2016App.Models.GraphModels
{
    public class ChoraleModel
    {
        public int ClassNumber { get; set; }
        public float MinLostEnegry { get; set; }
        public float MaxLostEnergy { get; set; }
        public float ClassWidthEnergy { get; set; }
        public int MinTransitTime { get; set; }
        public int MaxTransitTime { get; set; }
        public float ClassWidthTransitTime { get; set; }
    }
}
