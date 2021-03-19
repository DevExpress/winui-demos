using System;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.WinUI.Mvvm.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo.Common {
    public interface IResetScrollViewService {
        void Reset();
    }
    public class ResetScrollViewService : ServiceBase, IResetScrollViewService {
        public void Reset() {
            ScrollViewer viewer = (ScrollViewer)AssociatedObject;
            if(viewer.Parent != null) {
                ((ScrollViewer)AssociatedObject).Do(x => x.ChangeView(0, 0, null, true));
            } else {
                viewer.Loaded += OnScrollViewerLoaded;
            }
        }

        private void OnScrollViewerLoaded(object sender, RoutedEventArgs e) {
            ((ScrollViewer)AssociatedObject).Do(x => x.ChangeView(0, 0, null, true));
            ((ScrollViewer)AssociatedObject).Loaded -= OnScrollViewerLoaded;
        }
    }
}