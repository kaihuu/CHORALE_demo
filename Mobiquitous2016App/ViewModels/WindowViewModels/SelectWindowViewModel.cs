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
using Mobiquitous2016App.Models.EcologModels;
using Mobiquitous2016App.Utils;
using Mobiquitous2016App.Daos;
using System.Collections.ObjectModel;

namespace Mobiquitous2016App.ViewModels.WindowViewModels
{
    public class SelectWindowViewModel : ViewModel
    {


        #region SemanticLinks変更通知プロパティ
        private IList<SemanticLink> _SemanticLinks;

        public IList<SemanticLink> SemanticLinks
        {
            get
            { return _SemanticLinks; }
            set
            { 
                if (_SemanticLinks == value)
                    return;
                _SemanticLinks = value;
                RaisePropertyChanged("SemanticLinks");
            }
        }
        #endregion


        #region direction変更通知プロパティ
        private TripDirection _direction;

        public TripDirection direction
        {
            get
            { return _direction; }
            set
            { 
                if (_direction == value)
                    return;
                _direction = value;
                RaisePropertyChanged("direction");
            }
        }
        #endregion

        #region DirectionSelect変更通知プロパティ
        private string _DirectionSelect;

        public string DirectionSelect
        {
            get
            { return _DirectionSelect; }
            set
            { 
                if (_DirectionSelect == value)
                    return;
                _DirectionSelect = value;
                RaisePropertyChanged("DirectionSelect");
            }
        }
        #endregion



        public void Initialize()
        {
            
            Regkey reg = new Regkey();
            reg.changeRegkey();

            if (DirectionSelect == "homeward")
            {
                SetHomewardSemanticLinks();
            }
            else if(DirectionSelect == "outward")
            {
                SetOutwardSemanticLinks();
            }



        }

        public void SetOutwardSemanticLinks()
        {
            //_direction = new TripDirection { Direction = "outward" };
            SemanticLinks = SemanticLinkDao.OutwardSemanticLinks;
            //InvokeScript("initialize", null);
        }

        public void SetHomewardSemanticLinks()
        {
            //_direction = new TripDirection { Direction = "homeward" };
            SemanticLinks = SemanticLinkDao.HomewardSemanticLinks;
            //InvokeScript("initialize", null);
        }
    }
}
