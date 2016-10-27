using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobiquitous2016App.Daos;

namespace Mobiquitous2016App.Models.EcologModels
{
    public class SemanticLink
    {
        public static IList<SemanticLink> OutwardSemanticLinks => SemanticLinkDao.GetOutwardSemanticLinks();

        public static IList<SemanticLink> HomewardSemanticLinks => SemanticLinkDao.GetHomewardSemanticLinks();

        public int SemanticLinkId { get; set; }
        public int DriverId { get; set; }
        public List<Link> Links { get; set; }
        public string Semantics { get; set; }

        public SemanticLink()
        {
            Links = new List<Link>();
        }

        public void SetLinks()
        {
            Links = LinkDao.GetLinksOfSemanticLink(this);
        }
    }
}
