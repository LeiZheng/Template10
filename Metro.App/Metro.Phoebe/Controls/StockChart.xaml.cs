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
            List<FinancialDataIndicators> SampleData = new List<FinancialDataIndicators>();



            SampleData.Add(new FinancialDataIndicators() { High = 31.1, Open = 30.33, Low = 30.19, Close = 30.66, Date = new DateTime(2010, 1, 4) });

            SampleData.Add(new FinancialDataIndicators() { High = 31.24, Open = 30.62, Low = 29.91, Close = 30.86, Date = new DateTime(2010, 1, 11) });

            SampleData.Add(new FinancialDataIndicators() { High = 31.24, Open = 30.75, Low = 28.84, Close = 28.96, Date = new DateTime(2010, 1, 19) });

            SampleData.Add(new FinancialDataIndicators() { High = 29.92, Open = 29.24, Low = 27.66, Close = 28.18, Date = new DateTime(2010, 1, 25) });

            SampleData.Add(new FinancialDataIndicators() { High = 28.79, Open = 28.39, Low = 27.57, Close = 28.02, Date = new DateTime(2010, 2, 1) });



            SampleData.Add(new FinancialDataIndicators() { High = 28.4, Open = 28.01, Low = 27.57, Close = 27.93, Date = new DateTime(2010, 2, 8) });

            SampleData.Add(new FinancialDataIndicators() { High = 29.03, Open = 28.13, Low = 28.02, Close = 28.77, Date = new DateTime(2010, 2, 16) });

            SampleData.Add(new FinancialDataIndicators() { High = 28.94, Open = 28.84, Low = 28.02, Close = 28.67, Date = new DateTime(2010, 2, 22) });

            SampleData.Add(new FinancialDataIndicators() { High = 29.3, Open = 28.77, Low = 28.24, Close = 28.59, Date = new DateTime(2010, 3, 1) });

            SampleData.Add(new FinancialDataIndicators() { High = 29.38, Open = 28.52, Low = 28.5, Close = 29.27, Date = new DateTime(2010, 3, 8) });

            this.OhlcChart.DataContext = SampleData;

            this.secondChart.DataContext = SampleData;
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
