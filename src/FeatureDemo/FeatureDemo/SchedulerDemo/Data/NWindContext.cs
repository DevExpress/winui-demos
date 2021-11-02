
using DevExpress.Mvvm;


using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Windows.Storage;
using Microsoft.UI.Xaml.Data;
using DevExpress.Mvvm.CodeGenerators;

namespace DevExpress.DemoData.Models {
    
    
    

    
    

    
    
    
    

    
    
    

    
    
    

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    
    
    
    
    

    
    
    
    
    
    
    
    

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
        
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}



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



namespace DevExpress.DemoData.Models {
    public partial class CurrentProductList
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
    }
}



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



namespace DevExpress.DemoData.Models {
    public partial class CustomerAndSuppliersByCity
    {
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class CustomerReport
    {
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public decimal ProductAmount { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "DXCA003", Justification = "VB Convertion")]
    [Bindable]
    [GenerateViewModel]
    public partial class Employee
    {
        [GenerateProperty]
        long _EmployeeID;

        [GenerateProperty]
        string _LastName;

        [GenerateProperty]
        string _FirstName;

        [GenerateProperty]
        string _Title;

        [GenerateProperty]
        string _TitleOfCourtesy;

        [GenerateProperty]
        DateTime? _BirthDate;

        [GenerateProperty]
        DateTime? _HireDate;

        [GenerateProperty]
        string _Address;

        [GenerateProperty]
        string _City;

        [GenerateProperty]
        string _Region;

        [GenerateProperty]
        string _PostalCode;

        [GenerateProperty]
        string _Country;

        [GenerateProperty]
        string _HomePhone;

        [GenerateProperty]
        string _Extension;

        [GenerateProperty]
        byte[] _Photo;

        [GenerateProperty]
        string _Notes;

        [GenerateProperty]
        long? _ReportsTo;

        [GenerateProperty]
        string _Email;

        [GenerateProperty]
        string _GroupName;
        
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

		
  
  
  
  
  
  
  

  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
    }

    
    
    
    
    
}



namespace DevExpress.DemoData.Models {
    public partial class EmployeeTerritory
    {
        public long EmployeeID { get; set; }
        public string TerritoryID { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class InternationalOrder
    {
        public long OrderID { get; set; }
        public string CustomsDescription { get; set; }
        public decimal ExciseTax { get; set; }
        public virtual Order Order { get; set; }
    }
}



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



namespace DevExpress.DemoData.Models {
    public partial class OrderSubtotal
    {
        public long OrderID { get; set; }
    }
}



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



namespace DevExpress.DemoData.Models {
    public partial class ProductReport
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductSales { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}



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



namespace DevExpress.DemoData.Models {
    public partial class Region
    {
        public long RegionID { get; set; }
        public string RegionDescription { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class SalesByCategory
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }
}



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



namespace DevExpress.DemoData.Models {
    public partial class SalesTotalsByAmount
    {
        public long OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class Shipper
    {
        public long ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class SummaryOfSalesByQuarter
    {
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public long OrderID { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public partial class SummaryOfSalesByYear
    {
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public long OrderID { get; set; }
    }
}



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



namespace DevExpress.DemoData.Models {
    public partial class Territory
    {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public long RegionID { get; set; }
    }
}

































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































