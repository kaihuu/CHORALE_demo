using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Dapper.FluentMap;
using Livet;
using Livet.Messaging;
using Mobiquitous2016App.Daos;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.MapModels;
using Mobiquitous2016App.ObjectRelationalMaps;
using Mobiquitous2016App.ORMaps;
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

        private TripDirection _direction;

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
            SemanticLinks = SemanticLinkDao.OutwardSemanticLinks;

            Uri = $"{AppDomain.CurrentDomain.BaseDirectory}Resources\\index.html";

            MapHost = new MapHost()
            {
                MapWindowViewModel = this
            };
        }

        public void SetOutwardSemanticLinks()
        {
            _direction = new TripDirection { Direction = "outward" };
            SemanticLinks = SemanticLinkDao.OutwardSemanticLinks;
            InvokeScript("initialize", null);
        }

        public void SetHomewardSemanticLinks()
        {
            _direction = new TripDirection { Direction = "homeward" };
            SemanticLinks = SemanticLinkDao.HomewardSemanticLinks;
            InvokeScript("initialize", null);
        }

        public void DrawSemanticLinkLines()
        {
            //InvokeScript("addImageMarker", $"{AppDomain.CurrentDomain.BaseDirectory}Resources\\1.png", 35.473695, 139.590859);

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

        public void TransitToDetailWindow(int semanticLinkId)
        {
            var semanticLink = SemanticLinks.FirstOrDefault(s => s.SemanticLinkId == semanticLinkId);
            var message = new TransitionMessage(typeof(GraphWindow), new GraphWindowViewModel(semanticLink, _direction), TransitionMode.Normal);
            Messenger.Raise(message);
        }
    }
}
