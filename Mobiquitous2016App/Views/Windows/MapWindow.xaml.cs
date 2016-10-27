using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Mobiquitous2016App.ViewModels.WindowViewModels;

namespace Mobiquitous2016App.Views.Windows
{
    /* 
	 * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     *
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

    /// <summary>
    /// MapWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MapWindow : MetroWindow
    {
        public MapWindow()
        {
            InitializeComponent();

            var context = DataContext as MapWindowViewModel;
            if (context != null) context.InvokeScript = InvokeScript;
        }

        public  void InvokeScript(string scriptName, params object[] args)
        {
            //WebBrowser.InvokeScript(scriptName, args);
        }
    }
}