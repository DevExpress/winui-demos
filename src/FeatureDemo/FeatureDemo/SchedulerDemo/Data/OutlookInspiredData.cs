using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.WinUI.Scheduler;
using Microsoft.UI;
using Color = Windows.UI.Color;

namespace SchedulerDemo {
    public enum EventPriority { None = 0, HighImportance = 1, LowImportance = 2 }
    public class OutlookInspiredData {
        public OutlookInspiredData() {
            CreateCategories();
            CreateCalendars();
            CreateEvents();
        }

        public ObservableCollection<Calendar> Calendars { get; private set; }
        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<OutlookEvent> Events { get; private set; }

        Category NoneCategory { get { return Categories[0]; } }
        Category BlueCategory { get { return Categories[4]; } }
        Category VioletCategory { get { return Categories[6]; } }
        Category GreenCategory { get { return Categories[5]; } }

        Calendar MyCalendar { get; set; }
        Calendar Birthdays { get; set; }
        Calendar TeamCalendar { get; set; }

        void CreateCategories() {
            Categories = new ObservableCollection<Category>();
            Categories.Add(Category.Create(0, "None", ColorExtensions.FromRgb(188, 188, 188)));
            Categories.Add(Category.Create(1, "Yellow category", ColorExtensions.FromRgb(255, 241, 0)));
            Categories.Add(Category.Create(2, "Red category", ColorExtensions.FromRgb(240, 125, 136)));
            Categories.Add(Category.Create(3, "Orange category", ColorExtensions.FromRgb(255, 140, 0)));
            Categories.Add(Category.Create(4, "Blue category", ColorExtensions.FromRgb(85, 171, 229)));
            Categories.Add(Category.Create(5, "Green category", ColorExtensions.FromRgb(95, 190, 125)));
            Categories.Add(Category.Create(6, "Violet category", ColorExtensions.FromRgb(168, 149, 226)));
        }
        void CreateCalendars() {
            Calendars = new ObservableCollection<Calendar>();
            MyCalendar = Calendar.Create("My Calendars", "My Calendar");
            Birthdays = Calendar.Create("My Calendars", "Birthdays");
            TeamCalendar = Calendar.Create("Work Calendars", "Team Calendar");
            Calendars.Add(MyCalendar);
            Calendars.Add(Birthdays);
            Calendars.Add(TeamCalendar);
        }
        void CreateEvents() {
            Events = new ObservableCollection<OutlookEvent>();
            if(Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;
            foreach (TeamAppointment appt in TeamData.VacationAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));
            foreach (TeamAppointment appt in TeamData.PhoneCallsAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));
            foreach (TeamAppointment appt in TeamData.CarWashAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));
            foreach (TeamAppointment appt in TeamData.TrainingAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));
            foreach (TeamAppointment appt in TeamData.PayBillsAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));
            foreach (TeamAppointment appt in TeamData.DentistAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));
            foreach (TeamAppointment appt in TeamData.RestaurantAppointments)
                Events.Add(CreateEvent(appt, MyCalendar.Id, NoneCategory.Id));

            foreach (TeamAppointment appt in TeamData.BirthdayAppointments)
                Events.Add(CreateEvent(appt, Birthdays.Id, VioletCategory.Id));

            foreach (TeamAppointment appt in TeamData.ConferenceAppointments)
                Events.Add(CreateEvent(appt, TeamCalendar.Id, GreenCategory.Id));
            foreach (TeamAppointment appt in TeamData.MeetingAppointments)
                Events.Add(CreateEvent(appt, TeamCalendar.Id, GreenCategory.Id));
            foreach (TeamAppointment appt in TeamData.CompanyBirthdayAppointments)
                Events.Add(CreateEvent(appt, TeamCalendar.Id, GreenCategory.Id));

            int id = 0;
            foreach (OutlookEvent appt in Events) {
                appt.Id = id++;
            }
        }
        OutlookEvent CreateEvent(TeamAppointment appt, int? resourceId, int categoryId) {
            OutlookEvent outlookEvent = OutlookEvent.Create();
            outlookEvent.AllDay = appt.AllDay;
            outlookEvent.StartTime = appt.Start;
            outlookEvent.EndTime = appt.End;
            outlookEvent.ResourceId = resourceId;
            outlookEvent.Subject = appt.Subject;
            outlookEvent.Location = appt.Location;
            outlookEvent.StatusId = appt.Status;
            outlookEvent.CategorizeId = categoryId;
            outlookEvent.Type = appt.AppointmentType;
            outlookEvent.RecurrenceInfo = appt.RecurrenceInfo;
            outlookEvent.ReminderInfo = appt.ReminderInfo;
            return outlookEvent;
        }
    }

    public class Calendar {
        static int id = 0;
        public static Calendar Create(string group, string caption) {
            return new Calendar() {
                Group = group,
                Caption = caption,
                IsVisible = true,
            };
        }
        public static Calendar Create() {
            return new Calendar();
        }
        protected Calendar() {
            Id = id++;
        }

        public int Id { get; private set; }
        public virtual string Caption { get; set; }
        public virtual string Group { get; set; }
        public virtual bool IsVisible { get; set; }
    }
    public class OutlookEvent {
        public static OutlookEvent Create() {
            return new OutlookEvent();
        }
        protected OutlookEvent() {
            Priority = EventPriority.None;
        }

        public virtual int Id { get; set; }
        public virtual bool AllDay { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual int? ResourceId { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Location { get; set; }
        public virtual string Description { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int CategorizeId { get; set; }
        public virtual int Type { get; set; }
        public virtual string RecurrenceInfo { get; set; }
        public virtual string ReminderInfo { get; set; }
        public virtual string TimeZoneId { get; set; }
        public virtual bool IsPrivate { get; set; }
        public virtual EventPriority Priority { get; set; }
    }
    public class Category {
        public static Category Create(int id, string caption, Color color) {
            return new Category(id, caption, color);
        }
        protected Category(int id, string caption, Color color) {
            Id = id;
            Caption = caption;
            Color = color;
        }

        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual Color Color { get; set; }
        public virtual bool IsChecked { get; set; }
    }
    public class CheckedCategory {
        public static CheckedCategory Create(AppointmentLabelItem category, bool isChecked) {
            return new CheckedCategory(category, isChecked);
        }
        protected CheckedCategory(AppointmentLabelItem category, bool isChecked) {
            Category = category;
            IsChecked = isChecked;
        }

        public virtual AppointmentLabelItem Category { get; set; }
        public virtual bool IsChecked { get; set; }
    }
}