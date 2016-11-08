using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.GraphModels;

namespace Mobiquitous2016App.Daos
{
    public class EcologDao
    {
        public static List<GraphDatum> GetGraphDataOnSemanticLink(SemanticLink semanticLink, TripDirection direction)
        {
            using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                connection.Open();

                var query = new StringBuilder();

                query.AppendLine("DECLARE @semantic_link_id INT");
                query.AppendLine("DECLARE @direction VARCHAR(255)");
                query.AppendLine("DECLARE @links_count INT");
                query.AppendLine($"SET @semantic_link_id = {semanticLink.SemanticLinkId}");
                query.AppendLine($"SET @direction = '{direction.Direction}'");
                query.AppendLine("SET @links_count = (");
                query.AppendLine("		SELECT COUNT(*) AS links_count");
                query.AppendLine("		FROM semantic_links");
                query.AppendLine("		WHERE semantic_link_id = @semantic_link_id");
                query.AppendLine("		);");

                query.AppendLine("WITH selected_semantic_link");
                query.AppendLine("AS (");
                query.AppendLine("	SELECT *");
                query.AppendLine("	FROM semantic_links");
                query.AppendLine("	WHERE semantic_link_id = @semantic_link_id");
                query.AppendLine("	)");

                query.AppendLine("	,semantic_link_lat_long");
                query.AppendLine("AS(");
                query.AppendLine("  SELECT MIN(latitude) AS min_lat");
                query.AppendLine("		,MIN(longitude) AS min_long");
                query.AppendLine("		,MAX(latitude) AS max_lat");
                query.AppendLine("		,MAX(longitude) AS max_long");
                query.AppendLine("	FROM links INNER JOIN semantic_links ON links.link_id = semantic_links.link_id");
                query.AppendLine("	WHERE semantic_link_id = @semantic_link_id");
                query.AppendLine("	)");

                query.AppendLine("	,trip_link_count");
                query.AppendLine("AS(");
                query.AppendLine("	SELECT trip_id, COUNT(DISTINCT ecolog.link_id) AS link_count");
                query.AppendLine("	FROM ecolog");
                query.AppendLine("		INNER JOIN selected_semantic_link ON ecolog.link_id = selected_semantic_link.link_id");
                query.AppendLine("	GROUP BY trip_id");
                query.AppendLine("	)");

                query.AppendLine("SELECT ecolog.trip_id AS TripId");
                query.AppendLine("	,MIN(jst) AS Date");
                query.AppendLine("	,COUNT(*) AS TransitTime");
                query.AppendLine("	,SUM(consumed_electric_energy) AS ConsumedElectricEnergy");
                query.AppendLine("	,SUM(lost_energy) AS LostEnergy");
                query.AppendLine("	,SUM(ABS(regene_loss)) AS RegeneLoss");
                query.AppendLine("	,SUM(ABS(convert_loss)) AS convert_loss");
                query.AppendLine("	,SUM(energy_by_rolling_resistance) AS RollingResistance");
                query.AppendLine("	,SUM(energy_by_air_resistance) AS AirResistance");
                query.AppendLine("FROM ecolog, semantic_link_lat_long, trip_link_count");

                query.AppendLine("WHERE ecolog.driver_id = 1");
                query.AppendLine("  AND (ecolog.car_id = 1 OR ecolog.car_id = 3)");
                query.AppendLine("  AND ecolog.sensor_id = 12");
                query.AppendLine("	AND trip_direction = @Direction");
                //query.AppendLine("  AND JST < '2014-06-16 00:00:00'");
                query.AppendLine("  AND jst > '2013-10-30 00:00:00'");
                query.AppendLine("  AND latitude <= semantic_link_lat_long.max_lat + 0.0001");
                query.AppendLine("  AND LATITUDE >= semantic_link_lat_long.min_lat - 0.0001");
                query.AppendLine("	AND LONGITUDE <= semantic_link_lat_long.max_long");
                query.AppendLine("	AND LONGITUDE >= semantic_link_lat_long.min_long");
                query.AppendLine("	AND ecolog.trip_id = trip_link_count.trip_id");
                query.AppendLine("	AND trip_link_count.link_count > (@links_count * 0.8)");

                query.AppendLine("GROUP BY ecolog.trip_id");

                var list = connection.Query<GraphDatum>(query.ToString()).ToList();

                Console.WriteLine("List count: " + list.Count);
                
                return list;
            }
        }
    }
}
