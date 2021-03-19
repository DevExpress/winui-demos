using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Editors;
using DevExpress.WinUI.Grid.Internal;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GridDemo {
    public sealed class PersonalTasksViewModel : INotifyPropertyChanged {
        readonly ReadOnlyObservableCollection<EmployeeTask> allTasks;

        public ReadOnlyObservableCollection<TaskPriority> TaskPriorities { get; }
        public ReadOnlyObservableCollection<TaskStatus> TaskStatusList { get; }
        public ReadOnlyObservableCollection<TaskCategory> TaskCategories { get; }

        ReadOnlyObservableCollection<EmployeeTask> tasks;
        public ReadOnlyObservableCollection<EmployeeTask> Tasks {
            get => tasks; 
            set {
                if(tasks == value)
                    return;
                SetProperty(ref tasks, value);
            }
        }

        public PersonalTasksViewModel() {
            var generator = new EmployeeTasksDataGenerator();
            allTasks = generator.EmployeeTasks.ToReadOnlyObservableCollection();
            TaskPriorities = new TaskPriority[] { TaskPriority.Low, TaskPriority.Medium, TaskPriority.High }.ToReadOnlyObservableCollection();
            TaskStatusList = new TaskStatus[] { TaskStatus.NotStarted, TaskStatus.InProgress, TaskStatus.Completed, TaskStatus.WaitingOnSomeoneElse, TaskStatus.Deferred }.ToReadOnlyObservableCollection();
            TaskCategories = new TaskCategory[] { TaskCategory.HouseChores, TaskCategory.Shopping, TaskCategory.Work }.ToReadOnlyObservableCollection();
            Tasks = allTasks.Where(x => x.AssignTo == generator.Employees.First().Id).ToReadOnlyObservableCollection();
        }

        internal void ValidateCell(GridCellValidationEventArgs e) {
            if(e.Column.FieldName != nameof(EmployeeTask.DueDate))
                return;            
            var dueDate = (DateTime?)e.Value;
            var row = (EmployeeTask)e.Row;
            var startDate = row.StartDate;
            var status = row.Status;

            if((dueDate.HasValue && startDate.HasValue) && dueDate < startDate) {
                e.IsValid = false;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "Due Date can't be less than Start Date";
            }
            if(!dueDate.HasValue && status == TaskStatus.InProgress) {
                e.IsValid = false;
                e.ErrorType = ErrorType.Warning;
                e.ErrorContent = "Please enter Due Date";
            }
            e.Handled = true;
        }

        #region INotifyPropertyChanged members
        void SetProperty<T>(ref T storage, T value, Action callback = null, [CallerMemberName] string propertyName = null) {
            storage = value;
            OnPropertyChanged(propertyName);
            callback?.Invoke();
        }
        void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
