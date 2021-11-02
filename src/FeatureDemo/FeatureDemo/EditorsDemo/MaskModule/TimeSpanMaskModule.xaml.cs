using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using DevExpress.Mvvm.CodeGenerators;

namespace EditorsDemo {
    public sealed partial class TimeSpanMaskModule : DemoSubModuleView {
        public TimeSpanMaskModule() {
            ViewModel = new TimeSpanMaskViewModel();
            this.InitializeComponent();
            Root.Children.Where(x => x is TextEdit).ForEach(x => x.GotFocus += (s, e) => ViewModel.FocusedEditor = (TextEdit)x);
        }

        public TimeSpanMaskViewModel ViewModel { get; }
    }

    [GenerateViewModel(ImplementISupportServices = true)]
    public partial class TimeSpanMaskViewModel {
        [GenerateProperty]
        public TextEdit _FocusedEditor;

        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        TextInputTimeSpanMaskSettings _TextInputSettings;

        [GenerateProperty]
        string _Mask;

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
            }
            catch {
                await GetService<IMessageBoxService>().ShowAsync("Invalid mask", "Error");
                TextInputSettings.Mask = maskBackup;
                Mask = maskBackup;
            }
        }
    }
}
