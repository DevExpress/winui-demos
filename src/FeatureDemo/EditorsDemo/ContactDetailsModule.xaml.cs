using DevExpress.WinUI.Editors;
using FeatureDemo.Common;

namespace EditorsDemo {
    public sealed partial class ContactDetailsModule : DemoModuleView {
        public ContactDetailsModule() {
            this.InitializeComponent();
        }

        void OnValidate(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (string.IsNullOrEmpty(e.Value?.ToString())) {
                e.IsValid = false;
                e.Handled = true;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "The value cannot be empty.";
            }
        }
    }
}
