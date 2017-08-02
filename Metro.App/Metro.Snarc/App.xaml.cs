using System.Threading.Tasks;
using Sample.Services.SettingsServices;
using Template10.Common;
using Template10.Controls;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Metro.Snarc
{
    [Bindable]
    sealed partial class App : BootStrapper
    {
        SettingsService _settingsService = SettingsService.Instance;

        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Sample.Views.Splash(e);
            RequestedTheme = _settingsService.AppTheme;
            CacheMaxDuration = _settingsService.CacheMaxDuration;
            ShowShellBackButton = _settingsService.UseShellBackButton;
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog(new Sample.Views.Shell(service), ModalContent = new Sample.Views.Busy());
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await NavigationService.NavigateAsync(typeof(Sample.Views.MainPage));
        }
    }
}
