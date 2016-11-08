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

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    public class RPageViewModel : ViewModel
    {
        private readonly GraphWindowViewModel _pareViewModel;

        #region ROfConvertLoss変更通知プロパティ
        private IList<RModel.R> _ROfConvertLoss;

        public IList<RModel.R> ROfConvertLoss
        {
            get
            { return _ROfConvertLoss; }
            set
            { 
                if (_ROfConvertLoss == value)
                    return;
                _ROfConvertLoss = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ROfAirResistance変更通知プロパティ
        private IList<RModel.R> _ROfAirResistance;

        public IList<RModel.R> ROfAirResistance
        {
            get
            { return _ROfAirResistance; }
            set
            { 
                if (_ROfAirResistance == value)
                    return;
                _ROfAirResistance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ROfRollingResistance変更通知プロパティ
        private IList<RModel.R> _ROfRollingResistance;

        public IList<RModel.R> ROfRollingResistance
        {
            get
            { return _ROfRollingResistance; }
            set
            { 
                if (_ROfRollingResistance == value)
                    return;
                _ROfRollingResistance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ROfRegeneLoss変更通知プロパティ
        private IList<RModel.R> _ROfRegeneLoss;

        public IList<RModel.R> ROfRegeneLoss
        {
            get
            { return _ROfRegeneLoss; }
            set
            { 
                if (_ROfRegeneLoss == value)
                    return;
                _ROfRegeneLoss = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public RPageViewModel(GraphWindowViewModel parentViewModel)
        {
            _pareViewModel = parentViewModel;
            Initialize();
        }

        public void Initialize()
        {
            Console.WriteLine($"Convert: {_pareViewModel.RModel.ROfRegeneLoss[0].RForLostEnergy}");
            Console.WriteLine($"Convert: {_pareViewModel.RModel.ROfRegeneLoss[0].RForTransitTime}");

            ROfConvertLoss = _pareViewModel.RModel.ROfConvertLoss;
            ROfAirResistance = _pareViewModel.RModel.ROfAirResistance;
            ROfRollingResistance = _pareViewModel.RModel.ROfRollingResistace;
            ROfRegeneLoss = _pareViewModel.RModel.ROfRegeneLoss;
            /*ROfAirResistance = new List<RModel.R> { new RModel.R { RForTransitTime = 0.64124, RForLostEnergy = -0.566343} };
            ROfConvertLoss = new List<RModel.R> { new RModel.R { RForTransitTime = 0.84124, RForLostEnergy = 0.76343 } };
            ROfRollingResistance = new List<RModel.R> { new RModel.R { RForTransitTime = 0.3141415, RForLostEnergy = 0.251151 } };
            ROfRegeneLoss = new List<RModel.R> { new RModel.R { RForTransitTime = 0.8785113, RForLostEnergy = 0.896242 } };*/
        }
    }
}
