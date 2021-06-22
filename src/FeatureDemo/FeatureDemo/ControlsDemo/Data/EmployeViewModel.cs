using System;
using Windows.Graphics.Printing;
using Microsoft.UI.Xaml.Media;

namespace FeatureDemo.ControlsDemo {
    public class Employe : DevExpress.Mvvm.BindableBase {
        public BarCodeEmployeesViewModel ParentViewModel { get;  }
        public Employe(BarCodeEmployeesViewModel parentViewModel, FeatureDemo.Data.Employee employee) {
            ParentViewModel = parentViewModel;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Photo = employee.ImageSource;
            BirthDate = employee.BirthDate.HasValue ? employee.BirthDate.Value : DateTime.Now.AddYears(-32);
            Email = employee.EmailAddress;
            Phone = employee.Phone;
            City = employee.City;
            Region = employee.CountryRegionName;
            Address = employee.AddressLine1;
            Title = employee.JobTitle;
        }
        string firstNameCore;
        public string FirstName {
            get { return firstNameCore; }
            set { SetProperty(ref firstNameCore, value, nameof(FirstName), () => RaisePropertiesChanged("FullName")); }
        }
        string lastNameCore;
        public string LastName {
            get { return lastNameCore; }
            set { SetProperty(ref lastNameCore, value, nameof(LastName), () => RaisePropertiesChanged("FullName")); }
        }
        ImageSource photoCore;
        public ImageSource Photo {
            get { return photoCore; }
            set { SetProperty(ref photoCore, value, nameof(Photo)); }
        }
        public string FullName {
            get { return string.Join(", ", LastName, FirstName); }
        }
        System.DateTime birthDateCoreCore;
        public System.DateTime BirthDate {
            get { return birthDateCoreCore; }
            set { SetProperty(ref birthDateCoreCore, value, nameof(BirthDate)); }
        }
        string phoneCore;
        public string Phone {
            get { return phoneCore; }
            set { SetProperty(ref phoneCore, value, nameof(Phone)); }
        }
        string emailCore;
        public string Email {
            get { return emailCore; }
            set { SetProperty(ref emailCore, value, nameof(Email)); }
        }
        string cityCore;
        public string City {
            get { return cityCore; }
            set { SetProperty(ref cityCore, value, nameof(City)); }
        }
        string regionCore;
        public string Region {
            get { return regionCore; }
            set { SetProperty(ref regionCore, value, nameof(Region)); }
        }
        string addressCore;
        public string Address {
            get { return addressCore; }
            set { SetProperty(ref addressCore, value, nameof(Address)); }
        }
        string title;
        public string Title {
            get { return title; }
            set { SetProperty(ref title, value, nameof(Title)); }
        }
        IPrintDocumentSource printDocumentSource;
        public IPrintDocumentSource PrintDocumentSource {
            get { return printDocumentSource; }
            set { SetProperty(ref printDocumentSource, value, nameof(PrintDocumentSource)); }
        }
        public string Data {
            get {
                return string.Format("{1}{0}{2}{0}{3}{0}{4}", Environment.NewLine, FullName, Title, Phone, Email); 
            }
        }

    }
}