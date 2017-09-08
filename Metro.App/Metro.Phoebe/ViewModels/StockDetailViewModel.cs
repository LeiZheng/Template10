using Metro.Phoebe.Api.Impl;
using Metro.Phoebe.Shares.Model;
using Metro.Phoebe.Shares.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace Metro.Phoebe.ViewModels
{
    public class StockDetailViewModel : ViewModelBase
    {
        public StockDetailViewModel()
        {
            StockStatList = new ObservableCollection<StockStatItem>();
        }
        private StockServices services = new StockServices(new GoogleStockURIBuilder());
        private ObservableCollection<StockBarData> _stockHistories;
        public ObservableCollection<StockBarData> StockHistories { get => _stockHistories; set => Set(ref _stockHistories, value); }

        private ObservableCollection<StockStatItem> _StockStatList;
        public ObservableCollection<StockStatItem> StockStatList { get => _StockStatList; set => Set(ref _StockStatList, value); }
        
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
           
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {

            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async void LoadData(string v, DateTimeOffset? date1, DateTimeOffset? date2)
        {
            StockHistories = new ObservableCollection<StockBarData>(await services.GetStockHistoryBars(v, date1.Value.DateTime, date2.Value.DateTime));
            StockStatList.Clear();
            //OCLH
            var d1 = new StockStatItem
            {
                Desc = "Period Highest",
                Value = StockHistories.Select(x => x.High).Max()
            };

            var d2 = new StockStatItem
            {
                Desc = "Period Lowest",
                Value = StockHistories.Select(x => x.Low).Min()
            };
            StockStatList.Add(d1);
            StockStatList.Add(d2);

        }

    }
    public class StockStatItem
    {
        public string Desc { get; internal set; }
        public double Value { get; internal set; }
    }
}
