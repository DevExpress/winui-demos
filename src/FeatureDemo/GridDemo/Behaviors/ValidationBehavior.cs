using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Grid.Internal;
using DevExpress.WinUI.Mvvm.UI.Interactivity;

namespace GridDemo {
    public sealed class ValidationBehavior : Behavior<GridControl> {
        PersonalTasksViewModel viewModel;
        PersonalTasksViewModel ViewModel {
            get {
                if(viewModel == null)
                    viewModel = (PersonalTasksViewModel)AssociatedObject.DataContext;
                return viewModel;
            }
        } 

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.ValidateCell += AssociatedObject_ValidateCell;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.ValidateCell -= AssociatedObject_ValidateCell;
        }
        void AssociatedObject_ValidateCell(object sender, GridCellValidationEventArgs e) => ViewModel.ValidateCell(e);
    }
}
