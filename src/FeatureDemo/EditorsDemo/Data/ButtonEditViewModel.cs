using DevExpress.Mvvm;
using DevExpress.WinUI.Editors;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace EditorsDemo {
    public class ButtonEditViewModel : ViewModelBase {

        public ButtonEditViewModel() {
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
            get => GetProperty<int>();
            private set => SetProperty(value, OnValueChanged);
        }
        public string CustomText {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public bool IsBold {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public bool IsItalic {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public bool AlignLeft {
            get => GetProperty<bool>();
            set => SetProperty(value, () => TextAlignment = value ? TextAlignment.Left : TextAlignment);
        }
        public bool AlignCenter {
            get => GetProperty<bool>();
            set => SetProperty(value, () => TextAlignment = value ? TextAlignment.Center : TextAlignment);
        }
        public bool AlignRight {
            get => GetProperty<bool>();
            set => SetProperty(value, () => TextAlignment = value ? TextAlignment.Right : TextAlignment);
        }
        public TextAlignment TextAlignment {
            get => GetProperty<TextAlignment>();
            private set => SetProperty(value, OnTextAlignmentChanged);
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
            var dlg = new ContentDialog() {
                Title = "Result Dialog",
                Content = content,
                CloseButtonText = "OK"
            };
            await dlg.ShowAsync();
        }
    }
}
