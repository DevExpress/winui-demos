using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using System.Linq;

namespace EditorsDemo {
    public class SimpleMaskViewModel : ViewModelBase {
        public TextEdit FocusedEditor { get { return GetProperty<TextEdit>(); } set { SetProperty(value, OnFocusedEditorChanged); } }
        public TextInputMaskSettings TextInputSettings { get { return GetProperty<TextInputMaskSettings>(); } private set { SetProperty(value); } }
        public string Mask { get { return GetProperty<string>(); } set { SetProperty(value, OnMaskChanged); } }

        void OnFocusedEditorChanged() {
            TextInputSettings = FocusedEditor?.TextInputSettings as TextInputMaskSettings;
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
    public sealed partial class SimpleMaskModule : DemoSubModuleView {
        public SimpleMaskModule() {
            this.InitializeComponent();
            var viewModel = DataContext as SimpleMaskViewModel;
            Root.Children.Where(x => x is TextEdit).ForEach(x => x.GotFocus += (s, e) => viewModel.FocusedEditor = (TextEdit)x);
        }
    }
}
