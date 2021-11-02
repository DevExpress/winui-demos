using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DevExpress.Mvvm;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using DevExpress.Mvvm.CodeGenerators;
using System.Data;
using Windows.Devices.Usb;
using DevExpress.Pdf.Native;
using Microsoft.UI.Xaml.Media;

namespace FeatureDemo.Data {
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
                if (imageSource == null && ImageData != null) {
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageSource)));
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
    [XmlRoot("Items"), GenerateViewModel]
    public partial class Item {
        [GenerateProperty]
        DateTime createdDate;
        [GenerateProperty]
        int creatorID;
        [GenerateProperty]
        string description;
        [GenerateProperty]
        DateTime fixedDate;
        [GenerateProperty]
        int id;
        [GenerateProperty]
        DateTime modifiedDate;
        [GenerateProperty]
        string name;
        [GenerateProperty]
        int ownerID;
        [GenerateProperty]
        Priority priority;
        [GenerateProperty]
        Status status;
        [GenerateProperty]
        int projectID;
        [GenerateProperty]
        double progress;
        [GenerateProperty]
        bool hasAttachment;
    }

    public class DataStorage {
        static XmlSerializer EmployeeSerializer = XmlSerializer.FromTypes(new[] { typeof(Employees) })[0];
        static XmlSerializer ItemsSerializer = XmlSerializer.FromTypes(new[] { typeof(Items) })[0];

        Employees employees;
        public Employees Employees {
            get {
                if (employees != null)
                    return employees;
                try {
                    StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Employees.xml")).AsTask().Result;
                    Stream stream = file.OpenStreamForReadAsync().Result;
                    employees = EmployeeSerializer.Deserialize(stream) as Employees;
                }
                catch {
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
                if (items != null)
                    return items;
                
                StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Items.xml")).AsTask().Result;
                Stream stream = file.OpenStreamForReadAsync().Result;
                items = ItemsSerializer.Deserialize(stream) as Items;
                return items;
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

    public class VehiclesData {
        public static readonly Random random = new Random();
        public enum Category {
            Car = 1,
            CrossoverAndSUV = 2,
            Truck = 3,
            Minivan = 4
        }
        public class Trademark {
            
            public int ID { get; set; }
            public ImageSource Logo { get; set; }
            public byte[] LogoData { get; set; }
            public string Name { get; set; }
            
            
            
            
            
            
            
        }
        public class OrderItem {
            internal Model Model;
            public OrderItem(int totalCount, List<Model> models, int id)
                : this(totalCount, models[id % models.Count], id) {
            }
            public OrderItem(int totalCount, Model model, int id) {
                Model = model;
                ModelPrice = model.Price;
                Trademark = model.Trademark;
                Name = model.Name;
                Modification = model.Modification;
                Category = model.Category;
                MPGCity = model.MPGCity;
                MPGHighway = model.MPGHighway;
                Doors = model.Doors;
                BodyStyle = model.BodyStyle;
                Cylinders = model.Cylinders;
                Horsepower = model.Horsepower;
                Torque = model.Torque;
                TransmissionSpeeds = model.TransmissionSpeeds;
                TransmissionType = model.TransmissionType;
                Discount = Math.Round(0.05 * ((id * Trademark) % 4), 2);
                OrderID = id;
                if(totalCount > 0)
                    CreateSalesInfo(id, totalCount);
            }

            void CreateSalesInfo(int id, int totalCount) {
                var salesPerDay = totalCount / (365.25 * 7);
                var lastSaleDateTime = DateTime.Today.AddHours(-15);
                SalesDate = lastSaleDateTime.AddDays(-id / salesPerDay);
                var orderWithinYearId = (int)Math.Floor((SalesDate - new DateTime(SalesDate.Year, 1, 1)).TotalDays * salesPerDay) + 1;
                SalesID = string.Format("{0:d4}-<size=-1><b>{1:d6}</b>", SalesDate.Year, orderWithinYearId);
            }
            public OrderItem(Model model, int days, Random rnd, int id) : this(-1, model, id) {
                Discount = Math.Round(0.05 * rnd.Next(4), 2);
                SalesDate = DateTime.Now.AddDays(-rnd.Next(days));
            }
            public int OrderID { get; set; }
            public string SalesID { get; set; }
            
            public DateTime SalesDate { get; set; }
            
            public double Discount { get; set; }
            
            public decimal ModelPrice { get; set; }
            public int Trademark { get; set; }
            public string Name { get; set; }
            public string Modification { get; set; }
            public int Category { get; set; }
            public int? MPGCity { get; set; }
            public int? MPGHighway { get; set; }
            public int Doors { get; set; }
            public int BodyStyle { get; set; }
            public int Cylinders { get; set; }
            public string Horsepower { get; set; }
            public string Torque { get; set; }
            public int TransmissionSpeeds { get; set; }
            public int TransmissionType { get; set; }
            
        }
        public class Model {
            public int ID { get; set; }
            public int Trademark { get; set; }
            public string Name { get; set; }
            public string Modification { get; set; }
            public int Category { get; set; }
            public decimal Price { get; set; }
            public int? MPGCity { get; set; }
            public int? MPGHighway { get; set; }
            public int Doors { get; set; }
            public int BodyStyle { get; set; }
            public int Cylinders { get; set; }
            public string Horsepower { get; set; }
            public string Torque { get; set; }
            public int TransmissionSpeeds { get; set; }
            public int TransmissionType { get; set; }
            public string Description { get; set; }
            
            
            public DateTime DeliveryDate { get; set; }
            public bool InStock { get; set; }
            public ImageSource TrademarkImage => Trademarks?[Trademark - 1].Logo;
            public byte[] TrademarkImageData => Trademarks?[Trademark - 1].LogoData;
            public string TrademarkName { get { return Trademarks != null ? Trademarks[Trademark - 1].Name : string.Empty; } }
            public string CategoryName {
                get => Enum.GetName<Category>((Category)Category);
            }
            public List<VehiclesData.Trademark> Trademarks = null;

        }
        static IEnumerable<Model> models;
        public static async Task<IEnumerable<Model>> GetModels()  => models ?? (models = await CreateModels(15));

        async static Task<List<Model>> CreateModels(int dataInterval) {
            string Model = "Model";
            string Trademark = "Trademark";
            var dataFile = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Vehicles.xml")).AsTask().Result;
            var ds = new DataSet();
            using var stream = await dataFile.OpenStreamForReadAsync();
            ds.ReadXml(stream);
            var listTrademarks = new List<VehiclesData.Trademark>();
            foreach(DataRow row in ds.Tables[Trademark].Rows)
                listTrademarks.Add(new VehiclesData.Trademark() {
                    ID = (int)(row["ID"]),
                    Name = (string)row["Name"],
                    LogoData = (byte[])row["Logo"],
                    Logo = await ToImageAsync((byte[])row["Logo"])
                });

            var listModels = new List<VehiclesData.Model>();
            foreach(DataRow row in ds.Tables[Model].Rows)
                listModels.Add(new VehiclesData.Model() {
                    ID = (int)row["ID"],
                    Name = (string)row["Name"],
                    Trademark = (int)row["TrademarkID"],
                    Modification = (string)row["Modification"],
                    Category = (int)row["CategoryID"],
                    Price = (decimal)row["Price"],
                    MPGCity = System.DBNull.Value.Equals(row["MPG City"]) ? null : (int?)row["MPG City"],
                    MPGHighway = System.DBNull.Value.Equals(row["MPG City"]) ? null : (int?)row["MPG Highway"],
                    Doors = (int)row["Doors"],
                    BodyStyle = (int)row["BodyStyleID"],
                    Cylinders = (int)row["Cylinders"],
                    Horsepower = (string)row["Horsepower"],
                    Torque = (string)row["Torque"],
                    TransmissionSpeeds = Convert.ToInt32(row["Transmission Speeds"]),
                    TransmissionType = (int)row["Transmission Type"],
                    Description = string.Format("{0}", row["Description"]),
                    
                    
                    DeliveryDate = DateTime.Now.AddDays(random.Next(dataInterval)),
                    InStock = random.Next(100) < 95,
                    Trademarks = listTrademarks
                });
            return listModels;
        }
        public static async Task<BitmapImage> ToImageAsync(byte[] bytes) {
            using var stream = new InMemoryRandomAccessStream();
            var writer = new DataWriter(stream.GetOutputStreamAt(0));
            writer.WriteBytes(bytes);
            await writer.StoreAsync();
            var image = new BitmapImage();
            await image.SetSourceAsync(stream);
            return image;
        }
    }
}