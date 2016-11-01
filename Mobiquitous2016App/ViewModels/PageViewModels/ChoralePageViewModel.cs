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

        public GraphWindowViewModel ParentViewModel { get; set; }

        public ChoralePageViewModel(GraphWindowViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
        }

        public void Initialize()
        {
        }
    }
}
