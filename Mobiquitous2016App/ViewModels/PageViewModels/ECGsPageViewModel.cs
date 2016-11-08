using System;
using System.Collections.Generic;
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

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    // ReSharper disable once InconsistentNaming
    public class ECGsPageViewModel : ViewModel
    {
        private readonly IList<GraphDatum> _graphData;

        private readonly double _maximum;

        #region PlotModelConvertLoss変更通知プロパティ
        private PlotModel _PlotModelConvertLoss;

        public PlotModel PlotModelConvertLoss
        {
            get
            { return _PlotModelConvertLoss; }
            set
            {
                if (_PlotModelConvertLoss == value)
                    return;
                _PlotModelConvertLoss = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelAirResistance変更通知プロパティ
        private PlotModel _PlotModelAirResistance;

        public PlotModel PlotModelAirResistance
        {
            get
            { return _PlotModelAirResistance; }
            set
            {
                if (_PlotModelAirResistance == value)
                    return;
                _PlotModelAirResistance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelRollingResistance変更通知プロパティ
        private PlotModel _PlotModelRollingResistance;

        public PlotModel PlotModelRollingResistance
        {
            get
            { return _PlotModelRollingResistance; }
            set
            {
                if (_PlotModelRollingResistance == value)
                    return;
                _PlotModelRollingResistance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelRegeneLoss変更通知プロパティ
        private PlotModel _PlotModelRegeneLoss;

        public PlotModel PlotModelRegeneLoss
        {
            get
            { return _PlotModelRegeneLoss; }
            set
            {
                if (_PlotModelRegeneLoss == value)
                    return;
                _PlotModelRegeneLoss = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ECGsPageViewModel(GraphWindowViewModel parentViewModel)
        {
            _graphData = parentViewModel.GraphDataList;
            _maximum = new double[]
            {
                parentViewModel.GraphDataList.Max(v => v.ConvertLoss),
                parentViewModel.GraphDataList.Max(v => v.AirResistance),
                parentViewModel.GraphDataList.Max(v => v.RollingResistance),
                parentViewModel.GraphDataList.Max(v => v.RegeneLoss)
            }.Max();
            Initialize();
        }

        public void Initialize()
        {
            PlotModelConvertLoss = CreatePlotModel("ConvertLoss");
            PlotModelAirResistance = CreatePlotModel("AirResistance");
            PlotModelRollingResistance = CreatePlotModel("RollingResistance");
            PlotModelRegeneLoss = CreatePlotModel("RegeneLoss");
        }

        private PlotModel CreatePlotModel(string propertyName)
        {
            string title = null;
            switch (propertyName)
            {
                case "ConvertLoss":
                    title = "Convert loss";
                    break;
                case "AirResistance":
                    title = "Air resistance";
                    break;
                case "RollingResistance":
                    title = "Rolling resistance";
                    break;
                case "RegeneLoss":
                    title = "Regene loss";
                    break;
            }

            var model = new PlotModel
            {
                Subtitle = title,
                PlotMargins = new OxyThickness(double.NaN, double.NaN, 80, double.NaN)
            };

            var colorAxis = new LinearColorAxis
            {
                HighColor = OxyColors.Gray,
                LowColor = OxyColors.Black,
                Position = AxisPosition.Right,
                MajorStep = 0.02,
                Minimum = 0,
                Maximum = _maximum,
                Unit = "kWh",
                AxisTitleDistance = 0
            };
            model.Axes.Add(colorAxis);

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

            var scatterSeries = new ScatterSeries();

            foreach (var datum in _graphData)
            {
                scatterSeries.Points.Add(new ScatterPoint(datum.TransitTime, datum.LostEnergy)
                {
                    Value = (float)typeof(GraphDatum).GetProperty(propertyName).GetValue(datum)
                });
            }

            model.Series.Add(scatterSeries);

            return model;
        }
    }
}
