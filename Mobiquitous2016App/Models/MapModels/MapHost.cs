using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Mobiquitous2016App.ViewModels.WindowViewModels;

namespace Mobiquitous2016App.Models.MapModels
{
    [ComVisible(true)]
    public class MapHost : INotifyPropertyChanged
    {
        public MapWindowViewModel MapWindowViewModel { get; set; }

        public void OnInitCompleted()
        {
            MapWindowViewModel.DrawSemanticLinkLines();
        }

        public void OnLineClicked(int semanticLinkId)
        {
            MapWindowViewModel.ShowDialog(semanticLinkId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
