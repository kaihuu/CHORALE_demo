using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.FluentMap;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.ObjectRelationalMaps;

namespace Mobiquitous2016App.Daos
{
    public class LinkDao
    {

        public static List<Link> GetLinksOfSemanticLink(SemanticLink semanticLink)
        {
            using (var connection = new SqlConnection(DatabaseConnection.ConnectionString))
            {
                connection.Open();
                FluentMapper.Initialize(config => config.AddMap(new LinkMap()));

                var query = new StringBuilder();
                query.AppendLine("SELECT links.*");
                query.AppendLine("FROM semantic_links");
                query.AppendLine("  INNER JOIN links ON semantic_links.link_id = links.link_id");
                query.AppendLine("WHERE semantic_link_id = " + semanticLink.SemanticLinkId);
                query.AppendLine("ORDER BY link_id, num");

                return connection.Query<Link>(query.ToString()).ToList();
            }
        }
    }
}
