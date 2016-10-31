using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquitous2016App.Models.EcologModels
{
    public class Ecolog
    {
        public int TripId { get; set; }
        public int DriverId { get; set; }
        public int CarId { get; set; }
        public int SensorId { get; set; }
        public DateTime Jst { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float Speed { get; set; }
        public float Heading { get; set; }
        public float DistanceDifference { get; set; }
        public float TerrainAltitude { get; set; }
        public float TerrainAltitudeDifference { get; set; }
        public float LongitudinalAcc { get; set; }
        public float LateralAcc { get; set; }
        public float VerticalAcc { get; set; }
        public float EnergyByAirResistance { get; set; }
        public float EnergyByRollingResistance { get; set; }
        public float EnergyByClimbingResistance { get; set; }
        public float EnergyByAccResistance { get; set; }
        public float ConvertLoss { get; set; }
        public float RegeneLoss { get; set; }
        public float RegeneEnergy { get; set; }
        public float LostEnergy { get; set; }
        public float Efficiency { get; set; }
        public float ConsumedElectricEnergy { get; set; }
        public float LostEnergyByWellToWheel { get; set; }
        public float ConsumedFuel { get; set; }
        public float ConsumedFuelByWellToWheel { get; set; }
        public float EnergyByEquipment { get; set; }
        public float EnergyByCooling { get; set; }
        public float EnergyByHeating { get; set; }
        public string TripDirection { get; set; }
        public int MeshId { get; set; }
        public string LinkId { get; set; }
        public float RoadTheta { get; set; }
    }
}
