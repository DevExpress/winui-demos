using DevExpress.XtraReports.UI;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReportsDemo {
    public class ReportDemoModule : DemoModuleView, INotifyPropertyChanged {

        static bool isLicenseMessageVisibleStatic = true;

        public ReportDemoModule() {
            var resources = new ResourceDictionary();
            resources.MergedDictionaries.Add(new ResourceDictionary { Source =  new Uri("ms-appx:///ReportsDemo/ReportDemoResources.xaml") });
            Resources = resources;
            IsLicenseMessageVisible = isLicenseMessageVisibleStatic;
        }

        XtraReport report;
        public XtraReport Report {
            get => report;
            set {
                if(report != value) {
                    report = value;
                    RaisePropertyChanged();
                }
            }
        }
        bool isLicenseMessageVisible = true;
        public bool IsLicenseMessageVisible {
            get => isLicenseMessageVisible;
            set {
                isLicenseMessageVisible = isLicenseMessageVisibleStatic = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName] string propName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
