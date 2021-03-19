using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using DevExpress.Mvvm;
using Windows.Storage;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Media.Imaging;

using PropertyChangedEventHandler = Microsoft.UI.Xaml.Data.PropertyChangedEventHandler;
using INotifyPropertyChanged = Microsoft.UI.Xaml.Data.INotifyPropertyChanged;
using PropertyChangedEventArgs = Microsoft.UI.Xaml.Data.PropertyChangedEventArgs;

namespace FeatureDemo.Data {
    public class Photo : INotifyPropertyChanged {
        public byte[] Picture { get; set; }
        BitmapImage imageSource;
        [XmlIgnore]
        public BitmapImage ImageSource {
            get {
                if(imageSource == null && Picture != null) {
                    SetImageSource();
                }
                return imageSource;
            }
        }
        async void SetImageSource() {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(Picture.AsBuffer());
            stream.Seek(0);

            imageSource = new BitmapImage();
            imageSource.SetSource(stream);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public partial class Customer {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
    public partial class Order {
        public long OrderID { get; set; }
        public string CustomerID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public Nullable<long> ShipVia { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetailsExtended> OrderDetails { get; set; }
    }
    public partial class OrderDetailsExtended {
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
        public decimal ExtendedPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
    public partial class Product {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string EAN13 { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetailsExtended> OrderDetails { get; set; }
    }
    public partial class Category {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Icon25 { get; set; }
        public byte[] Icon17 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    [XmlRoot("Employees")]
    public class Employees : List<Employee> {
    }
    [XmlRoot("Employee")]
    public class Employee : INotifyPropertyChanged {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryRegionName { get; set; }
        public string GroupName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        BitmapImage imageSource;
        [XmlIgnore]
        public BitmapImage ImageSource {
            get {
                if(imageSource == null && ImageData != null) {
                    SetImageSource();
                }
                return imageSource;
            }
        }
        [XmlIgnore]
        public string FullName { get { return FirstName + " " + LastName; } }
        [XmlIgnore]
        public long EmployeeID { get { return Id; } }
        [XmlIgnore]
        public virtual ICollection<Customer> Customers { get; set; }
        [XmlIgnore]
        public virtual ICollection<Order> Orders { get; set; }
        [XmlIgnore]
        public virtual ICollection<Employee> Employees { get; set; }

        async void SetImageSource() {
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(ImageData.AsBuffer());
            stream.Seek(0);

            imageSource = new BitmapImage();
            imageSource.SetSource(stream);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [XmlRoot("Appointments")]
    public class Appointments : List<Appointment> {
    }

    [XmlRoot("Appointment")]
    public class Appointment {
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }
        [XmlAttribute("Description")]
        public string Description { get; set; }
        [XmlAttribute("Location")]
        public string Location { get; set; }
        [XmlAttribute("Priority")]
        public Priority Priority { get; set; }
        [XmlAttribute("Subject")]
        public string Subject { get; set; }
        [XmlIgnore]
        public string Summary {
            get {
                return string.Format("{0:hh:mm tt}  {1} ({2})", Date, Subject, Location);
            }
        }
    }

    [XmlRoot("Messages")]
    public class Messages : List<Message> {
    }
    [XmlRoot("Message")]
    public class Message {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool HasAttachement { get; set; }
        public string Folder { get; set; }
        public bool IsReply { get; set; }
    }
    [XmlRoot("Items")]
    public class Items : List<Item> {
        
    }
    public enum Priority { Low, Medium, High }
    public enum Status { New, Postponed, Fixed, Rejected }
    [XmlRoot("Items")]
    public class Item : BindableBase {
        DateTime createdDate;
        int creatorId;
        string description;
        DateTime fixedDate;
        int id;
        DateTime modifiedDate;
        string name;
        int ownerId;
        Priority priority;
        Status status;
        int projectId;
        double progress;
        bool hasAttachment;

        public DateTime CreatedDate {
            get { return createdDate; }
            set { SetProperty(ref createdDate, value, () => CreatedDate); }
        }
        public int CreatorID {
            get { return creatorId; }
            set { SetProperty(ref creatorId, value, () => CreatorID); }
        }
        public string Description {
            get { return description; }
            set { SetProperty(ref description, value, () => Description); }
        }
        public DateTime FixedDate {
            get { return fixedDate; }
            set { SetProperty(ref fixedDate, value, () => FixedDate); }
        }
        public int ID {
            get { return id; }
            set { SetProperty(ref id, value, () => ID); }
        }
        public DateTime ModifiedDate {
            get { return modifiedDate; }
            set { SetProperty(ref modifiedDate, value, () => ModifiedDate); }
        }
        public string Name {
            get { return name; }
            set { SetProperty(ref name, value, () => Name); }
        }
        public int OwnerID {
            get { return ownerId; }
            set { SetProperty(ref ownerId, value, () => OwnerID); }
        }
        public Priority Priority {
            get { return priority; }
            set { SetProperty(ref priority, value, () => Priority); }
        }
        public Status Status {
            get { return status; }
            set { SetProperty(ref status, value, () => Status); }
        }
        public int ProjectID {
            get { return projectId; }
            set { SetProperty(ref projectId, value, () => ProjectID); }
        }
        public double Progress {
            get { return progress; }
            set { SetProperty(ref progress, value, () => Progress); }
        }
        public bool HasAttachment {
            get { return hasAttachment; }
            set { SetProperty(ref hasAttachment, value, () => HasAttachment); }
        }
    }

    public class DataStorage {
        List<string> citiesPhotoLibrary;
        public List<string> CitiesPhotoLibrary {
            get {
                if(citiesPhotoLibrary != null)
                    return citiesPhotoLibrary;
                
                citiesPhotoLibrary = new List<string>();
                StorageFolder rootFolder = StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path +
                    @"\MapDemo\Data\Images\PhotoGallery").AsTask().Result;
                var folders = rootFolder.GetFoldersAsync().AsTask().Result;
                foreach(var folder in folders) {
                    var files = folder.GetFilesAsync().AsTask().Result;
                    foreach(var file in files) {
                        citiesPhotoLibrary.Add("ms-appx:///MapDemo/Data/Images/PhotoGallery/" + folder.Name + "/" + file.Name);
                    }
                }
                return citiesPhotoLibrary;
            }
        }

        Employees employees;
        public Employees Employees {
            get {
                if(employees != null)
                    return employees;
                try {
                    StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Employees.xml")).AsTask().Result;
                    Stream stream = file.OpenStreamForReadAsync().Result;
                    XmlSerializer serializier = new XmlSerializer(typeof(Employees));
                    employees = serializier.Deserialize(stream) as Employees;
                } catch {
                    employees = new Employees();
                    employees.Add(new Employee() {
                        Id = 109,
                        FirstName = "Bruce",
                        LastName = "Cambell",
                        JobTitle = "Chief Executive Officer",
                        Phone = "(417) 166-3268",
                        EmailAddress = "Bruce_Cambell@example.com",
                        AddressLine1 = "4228 S National Ave",
                        City = "Tokyo",
                        PostalCode = "65809",
                        CountryRegionName = "Japan",
                        GroupName = "Executive General and Administration",
                        Gender = "M",
                        Title = "Mr."
                    });
                }
                return employees;
            }
        }
        Items items;
        public Items Items {
            get {
                if(items != null)
                    return items;
                StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Items.xml")).AsTask().Result;
                Stream stream = file.OpenStreamForReadAsync().Result;
                XmlSerializer serializier = new XmlSerializer(typeof(Items));
                items = serializier.Deserialize(stream) as Items;
                return items;
            }
        }

        Appointments appointments;

        public Appointments Appointments {
            get {
                if(appointments != null)
                    return appointments;

                StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Appointments.xml")).AsTask().Result;
                Stream stream = file.OpenStreamForReadAsync().Result;
                XmlSerializer serializier = new XmlSerializer(typeof(Appointments));
                appointments = serializier.Deserialize(stream) as Appointments;
                return appointments;
            }
        }
        Messages messages;
        public Messages Messages {
            get {
                if(messages != null)
                    return messages;

                StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Messages.xml")).AsTask().Result;
                Stream stream = file.OpenStreamForReadAsync().Result;
                XmlSerializer serializier = new XmlSerializer(typeof(Messages));
                messages = serializier.Deserialize(stream) as Messages;
                return messages;
            }
        }
    }
    public class EmployeesData {
        public Employees DataSource {
            get { return new DataStorage().Employees; }
        }
    }
    public class Invoices {
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string CustomerID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Salesperson { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal ExtendedPrice { get; set; }
        public decimal Freight { get; set; }
    }
    public class ItemsData {
        public Items DataSource {
            get { return new DataStorage().Items; }
        }
        static readonly Priority[] priorities = new Priority[] { Priority.Low, Priority.Medium, Priority.High };
        public Priority[] Priorities { get { return priorities; } }
        static readonly Status[] statuses = new Status[] { Status.New, Status.Postponed, Status.Fixed, Status.Rejected };
        public Status[] Statuses { get { return statuses; } }

        public User[] Users { get { return User.Users; } }
    }
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }

        public static readonly User[] Users = new User[] {
            new User() { Id = 0, Name = "Peter Dolan"},
            new User() { Id = 1, Name = "Ryan Fischer"},
            new User() { Id = 2, Name = "Richard Fisher"},
            new User() { Id = 3, Name = "Tom Hamlett" },
            new User() { Id = 4, Name = "Mark Hamilton" },
            new User() { Id = 5, Name = "Steve Lee" },
            new User() { Id = 6, Name = "Jimmy Lewis" },
            new User() { Id = 7, Name = "Jeffrey W McClain" },
            new User() { Id = 8, Name = "Andrew Miller" },
            new User() { Id = 9, Name = "Dave Murrel" },
            new User() { Id = 10, Name = "Bert Parkins" },
            new User() { Id = 11, Name = "Mike Roller" },
            new User() { Id = 12, Name = "Ray Shipman" },
            new User() { Id = 13, Name = "Paul Bailey" },
            new User() { Id = 14, Name = "Brad Barnes" },
            new User() { Id = 15, Name = "Carl Lucas" },
            new User() { Id = 16, Name = "Jerry Campbell" },
        };
    }
}