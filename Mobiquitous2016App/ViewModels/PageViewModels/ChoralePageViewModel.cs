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
                Data = new double[_parentViewModel.ChoraleModel.ClassNumber, _parentViewModel.ChoraleModel.ClassNumber]
            };

            double preTimeLevel = 0;
            double currentTimeLevel = _parentViewModel.ChoraleModel.MinTransitTime;

            for (int i = 0; i < _parentViewModel.ChoraleModel.ClassNumber; i++)
            {
                double preEnergyLevel = 0;
                double currentEnergyLevel = _parentViewModel.ChoraleModel.MinLostEnegry;

                for (int j = 0; j < _parentViewModel.ChoraleModel.ClassNumber; j++)
                {
                    // ReSharper disable once ReplaceWithSingleCallToCount
                    heatMapSeries1.Data[i, j] = _parentViewModel.GraphDataList
                        .Where(d => d.LostEnergy > preEnergyLevel)
                        .Where(d => d.LostEnergy <= currentEnergyLevel)
                        .Where(d => d.TransitTime > preTimeLevel)
                        .Where(d => d.TransitTime <= currentTimeLevel)
                        .Count();

                    if (j == 0)
                    {
                        preEnergyLevel = _parentViewModel.ChoraleModel.MinLostEnegry;
                    }
                    else
                    {
                        preEnergyLevel += _parentViewModel.ChoraleModel.ClassWidthEnergy;
                    }

                    currentEnergyLevel += _parentViewModel.ChoraleModel.ClassWidthEnergy;
                }

                if (i == 0)
                {
                    preTimeLevel = _parentViewModel.ChoraleModel.MinTransitTime;
                }
                else
                {
                    preTimeLevel += _parentViewModel.ChoraleModel.ClassWidthTransitTime;
                }

                currentTimeLevel += _parentViewModel.ChoraleModel.ClassWidthTransitTime;
            }

            plotModel.Series.Add(heatMapSeries1);

            _parentViewModel.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            PlotModel = plotModel;
        }
    }
}
