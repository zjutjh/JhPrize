using JhPrize.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace JhPrize
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private SystemNavigationManager navManager;
        public SettingsPage()
        {
            this.InitializeComponent();
            navManager = SystemNavigationManager.GetForCurrentView();
            navManager.BackRequested += WorkPage_BackRequested;
            navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            this.TextBoxDomain.Text = Api.Domain;
            this.PasswordBoxPass.Password = Api.Pass;
            this.TextBoxDomain.TextChanged += TextBoxDomain_TextChanged;
            this.PasswordBoxPass.PasswordChanged += PasswordBoxPass_PasswordChanged;
        }

        private void PasswordBoxPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Api.Pass = PasswordBoxPass.Password;
        }

        private void TextBoxDomain_TextChanged(object sender, TextChangedEventArgs e)
        {
            Api.Domain = TextBoxDomain.Text;
        }

        private void WorkPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.Content is SettingsPage)
            {
                if (this.Frame.CanGoBack && e.Handled == false)
                {
                    this.Frame.GoBack();
                    navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
            }

        }
    }
}
