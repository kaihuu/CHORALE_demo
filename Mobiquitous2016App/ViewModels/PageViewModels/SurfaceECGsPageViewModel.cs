using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Mobiquitous2016App.Models.GraphModels;
using Mobiquitous2016App.ViewModels.WindowViewModels;
using Syncfusion.UI.Xaml.Charts;

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    // ReSharper disable once InconsistentNaming
    public class SurfaceECGsPageViewModel : ViewModel
    {
        private readonly GraphWindowViewModel _parentViewModel;

        #region GraphData変更通知プロパティ
        private IList<GraphDatum> _GraphData;

        public IList<GraphDatum> GraphData
        {
            get
            { return _GraphData; }
            set
            {
                if (_GraphData == value)
                    return;
                _GraphData = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Maximum変更通知プロパティ
        private double _Maximum;

        public double Maximum
        {
            get
            { return _Maximum; }
            set
            {
                if (_Maximum == value)
                    return;
                _Maximum = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public SurfaceECGsPageViewModel(GraphWindowViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            Initialize();
        }

        public void Initialize()
        {
            GraphData = _parentViewModel.GraphDataList;
            Maximum = new double[]
            {
                _parentViewModel.GraphDataList.Max(v => v.ConvertLoss),
                _parentViewModel.GraphDataList.Max(v => v.AirResistance),
                _parentViewModel.GraphDataList.Max(v => v.RollingResistance),
                _parentViewModel.GraphDataList.Max(v => v.RegeneLoss)
            }.Max();
        }
    }
}
