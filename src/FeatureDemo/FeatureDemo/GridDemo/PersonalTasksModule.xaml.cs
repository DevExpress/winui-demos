using DevExpress.WinUI.Grid;

namespace GridDemo {
    public sealed partial class PersonalTasksModule : GridDemoUserControl {
        public PersonalTasksViewModel ViewModel { get; } = new PersonalTasksViewModel();
        public PersonalTasksModule() {
            this.InitializeComponent();
        }
        protected internal override GridControl Grid => grid;
    }
}
