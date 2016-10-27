using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using Mobiquitous2016App.Models;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.MapModels;

namespace Mobiquitous2016App.ViewModels.WindowViewModels
{
    public class MapWindowViewModel : ViewModel
    {
        #region SemanticLinks変更通知プロパティ
        private List<SemanticLink> _SemanticLinks;

        public List<SemanticLink> SemanticLinks
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

        public delegate void InvokeScript(string scriptName, params object[] args);
        public MapHost MapHost { get; private set; }
        public string Uri { get; set; }
        private readonly InvokeScript _invokeScript;

        public MapWindowViewModel(InvokeScript invokeScript)
        {
            _invokeScript = invokeScript;
        }

        public async void Initialize()
        {
            Uri = $"file://{AppDomain.CurrentDomain.BaseDirectory}Resources\\index.html";
            MapHost = new MapHost()
            {
                MapWindowViewModel = this
            };

            await Task.Run(() =>
            {
                SetLinks();
            });

            DrawSemanticLinkLines();
        }

        public void SetLinks()
        {
            foreach (SemanticLink semanticLink in SemanticLinks)
            {
                semanticLink.SetLinks();
            }
        }

        public void DrawSemanticLinkLines()
        {
            foreach (SemanticLink semanticLink in SemanticLinks)
            {
                for (int i = 0; i < semanticLink.Links.Count - 1; i++)
                {
                    _invokeScript("addCircle",
                        semanticLink.Links[i].Latitude,
                        semanticLink.Links[i].Longitude);

                    if (semanticLink.Links[i].LinkId.Equals(semanticLink.Links[i + 1].LinkId))

                        _invokeScript("addLine",
                            semanticLink.SemanticLinkId,
                            semanticLink.Links[i].Latitude,
                            semanticLink.Links[i].Longitude,
                            semanticLink.Links[i + 1].Latitude,
                            semanticLink.Links[i + 1].Longitude);
                }
            }

            _invokeScript("moveMap", SemanticLinks.Average(v => v.Links.Average(l => l.Latitude)), SemanticLinks.Average(v => v.Links.Average(l => l.Longitude)));
        }
    }
}
