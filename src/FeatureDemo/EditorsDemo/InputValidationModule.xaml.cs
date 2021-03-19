using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using System;

namespace EditorsDemo {
    public sealed partial class InputValidationModule : DemoModuleView {
        public InputValidationModule() {
            this.InitializeComponent();
        }

        void ValidateCritical(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (e.Value == null) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "The value cannot be empty.";
            }

        }
        void ValidateInfo(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (e.Value == null) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Information;
                e.ErrorContent = "The value is empty.";
            }
        }
        void ValidateTitle(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
             if(string.IsNullOrEmpty(e.Value?.ToString())) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Warning;
                e.ErrorContent = "The job title is empty.";
            }
        }
        void ValidateHireDate(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (e.Value is DateTime date && date > DateTime.Now) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Warning;
                e.ErrorContent = "The hire date is greater than today.";
            }
            else if(e.Value == null) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "The hire date cannot be empty.";
            }
        }

    }
}
