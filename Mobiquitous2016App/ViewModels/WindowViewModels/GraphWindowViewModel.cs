using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;
using MaterialDesignThemes.Wpf;
using Mobiquitous2016App.Daos;
using Mobiquitous2016App.Models;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.GraphModels;
using Mobiquitous2016App.Properties;
using Mobiquitous2016App.Utils;
using Mobiquitous2016App.ViewModels.PageViewModels;
using Mobiquitous2016App.Views.Pages;
using Reactive.Bindings;

namespace Mobiquitous2016App.ViewModels.WindowViewModels
{
    public class GraphWindowViewModel : ViewModel
    {
        public SemanticLink SemanticLink { get; set; }
        public TripDirection Direction { get; set; }
        public List<GraphDatum> GraphDataList { get; set; }
        public List<GraphDatum> RecentGraphDataList { get; set; }
        public ChoraleModel ChoraleModel { get; set; }
        public RModel RModel { get; set; }

        #region Title変更通知プロパティ
        private string _Title;

        public string Title
        {
            get
            { return _Title; }
            set
            {
                if (_Title == value)
                    return;
                _Title = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region BackgroundColor変更通知プロパティ
        private string _BackgroundColor;

        public string BackgroundColor
        {
            get
            { return _BackgroundColor; }
            set
            {
                if (_BackgroundColor == value)
                    return;
                _BackgroundColor = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TextColor変更通知プロパティ
        private string _TextColor;

        public string TextColor
        {
            get
            { return _TextColor; }
            set
            {
                if (_TextColor == value)
                    return;
                _TextColor = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ProgressBarVisibility変更通知プロパティ
        private System.Windows.Visibility _ProgressBarVisibility;

        public System.Windows.Visibility ProgressBarVisibility
        {
            get
            { return _ProgressBarVisibility; }
            set
            {
                if (_ProgressBarVisibility == value)
                    return;
                _ProgressBarVisibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentPage変更通知プロパティ
        private Page _CurrentPage;

        public Page CurrentPage
        {
            get
            { return _CurrentPage; }
            set
            {
                if (_CurrentPage == value)
                    return;
                _CurrentPage = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public GraphWindowViewModel()
        {
        }

        public GraphWindowViewModel(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticLink = semanticLink;
            Direction = direction;

            Initialize();
        }

        public void OutlierExclusion()
        {
            var quartilesEnergy = MathUtil.Quartiles(GraphDataList.OrderBy(d => d.LostEnergy).Select(d => (double)d.LostEnergy).ToArray());
            var firstQuartileEnergy = quartilesEnergy.Item1;
            var thirdQuartileEnergy = quartilesEnergy.Item3;
            var iqrEnergy = thirdQuartileEnergy - firstQuartileEnergy;

            var quartilesTransitTime = MathUtil.Quartiles(GraphDataList.OrderBy(d => d.TransitTime).Select(d => (double)d.TransitTime).ToArray());
            var firstQuartileTransitTime = quartilesTransitTime.Item1;
            var thirdQuartileTransitTime = quartilesTransitTime.Item3;
            var iqrTransitTime = thirdQuartileTransitTime - firstQuartileTransitTime;

            GraphDataList = GraphDataList.Where(d => d.LostEnergy > firstQuartileEnergy - 1.5 * iqrEnergy)
                .Where(d => d.LostEnergy < thirdQuartileEnergy + 1.5 * iqrEnergy)
                .ToList();

            GraphDataList = GraphDataList.Where(d => d.TransitTime > firstQuartileTransitTime - 1.5 * iqrTransitTime)
                .Where(d => d.TransitTime < thirdQuartileTransitTime + 1.5 * iqrTransitTime)
                .ToList();
        }

        public async void Initialize()
        {
            var SemanticLinks = SemanticLinkDao.OutwardSemanticLinks;
            Direction = new TripDirection { Direction = "outward" };
            SemanticLink = SemanticLinks.FirstOrDefault(s => s.SemanticLinkId == 13);
            Title = "CHORALE";
            BackgroundColor = Resources.ColorBlue;
            TextColor = Resources.ColorWhite;
            ProgressBarVisibility = System.Windows.Visibility.Visible;

            await Task.Run(() =>
            {
                GraphDataList = EcologDao.GetGraphDataOnSemanticLink(SemanticLink, Direction);
                RecentGraphDataList = EcologDao.GetRecentGraphDataOnSemanticLink(SemanticLink, Direction);
                OutlierExclusion();
                ChoraleModel = ChoraleModel.Init(GraphDataList);
                RModel = RModel.Init(GraphDataList);
            });

            SwitchToChoralePage();
        }

        public void SwitchToChoralePage()
        {
            Title = "CHORALE";
            BackgroundColor = Resources.ColorBlue;
            TextColor = Resources.ColorWhite;

            CurrentPage = new ChoralePage { DataContext = new ChoralePageViewModel(this) }; ;
        }

        public void SwitchTo3DChoralePage()
        {
            Title = "3D CHORALE";
            BackgroundColor = Resources.ColorRed;
            TextColor = Resources.ColorWhite;

            CurrentPage = new SurfaceChoralePage { DataContext = new SurfaceChoraleViewModel(this) };
        }

        public void SwitchToEcgsPage()
        {
            Title = "ECGs";
            BackgroundColor = Resources.ColorYellow;
            TextColor = Resources.ColorBlack800;

            CurrentPage = new ECGsPage { DataContext = new ECGsPageViewModel(this) };
        }

        public void SwitchToRecentEcgsPage()
        {
            Title = "Recent ECGs";
            BackgroundColor = Resources.ColorGreen;
            TextColor = Resources.ColorWhite;

            CurrentPage = new RecentECGsPage() { DataContext = new RecentECGsPageViewModel(this) };
        }

        public void SwitchToRPage()
        {
            Title = "Elemental R";
            BackgroundColor = Resources.ColorPurple;
            TextColor = Resources.ColorWhite;

            CurrentPage = new RPage() { DataContext = new RPageViewModel(this) };
        }
    }
}
