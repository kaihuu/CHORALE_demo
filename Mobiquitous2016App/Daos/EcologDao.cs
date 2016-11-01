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

                query.AppendLine("WITH selected_semantic_links");
                query.AppendLine("AS (");
                query.AppendLine("	SELECT *");
                query.AppendLine("	FROM semantic_links");
                query.AppendLine("	WHERE semantic_link_id = @semantic_link_id");
                query.AppendLine("	)");

                query.AppendLine("SELECT trip_id AS TripId");
                query.AppendLine("	,MIN(jst) AS Date");
                query.AppendLine("	,COUNT(*) AS TransitTime");
                query.AppendLine("	,SUM(consumed_electric_energy) AS ConsumedElectricEnergy");
                query.AppendLine("	,SUM(lost_energy) AS LostEnergy");
                query.AppendLine("	,SUM(ABS(regene_loss)) AS RegeneLoss");
                query.AppendLine("	,SUM(ABS(convert_loss)) AS convert_loss");
                query.AppendLine("	,SUM(energy_by_rolling_resistance) AS RollingResistance");
                query.AppendLine("	,SUM(energy_by_air_resistance) AS AirResistance");
                query.AppendLine("FROM ecolog");
                query.AppendLine("INNER JOIN selected_semantic_links");
                query.AppendLine($"  ON ecolog.link_id = selected_semantic_links.link_id");

                query.AppendLine("WHERE ecolog.driver_id = 1");
                query.AppendLine("  AND (ecolog.car_id = 1 OR ecolog.car_id = 3)");
                query.AppendLine("  AND ecolog.sensor_id = 12");
                query.AppendLine("	AND trip_direction = @direction");

                query.AppendLine("GROUP BY trip_id");
                query.AppendLine("HAVING COUNT(DISTINCT ecolog.link_id) > (@links_count * 0.75)");
                query.AppendLine("ORDER BY trip_id");

                var list = connection.Query<GraphDatum>(query.ToString()).ToList();
                
                return list;
            }
        }
    }
}
