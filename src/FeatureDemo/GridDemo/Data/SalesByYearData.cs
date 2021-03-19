using FeatureDemo.Data;
using DevExpress.WinUI.Data.Internal;
using DevExpress.WinUI.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace GridDemo {
    public class SalesByYearData {
        IList totalSales;
        public IList TotalSales {
            get {
                if(totalSales == null) {
                    totalSales = GetSalesByYearData(false, false, false);
                }
                return totalSales;
            }
        }
        IList salesByMonth;
        public IList SalesByMonth { 
            get {
                if(salesByMonth == null) {
                    salesByMonth = GetSalesByYearData(true, true, false);
                }
                return salesByMonth;
            } 
        }
        IList totalSalesByMonth;
        public IList TotalSalesByMonth {
            get {
                if(totalSalesByMonth == null) {
                    totalSalesByMonth = GetSalesByYearData(true, true, true);
                }
                return totalSalesByMonth;
            }
        }

        public static Type GetColumnType(string fieldName) {
            return fieldName.Contains("Date") ? typeof(DateTime) : typeof(int);
        }
        public static IList GetSalesByYearData(bool includeMonth, bool includeYear, bool includeTotalSales) {
            List<string> columns = new List<string>();
            if(includeYear)
                columns.Add("Date");
            if(includeMonth)
                columns.Add("DateMonth");
            if(includeTotalSales)
                columns.Add("Sales");
            foreach(Employee employee in new DataStorage().Employees) {
                string name = employee.FirstName + " " + employee.LastName;
                if(!columns.Contains(name))
                    columns.Add(name);
            }
            CellSelectionList table = new CellSelectionList(columns);

            Random random = new Random();
            for(int yearIndex = 10; yearIndex > 0; yearIndex--) {
                int year = DateTime.Now.Year - yearIndex;
                for(int month = 1; month <= 12; month++) {
                    int daysCount = includeMonth ? DateTime.DaysInMonth(year, month) : 1;
                    for(int day = 1; day <= daysCount; day++) {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        int startColumnIndex = 0;
                        if(includeYear) {
                            row["Date"] = new DateTime(year, month, day);
                            startColumnIndex++;
                        }
                        if(includeMonth) {
                            row["DateMonth"] = row["Date"];
                            startColumnIndex++;
                        }
                        if(includeTotalSales) {
                            columns.Add("Sales");
                            row[columns[2]] = (int)0;
                            startColumnIndex++;
                        }
                        for(int columnIndex = startColumnIndex; columnIndex < columns.Count; columnIndex++) {
                            row[columns[columnIndex]] = random.Next(30000 / daysCount);
                            if(includeTotalSales)
                                row[columns[2]] = (int)row[columns[2]] + (int)row[columns[columnIndex]];
                        }
                        table.Add(row);
                    }
                }
            }
            return table;
        }

        public class CellSelectionList : IList, ITypedList {
            List<Dictionary<string, object>> list;
            PropertyDescriptorCollection columns;
            public CellSelectionList(List<string> columnNames) {
                list = new List<Dictionary<string, object>>();

                columns = CreateColumnCollection(columnNames);
            }
            PropertyDescriptorCollection CreateColumnCollection(List<string> columnNames) {
                CellSelectionPropertyDescriptor[] pds = new CellSelectionPropertyDescriptor[columnNames.Count];
                for(int i = 0; i < columnNames.Count; i++)
                    pds[i] = new CellSelectionPropertyDescriptor(this, columnNames[i], SalesByYearData.GetColumnType(columnNames[i]));
                return new PropertyDescriptorCollection(pds);
            }

            #region ITypedList Members
            PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors) {
                return columns;
            }
            string ITypedList.GetListName(PropertyDescriptor[] listAccessors) {
                return string.Empty;
            }
            #endregion

            public void SetPropertyValue(int rowIndex, string column, object value) {
                list[rowIndex][column] = value;
            }
            public object GetPropertyValue(int rowIndex, string column) {
                return list[rowIndex][column];
            }

            #region IList Members
            public int Add(object value) {
                list.Add((Dictionary<string, object>)value);
                return -1;
            }
            public void Clear() {
                throw new NotImplementedException();
            }
            public bool Contains(object value) {
                throw new NotImplementedException();
            }
            public int IndexOf(object value) {
                throw new NotImplementedException();
            }
            public void Insert(int index, object value) {
                throw new NotImplementedException();
            }
            public bool IsFixedSize {
                get { return true; }
            }
            public bool IsReadOnly {
                get { return false; }
            }
            public void Remove(object value) {
                throw new NotImplementedException();
            }
            public void RemoveAt(int index) {
                throw new NotImplementedException();
            }
            public object this[int index] {
                get {
                    return list[index];
                }
                set {
                    throw new NotImplementedException();
                }
            }
            #endregion

            #region ICollection Members
            public void CopyTo(Array array, int index) {
                throw new NotImplementedException();
            }
            public int Count {
                get { return list.Count; }
            }
            public bool IsSynchronized {
                get { return true; }
            }
            public object SyncRoot {
                get { return true; }
            }
            #endregion

            #region IEnumerable Members
            public IEnumerator GetEnumerator() {
                return null;
            }
            #endregion
        }
        public class CellSelectionPropertyDescriptor : PropertyDescriptor {
            readonly string propertyName;
            readonly CellSelectionList list;
            readonly Type propertyType;
            public CellSelectionPropertyDescriptor(CellSelectionList list, string propertyName, Type propertyType)
                : base(propertyName, null) {
                this.propertyName = propertyName;
                this.list = list;
                this.propertyType = propertyType;
            }
            public override object GetValue(object component) {
                return ((Dictionary<string, object>)component)[propertyName];
            }
            public override void SetValue(object component, object val) {
                ((Dictionary<string, object>)component)[propertyName] = val;
            }
            public override bool CanResetValue(object component) {
                return false;
            }
            public override bool IsReadOnly { get { return false; } }
            public override Type ComponentType { get { return typeof(Employees); } }
            public override Type PropertyType { get { return propertyType; } }
            public override void ResetValue(object component) {
            }
            public override bool ShouldSerializeValue(object component) { return true; }
        }        
    }
    public class SalesByYearDataColumnTemplateSelector : DataTemplateSelector {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            ColumnGeneratorItemContext context = (ColumnGeneratorItemContext)item;
            GridControl grid = (GridControl)container;
            return (DataTemplate)grid.Resources[context.PropertyDescriptor.Name == "Date" ? "DateColumnTemplate" : "EmployeeColumnTemplate"];
        }
    }
}