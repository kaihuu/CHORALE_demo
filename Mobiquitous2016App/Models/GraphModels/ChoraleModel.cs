using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobiquitous2016App.Utils;

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
        public IList<SurfaceData> SurfaceDataList { get; set; }

        private ChoraleModel()
        {
            
        }

        public static ChoraleModel Init(IList<GraphDatum> list)
        {
            var model = new ChoraleModel
            {
                ClassNumber = MathUtil.CalculateClassNumber((IList) list),
                MinLostEnegry = list.Min(d => d.LostEnergy),
                MaxLostEnergy = list.Max(d => d.LostEnergy),
                ClassWidthEnergy = (list.Max(d => d.LostEnergy) - list.Min(d => d.LostEnergy)) / MathUtil.CalculateClassNumber((IList) list),
                MinTransitTime = list.Min(d => d.TransitTime),
                MaxTransitTime = list.Max(d => d.TransitTime),
                ClassWidthTransitTime = (float)(list.Max(d => d.TransitTime) - list.Min(d => d.TransitTime)) / MathUtil.CalculateClassNumber((IList) list)
            };
            model.SetData(list);

            return model;
        }

        private void SetData(IList<GraphDatum> list)
        {
            Data = new double[ClassNumber + 1, ClassNumber + 1];
            SurfaceDataList = new List<SurfaceData>();

            double preTimeLevel = 0;
            double currentTimeLevel = MinTransitTime;

            for (int i = 0; i < ClassNumber + 1; i++)
            {
                double preEnergyLevel = 0;
                double currentEnergyLevel = MinLostEnegry;

                for (int j = 0; j < ClassNumber + 1; j++)
                {
                    // ReSharper disable once ReplaceWithSingleCallToCount
                    Data[i, j] = list
                        .Where(d => d.LostEnergy > preEnergyLevel)
                        .Where(d => d.LostEnergy <= currentEnergyLevel)
                        .Where(d => d.TransitTime > preTimeLevel)
                        .Where(d => d.TransitTime <= currentTimeLevel)
                        .Count();

                    SurfaceDataList.Add(new SurfaceData
                    {
                        X = currentTimeLevel,
                        Z = currentEnergyLevel,
                        Y = Data[i,j]
                    });

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
