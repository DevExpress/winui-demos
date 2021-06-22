using System;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using FeatureDemo.Data;
using Microsoft.UI.Xaml.Controls;

namespace EditorsDemo {
    public sealed partial class ContactDetailsModule : DemoModuleView {
        public ContactDetailsModule() {
            ViewModel = new ContactDetailsViewModel();
            this.InitializeComponent();
        }

        public ContactDetailsViewModel ViewModel { get; }

        void OnValidate(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (string.IsNullOrEmpty(e.Value?.ToString())) {
                e.IsValid = false;
                e.Handled = true;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "The value cannot be empty.";
            }
        }
    }

    public class ContactDetailsViewModel : ViewModelBase {

        public ContactDetailsViewModel() {
            var employee = new DataStorage().Employees.First();
            Employee = new Employee() {
                Id = employee.Id,
                ParentId = employee.ParentId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                JobTitle = employee.JobTitle,
                Phone = employee.Phone,
                EmailAddress = employee.EmailAddress,
                AddressLine1 = employee.AddressLine1,
                City = employee.City,
                PostalCode = employee.PostalCode,
                CountryRegionName = employee.CountryRegionName,
                GroupName = employee.GroupName,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Gender = employee.Gender,
                MaritalStatus = employee.MaritalStatus,
                Title = employee.Title,
                ImageData = employee.ImageData,
            };
            Employee.LastName = null;
            CallPhoneCommand = new DelegateCommand(() => ShowDialog("The CallPhoneCommand executed!"));
            SendEmailCommand = new DelegateCommand(() => ShowDialog("The SendEmailCommand executed!"));
            LoadPhotoCommand = new DelegateCommand(() => ShowDialog("The LoadPhotoCommand executed!"));
        }

        public Employee Employee { get; }
        public ICommand CallPhoneCommand { get; }
        public ICommand SendEmailCommand { get; }
        public ICommand LoadPhotoCommand { get; }

        async void ShowDialog(string content) {
            var xamlRoot = CurrentWindowHelper.CurrentWindow?.Content?.XamlRoot;
            if (xamlRoot != null) {
                var dlg = new ContentDialog() {
                    Title = "Result Dialog",
                    Content = content,
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dlg.ShowAsync().AsTask();
            }
        }
    }
}
