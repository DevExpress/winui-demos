using DevExpress.WinUI.Editors;
using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Grid.Internal;

namespace GridDemo {
    public sealed partial class InputValidation : GridDemoUserControl {
        public InputValidationViewModel ViewModel { get; } = new InputValidationViewModel();
        public InputValidation() {
            InitializeComponent();
        }
        protected internal override GridControl Grid => grid;

        private void ValidateDemoRow(object sender, GridRowValidationEventArgs e) {
            e.Handled = true;
            if (e.Row is ValidationInvoices row) {
                e.IsValid = row.OrderDate < row.RequiredDate;
                if (!e.IsValid)
                    e.ErrorContent = "\"Order Date\" must precede \"Required Date\"";
            }
        }

        private void grid_InvalidRowException(object sender, InvalidRowExceptionEventArgs e) {
            if (e.Row is ValidationInvoices row) {
                if (row.OrderDate >= row.RequiredDate) {
                    e.ExceptionMode = ExceptionMode.DisplayError;
                    e.WindowCaption = "Error";
                    e.ErrorText = "\"Order Date\" must precede \"Required Date\"";
                    return;
                }
            }
        }

        private void ProductNameValidate(object sender, GridCellValidationEventArgs e) {
            if (string.IsNullOrEmpty(e.Value?.ToString()))
                e.IsValid = false;
            if (!e.IsValid) {
                e.Handled = true;
                e.ErrorType = ErrorType.Warning;
                e.ErrorContent = "Product Name can't be empty";
            }
        }

        private void colOrderIDValidate(object sender, GridCellValidationEventArgs e) {
            var stringId = e.Value?.ToString();
            if (string.IsNullOrEmpty(stringId)) {
                e.IsValid = false;
                e.ErrorContent = "Order ID can't be empty";
                e.ErrorType = ErrorType.Information;
                e.Handled = true;
                return;
            }
            e.IsValid = int.TryParse(stringId, out var id);
            if (!e.IsValid) {
                e.ErrorContent = "Order ID must be integer";
                e.ErrorType = ErrorType.Critical;
                e.Handled = true;
            }
            if (id <= 0) {
                e.IsValid = false;
                e.ErrorContent = "Order ID must be greater than 0";
                e.ErrorType = ErrorType.Default;
                e.Handled = true;
            }
        }

        void colCustomerID_Validate(object sender, GridCellValidationEventArgs e) {
            var stringId = e.Value?.ToString();
            if (string.IsNullOrEmpty(stringId)) {
                e.ErrorContent = "Customer ID can't be empty";
                e.ErrorType = ErrorType.Information;
                e.Handled = true;
                e.IsValid = false;
                return;
            }
            
            
            
            
            
            
            
            if (stringId.ToUpper() != stringId) {
                e.ErrorContent = "Customer ID must contain only upper case letters";
                e.ErrorType = ErrorType.Information;
                e.Handled = true;
                e.IsValid = false;
            }
        }

        private void colFreight_Validate(object sender, GridCellValidationEventArgs e) {
            var stringFreight = e.Value?.ToString();
            if (string.IsNullOrEmpty(stringFreight)) {
                e.IsValid = false;
                e.ErrorContent = "Freight can't be empty";
                e.ErrorType = ErrorType.Information;
                e.Handled = true;
                return;
            }
            e.IsValid = decimal.TryParse(stringFreight, out var freight);
            if (!e.IsValid) {
                e.ErrorContent = "Freight must be decimal";
                e.ErrorType = ErrorType.Critical;
                e.Handled = true;
            }
            if (freight <= 0) {
                e.IsValid = false;
                e.ErrorContent = "Freight must be greater than 0";
                e.ErrorType = ErrorType.Default;
                e.Handled = true;
            }
        }
    }
}
