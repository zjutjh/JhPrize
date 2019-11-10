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
using Windows.System;

using System.Collections.ObjectModel;
using JhPrize.Core;
using System.Threading.Tasks;

namespace JhPrize
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WorkPage : Page
    {
        private SystemNavigationManager navManager;
        private WorkMode mode;

        public WorkPage()
        {
            this.InitializeComponent();
            this.Loaded += WorkPage_Loaded;
            navManager = SystemNavigationManager.GetForCurrentView();
            navManager.BackRequested += WorkPage_BackRequested;
            navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            ButtonTest.Click += ButtonTest_Click;
            TextBoxNo.KeyUp += TextBoxNo_KeyUp;
        }

        private async void TextBoxNo_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                if (TextBoxNo.Text != "")
                {

                    try
                    {
                        BorderErrorMsg.Visibility = Visibility.Collapsed;
                        TextBoxNo.IsEnabled = false;
                        var title = mode == WorkMode.Completed ? "完成" : "未完成";
                        var groupNo = TextBoxNo.Text;
                        ResponseModel<PrizeModel> result = null;
                        if (mode == WorkMode.AcceptPrize)
                        {
                            result = await Api.AcceptPrizeAsync(groupNo);
                        }
                        else
                        {
                            result = await Api.DrawPrizeAsync(title, groupNo);
                        }
                        TextBlockResult.Text = result.msg;
                        if (result.data != null)
                        {
                            StackPanelPrize.Visibility = Visibility.Visible;
                            TextBlockPrize.Text = result.data.captain;
                            TextBlockPrizeDetail.Text = result.data.content;
                            TextBlockGroup.Text = $"{result.data.title}-{groupNo}";
                        }
                        else
                        {
                            StackPanelPrize.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch (Exception)
                    {
                        BorderErrorMsg.Visibility = Visibility.Visible;
                    }
                    finally
                    {
                        TextBoxNo.IsEnabled = true;
                        TextBoxNo.Text = "";
                    }


                }
                await UpdatePrizePool();

            }
        }

        private async void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
             await UpdatePrizePool();
        }

        private async void WorkPage_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdatePrizePool();
        }

        public async Task UpdatePrizePool()
        {
            try
            {
                ButtonTest.IsEnabled = false;
                TextBlockFresh.Text = "正在刷新";
                var data = await Api.GetDataAsync();
                this.PrizePools.Clear();
                foreach (var item in data)
                {
                    this.PrizePools.Add(item);
                }
                this.BorderErrorMsg.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                this.BorderErrorMsg.Visibility = Visibility.Visible;
            }
            finally
            {
                ButtonTest.IsEnabled = true;
                TextBlockFresh.Text = "刷新";
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is WorkMode mode)
            {
                this.Mode = mode;
            }

            base.OnNavigatedTo(e);
        }
        private void WorkPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.Content is WorkPage)
            {
                if (this.Frame.CanGoBack && e.Handled == false)
                {
                    this.Frame.GoBack();
                    navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
            }

        }

        public ObservableCollection<String> TestStr
        {
            get { return (ObservableCollection<String>)GetValue(TestStrProperty); }
            set { SetValue(TestStrProperty, value); }
        }

        public static readonly DependencyProperty TestStrProperty =
            DependencyProperty.Register("TestStr", typeof(ObservableCollection<String>), typeof(WorkPage), new PropertyMetadata(new ObservableCollection<String>()));



        public ObservableCollection<PrizePool> PrizePools
        {
            get { return (ObservableCollection<PrizePool>)GetValue(PrizePoolsProperty); }
            set { SetValue(PrizePoolsProperty, value); }
        }

        public WorkMode Mode
        {
            get => mode; set
            {
                mode = value;
                if (value == WorkMode.AcceptPrize)
                {
                    TextBlockTitle.Text = "领奖";
                }
                else if (value == WorkMode.Completed)
                {
                    TextBlockTitle.Text = "抽奖(完成)";
                }
                else if (value == WorkMode.NoCompleted)
                {
                    TextBlockTitle.Text = "抽奖(未完成)";
                }
            }
        }

        public static readonly DependencyProperty PrizePoolsProperty =
            DependencyProperty.Register("PrizePools", typeof(ObservableCollection<PrizePool>), typeof(WorkPage), new PropertyMetadata(new ObservableCollection<PrizePool>()));

    }
}
