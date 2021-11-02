using DevExpress.Mvvm;
using DevExpress.WinUI.Controls.Internal;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm.CodeGenerators;

namespace FeatureDemo.ControlsDemo {
    [GenerateViewModel]
    public partial class BarCodeSample2DViewModel {
        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        object _SelectedView;

        [GenerateProperty]
        int _SelectedViewIndex;
        void OnSelectedViewIndexChanged() => SelectedView = Views[SelectedViewIndex];

        public List<object> Views { get; }
        public BarCodeSample2DViewModel() {
            Views = new List<object>() {
                new QRCodeViewModel(),
                new PDF417ViewModel(),
                new DataMatrixViewModel(),
                new DataMatrixGS1ViewModel(),
            };
            SelectedView = Views[0];
        }

        [GenerateCommand]
        public void SetViewIndex(int index) => SelectedViewIndex = index;
    }
    [GenerateViewModel]
    public partial class BarCode2DViewModelBase {
        [GenerateProperty]
        string _Text;
    }
    [GenerateViewModel]
    public partial class QRCodeViewModel : BarCode2DViewModelBase {
        [GenerateProperty]
        QRCodeVersion _Version;
        public QRCodeViewModel() {
            Text = "QRCode";
        }
    }
    [GenerateViewModel]
    public partial class PDF417ViewModel : BarCode2DViewModelBase {
        [GenerateProperty]
        ErrorCorrectionLevel _ErrorCorrectionLevel;
        public PDF417ViewModel() {
            Text = "PDF417";
        }
    }
    [GenerateViewModel]
    public partial class DataMatrixViewModel : BarCode2DViewModelBase {
        [GenerateProperty]
        DataMatrixCompactionMode _CompactionMode;
        public DataMatrixViewModel() {
            Text = "DataMatrix";
            CompactionMode = DataMatrixCompactionMode.C40;
        }
    }
    [GenerateViewModel]
    public partial class DataMatrixGS1ViewModel : BarCode2DViewModelBase {
        [GenerateProperty]
        DataMatrixSize _MatrixSize;
        public DataMatrixGS1ViewModel() {
            Text = "DataMatrixGS1";
        }
    }
}
