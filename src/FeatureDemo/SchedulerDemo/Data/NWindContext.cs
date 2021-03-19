using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Windows.Storage;
using Microsoft.UI.Xaml.Data;

namespace DevExpress.DemoData.Models {
}

// AlphabeticalListOfProduct.cs

namespace DevExpress.DemoData.Models {
    public partial class AlphabeticalListOfProduct
    {
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
        public string CategoryName { get; set; }
    }
}

// Category.cs

namespace DevExpress.DemoData.Models {
    public partial class Category
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Icon25 { get; set; }
        public byte[] Icon17 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

// CategoryProduct.cs

namespace DevExpress.DemoData.Models {
    public partial class CategoryProduct
    {
        public long ProductID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }
    }
}

// CurrentProductList.cs

namespace DevExpress.DemoData.Models {
    public partial class CurrentProductList
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
    }
}

// Customer.cs

namespace DevExpress.DemoData.Models {
    [Bindable]
    public class CustomerEmployee {
        public string CustomerID { get; set; }
        [XmlIgnore]
        public Customer Customer { get; set; }

        public long EmployeeId { get; set; }
        [XmlIgnore]
        public Employee Employee { get; set; }
    }
    [Bindable]
    public partial class Customer
    {
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

        [XmlArrayItem(Type = typeof(CustomerEmployee))]
        public virtual List<CustomerEmployee> CustomerEmployee { get; set; }
        [XmlArrayItem(Type = typeof(Order))]
        public virtual List<Order> Orders { get; set; }
    }
}

// CustomerAndSuppliersByCity.cs

namespace DevExpress.DemoData.Models {
    public partial class CustomerAndSuppliersByCity
    {
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }
}

// CustomerReport.cs

namespace DevExpress.DemoData.Models {
    public partial class CustomerReport
    {
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public decimal ProductAmount { get; set; }
    }
}

// Employee.cs

namespace DevExpress.DemoData.Models {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "DXCA003", Justification = "VB Convertion")]
    [Bindable]
    public partial class Employee : BindableBase
    {
        long _EmployeeID;
        public long EmployeeID { get { return _EmployeeID; } set { SetProperty(ref _EmployeeID, value, "EmployeeID"); } }
        string _LastName;
        public string LastName { get { return _LastName; } set { SetProperty(ref _LastName, value, "LastName"); } }
        string _FirstName;
        public string FirstName { get { return _FirstName; } set { SetProperty(ref _FirstName, value, "FirstName"); } }
        string _Title;
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value, "Title"); } }
        string _TitleOfCourtesy;
        public string TitleOfCourtesy { get { return _TitleOfCourtesy; } set { SetProperty(ref _TitleOfCourtesy, value, "TitleOfCourtesy"); } }
        Nullable<System.DateTime> _BirthDate;
        public Nullable<System.DateTime> BirthDate { get { return _BirthDate; } set { SetProperty(ref _BirthDate, value, "BirthDate"); } }
        Nullable<System.DateTime> _HireDate;
        public Nullable<System.DateTime> HireDate { get { return _HireDate; } set { SetProperty(ref _HireDate, value, "HireDate"); } }
        string _Address;
        public string Address { get { return _Address; } set { SetProperty(ref _Address, value, "Address"); } }
        string _City;
        public string City { get { return _City; } set { SetProperty(ref _City, value, "City"); } }
        string _Region;
        public string Region { get { return _Region; } set { SetProperty(ref _Region, value, "Region"); } }
        string _PostalCode;
        public string PostalCode { get { return _PostalCode; } set { SetProperty(ref _PostalCode, value, "PostalCode"); } }
        string _Country;
        public string Country { get { return _Country; } set { SetProperty(ref _Country, value, "Country"); } }
        string _HomePhone;
        public string HomePhone { get { return _HomePhone; } set { SetProperty(ref _HomePhone, value, "HomePhone"); } }
        string _Extension;
        public string Extension { get { return _Extension; } set { SetProperty(ref _Extension, value, "Extension"); } }
        byte[] _Photo;
        public byte[] Photo { get { return _Photo; } set { SetProperty(ref _Photo, value, "Photo"); } }
        string _Notes;
        public string Notes { get { return _Notes; } set { SetProperty(ref _Notes, value, "Notes"); } }
        Nullable<long> _ReportsTo;
        public Nullable<long> ReportsTo { get { return _ReportsTo; } set { SetProperty(ref _ReportsTo, value, "ReportsTo"); } }
        string _Email;
        public string Email { get { return _Email; } set { SetProperty(ref _Email, value, "Email"); } }
        string _GroupName;
        public string GroupName { get { return _GroupName; } set { SetProperty(ref _GroupName, value, "GroupName"); } }

        string _FullName = null;
        public string FullName {
            get {
                if(_FullName == null)
                    _FullName = String.Format("{0} {1}", FirstName, LastName);
                return _FullName;
            }
        }
        [XmlArrayItem(Type = typeof(CustomerEmployee))]
        public virtual List<CustomerEmployee> CustomerEmployee { get; set; }
        [XmlArrayItem(Type = typeof(Order))]
        public virtual List<Order> Orders { get; set; }
        [XmlIgnore]
        public virtual List<Employee> Employees { get; set; }
        [XmlIgnore]
        public virtual Employee SubEmployee { get; set; }
        public string PageHeader { get { return (FirstName + " " + LastName).ToUpper(); } }
        public string PageContent {
            get {
                return FirstName + " " + LastName + " was born on " + DateToString(BirthDate) + ". Now lives in " +
                    City + ", " + Country + ". " + TitleOfCourtesy + " " + LastName + " holds a position of " + Title + " our " +
                    Region + " deparment, (" + City + " " + Country + "). Joined our company on " + DateToString(HireDate) + ".";
            }
        }
        string DateToString(DateTime? date) {
            if(date == null)
                return null;
            string[] Months = { "January", "February", "Marth", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return date.Value.Day + "th of " + Months[date.Value.Month - 1] + " in " + date.Value.Year;
        }

		//IEnumerable<ChartPoint> _ChartSource = null;
  //      public IEnumerable<ChartPoint> ChartSource {
  //          get {
  //              if(_ChartSource == null)
  //                  CreateChartSource();
  //              return _ChartSource;
  //          }
  //      }

  //      void CreateChartSource() {
  //          IEnumerable<ChartPoint> list = (from o in Orders
  //                                          group o by o.OrderDate into cp
  //                                          select new ChartPoint() {
  //                                              ArgumentMember = cp.Key,
  //                                              Orders = cp.ToList()
  //                                          }).ToList();
  //          foreach(ChartPoint cp in list) {
  //              decimal value = 0;
  //              foreach(Order order in cp.Orders)
  //                  foreach(OrderDetailsExtended inv in order.OrderDetails)
  //                      value += inv.Quantity * inv.UnitPrice;
  //              cp.ValueMember = (int)value;
  //          }
  //          _ChartSource = list;
  //      }
    }

    //public class ChartPoint {
    //    public DateTime? ArgumentMember { get; internal set; }
    //    public int ValueMember { get; set; }
    //    internal IList<Order> Orders { get; set; }
    //}
}

// EmployeeTerritory.cs

namespace DevExpress.DemoData.Models {
    public partial class EmployeeTerritory
    {
        public long EmployeeID { get; set; }
        public string TerritoryID { get; set; }
    }
}

// InternationalOrder.cs

namespace DevExpress.DemoData.Models {
    public partial class InternationalOrder
    {
        public long OrderID { get; set; }
        public string CustomsDescription { get; set; }
        public decimal ExciseTax { get; set; }
        public virtual Order Order { get; set; }
    }
}

// Invoice.cs

namespace DevExpress.DemoData.Models {
    public partial class Invoice
    {
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public long OrderID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public string ShipperName { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
        public Nullable<decimal> Freight { get; set; }
    }
}

// Order.cs

namespace DevExpress.DemoData.Models {
    public partial class Order
    {
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
        [XmlIgnore]
		public virtual Employee Employee { get; set; }
        [XmlIgnore]
        public virtual Customer Customer { get; set; }
        [XmlIgnore]
        public virtual ICollection<OrderDetailsExtended> OrderDetails { get; set; }
    }
}

// OrderDetail.cs

namespace DevExpress.DemoData.Models {
    public partial class OrderDetail
    {
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
    }
}

// OrderDetailsExtended.cs

namespace DevExpress.DemoData.Models {
    public partial class OrderDetailsExtended
    {
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
}

// OrderReport.cs

namespace DevExpress.DemoData.Models {
    public partial class OrderReport
    {
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}

// OrdersQry.cs

namespace DevExpress.DemoData.Models {
    public partial class OrdersQry
    {
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
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}

// OrderSubtotal.cs

namespace DevExpress.DemoData.Models {
    public partial class OrderSubtotal
    {
        public long OrderID { get; set; }
    }
}

// PreviousEmployee.cs

namespace DevExpress.DemoData.Models {
    public partial class PreviousEmployee
    {
        public long EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public string PhotoPath { get; set; }
    }
}

// Product.cs

namespace DevExpress.DemoData.Models {
    public partial class Product
    {
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
}

// ProductReport.cs

namespace DevExpress.DemoData.Models {
    public partial class ProductReport
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductSales { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}

// ProductsAboveAveragePrice.cs

namespace DevExpress.DemoData.Models {
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}

// ProductsByCategory.cs

namespace DevExpress.DemoData.Models {
    public partial class ProductsByCategory
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}

// Region.cs

namespace DevExpress.DemoData.Models {
    public partial class Region
    {
        public long RegionID { get; set; }
        public string RegionDescription { get; set; }
    }
}

// SalesByCategory.cs

namespace DevExpress.DemoData.Models {
    public partial class SalesByCategory
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }
}

// SalesPerson.cs

namespace DevExpress.DemoData.Models {
    public partial class SalesPerson
    {
        public long OrderID { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
        public decimal ExtendedPrice { get; set; }
        public string FullName { get; set; }
    }
}

// SalesTotalsByAmount.cs

namespace DevExpress.DemoData.Models {
    public partial class SalesTotalsByAmount
    {
        public long OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}

// Shipper.cs

namespace DevExpress.DemoData.Models {
    public partial class Shipper
    {
        public long ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}

// SummaryOfSalesByQuarter.cs

namespace DevExpress.DemoData.Models {
    public partial class SummaryOfSalesByQuarter
    {
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public long OrderID { get; set; }
    }
}

// SummaryOfSalesByYear.cs

namespace DevExpress.DemoData.Models {
    public partial class SummaryOfSalesByYear
    {
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public long OrderID { get; set; }
    }
}

// Supplier.cs

namespace DevExpress.DemoData.Models {
    public partial class Supplier
    {
        public long SupplierID { get; set; }
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
        public string HomePage { get; set; }
    }
}

// Territory.cs

namespace DevExpress.DemoData.Models {
    public partial class Territory
    {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public long RegionID { get; set; }
    }
}

// AlphabeticalListOfProductMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class AlphabeticalListOfProductMap : IEntityTypeConfiguration<AlphabeticalListOfProduct>
//    {
//        public void Configure(EntityTypeBuilder<AlphabeticalListOfProduct> builder) {
//            // Primary Key
//            builder.HasKey(t => t.ProductID);

//            // Properties
//            builder.Property(t => t.ProductID).ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                  .IsRequired()
//                  .HasMaxLength(40);

//            builder.Property(t => t.QuantityPerUnit)
//                .HasMaxLength(20);

//            builder.Property(t => t.EAN13)
//                .HasMaxLength(2147483647);

//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            // Table & Column Mappings
//            builder.ToTable("AlphabeticalListOfProducts");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.SupplierID).HasColumnName("SupplierID");
//            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
//            builder.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
//            builder.Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
//            builder.Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
//            builder.Property(t => t.Discontinued).HasColumnName("Discontinued");
//            builder.Property(t => t.EAN13).HasColumnName("EAN13");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//        }
//    }
//}

//// CategoryMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class CategoryMap : IEntityTypeConfiguration<Category>
//    {

//        public void Configure(EntityTypeBuilder<Category> builder) {
//            // Primary Key
//            builder.HasKey(t => t.CategoryID);

//            // Properties
//            builder.Property(t => t.CategoryID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            builder.Property(t => t.Description)
//                .HasMaxLength(1073741823);

//            builder.Property(t => t.Picture)
//                .HasMaxLength(2147483647);

//            // Table & Column Mappings
//            builder.ToTable("Categories");
//            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//            builder.Property(t => t.Description).HasColumnName("Description");
//            builder.Property(t => t.Picture).HasColumnName("Picture");
//        }
//    }
//}

//// CategoryProductMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class CategoryProductMap : IEntityTypeConfiguration<CategoryProduct>
//    {
//        public void Configure(EntityTypeBuilder<CategoryProduct> builder) {
//            // Primary Key
//            builder.HasKey(t => t.ProductID);

//            // Properties
//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            builder.Property(t => t.Picture)
//                .HasMaxLength(2147483647);

//            builder.Property(t => t.Description)
//                .HasMaxLength(1073741823);

//            // Table & Column Mappings
//            builder.ToTable("CategoryProducts");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.SupplierID).HasColumnName("SupplierID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//            builder.Property(t => t.Picture).HasColumnName("Picture");
//            builder.Property(t => t.Description).HasColumnName("Description");
//        }
//    }
//}

//// CurrentProductListMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class CurrentProductListMap : IEntityTypeConfiguration<CurrentProductList>
//    {

//        public void Configure(EntityTypeBuilder<CurrentProductList> builder) {
//            // Primary Key
//            builder.HasKey(t => t.ProductID);

//            // Properties
//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            // Table & Column Mappings
//            builder.ToTable("CurrentProductList");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//        }
//    }
//}

//// CustomerAndSuppliersByCityMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class CustomerAndSuppliersByCityMap : IEntityTypeConfiguration<CustomerAndSuppliersByCity>
//    {
//        public void Configure(EntityTypeBuilder<CustomerAndSuppliersByCity> builder) {
//            // Primary Key
//            builder.HasKey(t => t.CompanyName);

//            // Properties
//            builder.Property(t => t.City)
//                .HasMaxLength(15);

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.ContactName)
//                .HasMaxLength(30);

//            // Table & Column Mappings
//            builder.ToTable("CustomerAndSuppliersByCity");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.ContactName).HasColumnName("ContactName");
//        }
//    }
//}

//// CustomerMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class EmployeeCustomersMap : IEntityTypeConfiguration<CustomerEmployee> {
//        public void Configure(EntityTypeBuilder<CustomerEmployee> builder) {
//            builder.HasKey(t => new { t.EmployeeId, t.CustomerID });
//            builder.ToTable("EmployeeCustomers");
//            builder.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
//            builder.Property(t => t.CustomerID).HasColumnName("CustomerId");
//            builder.HasOne(ce => ce.Employee).WithMany(e => e.CustomerEmployee).HasForeignKey(ce => ce.EmployeeId);

//            builder.HasOne(ce => ce.Customer).WithMany(e => e.CustomerEmployee).HasForeignKey(ce => ce.CustomerID);
//        }
//    }

//    public class CustomerMap : IEntityTypeConfiguration<Customer> {

//        public void Configure(EntityTypeBuilder<Customer> builder) {
//            // Primary Key
//                builder.HasKey(t => t.CustomerID);

//            // Properties
//            builder.Property(t => t.CustomerID)
//                .IsRequired()
//                .IsFixedLength()
//                .HasMaxLength(5);

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.ContactName)
//                .HasMaxLength(30);

//            builder.Property(t => t.ContactTitle)
//                .HasMaxLength(30);

//            builder.Property(t => t.Address)
//                .HasMaxLength(60);

//            builder.Property(t => t.City)
//                .HasMaxLength(15);

//            builder.Property(t => t.Region)
//                .HasMaxLength(15);

//            builder.Property(t => t.PostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.Country)
//                .HasMaxLength(15);

//            builder.Property(t => t.Phone)
//                .HasMaxLength(24);

//            builder.Property(t => t.Fax)
//                .HasMaxLength(24);

//            // Table & Column Mappings
//            builder.ToTable("Customers");
//            builder.Property(t => t.CustomerID).HasColumnName("CustomerID");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.ContactName).HasColumnName("ContactName");
//            builder.Property(t => t.ContactTitle).HasColumnName("ContactTitle");
//            builder.Property(t => t.Address).HasColumnName("Address");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.Region).HasColumnName("Region");
//            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
//            builder.Property(t => t.Country).HasColumnName("Country");
//            builder.Property(t => t.Phone).HasColumnName("Phone");
//            builder.Property(t => t.Fax).HasColumnName("Fax");
//            builder.HasMany(x => x.Orders)
//                .WithOne(x => x.Customer)
//                .HasForeignKey(o => o.CustomerID);

//            builder.HasMany(x => x.CustomerEmployee)
//                .WithOne(x => x.Customer)
//                .HasForeignKey(x => x.CustomerID);

//        }
//    }
//}

//// CustomerReportMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class CustomerReportMap : IEntityTypeConfiguration<CustomerReport>
//    {
//        public void Configure(EntityTypeBuilder<CustomerReport> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.ProductName, t.CompanyName });

//            // Properties
//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            // Table & Column Mappings
//            builder.ToTable("CustomerReports");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.OrderDate).HasColumnName("OrderDate");
//        }
//    }
//}

//// EmployeeMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class EmployeeMap : IEntityTypeConfiguration<Employee> {
//        public void Configure(EntityTypeBuilder<Employee> builder) { 
//            // Primary Key
//            builder.HasKey(t => t.EmployeeID);

//            // Properties
//            builder.Property(t => t.EmployeeID)
//            .ValueGeneratedNever();

//            builder.Property(t => t.LastName)
//            .IsRequired()
//            .HasMaxLength(20);

//            builder.Property(t => t.FirstName)
//            .IsRequired()
//            .HasMaxLength(10);

//            builder.Property(t => t.Title)
//            .HasMaxLength(30);

//            builder.Property(t => t.TitleOfCourtesy)
//            .HasMaxLength(25);

//            builder.Property(t => t.Address)
//            .HasMaxLength(60);

//            builder.Property(t => t.City)
//            .HasMaxLength(15);

//            builder.Property(t => t.Region)
//            .HasMaxLength(15);

//            builder.Property(t => t.PostalCode)
//            .HasMaxLength(10);

//            builder.Property(t => t.Country)
//            .HasMaxLength(15);

//            builder.Property(t => t.HomePhone)
//            .HasMaxLength(24);

//            builder.Property(t => t.Extension)
//            .HasMaxLength(4);

//            builder.Property(t => t.Photo)
//            .HasMaxLength(2147483647);

//            builder.Property(t => t.Notes)
//            .HasMaxLength(1073741823);

//            // Table & Column Mappings
//            builder.ToTable("Employees");
//            builder.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
//            builder.Property(t => t.LastName).HasColumnName("LastName");
//            builder.Property(t => t.FirstName).HasColumnName("FirstName");
//            builder.Property(t => t.Title).HasColumnName("Title");
//            builder.Property(t => t.TitleOfCourtesy).HasColumnName("TitleOfCourtesy");
//            builder.Property(t => t.BirthDate).HasColumnName("BirthDate");
//            builder.Property(t => t.HireDate).HasColumnName("HireDate");
//            builder.Property(t => t.Address).HasColumnName("Address");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.Region).HasColumnName("Region");
//            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
//            builder.Property(t => t.Country).HasColumnName("Country");
//            builder.Property(t => t.HomePhone).HasColumnName("HomePhone");
//            builder.Property(t => t.Extension).HasColumnName("Extension");
//            builder.Property(t => t.Photo).HasColumnName("Photo");
//            builder.Property(t => t.Notes).HasColumnName("Notes");
//            builder.Property(t => t.ReportsTo).HasColumnName("ReportsTo");
//            builder.Property(t => t.GroupName).HasColumnName("GroupName");

//            builder.HasMany(x => x.CustomerEmployee)
//                .WithOne(x => x.Employee)
//                .HasForeignKey(x => x.EmployeeId);

//            builder.HasMany(x => x.Employees)
//                .WithOne(x => x.SubEmployee)
//                .HasForeignKey(x => x.ReportsTo);
//            builder.HasMany(x => x.Orders)
//                .WithOne(x => x.Employee)
//                .HasForeignKey(x => x.EmployeeID);
//        }
//    }
//}

//// EmployeeTerritoryMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class EmployeeTerritoryMap : IEntityTypeConfiguration<EmployeeTerritory> {
//        public EmployeeTerritoryMap() {

//        }

//        public void Configure(EntityTypeBuilder<EmployeeTerritory> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.EmployeeID, t.TerritoryID });

//            // Properties
//            builder.Property(t => t.EmployeeID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.TerritoryID)
//                .IsRequired()
//                .HasMaxLength(20);

//            // Table & Column Mappings
//            builder.ToTable("EmployeeTerritories");
//            builder.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
//            builder.Property(t => t.TerritoryID).HasColumnName("TerritoryID");
//        }
//    }
//}

//// InvoiceMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class InvoiceMap : IEntityTypeConfiguration<Invoice> {
//        public InvoiceMap() {

//        }

//        public void Configure(EntityTypeBuilder<Invoice> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.CustomerName, t.OrderID, t.ShipperName, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });

//            // Properties
//            builder.Property(t => t.ShipName)
//                .HasMaxLength(40);

//            builder.Property(t => t.ShipAddress)
//                .HasMaxLength(60);

//            builder.Property(t => t.ShipCity)
//                .HasMaxLength(15);

//            builder.Property(t => t.ShipRegion)
//                .HasMaxLength(15);

//            builder.Property(t => t.ShipPostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.ShipCountry)
//                .HasMaxLength(15);

//            builder.Property(t => t.CustomerID)
//                .IsFixedLength()
//                .HasMaxLength(5);

//            builder.Property(t => t.CustomerName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.Address)
//                .HasMaxLength(60);

//            builder.Property(t => t.City)
//                .HasMaxLength(15);

//            builder.Property(t => t.Region)
//                .HasMaxLength(15);

//            builder.Property(t => t.PostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.Country)
//                .HasMaxLength(15);

//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ShipperName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.UnitPrice)
//                .ValueGeneratedNever();

//            builder.Property(t => t.Quantity)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("Invoices");
//            builder.Property(t => t.ShipName).HasColumnName("ShipName");
//            builder.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
//            builder.Property(t => t.ShipCity).HasColumnName("ShipCity");
//            builder.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
//            builder.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
//            builder.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
//            builder.Property(t => t.CustomerID).HasColumnName("CustomerID");
//            builder.Property(t => t.CustomerName).HasColumnName("CustomerName");
//            builder.Property(t => t.Address).HasColumnName("Address");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.Region).HasColumnName("Region");
//            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
//            builder.Property(t => t.Country).HasColumnName("Country");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.OrderDate).HasColumnName("OrderDate");
//            builder.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//            builder.Property(t => t.ShipperName).HasColumnName("ShipperName");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.Quantity).HasColumnName("Quantity");
//            builder.Property(t => t.Discount).HasColumnName("Discount");
//            builder.Property(t => t.Freight).HasColumnName("Freight");
//        }
//    }
//}

//// OrderDetailMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail> {


//        public void Configure(EntityTypeBuilder<OrderDetail> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.OrderID, t.ProductID, t.UnitPrice, t.Quantity, t.Discount });

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.UnitPrice)
//                .ValueGeneratedNever();

//            builder.Property(t => t.Quantity)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("OrderDetails");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.Quantity).HasColumnName("Quantity");
//            builder.Property(t => t.Discount).HasColumnName("Discount");
//        }
//    }
//}

//// OrderDetailsExtendedMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class OrderDetailsExtendedMap : IEntityTypeConfiguration<OrderDetailsExtended> {

//        public void Configure(EntityTypeBuilder<OrderDetailsExtended> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.UnitPrice)
//                .ValueGeneratedNever();

//            builder.Property(t => t.Quantity)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("OrderDetailsExtended");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.Quantity).HasColumnName("Quantity");
//            builder.Property(t => t.Discount).HasColumnName("Discount");
//        }
//    }
//}

//// OrderMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class OrderMap : IEntityTypeConfiguration<Order> {

//        public void Configure(EntityTypeBuilder<Order> builder) {
//            // Primary Key
//            builder.HasKey(t => t.OrderID);

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CustomerID)
//                .IsFixedLength()
//                .HasMaxLength(5);

//            builder.Property(t => t.ShipName)
//                .HasMaxLength(40);

//            builder.Property(t => t.ShipAddress)
//                .HasMaxLength(60);

//            builder.Property(t => t.ShipCity)
//                .HasMaxLength(15);

//            builder.Property(t => t.ShipRegion)
//                .HasMaxLength(15);

//            builder.Property(t => t.ShipPostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.ShipCountry)
//                .HasMaxLength(15);

//            // Table & Column Mappings
//            builder.ToTable("Orders");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.CustomerID).HasColumnName("CustomerID");
//            builder.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
//            builder.Property(t => t.OrderDate).HasColumnName("OrderDate");
//            builder.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//            builder.Property(t => t.ShipVia).HasColumnName("ShipVia");
//            builder.Property(t => t.Freight).HasColumnName("Freight");
//            builder.Property(t => t.ShipName).HasColumnName("ShipName");
//            builder.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
//            builder.Property(t => t.ShipCity).HasColumnName("ShipCity");
//            builder.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
//            builder.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
//            builder.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
//            //builder.HasMany(x => x.OrderDetails)
//            //    .WithRequired(x => x.Order)
//            //    .HasForeignKey(x => x.OrderID);
//        }
//    }
//}

//// OrderReportMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class OrderReportMap : IEntityTypeConfiguration<OrderReport> {
//        public void Configure(EntityTypeBuilder<OrderReport> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.UnitPrice)
//                .ValueGeneratedNever();

//            builder.Property(t => t.Quantity)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("OrderReports");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.Quantity).HasColumnName("Quantity");
//            builder.Property(t => t.Discount).HasColumnName("Discount");
//        }
//    }
//}

//// OrdersQryMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class OrdersQryMap : IEntityTypeConfiguration<OrdersQry> {

//        public void Configure(EntityTypeBuilder<OrdersQry> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.OrderID, t.CompanyName });

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CustomerID)
//                .IsFixedLength()
//                .HasMaxLength(5);

//            builder.Property(t => t.ShipName)
//                .HasMaxLength(40);

//            builder.Property(t => t.ShipAddress)
//                .HasMaxLength(60);

//            builder.Property(t => t.ShipCity)
//                .HasMaxLength(15);

//            builder.Property(t => t.ShipRegion)
//                .HasMaxLength(15);

//            builder.Property(t => t.ShipPostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.ShipCountry)
//                .HasMaxLength(15);

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.Address)
//                .HasMaxLength(60);

//            builder.Property(t => t.City)
//                .HasMaxLength(15);

//            builder.Property(t => t.Region)
//                .HasMaxLength(15);

//            builder.Property(t => t.PostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.Country)
//                .HasMaxLength(15);

//            // Table & Column Mappings
//            builder.ToTable("OrdersQry");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.CustomerID).HasColumnName("CustomerID");
//            builder.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
//            builder.Property(t => t.OrderDate).HasColumnName("OrderDate");
//            builder.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//            builder.Property(t => t.ShipVia).HasColumnName("ShipVia");
//            builder.Property(t => t.Freight).HasColumnName("Freight");
//            builder.Property(t => t.ShipName).HasColumnName("ShipName");
//            builder.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
//            builder.Property(t => t.ShipCity).HasColumnName("ShipCity");
//            builder.Property(t => t.ShipRegion).HasColumnName("ShipRegion");
//            builder.Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
//            builder.Property(t => t.ShipCountry).HasColumnName("ShipCountry");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.Address).HasColumnName("Address");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.Region).HasColumnName("Region");
//            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
//            builder.Property(t => t.Country).HasColumnName("Country");
//        }
//    }
//}

//// OrderSubtotalMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class OrderSubtotalMap : IEntityTypeConfiguration<OrderSubtotal> {

//        public void Configure(EntityTypeBuilder<OrderSubtotal> builder) {
//            // Primary Key
//            builder.HasKey(t => t.OrderID);

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("OrderSubtotals");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//        }
//    }
//}

//// PreviousEmployeeMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class PreviousEmployeeMap : IEntityTypeConfiguration<PreviousEmployee> {

//        public void Configure(EntityTypeBuilder<PreviousEmployee> builder) {
//            // Primary Key
//            builder.HasKey(t => t.EmployeeID);

//            // Properties
//            builder.Property(t => t.EmployeeID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.LastName)
//                .IsRequired()
//                .HasMaxLength(20);

//            builder.Property(t => t.FirstName)
//                .IsRequired()
//                .HasMaxLength(10);

//            builder.Property(t => t.Title)
//                .HasMaxLength(30);

//            builder.Property(t => t.TitleOfCourtesy)
//                .HasMaxLength(25);

//            builder.Property(t => t.Address)
//                .HasMaxLength(60);

//            builder.Property(t => t.City)
//                .HasMaxLength(15);

//            builder.Property(t => t.Region)
//                .HasMaxLength(15);

//            builder.Property(t => t.PostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.Country)
//                .HasMaxLength(15);

//            builder.Property(t => t.HomePhone)
//                .HasMaxLength(24);

//            builder.Property(t => t.Extension)
//                .HasMaxLength(4);

//            builder.Property(t => t.Photo)
//                .HasMaxLength(2147483647);

//            builder.Property(t => t.Notes)
//                .HasMaxLength(2147483647);

//            builder.Property(t => t.PhotoPath)
//                .HasMaxLength(255);

//            // Table & Column Mappings
//            builder.ToTable("PreviousEmployees");
//            builder.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
//            builder.Property(t => t.LastName).HasColumnName("LastName");
//            builder.Property(t => t.FirstName).HasColumnName("FirstName");
//            builder.Property(t => t.Title).HasColumnName("Title");
//            builder.Property(t => t.TitleOfCourtesy).HasColumnName("TitleOfCourtesy");
//            builder.Property(t => t.BirthDate).HasColumnName("BirthDate");
//            builder.Property(t => t.HireDate).HasColumnName("HireDate");
//            builder.Property(t => t.Address).HasColumnName("Address");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.Region).HasColumnName("Region");
//            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
//            builder.Property(t => t.Country).HasColumnName("Country");
//            builder.Property(t => t.HomePhone).HasColumnName("HomePhone");
//            builder.Property(t => t.Extension).HasColumnName("Extension");
//            builder.Property(t => t.Photo).HasColumnName("Photo");
//            builder.Property(t => t.Notes).HasColumnName("Notes");
//            builder.Property(t => t.PhotoPath).HasColumnName("PhotoPath");
//        }
//    }
//}

//// ProductMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class ProductMap : IEntityTypeConfiguration<Product> {
//        public void Configure(EntityTypeBuilder<Product> builder) {
//            // Primary Key
//            builder.HasKey(t => t.ProductID);

//            // Properties
//            builder.Property(t => t.ProductID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.QuantityPerUnit)
//                .HasMaxLength(20);

//            builder.Property(t => t.EAN13)
//                .HasMaxLength(2147483647);

//            // Table & Column Mappings
//            builder.ToTable("Products");
//            builder.Property(t => t.ProductID).HasColumnName("ProductID");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.SupplierID).HasColumnName("SupplierID");
//            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
//            builder.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
//            builder.Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
//            builder.Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
//            builder.Property(t => t.Discontinued).HasColumnName("Discontinued");
//            builder.Property(t => t.EAN13).HasColumnName("EAN13");
//            //builder.HasOptional(p => p.Category)
//            //    .WithMany(c => c.Products)
//            //    .HasForeignKey(p => p.CategoryID);

//            //builder.HasMany(x => x.OrderDetails)
//            //    .WithRequired(x => x.Product)
//            //    .HasForeignKey(x => x.ProductID);
//        }
//    }
//}

//// ProductReportMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class ProductReportMap : IEntityTypeConfiguration<ProductReport> {

//        public void Configure(EntityTypeBuilder<ProductReport> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.CategoryName, t.ProductName, t.ShippedDate });

//            // Properties
//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            // Table & Column Mappings
//            builder.ToTable("ProductReports");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.ProductSales).HasColumnName("ProductSales");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//        }
//    }
//}

//// ProductsAboveAveragePriceMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class ProductsAboveAveragePriceMap : IEntityTypeConfiguration<ProductsAboveAveragePrice> {

//        public void Configure(EntityTypeBuilder<ProductsAboveAveragePrice> builder) {
//            // Primary Key
//            builder.HasKey(t => t.ProductName);

//            // Properties
//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            // Table & Column Mappings
//            builder.ToTable("ProductsAboveAveragePrice");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//        }
//    }
//}

//// ProductsByCategoryMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class ProductsByCategoryMap : IEntityTypeConfiguration<ProductsByCategory> {

//        public void Configure(EntityTypeBuilder<ProductsByCategory> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.CategoryName, t.ProductName, t.Discontinued });

//            // Properties
//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.QuantityPerUnit)
//                .HasMaxLength(20);

//            // Table & Column Mappings
//            builder.ToTable("ProductsByCategory");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
//            builder.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
//            builder.Property(t => t.Discontinued).HasColumnName("Discontinued");
//        }
//    }
//}

//// RegionMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class RegionMap : IEntityTypeConfiguration<Region> {

//        public void Configure(EntityTypeBuilder<Region> builder) {
//            // Primary Key
//            builder.HasKey(t => t.RegionID);

//            // Properties
//            builder.Property(t => t.RegionID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.RegionDescription)
//                .IsRequired()
//                .IsFixedLength()
//                .HasMaxLength(50);

//            // Table & Column Mappings
//            builder.ToTable("Region");
//            builder.Property(t => t.RegionID).HasColumnName("RegionID");
//            builder.Property(t => t.RegionDescription).HasColumnName("RegionDescription");
//        }
//    }
//}

//// SalesByCategoryMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class SalesByCategoryMap : IEntityTypeConfiguration<SalesByCategory> {
//        public void Configure(EntityTypeBuilder<SalesByCategory> builder) {
//            // Primary Key
//            builder.HasKey(t => t.CategoryID);

//            // Properties
//            builder.Property(t => t.CategoryID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            // Table & Column Mappings
//            builder.ToTable("SalesByCategory");
//            builder.Property(t => t.CategoryID).HasColumnName("CategoryID");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//        }
//    }
//}

//// SalesPersonMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class SalesPersonMap : IEntityTypeConfiguration<SalesPerson> {

//        public void Configure(EntityTypeBuilder<SalesPerson> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.OrderID, t.FirstName, t.LastName, t.ProductName, t.CategoryName, t.UnitPrice, t.Quantity, t.Discount });

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.Country)
//                .HasMaxLength(15);

//            builder.Property(t => t.FirstName)
//                .IsRequired()
//                .HasMaxLength(10);

//            builder.Property(t => t.LastName)
//                .IsRequired()
//                .HasMaxLength(20);

//            builder.Property(t => t.ProductName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.CategoryName)
//                .IsRequired()
//                .HasMaxLength(15);

//            builder.Property(t => t.UnitPrice)
//                .ValueGeneratedNever();

//            builder.Property(t => t.Quantity)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("SalesPerson");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.Country).HasColumnName("Country");
//            builder.Property(t => t.FirstName).HasColumnName("FirstName");
//            builder.Property(t => t.LastName).HasColumnName("LastName");
//            builder.Property(t => t.ProductName).HasColumnName("ProductName");
//            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
//            builder.Property(t => t.OrderDate).HasColumnName("OrderDate");
//            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
//            builder.Property(t => t.Quantity).HasColumnName("Quantity");
//            builder.Property(t => t.Discount).HasColumnName("Discount");
//        }
//    }
//}

//// SalesTotalsByAmountMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class SalesTotalsByAmountMap : IEntityTypeConfiguration<SalesTotalsByAmount> {

//        public void Configure(EntityTypeBuilder<SalesTotalsByAmount> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.OrderID, t.CompanyName });

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            // Table & Column Mappings
//            builder.ToTable("SalesTotalsByAmount");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//        }
//    }
//}

//// ShipperMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class ShipperMap : IEntityTypeConfiguration<Shipper> {
//        public void Configure(EntityTypeBuilder<Shipper> builder) {
//            // Primary Key
//            builder.HasKey(t => new { t.ShipperID, t.CompanyName });

//            // Properties
//            builder.Property(t => t.ShipperID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.Phone)
//                .HasMaxLength(24);

//            // Table & Column Mappings
//            builder.ToTable("Shippers");
//            builder.Property(t => t.ShipperID).HasColumnName("ShipperID");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.Phone).HasColumnName("Phone");
//        }
//    }
//}

//// SummaryOfSalesByQuarterMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class SummaryOfSalesByQuarterMap : IEntityTypeConfiguration<SummaryOfSalesByQuarter> {
//        public SummaryOfSalesByQuarterMap() {

//        }

//        public void Configure(EntityTypeBuilder<SummaryOfSalesByQuarter> builder) {
//            // Primary Key
//            builder.HasKey(t => t.OrderID);

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("SummaryOfSalesByQuarter");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//        }
//    }
//}

//// SummaryOfSalesByYearMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class SummaryOfSalesByYearMap : IEntityTypeConfiguration<SummaryOfSalesByYear> {

//        public void Configure(EntityTypeBuilder<SummaryOfSalesByYear> builder) {
//            // Primary Key
//            builder.HasKey(t => t.OrderID);

//            // Properties
//            builder.Property(t => t.OrderID)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("SummaryOfSalesByYear");
//            builder.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
//            builder.Property(t => t.OrderID).HasColumnName("OrderID");
//        }
//    }
//}

//// SupplierMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class SupplierMap : IEntityTypeConfiguration<Supplier> {
//        public void Configure(EntityTypeBuilder<Supplier> builder) {
//            // Primary Key
//            builder.HasKey(t => t.SupplierID);

//            // Properties
//            builder.Property(t => t.SupplierID)
//                .ValueGeneratedNever();

//            builder.Property(t => t.CompanyName)
//                .IsRequired()
//                .HasMaxLength(40);

//            builder.Property(t => t.ContactName)
//                .HasMaxLength(30);

//            builder.Property(t => t.ContactTitle)
//                .HasMaxLength(30);

//            builder.Property(t => t.Address)
//                .HasMaxLength(60);

//            builder.Property(t => t.City)
//                .HasMaxLength(15);

//            builder.Property(t => t.Region)
//                .HasMaxLength(15);

//            builder.Property(t => t.PostalCode)
//                .HasMaxLength(10);

//            builder.Property(t => t.Country)
//                .HasMaxLength(15);

//            builder.Property(t => t.Phone)
//                .HasMaxLength(24);

//            builder.Property(t => t.Fax)
//                .HasMaxLength(24);

//            builder.Property(t => t.HomePage)
//                .HasMaxLength(1073741823);

//            // Table & Column Mappings
//            builder.ToTable("Suppliers");
//            builder.Property(t => t.SupplierID).HasColumnName("SupplierID");
//            builder.Property(t => t.CompanyName).HasColumnName("CompanyName");
//            builder.Property(t => t.ContactName).HasColumnName("ContactName");
//            builder.Property(t => t.ContactTitle).HasColumnName("ContactTitle");
//            builder.Property(t => t.Address).HasColumnName("Address");
//            builder.Property(t => t.City).HasColumnName("City");
//            builder.Property(t => t.Region).HasColumnName("Region");
//            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
//            builder.Property(t => t.Country).HasColumnName("Country");
//            builder.Property(t => t.Phone).HasColumnName("Phone");
//            builder.Property(t => t.Fax).HasColumnName("Fax");
//            builder.Property(t => t.HomePage).HasColumnName("HomePage");
//        }
//    }
//}

//// TerritoryMap.cs

//namespace DevExpress.DemoData.Models.Mapping {
//    public class TerritoryMap : IEntityTypeConfiguration<Territory> {
//        public void Configure(EntityTypeBuilder<Territory> builder) {
//            // Primary Key
//            builder.HasKey(t => t.TerritoryID);

//            // Properties
//            builder.Property(t => t.TerritoryID)
//                .IsRequired()
//                .HasMaxLength(20);

//            builder.Property(t => t.TerritoryDescription)
//                .IsRequired()
//                .IsFixedLength()
//                .HasMaxLength(50);

//            builder.Property(t => t.RegionID)
//                .ValueGeneratedNever();

//            // Table & Column Mappings
//            builder.ToTable("Territories");
//            builder.Property(t => t.TerritoryID).HasColumnName("TerritoryID");
//            builder.Property(t => t.TerritoryDescription).HasColumnName("TerritoryDescription");
//            builder.Property(t => t.RegionID).HasColumnName("RegionID");
//        }
//    }
//}


