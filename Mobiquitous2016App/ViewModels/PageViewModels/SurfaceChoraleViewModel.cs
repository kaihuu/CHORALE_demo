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
using Mobiquitous2016App.Models.GraphModels;
using Mobiquitous2016App.ViewModels.WindowViewModels;

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    public class SurfaceChoraleViewModel : ViewModel
    {
        private readonly GraphWindowViewModel _parentViewModel;

        #region DataList変更通知プロパティ
        private IList<SurfaceData> _DataList;

        public IList<SurfaceData> DataList
        {
            get
            { return _DataList; }
            set
            { 
                if (_DataList == value)
                    return;
                _DataList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public SurfaceChoraleViewModel(GraphWindowViewModel parenViewModel)
        {
            _parentViewModel = parenViewModel;
        }
        
        public void Initialize()
        {
            DataList = _parentViewModel.ChoraleModel.SurfaceDataList;
        }
    }
}
