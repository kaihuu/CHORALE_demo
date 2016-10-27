using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquitous2016App.Models.EcologModels
{
    public class Link
    {
        public int Num { get; set; }

        public string LinkId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string NodeId { get; set; }

        public string Direction { get; set; }
    }
}
