using System;
using DevExpress.Mvvm;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Text;
using DevExpress.Mvvm.CodeGenerators;
using FeatureDemo;

namespace EditorsDemo {
    public sealed partial class EditorButtonsModule : DemoModuleView {
        public EditorButtonsModule() {
            ViewModel = new EditorButtonsViewModel();
            this.InitializeComponent();
        }

        public EditorButtonsViewModel ViewModel { get; }
    }

    [GenerateViewModel]
    public partial class EditorButtonsViewModel {
        public EditorButtonsViewModel() {
            CustomText = "Editor with custom buttons";
            TextAlignment = TextAlignment.Left;
            IsBold = true;
        }

        [GenerateCommand]
        void Increase() => Value++;
        bool CanIncrease() => Value< 10;

        [GenerateCommand]
        void Decrease() => Value--;
        bool CanDecrease() => Value > 0;

        [GenerateCommand]
        void Undo(TextEdit edit) => edit.Undo();

        [GenerateCommand]
        void Edit() => ShowDialog("The EditCommand executed!");

        [GenerateCommand]
        void Copy() {
            var dataPackage = new DataPackage();
            dataPackage.SetText(CustomText);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            ShowDialog($"The text '{CustomText}' was copied");
        }

        [GenerateProperty]
        int _Value;

        [GenerateProperty]
        string _CustomText;

        [GenerateProperty]
        bool _IsBold;

        [GenerateProperty]
        bool _IsItalic;

        [GenerateProperty]
        bool _AlignLeft;
        void OnAlignLeftChanged() => TextAlignment = (AlignLeft ? TextAlignment.Left : TextAlignment);

        [GenerateProperty]
        bool _AlignCenter;
        void OnAlignCenterChanged() => TextAlignment = (AlignCenter ? TextAlignment.Center : TextAlignment);

        [GenerateProperty]
        bool _AlignRight;
        void OnAlignRightChanged() => TextAlignment = (AlignRight ? TextAlignment.Right : TextAlignment);

        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        TextAlignment _TextAlignment;
        void OnTextAlignmentChanged() {
            AlignLeft = TextAlignment == TextAlignment.Left;
            AlignCenter = TextAlignment == TextAlignment.Center;
            AlignRight = TextAlignment == TextAlignment.Right;
        }

        void OnValueChanged() {
            IncreaseCommand.RaiseCanExecuteChanged();
            DecreaseCommand.RaiseCanExecuteChanged();
        }
        async void ShowDialog(string content) {
            var xamlRoot = ((App)App.Current).MainWindow.Content.XamlRoot;
            if (xamlRoot != null) {
                var dlg = new ContentDialog() {
                    Title = "Result Dialog",
                    Content = content,
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot,
                    RequestedTheme = ((FrameworkElement)xamlRoot.Content).RequestedTheme
                };
                await dlg.ShowAsync().AsTask();
            }
        }
    }

    public class BoolToBoldConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) => (bool)value ? FontWeights.Bold : FontWeights.Normal;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }

    public class BoolToItalicConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) => (bool)value ? FontStyle.Italic : FontStyle.Normal;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
