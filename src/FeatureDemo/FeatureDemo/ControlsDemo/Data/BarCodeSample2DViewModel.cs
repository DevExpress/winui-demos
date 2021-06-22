using DevExpress.Mvvm;
using DevExpress.WinUI.Controls.Internal;
using System.Collections.Generic;
using System.Windows.Input;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeSample2DViewModel : ViewModelBase {
        public ICommand SetViewIndexCommand { get; }
        public object SelectedView {
            get => GetValue<object>();
            private set => SetValue<object>(value);
        }
        public int SelectedViewIndex {
            get => GetValue<int>();
            set => SetValue(value, OnSelectedViewModelIndexChanged);
        }

        private void OnSelectedViewModelIndexChanged() => SelectedView = Views[SelectedViewIndex];

        public List<object> Views { get; }
        public BarCodeSample2DViewModel() {
            SetViewIndexCommand = new DelegateCommand<int>(OnSetViewIndexCommandExecuted);
            Views = new List<object>() {
                new QRCodeViewModel(),
                new PDF417ViewModel(),
                new DataMatrixViewModel(),
                new DataMatrixGS1ViewModel(),
            };
            SelectedView = Views[0];
        }

        private void OnSetViewIndexCommandExecuted(int index) => SelectedViewIndex = index;
    }
    public class BarCode2DViewModelBase : ViewModelBase {
        public string Text {
            get => GetValue<string>();
            set => SetValue(value);
        }
    }
    public class QRCodeViewModel : BarCode2DViewModelBase {
        public QRCodeVersion Version {
            get => GetValue<QRCodeVersion>();
            set => SetValue(value);
        }
        public QRCodeViewModel() {
            Text = "QRCode";
        }
    }
    public class PDF417ViewModel : BarCode2DViewModelBase {
        public ErrorCorrectionLevel ErrorCorrectionLevel {
            get => GetValue<ErrorCorrectionLevel>();
            set => SetValue(value);
        }
        public PDF417ViewModel() {
            Text = "PDF417";
        }
    }
    public class DataMatrixViewModel : BarCode2DViewModelBase {
        public DataMatrixCompactionMode CompactionMode {
            get => GetValue<DataMatrixCompactionMode>();
            set => SetValue(value);
        }
        public DataMatrixViewModel() {
            Text = "DataMatrix";
            CompactionMode = DataMatrixCompactionMode.C40;
        }
    }
    public class DataMatrixGS1ViewModel : BarCode2DViewModelBase {
        public DataMatrixSize MatrixSize {
            get => GetValue<DataMatrixSize>();
            set => SetValue(value);
        }
        public DataMatrixGS1ViewModel() {
            Text = "DataMatrixGS1";
        }
    }
}
