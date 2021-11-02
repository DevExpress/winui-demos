using System;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using FeatureDemo.Data;
using Microsoft.UI.Xaml.Controls;
using DevExpress.Mvvm.CodeGenerators;
using FeatureDemo;
using Microsoft.UI.Xaml;

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

    [GenerateViewModel]
    public partial class ContactDetailsViewModel {

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
        }

        public Employee Employee { get; }
        [GenerateCommand]
        void CallPhone() => ShowDialog("The CallPhoneCommand executed!");
        [GenerateCommand]
        void SendEmail() => ShowDialog("The SendEmailCommand executed!");
        [GenerateCommand]
        void LoadPhoto() => ShowDialog("The LoadPhotoCommand executed!");

        async void ShowDialog(string content) {
            var xamlRoot = ((App)App.Current).MainWindow.Content?.XamlRoot;
            if (xamlRoot != null) {
                var dlg = new ContentDialog() {
                    Title = "Result Dialog",
                    Content = content,
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot,
                    RequestedTheme = ((FrameworkElement)xamlRoot.Content).RequestedTheme
                };
                await dlg.ShowAsync().AsTask();
            }
        }
    }
}
