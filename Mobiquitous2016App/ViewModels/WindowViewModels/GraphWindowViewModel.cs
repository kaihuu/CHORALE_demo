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
using MaterialDesignThemes.Wpf;
using Mobiquitous2016App.Daos;
using Mobiquitous2016App.Models;
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Models.GraphModels;
using Mobiquitous2016App.Properties;
using Reactive.Bindings;

namespace Mobiquitous2016App.ViewModels.WindowViewModels
{
    public class GraphWindowViewModel : ViewModel
    {
        private readonly SemanticLink _semanticLink;
        private readonly TripDirection _direction;
        private readonly List<GraphDatum> _graphDataList;

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

        public GraphWindowViewModel()
        {
            
        }

        public GraphWindowViewModel(SemanticLink semanticLink, TripDirection direction)
        {
            _semanticLink = semanticLink;
            _direction = direction;
            _graphDataList = EcologDao.GetGraphDataOnSemanticLink(_semanticLink, _direction);
        }

        public void Initialize()
        {
            Title = "CHORALE";
            BackgroundColor = Resources.ColorBlue;
            TextColor = Resources.ColorWhite;
        }

        public void SwitchToChorale()
        {
            Title = "CHORALE";
            BackgroundColor = Resources.ColorBlue;
            TextColor = Resources.ColorWhite;
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
