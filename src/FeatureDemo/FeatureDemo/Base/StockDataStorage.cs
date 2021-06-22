using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace FeatureDemo.Data {
    [XmlRoot(nameof(StockItems))]
    public class StockItems : List<StockItem> {
    }
    [XmlRoot(nameof(StockItem))]
    public class StockItem : INotifyPropertyChanged {
        internal const int HistoryLength = 10;

        string companyName;
        public string CompanyName {
            get { return companyName; }
            set {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }
        double price;
        public double Price {
            get { return price; }
            set {
                price = value;
                OnPropertyChanged(nameof(Price));
                if(LowPrice > value)
                    LowPrice = value;
                if(HighPrice < value)
                    HighPrice = value;
            }
        }
        int volume;
        public int Volume {
            get { return volume; }
            set {
                volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
        double lowPrice;
        public double LowPrice {
            get { return lowPrice; }
            set {
                lowPrice = value;
                OnPropertyChanged(nameof(LowPrice));
            }
        }
        double highPrice;
        public double HighPrice {
            get { return highPrice; }
            set {
                highPrice = value;
                OnPropertyChanged(nameof(HighPrice));
            }
        }

        double deltaPrice;
        public double DeltaPrice {
            get { return deltaPrice; }
            set {
                deltaPrice = value;
                Price += value;
                OnPropertyChanged(nameof(DeltaPrice));
            }
        }
        int deltaPriceSignChange;
        public int DeltaPriceSignChange {
            get { return deltaPriceSignChange; }
            set {
                deltaPriceSignChange = value;
                OnPropertyChanged(nameof(DeltaPriceSignChange));
            }
        }

        double deltaPricePercent;
        public double DeltaPricePercent {
            get { return deltaPricePercent; }
            set {
                deltaPricePercent = value;
                OnPropertyChanged(nameof(DeltaPricePercent));
            }
        }


        List<double> priceHistory = new List<double>(new double[HistoryLength]);
        public List<double> PriceHistory {
            get { return priceHistory; }
            set {
                priceHistory = value;
                OnPropertyChanged(nameof(PriceHistory));
            }
        }

        public void UpdatePrice(Random rnd) {
            var oldDeltaPrice = DeltaPrice;
            DeltaPrice = rnd.NextDouble() * 0.5 - 0.25;
            DeltaPricePercent = (Price + DeltaPrice) / Price * 100 - 100;
            DeltaPriceSignChange = Comparer<int>.Default.Compare(Math.Sign(DeltaPrice), Math.Sign(oldDeltaPrice));
            Volume += Convert.ToInt32(rnd.NextDouble() * Volume * 0.005 - 0.0025);
            UpdatePriceHistory();
        }

        void UpdatePriceHistory() {
            List<double> newPriceHistory = new List<double>(new double[HistoryLength]);
            for(int i = 1; i < HistoryLength; i++)
                newPriceHistory[i - 1] = PriceHistory[i];
            newPriceHistory[HistoryLength - 1] = Price;
            PriceHistory = newPriceHistory;
        }

        readonly Dictionary<string, PropertyChangedEventArgs> argsCache = new Dictionary<string, PropertyChangedEventArgs>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void OnPropertyChanged(string propName) {
            if(PropertyChanged != null) {
                if(!argsCache.TryGetValue(propName, out var args))
                    args = argsCache[propName] = new PropertyChangedEventArgs(propName);
                PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
