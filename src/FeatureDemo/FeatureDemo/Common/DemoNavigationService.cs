using DevExpress.WinUI.Core;
using FeatureDemo.View;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI.ViewManagement;

namespace FeatureDemo.Common {
    public interface IDemoNavigationService {
        void Navigate(string pageName, object parameter);
    }
    public class DemoNavigationService : ServiceBase, IDemoNavigationService {
        public DemoNavigationService() {

        }
        public void Navigate(string pageName, object parameter) {
            var frame = AssociatedObject as Frame;
            if(frame == null) return;
            Type pageType = null;
            switch(pageName) {
                case nameof(DemoModulePage):
                    pageType = typeof(DemoModulePage);
                  break;
                case nameof(GetStartedPage):
                    pageType = typeof(GetStartedPage);
                  break;
            }
            if(pageType == null) return;

            frame.Navigate(pageType, parameter);
        }
    }
}