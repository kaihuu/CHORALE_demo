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

namespace Mobiquitous2016App.ViewModels.PageViewModels
{
    // ReSharper disable once InconsistentNaming
    public class SurfaceECGsPageViewModel : ViewModel
    {
        public class Data
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        public ObservableCollection<Data> DataValues => new ObservableCollection<Data>
        {
            new Data { X = 100, Z = 0.10, Y = 0},
            new Data { X = 100, Z = 0.11, Y = 0},
            new Data { X = 100, Z = 0.12, Y = 0},
            new Data { X = 100, Z = 0.13, Y = 2},
            new Data { X = 100, Z = 0.14, Y = 0},
            new Data { X = 100, Z = 0.15, Y = 0},
            new Data { X = 100, Z = 0.16, Y = 0},
            new Data { X = 100, Z = 0.17, Y = 0},
            new Data { X = 100, Z = 0.18, Y = 0},
            new Data { X = 100, Z = 0.19, Y = 0},

            new Data { X = 125, Z = 0.10, Y = 0},
            new Data { X = 125, Z = 0.11, Y = 0},
            new Data { X = 125, Z = 0.12, Y = 1},
            new Data { X = 125, Z = 0.13, Y = 10},
            new Data { X = 125, Z = 0.14, Y = 5},
            new Data { X = 125, Z = 0.15, Y = 3},
            new Data { X = 125, Z = 0.16, Y = 0},
            new Data { X = 125, Z = 0.17, Y = 0},
            new Data { X = 125, Z = 0.18, Y = 0},
            new Data { X = 125, Z = 0.19, Y = 0},

            new Data { X = 150, Z = 0.10, Y = 1},
            new Data { X = 150, Z = 0.11, Y = 1},
            new Data { X = 150, Z = 0.12, Y = 6},
            new Data { X = 150, Z = 0.13, Y = 29},
            new Data { X = 150, Z = 0.14, Y = 20},
            new Data { X = 150, Z = 0.15, Y = 11},
            new Data { X = 150, Z = 0.16, Y = 3},
            new Data { X = 150, Z = 0.17, Y = 1},
            new Data { X = 150, Z = 0.18, Y = 2},
            new Data { X = 150, Z = 0.19, Y = 0},

            new Data { X = 200, Z = 0.10, Y = 0},
            new Data { X = 200, Z = 0.11, Y = 0},
            new Data { X = 200, Z = 0.12, Y = 9},
            new Data { X = 200, Z = 0.13, Y = 28},
            new Data { X = 200, Z = 0.14, Y = 24},
            new Data { X = 200, Z = 0.15, Y = 26},
            new Data { X = 200, Z = 0.16, Y = 9},
            new Data { X = 200, Z = 0.17, Y = 3},
            new Data { X = 200, Z = 0.18, Y = 2},
            new Data { X = 200, Z = 0.19, Y = 1},

            new Data { X = 225, Z = 0.10, Y = 0},
            new Data { X = 225, Z = 0.11, Y = 0},
            new Data { X = 225, Z = 0.12, Y = 3},
            new Data { X = 225, Z = 0.13, Y = 15},
            new Data { X = 225, Z = 0.14, Y = 10},
            new Data { X = 225, Z = 0.15, Y = 25},
            new Data { X = 225, Z = 0.16, Y = 15},
            new Data { X = 225, Z = 0.17, Y = 3},
            new Data { X = 225, Z = 0.18, Y = 0},
            new Data { X = 225, Z = 0.19, Y = 1},

            new Data { X = 250, Z = 0.10, Y = 0},
            new Data { X = 250, Z = 0.11, Y = 0},
            new Data { X = 250, Z = 0.12, Y = 0},
            new Data { X = 250, Z = 0.13, Y = 1},
            new Data { X = 250, Z = 0.14, Y = 18},
            new Data { X = 250, Z = 0.15, Y = 19},
            new Data { X = 250, Z = 0.16, Y = 14},
            new Data { X = 250, Z = 0.17, Y = 9},
            new Data { X = 250, Z = 0.18, Y = 7},
            new Data { X = 250, Z = 0.19, Y = 1},

            new Data { X = 275, Z = 0.10, Y = 0},
            new Data { X = 275, Z = 0.11, Y = 0},
            new Data { X = 275, Z = 0.12, Y = 0},
            new Data { X = 275, Z = 0.13, Y = 5},
            new Data { X = 275, Z = 0.14, Y = 5},
            new Data { X = 275, Z = 0.15, Y = 12},
            new Data { X = 275, Z = 0.16, Y = 10},
            new Data { X = 275, Z = 0.17, Y = 5},
            new Data { X = 275, Z = 0.18, Y = 3},
            new Data { X = 275, Z = 0.19, Y = 5},

            new Data { X = 300, Z = 0.10, Y = 0},
            new Data { X = 300, Z = 0.11, Y = 0},
            new Data { X = 300, Z = 0.12, Y = 0},
            new Data { X = 300, Z = 0.13, Y = 0},
            new Data { X = 300, Z = 0.14, Y = 0},
            new Data { X = 300, Z = 0.15, Y = 6},
            new Data { X = 300, Z = 0.16, Y = 5},
            new Data { X = 300, Z = 0.17, Y = 9},
            new Data { X = 300, Z = 0.18, Y = 8},
            new Data { X = 300, Z = 0.19, Y = 2},

            new Data { X = 325, Z = 0.10, Y = 0},
            new Data { X = 325, Z = 0.11, Y = 0},
            new Data { X = 325, Z = 0.12, Y = 0},
            new Data { X = 325, Z = 0.13, Y = 0},
            new Data { X = 325, Z = 0.14, Y = 2},
            new Data { X = 325, Z = 0.15, Y = 1},
            new Data { X = 325, Z = 0.16, Y = 3},
            new Data { X = 325, Z = 0.17, Y = 3},
            new Data { X = 325, Z = 0.18, Y = 6},
            new Data { X = 325, Z = 0.19, Y = 2},

            new Data { X = 350, Z = 0.10, Y = 0},
            new Data { X = 350, Z = 0.11, Y = 0},
            new Data { X = 350, Z = 0.12, Y = 0},
            new Data { X = 350, Z = 0.13, Y = 0},
            new Data { X = 350, Z = 0.14, Y = 0},
            new Data { X = 350, Z = 0.15, Y = 1},
            new Data { X = 350, Z = 0.16, Y = 0},
            new Data { X = 350, Z = 0.17, Y = 1},
            new Data { X = 350, Z = 0.18, Y = 1},
            new Data { X = 350, Z = 0.19, Y = 0},
        };

        public SurfaceECGsPageViewModel(GraphWindowViewModel parentViewModel)
        {
            
        }

        public void Initialize()
        {
        }
    }
}
