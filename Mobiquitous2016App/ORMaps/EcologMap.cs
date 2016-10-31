using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.FluentMap.Mapping;
using Mobiquitous2016App.Models.EcologModels;

namespace Mobiquitous2016App.ORMaps
{
    public class EcologMap : EntityMap<Ecolog>
    {
        public EcologMap()
        {
            Map(p => p.TripId).ToColumn("trip_id", false);
            Map(p => p.DriverId).ToColumn("driver_id", false);
            Map(p => p.CarId).ToColumn("car_id", false);
            Map(p => p.SensorId).ToColumn("sensor_id", false);
            Map(p => p.DistanceDifference).ToColumn("distance_difference", false);
            Map(p => p.TerrainAltitude).ToColumn("terrain_altitude", false);
            Map(p => p.TerrainAltitudeDifference).ToColumn("terrain_altitude_diffrence", false);
            Map(p => p.LongitudinalAcc).ToColumn("longitudinal_acc", false);
            Map(p => p.LateralAcc).ToColumn("lateral_acc", false);
            Map(p => p.VerticalAcc).ToColumn("vertical_acc", false);
            Map(p => p.EnergyByAirResistance).ToColumn("energy_by_air_resistance", false);
            Map(p => p.EnergyByRollingResistance).ToColumn("energy_by_rolling_resistance", false);
            Map(p => p.EnergyByClimbingResistance).ToColumn("energy_by_climbing_resistance", false);
            Map(p => p.EnergyByAccResistance).ToColumn("energy_by_acc_resistance", false);
            Map(p => p.ConvertLoss).ToColumn("convert_loss", false);
            Map(p => p.RegeneLoss).ToColumn("regene_loss", false);
            Map(p => p.RegeneEnergy).ToColumn("regene_energy", false);
            Map(p => p.LostEnergy).ToColumn("lost_energy", false);
            Map(p => p.ConsumedElectricEnergy).ToColumn("consumed_electric_energy");
            Map(p => p.LostEnergyByWellToWheel).ToColumn("lost_energy_by_well_to_wheel");
            Map(p => p.ConsumedFuel).ToColumn("consumed_fuel", false);
            Map(p => p.ConsumedFuelByWellToWheel).ToColumn("consumed_fuel_by_well_to_wheel");
            Map(p => p.EnergyByEquipment).ToColumn("energy_by_equipment");
            Map(p => p.EnergyByCooling).ToColumn("energy_by_cooling", false);
            Map(p => p.EnergyByHeating).ToColumn("energy_by_heating", false);
            Map(p => p.TripDirection).ToColumn("trip_direction", false);
            Map(p => p.MeshId).ToColumn("mesh_id", false);
            Map(p => p.LinkId).ToColumn("link_id", false);
            Map(p => p.RoadTheta).ToColumn("road_theta", false);
        }
    }
}
