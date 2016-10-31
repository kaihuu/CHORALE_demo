using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.FluentMap.Mapping;
using Mobiquitous2016App.Models.EcologModels;

namespace Mobiquitous2016App.ObjectRelationalMaps
{
    public class LinkMap : EntityMap<Link>
    {
        public LinkMap()
        {
            Map(p => p.LinkId).ToColumn("link_id", false);
            Map(p => p.NodeId).ToColumn("node_id", false);
            Map(p => p.Direction).ToColumn("direction", false);
        }
    }
}
