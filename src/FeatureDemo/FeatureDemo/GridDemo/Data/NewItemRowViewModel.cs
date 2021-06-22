using FeatureDemo.Data;
using System.Collections.ObjectModel;

namespace GridDemo {
    public class NewItemRowViewModel {
        public NewItemRowViewModel() {
            ItemsData data = new ItemsData();
            Items = new ObservableCollection<Item>(data.DataSource);
            Priorities = data.Priorities;
            Statuses = data.Statuses;
            Users = data.Users;
        }
        public ObservableCollection<Item> Items { get; }
        public FeatureDemo.Data.Priority[] Priorities { get; }
        public Status[] Statuses { get; }
        public User[] Users { get; }
    }
}

