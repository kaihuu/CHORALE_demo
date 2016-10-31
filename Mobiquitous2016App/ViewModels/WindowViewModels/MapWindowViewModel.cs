using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Dapper.FluentMap;
using Livet;
using Livet.Messaging;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.MapModels;
using Mobiquitous2016App.ObjectRelationalMaps;
using Mobiquitous2016App.Views.Windows;

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

        #region MapHost変更通知プロパティ
        private MapHost _MapHost;

        public MapHost MapHost
        {
            get
            { return _MapHost; }
            set
            { 
                if (_MapHost == value)
                    return;
                _MapHost = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Uri変更通知プロパティ
        private string _Uri;

        public string Uri
        {
            get
            { return _Uri; }
            set
            { 
                if (_Uri == value)
                    return;
                _Uri = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public InvokeScriptDelegate InvokeScript { get; set; }

        public void Initialize()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new LinkMap());
                config.AddMap(new SemanticLinkMap());
            });

            SemanticLinks = SemanticLink.OutwardSemanticLinks;

            Uri = $"{AppDomain.CurrentDomain.BaseDirectory}Resources\\index.html";

            MapHost = new MapHost()
            {
                MapWindowViewModel = this
            };
        }

        public void SetOutwardSemanticLinks()
        {
            SemanticLinks = SemanticLink.OutwardSemanticLinks;
            InvokeScript("initialize", null);
        }

        public void SetHomewardSemanticLinks()
        {
            SemanticLinks = SemanticLink.HomewardSemanticLinks;
            InvokeScript("initialize", null);
        }

        public void DrawSemanticLinkLines()
        {
            foreach (var semanticLink in SemanticLinks)
            {
                for (int i = 0; i < semanticLink.Links.Count - 1; i++)
                {
                    /*InvokeScript("addCircle",
                        semanticLink.Links[i].Latitude,
                        semanticLink.Links[i].Longitude);*/

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

        public void TransitToDetailWindow(int semanticLinkId)
        {
            var semanticLink = SemanticLinks.FirstOrDefault(s => s.SemanticLinkId == semanticLinkId);
            var message = new TransitionMessage(typeof(DetailWindow), new DetailWindowViewModel(semanticLink), TransitionMode.Normal);
            Messenger.Raise(message);
        }
    }
}
