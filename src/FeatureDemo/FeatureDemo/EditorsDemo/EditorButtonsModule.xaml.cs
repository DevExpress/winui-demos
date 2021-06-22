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
using DevExpress.WinUI.Core.Internal;

namespace EditorsDemo {
    public sealed partial class EditorButtonsModule : DemoModuleView {
        public EditorButtonsModule() {
            ViewModel = new EditorButtonsViewModel();
            this.InitializeComponent();
        }

        public EditorButtonsViewModel ViewModel { get; }
    }

    public class EditorButtonsViewModel : ViewModelBase {

        public EditorButtonsViewModel() {
            IncreaseCommand = new DelegateCommand(() => Value++, () => Value < 10);
            DecreaseCommand = new DelegateCommand(() => Value--, () => Value > 0);
            UndoCommand = new DelegateCommand<TextEdit>(x => x.Undo());
            EditCommand = new DelegateCommand(() => ShowDialog("The EditCommand executed!"));
            CopyCommand = new DelegateCommand(() => {
                var dataPackage = new DataPackage();
                dataPackage.SetText(CustomText);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
                ShowDialog($"The text '{CustomText}' was copied");
            });
            CustomText = "Editor with custom buttons";
            TextAlignment = TextAlignment.Left;
            IsBold = true;
        }

        public DelegateCommand IncreaseCommand { get; }
        public DelegateCommand DecreaseCommand { get; }
        public DelegateCommand CopyCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand<TextEdit> UndoCommand { get; }
        public int Value {
            get => GetValue<int>();
            private set => SetValue(value, OnValueChanged);
        }
        public string CustomText {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public bool IsBold {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool IsItalic {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool AlignLeft {
            get => GetValue<bool>();
            set => SetValue(value, () => TextAlignment = value ? TextAlignment.Left : TextAlignment);
        }
        public bool AlignCenter {
            get => GetValue<bool>();
            set => SetValue(value, () => TextAlignment = value ? TextAlignment.Center : TextAlignment);
        }
        public bool AlignRight {
            get => GetValue<bool>();
            set => SetValue(value, () => TextAlignment = value ? TextAlignment.Right : TextAlignment);
        }
        public TextAlignment TextAlignment {
            get => GetValue<TextAlignment>();
            private set => SetValue(value, OnTextAlignmentChanged);
        }

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
            var xamlRoot = CurrentWindowHelper.CurrentWindow?.Content?.XamlRoot;
            if (xamlRoot != null) {
                var dlg = new ContentDialog() {
                    Title = "Result Dialog",
                    Content = content,
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
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
