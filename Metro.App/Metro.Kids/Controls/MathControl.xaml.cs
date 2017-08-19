using Metro.Kids.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Metro.Kids.Controls
{
    public sealed partial class MathControl : UserControl
    {
        public MathControl()
        {
            this.InitializeComponent();
            inkCanvas.InkPresenter.InputDeviceTypes =
            Windows.UI.Core.CoreInputDeviceTypes.Mouse |
            Windows.UI.Core.CoreInputDeviceTypes.Pen;

            // Set initial ink stroke attributes.
            InkDrawingAttributes drawingAttributes = new InkDrawingAttributes();
            drawingAttributes.Color = Windows.UI.Colors.LightYellow;
            drawingAttributes.IgnorePressure = false;
            drawingAttributes.FitToCurve = true;
            inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MathPageViewModel;
            vm.HandlePlayOrPauseCommand();
            if(vm.IsStudying)
            {
                Result.Focus(FocusState.Programmatic);
            }
            else
            {
                Exp.Focus(FocusState.Programmatic);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MathPageViewModel;
            if (vm.IsSession)
                vm.EndSession();
            else
                vm.StartNewSession();
            
        }

        private void AppBarToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            inkCanvas.InkPresenter.StrokeContainer.Clear();
        }
    }
}
