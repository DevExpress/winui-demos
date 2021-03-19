using DevExpress.Mvvm;
using DevExpress.WinUI.Controls;
using DevExpress.WinUI.Mvvm.Internal;
using DevExpress.WinUI.XtraPrinting.BarCode;
using DevExpress.WinUI.XtraPrinting.BarCode.Internal;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.PointOfService;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeSample2DViewModel : ViewModelBase {
        public ICommand SetViewIndexCommand { get; }
        public object SelectedView {
            get => GetProperty<object>();
            private set => SetProperty<object>(value);
        }
        public int SelectedViewIndex {
            get => GetProperty<int>();
            set => SetProperty(value, OnSelectedViewModelIndexChanged);
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
            get => GetProperty<string>();
            set => SetProperty(value);
        }
    }
    public class QRCodeViewModel : BarCode2DViewModelBase {
        public QRCodeVersion Version {
            get => GetProperty<QRCodeVersion>();
            set => SetProperty(value);
        }
        public QRCodeViewModel() {
            Text = "QRCode";
        }
    }
    public class PDF417ViewModel : BarCode2DViewModelBase {
        public ErrorCorrectionLevel ErrorCorrectionLevel {
            get => GetProperty<ErrorCorrectionLevel>();
            set => SetProperty(value);
        }
        public PDF417ViewModel() {
            Text = "PDF417";
        }
    }
    public class DataMatrixViewModel : BarCode2DViewModelBase {
        public DataMatrixCompactionMode CompactionMode {
            get => GetProperty<DataMatrixCompactionMode>();
            set => SetProperty(value);
        }
        public DataMatrixViewModel() {
            Text = "DataMatrix";
            CompactionMode = DataMatrixCompactionMode.C40;
        }
    }
    public class DataMatrixGS1ViewModel : BarCode2DViewModelBase {
        public DataMatrixSize MatrixSize {
            get => GetProperty<DataMatrixSize>();
            set => SetProperty(value);
        }
        public DataMatrixGS1ViewModel() {
            Text = "DataMatrixGS1";
        }
    }
}
