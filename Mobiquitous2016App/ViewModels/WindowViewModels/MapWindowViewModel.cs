using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Dapper.FluentMap;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using Mobiquitous2016App.Models;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.MapModels;
using Mobiquitous2016App.ObjectRelationalMaps;

namespace Mobiquitous2016App.ViewModels.WindowViewModels
{
    public class MapWindowViewModel : ViewModel
    {
        #region SemanticLinks変更通知プロパティ
        private IList<SemanticLink> _SemanticLinks;

        public IList<SemanticLink> SemanticLinks
        {
            get
            { return _SemanticLinks; }
            set
            {
                if (_SemanticLinks == value)
                    return;
                _SemanticLinks = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public delegate void InvokeScriptDelegate(string scriptName, params object[] args);
        public MapHost MapHost { get; private set; }
        public string Uri { get; set; }
        public InvokeScriptDelegate InvokeScript { get; set; }

        public void Initialize()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new LinkMap());
                config.AddMap(new SemanticLinkMap());
            });

            SemanticLinks = SemanticLink.OutwardSemanticLinks;

            Uri = $"file://{AppDomain.CurrentDomain.BaseDirectory}Resources\\index.html";
            MapHost = new MapHost()
            {
                MapWindowViewModel = this
            };

            DrawSemanticLinkLines();
        }

        public void DrawSemanticLinkLines()
        {
            foreach (var semanticLink in SemanticLinks)
            {
                for (int i = 0; i < semanticLink.Links.Count - 1; i++)
                {
                    InvokeScript("addCircle",
                        semanticLink.Links[i].Latitude,
                        semanticLink.Links[i].Longitude);

                    if (semanticLink.Links[i].LinkId.Equals(semanticLink.Links[i + 1].LinkId))

                        InvokeScript("addLine",
                            semanticLink.SemanticLinkId,
                            semanticLink.Links[i].Latitude,
                            semanticLink.Links[i].Longitude,
                            semanticLink.Links[i + 1].Latitude,
                            semanticLink.Links[i + 1].Longitude);
                }
            }

            InvokeScript("moveMap", SemanticLinks.Average(v => v.Links.Average(l => l.Latitude)), SemanticLinks.Average(v => v.Links.Average(l => l.Longitude)));
        }
    }
}
