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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace JhPrize.Widget
{
    public sealed partial class PrizePoolCard : UserControl
    {
        public PrizePoolCard()
        {
            this.InitializeComponent();
        }


        public int AcceptCount
        {
            get { return (int)GetValue(AcceptCountProperty); }
            set { SetValue(AcceptCountProperty, value); }
        }

        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public int Capacity
        {
            get { return (int)GetValue(CapacityProperty); }
            set { SetValue(CapacityProperty, value); }
        }

        public static readonly DependencyProperty AcceptCountProperty =
            DependencyProperty.Register("AcceptCount", typeof(int), typeof(PrizePoolCard), new PropertyMetadata(1));

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(PrizePoolCard), new PropertyMetadata(2));

        public static readonly DependencyProperty CapacityProperty =
            DependencyProperty.Register("Capacity", typeof(int), typeof(PrizePoolCard), new PropertyMetadata(3));

        private void OnPrizePoolChanged()
        {
            AcceptCount =  (from item in PrizePool.Prizes select item.AcceptCount).Sum();
            Count = (from item in PrizePool.Prizes select item.Count).Sum();
            Capacity = (from item in PrizePool.Prizes select item.Capacity).Sum();
        }

        public PrizePool PrizePool
        {
            get { return (PrizePool)GetValue(PrizePoolProperty); }
            set { SetValue(PrizePoolProperty, value); }
        }

        public static readonly DependencyProperty PrizePoolProperty =
            DependencyProperty.Register("PrizePool", typeof(PrizePool), typeof(PrizePoolCard), new PropertyMetadata(new PrizePool(), PrizePoolChangedCallback));

        private static void PrizePoolChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PrizePoolCard)d).OnPrizePoolChanged();
        }
    }
}
