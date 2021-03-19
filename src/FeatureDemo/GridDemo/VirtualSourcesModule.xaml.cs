using DevExpress.WinUI.Grid;
using GridDemo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace GridDemo {
    public sealed partial class VirtualSourcesModule : GridDemoUserControl {
        public VirtualSourcesModule() {
            this.InitializeComponent();
        }

        protected internal override GridControl Grid => throw new NotImplementedException();

    }
}
