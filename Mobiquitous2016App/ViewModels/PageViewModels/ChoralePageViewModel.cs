using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using Mobiquitous2016App.Models;
using Mobiquitous2016App.ViewModels.WindowViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    public class ChoralePageViewModel : ViewModel
    {
        #region PlotModel変更通知プロパティ
        private PlotModel _PlotModel;

        public PlotModel PlotModel
        {
            get
            { return _PlotModel; }
            set
            {
                if (_PlotModel == value)
                    return;
                _PlotModel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        private readonly GraphWindowViewModel _parentViewModel;

        public ChoralePageViewModel(GraphWindowViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            Initialize();
        }

        public void Initialize()
        {
            var plotModel = new PlotModel
            {
                Subtitle = $"Semanantic Link: {_parentViewModel.SemanticLink.Semantics}, Direction: {_parentViewModel.Direction.Direction}"
            };

            var colorAxis = new LinearColorAxis
            {
                HighColor = OxyColors.Gray,
                LowColor = OxyColors.Black,
                Position = AxisPosition.Right
            };
            plotModel.Axes.Add(colorAxis);

            var xAxis = new LinearAxis
            {
                Title = "Time",
                Unit = "s",
                Position = AxisPosition.Bottom
            };
            plotModel.Axes.Add(xAxis);

            var yAxis = new LinearAxis
            {
                Title = "Lost Energy",
                Unit = "kWh"
            };
            plotModel.Axes.Add(yAxis);

            var heatMapSeries1 = new HeatMapSeries
            {
                LabelFormatString = "0",
                X0 = _parentViewModel.ChoraleModel.MinTransitTime,
                X1 = _parentViewModel.ChoraleModel.MaxTransitTime,
                Y0 = _parentViewModel.ChoraleModel.MinLostEnegry,
                Y1 = _parentViewModel.ChoraleModel.MaxLostEnergy,
                LabelFontSize = 0.2,
                Data = _parentViewModel.ChoraleModel.Data
            };
            plotModel.Series.Add(heatMapSeries1);

            PlotModel = plotModel;
            _parentViewModel.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
        }
    }
}
