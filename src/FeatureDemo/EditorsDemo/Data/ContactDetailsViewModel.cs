using DevExpress.Mvvm;
using FeatureDemo.Data;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Linq;

namespace EditorsDemo {
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
            var dlg = new ContentDialog() {
                Title = "Result Dialog",
                Content = content,
                CloseButtonText = "OK"
            };
            await dlg.ShowAsync();
        }
    }
}
