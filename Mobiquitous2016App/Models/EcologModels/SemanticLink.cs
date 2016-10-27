using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquitous2016App.Models.EcologModels
{
    public class SemanticLink
    {
        public int SemanticLinkId { get; set; }
        public int DriverId { get; set; }
        public List<Link> Links { get; set; }
        public string Semantics { get; set; }

        public SemanticLink()
        {
            Links = new List<Link>();
        }
    }
}
