using DevExpress.WinUI.Core;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeTypeTemplateSelector : TypeTemplateSelector {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            var viewModel = item as SymbologyViewModel;            
            string key = viewModel?.Symbology?.GeneratorBase.Name;
            if (string.IsNullOrEmpty(key))
                return null;
            IEnumerator<DataTemplate> t = (from p in Templates where p.Key == key select p.Value).GetEnumerator();
            return t.MoveNext() ? t.Current : null;
        }
    }
}
