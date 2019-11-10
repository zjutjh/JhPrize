using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using JhPrize.Core;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace JhPrize
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.ButtonCompleted.Click += ButtonCompleted_Click;
            this.ButtonNoCompleted.Click += ButtonNoCompleted_Click;
            this.ButtonAcceptPrize.Click += ButtonAcceptPrize_Click;
            this.ButtonSettings.Click += ButtonSettings_Click;        
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void ButtonNoCompleted_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WorkPage), WorkMode.NoCompleted);
        }

        private void ButtonCompleted_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WorkPage), WorkMode.Completed);
        }

        private void ButtonAcceptPrize_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WorkPage),WorkMode.AcceptPrize);
        }
    }
}
