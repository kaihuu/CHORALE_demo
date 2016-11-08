using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobiquitous2016App.Utils;

namespace Mobiquitous2016App.Models.GraphModels
{
    public class RModel
    {
        public class R
        {
            public double RForLostEnergy { get; set; }
            public double RForTransitTime { get; set; }
        }

        private RModel()
        {
            
        }

        public static RModel Init(IList<GraphDatum> list)
        {
            var model = new RModel
            {
                ROfConvertLoss = new List<R>
                {
                    new R
                    {
                        RForLostEnergy = list.Average(v => (v.ConvertLoss - list.Average(avg => avg.ConvertLoss)) * (v.LostEnergy - list.Average(avg => avg.LostEnergy))) / (list.StdDev(v => v.ConvertLoss) * list.StdDev(v => v.LostEnergy)),
                        RForTransitTime = list.Average(v => (v.ConvertLoss - list.Average(avg => avg.ConvertLoss)) * (v.TransitTime - list.Average(avg => avg.TransitTime))) / (list.StdDev(v => v.ConvertLoss) * list.StdDev(v => v.TransitTime))
                    }
                },
                ROfAirResistance = new List<R>
                {
                    new R
                    {
                        RForLostEnergy = list.Average(v => (v.AirResistance - list.Average(avg => avg.AirResistance)) * (v.LostEnergy - list.Average(avg => avg.LostEnergy))) / (list.StdDev(v => v.AirResistance) * list.StdDev(v => v.LostEnergy)),
                        RForTransitTime = list.Average(v => (v.AirResistance - list.Average(avg => avg.AirResistance)) * (v.TransitTime - list.Average(avg => avg.TransitTime))) / (list.StdDev(v => v.AirResistance) * list.StdDev(v => v.TransitTime))
                    }
                },
                ROfRollingResistace = new List<R>
                {
                    new R
                    {
                        RForLostEnergy = list.Average(v => (v.RollingResistance - list.Average(avg => avg.RollingResistance)) * (v.LostEnergy - list.Average(avg => avg.LostEnergy))) / (list.StdDev(v => v.RollingResistance) * list.StdDev(v => v.LostEnergy)),
                        RForTransitTime = list.Average(v => (v.RollingResistance - list.Average(avg => avg.RollingResistance)) * (v.TransitTime - list.Average(avg => avg.TransitTime))) / (list.StdDev(v => v.RollingResistance) * list.StdDev(v => v.TransitTime))
                    }
                },
                ROfRegeneLoss = new List<R>
                {
                    new R
                    {
                        RForLostEnergy = list.Average(v => (v.RegeneLoss - list.Average(avg => avg.RegeneLoss)) * (v.LostEnergy - list.Average(avg => avg.LostEnergy))) / (list.StdDev(v => v.RegeneLoss) * list.StdDev(v => v.LostEnergy)),
                        RForTransitTime = list.Average(v => (v.RegeneLoss - list.Average(avg => avg.RegeneLoss)) * (v.TransitTime - list.Average(avg => avg.TransitTime))) / (list.StdDev(v => v.RegeneLoss) * list.StdDev(v => v.TransitTime))
                    }
                }
            };

            return model;
        }

        public IList<R> ROfConvertLoss { get; set; }
        public IList<R> ROfAirResistance { get; set; }
        public IList<R> ROfRollingResistace { get; set; }
        public IList<R> ROfRegeneLoss { get; set; }
    }
}
