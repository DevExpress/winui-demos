﻿using DevExpress.Mvvm;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.CodeGenerators;

namespace EditorsDemo {
    public sealed partial class NumericMaskModule : DemoSubModuleView {
        public NumericMaskModule() {
            ViewModel = new NumericMaskViewModel();
            InitializeComponent();
            Root.Children.Where(x => x is TextEdit).ForEach(x => x.GotFocus += (s, e) => ViewModel.FocusedEditor = (TextEdit)x);
        }

        public NumericMaskViewModel ViewModel { get; }

    }

    [GenerateViewModel(ImplementISupportServices = true)]
    public partial class NumericMaskViewModel {
        [GenerateProperty]
        public TextEdit _FocusedEditor;

        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        TextInputMaskSettings _TextInputSettings;

        [GenerateProperty]
        string _Mask;

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
            }
            catch {
                await GetService<IMessageBoxService>().ShowAsync("Invalid mask", "Error");
                TextInputSettings.Mask = maskBackup;
                Mask = maskBackup;
            }
        }
    }
}
