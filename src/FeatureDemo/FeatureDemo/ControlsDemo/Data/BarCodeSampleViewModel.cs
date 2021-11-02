using DevExpress.WinUI.Controls;
using DevExpress.WinUI.Controls.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.PointOfService;
using DevExpress.Mvvm.CodeGenerators;

namespace FeatureDemo.ControlsDemo {
    [GenerateViewModel]
    public partial class BarCodeSampleViewModel  {
        #region props
        [GenerateProperty]
        bool _ShowText;

        [GenerateProperty]
        bool _AutoModule;

        [GenerateProperty]
        string _Text;

        [GenerateProperty]
        double _BarCodeModule;

        [GenerateProperty]
        BarCodeSymbology _BarCodeType;

        [GenerateProperty]
        SymbologyViewModel _SymbologyViewModel;

        #endregion
        public BarCodeSampleViewModel() {
            Text = "101";
            ShowText = true;
            BarCodeTypes = GetBarCodeTypes();
            BarCodeType = this.BarCodeTypes.First();
            AutoModule = false;
            BarCodeModule = 3d;
        }
        Dictionary<BarCodeSymbology, SymbologyViewModel> symbologyCache = new Dictionary<BarCodeSymbology, SymbologyViewModel>();
        private void OnBarCodeTypeChanged() {            
            if(!symbologyCache.TryGetValue(BarCodeType, out var res)) {
                res = new SymbologyViewModel(BarCodeSymbologyStorage.Create(BarCodeType), BarCodeType);
                symbologyCache.Add(BarCodeType, res);
            }
            SymbologyViewModel = res;
        }

        public IEnumerable<BarCodeSymbology> BarCodeTypes { get; private set; }
        IEnumerable<BarCodeSymbology> GetBarCodeTypes() {
            var symbologies = Enum.GetValues(typeof(BarCodeSymbology)).Cast<BarCodeSymbology>();
            return symbologies.TakeWhile(x => !IsSymbology2D(x)).ToArray();
        }
        static bool IsSymbology2D(BarCodeSymbology symbology) {
            return symbology == BarCodeSymbology.QRCode || 
                symbology == BarCodeSymbology.DataMatrix || 
                symbology == BarCodeSymbology.DataMatrixGS1 || 
                symbology == BarCodeSymbology.PDF417;
        }
    }
    public class SymbologyViewModel {
        public BarCodeSymbology BarCodeType { get; }
        public SymbologyBase Symbology { get;  }
        public SymbologyViewModel(SymbologyBase symbology, BarCodeSymbology barCodeType) {
            BarCodeType = barCodeType;
            Symbology = symbology;
        }
    }
}
