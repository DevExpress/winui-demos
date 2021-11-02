using DevExpress.Mvvm;
using DevExpress.Data.Helpers;
using DevExpress.WinUI.Editors.Internal;
using FeatureDemo.Data;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.CodeGenerators;

namespace GridDemo {
    public enum TaskStatus { NotStarted, InProgress, Completed, WaitingOnSomeoneElse, Deferred }
    public enum TaskPriority { Low, Medium, High }
    public enum TaskCategory { HouseChores, Shopping, Work }
    public enum FlagStatus { Today, Tomorrow, ThisWeek, NextWeek, NoDate, Custom, Completed }

    public sealed class EmployeeTasksDataGenerator {
        #region Collection Resources

        static readonly string[] EmployeeFullNames = new string[] {
            "Marcus Orbison",
            "James Packard",
            "Samantha Bright",
            "Bradley Jameson",
            "Kelly Rodriguez",
            "Margaret Boxter",
            "Clark Morgan",
            "Jack Garmin",
            "David Jones",
            "Harvey Mudd"
        };
        static readonly string[] HouseTasks = new string[] {
            "Set-up home theater system",
            "Install 3 overhead lights in bedroom",
            "Change light bulbs in backyard",
            "Install a programmable thermostat",
            "Install ceiling fan in John's bedroom",
            "Install LED lights in kitchen",
            "Check wiring in main electricity panel",
            "Replace bedroom light switch with dimmer",
            "Install an new electric outlet in garage",
            "Install electric outlet in closet",
            "Install chandelier in dining room",
            "Hook up DVD Player to TV for kids",
            "Clean the House top to bottom",
            "Light cleaning of the house",
            "Clean the entire house",
            "Clean and organize basement",
            "Pick up clothes for charity event",
            "Ironing, laundry and vacuuming",
            "Take kids to park on Sunday",
            "Clean art studio",
            "Bake brownies",
            "Assemble Kitchen Cart",
            "Move piano",
            "Clean backyard",
            "Clean out garage",
            "Organize guest bedroom",
            "Clean out closet",
            "Prepare for yard sale",
            "Sorting clothing for give-away",
            "Organize Storage Room"};
        static readonly string[] ShoppingTasks = new string[] {
            "Shopping at Macy's",
            "Return coffee machine",
            "Purchase plastic trash bins",
            "Shop for dinner ingredients",
            "Buy new utensils for kitchen",
            "Send post card to Timothy",
            "Buy dining table and TV stand online",
            "Buy ingredients for Pasta Bolognese",
            "Size 3 diapers (3 cases)",
            "Order 3 pizzas",
            "Find out where to buy the new tablet",
            "Buy broccoli and tomatoes",
            "Buy bottle of Champagne",
            "Grocery shopping at Market Basket",
            "Find a bike at a store close to me",
            "Return jeans at JCrew",
            "Buy dog food for Fido",
            "Buy rigid foam insulation",
            "Purchase 3 24-packs of bottled Coke",
            "Purchase & deliver flowers to my home"};
        static readonly string[] OfficeTasks = new string[] {
            "Input bank statements into Excel",
            "Schedule appointments and pay bills",
            "Place new address stickers on envelopes",
            "Set up and arrange appointments",
            "Copy PDF file into Word",
            "Organize business expenses",
            "Return samples to vendor",
            "Organize and match receipts",
            "File papers and receipts",
            "Ship out CDs to customers",
            "Respond to e-mails until noon",
            "Enter expenses into an accounting system",
            "Conduct furniture inventory in office",
            "Arrange travel to conference",
            "Staple flyers to gift bags",
            "File and shred mail",
            "Print copies of brochures",
            "Enter all receipts into Excel",
            "Research possible vendors",
            "Sort through paper receipts",
            "Re-package products for retail sale",
            "Scan docs, and put in desktop folder",
            "Print registration stickers"};

        #endregion

        Random rndGenerator = new Random();

        List<EmployeeTask> GenerateEmployeeTasks() {
            List<EmployeeTask> tasks = new List<EmployeeTask>();
            for(int i = 0; i < 10; i++)
                foreach(string s in OfficeTasks)
                    tasks.Add(CreateTask(s, TaskCategory.Work));
            foreach(string s in HouseTasks)
                tasks.Add(CreateTask(s, TaskCategory.HouseChores));
            foreach(string s in ShoppingTasks)
                tasks.Add(CreateTask(s, TaskCategory.Shopping));
            return tasks;
        }
        EmployeeTask CreateTask(string subject, TaskCategory category) {
            EmployeeTask task = new EmployeeTask(subject, category, DateTime.Now.AddHours(-rndGenerator.Next(96)));
            if (task.TimeDiff.TotalHours > 80)
                task.Status = TaskStatus.Completed;
            else if (task.TimeDiff.TotalHours > 8) {
                task.Status = TaskStatus.InProgress;
                task.PercentComplete = rndGenerator.Next(8) * 10 + 10;
            }
            task.StartDate = task.CreatedDate.AddMinutes(rndGenerator.Next(720)).Date;

            int rndStatus = rndGenerator.Next(10);
            if (rndStatus != 5)
                task.DueDate = task.CreatedDate.AddHours((90 - rndStatus * 9) + 24).Date;
            if (rndStatus > 8)
                task.Priority = TaskPriority.High;
            if (rndStatus < 3)
                task.Priority = TaskPriority.Low;
            if (rndStatus == 6 && task.Status == TaskStatus.InProgress)
                task.Status = TaskStatus.Deferred;            
            if (rndStatus >= 4 && rndStatus <= 7 && task.Status == TaskStatus.InProgress && task.PercentComplete < 40)
                task.Status = TaskStatus.WaitingOnSomeoneElse;
            if (task.Category == TaskCategory.Work && rndStatus != 7 && EmployeeFullNames.Length > 0)
                task.AssignTo = Employees[rndGenerator.Next(EmployeeFullNames.Length)].Id;
            if (task.Status == TaskStatus.Completed) {
                if (task.StartDate == null) task.StartDate = task.CreatedDate.AddHours(12).Date;
                task.FinishDate = task.StartDate.Value.AddHours(rndGenerator.Next(48) + 24);
            }
            return task;
        }
        List<GridDemoEmployee> FillEmployees() {
            var result = new List<GridDemoEmployee>();
            for(var id = 0; id < EmployeeFullNames.Length; id++)
                result.Add(new GridDemoEmployee(id, EmployeeFullNames[id]));
            return result;
        }

        List<EmployeeTask> employeeTasks;
        public List<EmployeeTask> EmployeeTasks {
            get {
                if(employeeTasks == null)
                    employeeTasks = GenerateEmployeeTasks();
                return employeeTasks;
            }
        }
        List<GridDemoEmployee> employees;
        public List<GridDemoEmployee> Employees {
            get {
                if(employees == null)
                    employees = FillEmployees();
                return employees;
            }
        }
    }
    public sealed class GridDemoEmployee {
        public GridDemoEmployee(int id, string fullName) {
            Id = id;
            FullName = fullName;
        }

        public int Id { get; }
        public string FullName { get; }
    }

    [GenerateViewModel]
    public partial class EmployeeTask {
        public EmployeeTask(string subject, TaskCategory category)
            : this(subject, category, DateTime.Now) {
        }
        internal EmployeeTask(string subject, TaskCategory category, DateTime date) {
            Subject = subject;
            Category = category;
            CreatedDate = date;
        }

        [GenerateProperty]
        TaskPriority priority = TaskPriority.Medium;
        [GenerateProperty]
        int percentComplete;
        [GenerateProperty]
        DateTime createdDate;
        [GenerateProperty]
        DateTime? startDate;
        [GenerateProperty]
        DateTime? dueDate;
        [GenerateProperty]
        DateTime? finishDate;
        [GenerateProperty]
        string subject;
        [GenerateProperty]
        TaskCategory category;
        [GenerateProperty]
        TaskStatus status;
        [GenerateProperty]
        int assignTo;
        [GenerateProperty]
        bool complete;
        [GenerateProperty]
        bool overdue;
        [GenerateProperty]
        FlagStatus flagStatus;

        void OnDueDateChanged() {
            UpdateOverdue(); 
            UpdateFlagStatus();
        }
        internal TimeSpan TimeDiff {
            get { return DateTime.Now - CreatedDate; }
        }

        bool lockUpdateOverdue;
        bool lockUpdateFlagStatus;
        bool lockOnCompleteChanged;
        bool lockOnPercentCompleteChanged;
        void UpdateOverdue() {
            if(lockUpdateOverdue) return;
            Func<bool> calc = () => {
                if(Status == TaskStatus.Completed || !DueDate.HasValue) return false;
                DateTime dDate = DueDate.Value.Date.AddDays(1);
                if(DateTime.Now >= dDate) return true;
                return false;
            };
            Overdue = calc();
        }
        void UpdateFlagStatus() {
            if(lockUpdateFlagStatus) return;
            Func<FlagStatus> calc = () => {
                DateTime today = DateTime.Today;
                if(Complete) return FlagStatus.Completed;
                if(!DueDate.HasValue) return FlagStatus.NoDate;
                if(DueDate.Value.Date.Equals(today)) return FlagStatus.Today;
                if(DueDate.Value.Date.Equals(today.AddDays(1))) return FlagStatus.Tomorrow;
                DateTime thisWeekStart = OutlookDateHelper.GetWeekStart(today);
                if(DueDate.Value.Date >= thisWeekStart && DueDate.Value.Date < thisWeekStart.AddDays(7)) return FlagStatus.ThisWeek;
                if(DueDate.Value.Date >= thisWeekStart.AddDays(7) && DueDate.Value.Date < thisWeekStart.AddDays(14)) return FlagStatus.NextWeek;
                return FlagStatus.Custom;
            };
            FlagStatus = calc();
        }

        void OnPercentCompleteChanged() {
            if(lockOnPercentCompleteChanged) return;
            switch(percentComplete) {
                case 100:
                    Status = TaskStatus.Completed;
                    break;
                case var v when v > 0:
                    Status = TaskStatus.InProgress;
                    break;
            }
        }
        void OnStartDateChanged() {
            if(StartDate != null && StartDate > FinishDate)
                FinishDate = StartDate;
        }
        void OnFinishDateChanged() {
            if(FinishDate != null) {
                PercentComplete = 100;
                if(StartDate != null && StartDate > FinishDate)
                    StartDate = FinishDate;
            }
            UpdateOverdue();
        }
        void OnStatusChanged() {
            lockUpdateOverdue = true;
            lockUpdateFlagStatus = true;
            lockOnCompleteChanged = true;
            lockOnPercentCompleteChanged = true;
            switch(Status) {
                case TaskStatus.NotStarted:
                    FinishDate = null;
                    PercentComplete = 0;
                    break;
                case TaskStatus.InProgress:
                    FinishDate = null;
                    if(PercentComplete == 100)
                        PercentComplete = 99;
                    break;
                case TaskStatus.Completed:
                    PercentComplete = 100;
                    FinishDate = DateTime.Now;
                    break;
                case TaskStatus.WaitingOnSomeoneElse:
                    FinishDate = null;
                    if(PercentComplete == 100)
                        PercentComplete = 99;
                    break;
                case TaskStatus.Deferred:
                    FinishDate = null;
                    DueDate = null;
                    if(PercentComplete == 100)
                        PercentComplete = 99;
                    break;
            }
            Complete = Status == TaskStatus.Completed;
            lockOnPercentCompleteChanged = false;
            lockOnCompleteChanged = false;
            lockUpdateFlagStatus = false;
            lockUpdateOverdue = false;
            UpdateOverdue();
            UpdateFlagStatus();
        }
        void OnCompleteChanged() {
            if(lockOnCompleteChanged) return;
            Status = Complete ? TaskStatus.Completed : TaskStatus.NotStarted;
            UpdateFlagStatus();
        }
    }
}
