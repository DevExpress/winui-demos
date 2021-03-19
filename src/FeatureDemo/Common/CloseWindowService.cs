using DevExpress.Mvvm.UI;
using System;
using Microsoft.UI.Xaml;
using DevExpress.WinUI.Mvvm.UI;

namespace FeatureDemo.Common {
    public interface ICloseWindowService {
        void Close();
    }
    public class CloseWindowService : ServiceBase, ICloseWindowService {
        private Action CloseProc { get; set; }
        public CloseWindowService(Action closeProc) {
            CloseProc = closeProc;
        }
        public void Close() {
            CloseProc?.Invoke();
        }
    }
}