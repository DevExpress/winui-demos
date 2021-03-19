using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace GridDemo {
    public class StockDataViewModel : INotifyPropertyChanged {
        readonly DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        readonly Random rnd = new Random();

        readonly double[] positivePriceHistory = new double[StockItem.HistoryLength];
        readonly double[] negativePriceHistory = new double[StockItem.HistoryLength];

        public StockDataViewModel() {
            for(int i = 0; i < StockItem.HistoryLength; i++)
                UpdateItems();
            timer.Tick += OnTimerRaized;
        }
        public void Unload() {
            Pause();
            timer.Tick -= OnTimerRaized;
        }
        void OnTimerRaized(object sender, object e) {
            UpdateItems();
        }
        void UpdateItems() {
            StepLeftCustomSummaryInfo();
            DoWithUpdate(() => {
                foreach(StockItem item in Data) {
                    item.UpdatePrice(rnd);
                    if(Math.Sign(item.DeltaPrice) > 0) positivePriceHistory[StockItem.HistoryLength - 1]++; else negativePriceHistory[StockItem.HistoryLength - 1]++;
                }
            });
        }

        void DoWithUpdate(Action action) {
            BeginUpdate?.Invoke(this, EventArgs.Empty);
            action();
            EndUpdate?.Invoke(this, EventArgs.Empty);
        }

        void StepLeftCustomSummaryInfo() {
            for(int i = 1; i < StockItem.HistoryLength; i++) {
                positivePriceHistory[i - 1] = positivePriceHistory[i];
                negativePriceHistory[i - 1] = negativePriceHistory[i];
            }
            positivePriceHistory[StockItem.HistoryLength - 1] = 0;
            negativePriceHistory[StockItem.HistoryLength - 1] = 0;
        }

        public void Pause() {
            if(timer.IsEnabled)
                timer.Stop();
        }
        public void Resume() {
            if(!timer.IsEnabled)
                timer.Start();
        }

        public ReadOnlyObservableCollection<StockItem> Data { get; } = new StockItemsData().DataSource.ToList().ToReadOnlyObservableCollection();
        public double[] PositivePriceCountList { get { return positivePriceHistory; } }
        public double[] NegativePriceHistoryList { get { return negativePriceHistory; } }

        public event EventHandler BeginUpdate;
        public event EventHandler EndUpdate;

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
