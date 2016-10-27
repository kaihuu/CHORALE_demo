using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.FluentMap.Mapping;
using Mobiquitous2016App.Models.EcologModels;

namespace Mobiquitous2016App.ObjectRelationalMaps
{
    public class SemanticLinkMap : EntityMap<SemanticLink>
    {
        public SemanticLinkMap()
        {
            Map(p => p.SemanticLinkId).ToColumn("semantic_link_id");
            Map(p => p.DriverId).ToColumn("driver_id");
            Map(p => p.Semantics).ToColumn("semantics");
        }
    }
}
