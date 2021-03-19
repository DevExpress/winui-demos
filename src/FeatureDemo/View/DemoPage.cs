using DevExpress.WinUI.Core.Internal;
using FeatureDemo.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo.View {
    public class DemoPageBase : Page {
        #region static
        public static readonly DependencyProperty ViewModelProperty;
        static DemoPageBase() {
            DependencyPropertyRegistrator<DemoPageBase> registrator = new DependencyPropertyRegistrator<DemoPageBase>();
            registrator.Register<MainViewModel>(nameof(ViewModel), ref ViewModelProperty, null);
        }
        #endregion
        #region dep props
        public MainViewModel ViewModel {
            get { return (MainViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        #endregion

        public DemoPageBase() {
            RegisterPropertyChangedCallback(FrameProperty, OnFrameChanged);
        }

        private void OnFrameChanged(DependencyObject sender, DependencyProperty dp) {
            ViewModel = Frame.DataContext as MainViewModel;
        }
    }
}