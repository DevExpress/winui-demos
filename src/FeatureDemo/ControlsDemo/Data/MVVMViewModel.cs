using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.WinUI.Core;
using Windows.ApplicationModel.DataTransfer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace ControlsDemo {
    public class ViewModel {
        public List<Item> Items { get; set; }
    }
    public class Container : Item {
        public List<Item> Items { get; set; }
    }
    public class Item {
        public Item() {
            Index = -1;
        }
        public string Label { get; set; }
        public Uri Uri { get; set; }
        public int Index { get; set; }
    }
}
