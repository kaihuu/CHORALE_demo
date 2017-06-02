using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using Mobiquitous2016App.Models;
using Mobiquitous2016App.Models.GraphModels;
using Mobiquitous2016App.ViewModels.WindowViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Syncfusion.UI.Xaml.Charts;
using ScatterSeries = OxyPlot.Series.ScatterSeries;

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    // ReSharper disable once InconsistentNaming
    public class RecentECGsPageViewModel : ViewModel
    {
        private readonly GraphWindowViewModel _parentViewModel;
        private readonly IList<GraphDatum> _graphData;
        private readonly IList<GraphDatum> _recentGraphData;

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

        public RecentECGsPageViewModel(GraphWindowViewModel parentViewModel)
        {
            _graphData = parentViewModel.GraphDataList;
            _recentGraphData = parentViewModel.RecentGraphDataList;
            Initialize();
        }

        public void Initialize()
        {
            PlotModel = CreatePlotModel();
        }

        private PlotModel CreatePlotModel()
        {
            var model = new PlotModel
            {
                Title = "Recent ECGs",
            };

            var xAxis = new LinearAxis
            {
                Title = "Transit time",
                Unit = "s",
                Position = AxisPosition.Bottom
            };
            model.Axes.Add(xAxis);
            var yAxis = new LinearAxis
            {
                Title = "Lost energy",
                Unit = "kWh"
            };
            model.Axes.Add(yAxis);

            var scatterSeries = new ScatterSeries
            {
                MarkerFill = OxyColors.Gray
            };
            foreach (var datum in _graphData)
            {
                scatterSeries.Points.Add(new ScatterPoint(datum.TransitTime, datum.LostEnergy));
            }
            model.Series.Add(scatterSeries);

            var recentSeries = new ScatterSeries
            {
                MarkerFill = OxyColors.Blue,
                MarkerType = MarkerType.Circle
            };
            foreach (var datum in _recentGraphData)
            {
                recentSeries.Points.Add(new ScatterPoint(datum.TransitTime, datum.LostEnergy));
            }
            model.Series.Add(recentSeries);

            return model;
        }
    }
}
