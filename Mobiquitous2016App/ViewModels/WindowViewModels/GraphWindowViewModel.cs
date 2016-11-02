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
        public ChoraleModel ChoraleModel { get; set; }

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
        }

        public void OutlierExclusion()
        {
            var quartilesEnergy = MathUtil.Quartiles(GraphDataList.OrderBy(d => d.LostEnergy).Select(d => (double)d.LostEnergy).ToArray());
            var firstQuartileEnergy = quartilesEnergy.Item1;
            var thirdQuartileEnergy = quartilesEnergy.Item3;
            var iqrEnergy = thirdQuartileEnergy - firstQuartileEnergy;

            GraphDataList = GraphDataList.Where(d => d.LostEnergy > firstQuartileEnergy - 1.5 * iqrEnergy)
                .Where(d => d.LostEnergy < thirdQuartileEnergy + 1.5 * iqrEnergy)
                .ToList();

            var quartilesTransitTime = MathUtil.Quartiles(GraphDataList.OrderBy(d => d.TransitTime).Select(d => (double)d.TransitTime).ToArray());
            var firstQuartileTransitTime = quartilesTransitTime.Item1;
            var thirdQuartileTransitTime = quartilesTransitTime.Item3;
            var iqrTransitTime = thirdQuartileTransitTime - thirdQuartileTransitTime;

            GraphDataList = GraphDataList.Where(d => d.TransitTime > firstQuartileTransitTime - 1.5 * iqrTransitTime)
                .Where(d => d.TransitTime < thirdQuartileTransitTime + 1.5 * iqrTransitTime)
                .ToList();

            ChoraleModel = new ChoraleModel
            {
                ClassNumber = MathUtil.CalculateClassNumber(GraphDataList),
                MinLostEnegry = GraphDataList.Min(d => d.LostEnergy),
                MaxLostEnergy = GraphDataList.Max(d => d.LostEnergy),
                ClassWidthEnergy = (GraphDataList.Max(d => d.LostEnergy) - GraphDataList.Min(d => d.LostEnergy)) / MathUtil.CalculateClassNumber(GraphDataList),
                MinTransitTime = GraphDataList.Min(d => d.TransitTime),
                MaxTransitTime = GraphDataList.Max(d => d.TransitTime),
                ClassWidthTransitTime = (float)(GraphDataList.Max(d => d.TransitTime) - GraphDataList.Min(d => d.TransitTime)) / MathUtil.CalculateClassNumber(GraphDataList)
            };
            ChoraleModel.SetData(GraphDataList);
        }

        public async void Initialize()
        {
            Title = "CHORALE";
            BackgroundColor = Resources.ColorBlue;
            TextColor = Resources.ColorWhite;
            ProgressBarVisibility = System.Windows.Visibility.Visible;

            await Task.Run(() =>
            {
                GraphDataList = EcologDao.GetGraphDataOnSemanticLink(SemanticLink, Direction);
                OutlierExclusion();
            });

            SwitchToChorale();
        }

        public void SwitchToChorale()
        {
            Title = "CHORALE";
            BackgroundColor = Resources.ColorBlue;
            TextColor = Resources.ColorWhite;

            var page = new ChoralePage { DataContext = new ChoralePageViewModel(this) };
            CurrentPage = page;
        }

        public void SwitchTo3DChorale()
        {
            Title = "3D CHORALE";
            BackgroundColor = Resources.ColorRed;
            TextColor = Resources.ColorWhite;
        }

        public void SwitchToEcgs()
        {
            Title = "ECGs";
            BackgroundColor = Resources.ColorYellow;
            TextColor = Resources.ColorBlack800;
        }

        public void SwitchTo3DEcgs()
        {
            Title = "3D ECGs";
            BackgroundColor = Resources.ColorGreen;
            TextColor = Resources.ColorWhite;
        }
    }
}
