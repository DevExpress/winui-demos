using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using System;
using System.ComponentModel;

namespace GridDemo {
    public sealed partial class NewItemRowModule : GridDemoUserControl {
        public NewItemRowViewModel Data { get; } = new NewItemRowViewModel();
        public NewItemRowModule() {
            InitializeComponent();
        }
        public NewItemRowPosition GetNewItemRowPosition(bool top) {
            return top ? NewItemRowPosition.Top : NewItemRowPosition.Bottom;
        }
        protected internal override GridControl Grid { get { return grid; } }

        private void grid_AddingNewRow(object sender, AddingNewEventArgs e) {
            e.NewObject = new Item() {
                Priority = FeatureDemo.Data.Priority.Low,
                Status = FeatureDemo.Data.Status.New,
                CreatedDate = DateTime.Now,
                FixedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
