using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;

namespace FeatureDemo.Data {
    public class NWindDataLoader {
        public object Invoices {
            get {
                IList data = NWindData.Invoices;
                return data.Count != 0 ? data : null;
            }
        }
    }
    public static class NWindData {
        public static IList Invoices { get { return NWindData<Invoices>.DataSource; } }
    }
    [XmlRoot("NewDataSet")]
    public class NWindData<T> : List<T> {
        static List<T> dataSource;
        static XmlSerializer Serializer = new XmlSerializer(typeof(NWindData<T>));
        public static List<T> NewDataSource {
            get {
                return (List<T>)Serializer.Deserialize(GetDataStream());
            }
        }
        public static List<T> DataSource {
            get {
                if(dataSource == null)
                    dataSource = NewDataSource;
                return dataSource;
            }
        }
        public static Stream GetDataStream() {
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri(string.Format("ms-appx:///Data/NWind{0}.xml", typeof(T).Name))).AsTask().Result;
            return file.OpenStreamForReadAsync().Result;
        }
    }
}