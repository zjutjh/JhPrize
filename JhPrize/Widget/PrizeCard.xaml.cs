using JhPrize.Core;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace JhPrize.Widget
{
    public sealed partial class PrizeCard : UserControl
    {

        public Prize Prize
        {
            get { return (Prize)GetValue(PrizeProperty); }
            set { SetValue(PrizeProperty, value); }
        }

        public static readonly DependencyProperty PrizeProperty =
            DependencyProperty.Register("Prize", typeof(Prize), typeof(PrizeCard), new PropertyMetadata(new Prize()));

        public PrizeCard()
        {
            this.InitializeComponent();
            this.Prize.PropertyChanged += Prize_PropertyChanged;
        }

        private void Prize_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Prize = Prize;
        }
    }
}
