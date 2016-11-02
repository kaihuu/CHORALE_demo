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
        public double[,] Data { get; set; }

        public void SetData(IList<GraphDatum> list)
        {
            Data = new double[ClassNumber, ClassNumber];

            double preTimeLevel = 0;
            double currentTimeLevel = MinTransitTime;

            for (int i = 0; i < ClassNumber; i++)
            {
                double preEnergyLevel = 0;
                double currentEnergyLevel = MinLostEnegry;

                for (int j = 0; j < ClassNumber; j++)
                {
                    // ReSharper disable once ReplaceWithSingleCallToCount
                    Data[i, j] = list
                        .Where(d => d.LostEnergy > preEnergyLevel)
                        .Where(d => d.LostEnergy <= currentEnergyLevel)
                        .Where(d => d.TransitTime > preTimeLevel)
                        .Where(d => d.TransitTime <= currentTimeLevel)
                        .Count();

                    if (j == 0)
                    {
                        preEnergyLevel = MinLostEnegry;
                    }
                    else
                    {
                        preEnergyLevel += ClassWidthEnergy;
                    }

                    currentEnergyLevel += ClassWidthEnergy;
                }

                if (i == 0)
                {
                    preTimeLevel = MinTransitTime;
                }
                else
                {
                    preTimeLevel += ClassWidthTransitTime;
                }

                currentTimeLevel += ClassWidthTransitTime;
            }
        }
    }
}
