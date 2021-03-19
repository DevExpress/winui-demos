using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using System.Linq;

namespace EditorsDemo {
    public class TimeSpanMaskViewModel : ViewModelBase {
        public TextEdit FocusedEditor { get { return GetProperty<TextEdit>(); } set { SetProperty(value, OnFocusedEditorChanged); } }
        public TextInputTimeSpanMaskSettings TextInputSettings { get { return GetProperty<TextInputTimeSpanMaskSettings>(); } private set { SetProperty(value); } }
        public string Mask { get { return GetProperty<string>(); } set { SetProperty(value, OnMaskChanged); } }

        void OnFocusedEditorChanged() {
            TextInputSettings = FocusedEditor?.TextInputSettings as TextInputTimeSpanMaskSettings;
            Mask = TextInputSettings?.Mask;
        }
        async void OnMaskChanged() {
            if (TextInputSettings == null || Mask == TextInputSettings.Mask)
                return;
            string maskBackup = TextInputSettings.Mask;
            try {
                TextInputSettings.Mask = Mask;
            } catch {
                await GetService<IMessageBoxService>().ShowAsync("Invalid mask", "Error");
                TextInputSettings.Mask = maskBackup;
                Mask = maskBackup;
            }
        }
    }
    public sealed partial class TimeSpanMaskModule : DemoSubModuleView {
        public TimeSpanMaskModule() {
            this.InitializeComponent();
            var viewModel = DataContext as TimeSpanMaskViewModel;
            Root.Children.Where(x => x is TextEdit).ForEach(x => x.GotFocus += (s, e) => viewModel.FocusedEditor = (TextEdit)x);
        }
    }
}
