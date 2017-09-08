using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10.Utils;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Sample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        private ObservableCollection<string> _StockSymbalLists;
        public ObservableCollection<string> StockSymbalLists { get => _StockSymbalLists; set => Set(() => StockSymbalLists, ref _StockSymbalLists, value); }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            StockSymbalLists = new ObservableCollection<string>();
            StockSymbalLists.Add("AAPL");
            StockSymbalLists.Add("BABA");
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            /*
            var goingToDetails = args.TargetPageType == typeof(Metro.Phoebe.Pages.StockDetailPage);
            if (goingToDetails)
            {
                var dialog = new ContentDialog
                {
                    Title = "Confirmation",
                    Content = "Are you sure?",
                    PrimaryButtonText = "Continue",
                    SecondaryButtonText = "Cancel",
                };
                var result = await dialog.ShowAsyncEx();
                args.Cancel = result == ContentDialogResult.Secondary;
            }
            */
        }

        public void GotoDetailsPage() => NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() => NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() => NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() => NavigationService.Navigate(typeof(Views.SettingsPage), 2);

        public void GoToStockChartPage() => NavigationService.Navigate(typeof(Metro.Phoebe.Pages.StockDetailPage), Value);
    }
}
