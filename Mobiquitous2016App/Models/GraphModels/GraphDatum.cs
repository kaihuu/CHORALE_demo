using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquitous2016App.Models.GraphModels
{
    public class GraphDatum
    {
        public int TripId { get; set; }
        public DateTime Date { get; set; }
        public float ConsumedElectricEnergy { get; set; }
        public float LostEnergy { get; set; }
        public float ConvertLoss { get; set; }
        public float AirResistance { get; set; }
        public float RollingResistance { get; set; }
        public float RegeneLoss { get; set; }
        public int TransitTime { get; set; }
        public int Zero => 0;
    }
}
