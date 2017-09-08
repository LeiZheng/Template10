using Metro.Phoebe.Api.Impl;
using Metro.Phoebe.Shares.Model;
using Metro.Phoebe.Shares.Services;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Metro.Phoebe.Controls
{
    public sealed partial class StockChart : UserControl
    {
        
        public StockChart()
        {
            this.InitializeComponent();
        }
        public static readonly DependencyProperty TitleProperty =

          DependencyProperty.Register("Title", typeof(string), typeof(ExamplePageBase), new PropertyMetadata(null));



        public string Title

        {

            get { return (string)GetValue(TitleProperty); }

            set { SetValue(TitleProperty, value); }

        }

    }
    public class FinancialDataIndicators

    {

        public double High { get; set; }

        public double Low { get; set; }

        public double Open { get; set; }

        public double Close { get; set; }

        public DateTime Date { get; set; }

    }
    public class ExamplePageBase : Page

    {

        public static readonly DependencyProperty TitleProperty =

            DependencyProperty.Register("Title", typeof(string), typeof(ExamplePageBase), new PropertyMetadata(null));



        public string Title

        {

            get { return (string)GetValue(TitleProperty); }

            set { SetValue(TitleProperty, value); }

        }



        protected override void OnNavigatedTo(NavigationEventArgs e)

        {

            base.OnNavigatedTo(e);

            this.Title = (string)e.Parameter;

        }

    }

}
