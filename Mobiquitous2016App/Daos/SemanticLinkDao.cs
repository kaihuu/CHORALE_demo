using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.FluentMap;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.ObjectRelationalMaps;

namespace Mobiquitous2016App.Daos
{
    public class SemanticLinkDao
    {
        public static List<SemanticLink> GetOutwardSemanticLinks()
        {
            using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                connection.Open();
                
                var query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("  DISTINCT semantic_link_id,");
                query.AppendLine("  driver_id,");
                query.AppendLine("  semantics");
                query.AppendLine("FROM semantic_links");
                query.AppendLine("WHERE (semantic_link_id >= 187");
                query.AppendLine("  AND semantic_link_id <= 196)");
                query.AppendLine("  OR semantic_link_id = 123");
                query.AppendLine("  OR semantic_link_id = 12");
                query.AppendLine("  OR semantic_link_id = 13");
                query.AppendLine("  OR semantic_link_id = 198");
                query.AppendLine("  OR semantic_link_id = 199");
                query.AppendLine("  OR semantic_link_id = 1");
                query.AppendLine("  OR semantic_link_id = 16");
                query.AppendLine("  OR semantic_link_id = 19");
                query.AppendLine("ORDER BY semantic_link_id");

                var list = connection.Query<SemanticLink>(query.ToString()).ToList();
                list.ForEach(s => s.Links = LinkDao.GetLinksOfSemanticLink(s));

                list.ForEach(s => Debug.WriteLine($"Links.Count: {s.SemanticLinkId}"));

                return list;
            }
        }

        public static List<SemanticLink> GetHomewardSemanticLinks()
        {
            using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                connection.Open();
                
                var query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("  DISTINCT semantic_link_id,");
                query.AppendLine("  driber_id,");
                query.AppendLine("  semantics");
                query.AppendLine("FROM semantic_links");
                query.AppendLine("WHERE (semantic_link_id >= 202");
                query.AppendLine("  AND semantic_link_id <= 218)");
                query.AppendLine("  OR semantic_link_id = 155");

                var list = connection.Query<SemanticLink>(query.ToString()).ToList();
                list.ForEach(s => s.Links = LinkDao.GetLinksOfSemanticLink(s));

                return list;
            }
        }
    }
}
