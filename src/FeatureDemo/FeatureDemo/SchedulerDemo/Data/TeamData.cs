using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DevExpress.DemoData.Models;

using DevExpress.Mvvm;
using DevExpress.WinUI.Scheduler;
using Windows.Storage;



namespace SchedulerDemo {
    public class TeamCalendar {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TeamAppointment {
        public int Id { get; set; }
        public int AppointmentType { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Label { get; set; }
        public string Location { get; set; }
        public int ResourceId { get; set; }

        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
    }
    public static class TeamData {
        static XmlSerializer EmployeeSerializer = XmlSerializer.FromTypes(new[] { typeof(Employee[]) })[0];

        static void LoadFromXml() {
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/SchedulerDemoData/Employees.xml")).AsTask().Result;
            Stream stream = file.OpenStreamForReadAsync().Result;
            var employees = EmployeeSerializer.Deserialize(stream) as Employee[];
            stream.Dispose();
            foreach (var employee in employees)
                if (employee.CustomerEmployee != null)
                    foreach (var ce in employee.CustomerEmployee) 
                        ce.Employee = employees.Where(e => e.EmployeeID == ce.EmployeeId).FirstOrDefault();
            Employees = new List<Employee>(employees);

        }
        static TeamData() {
            Random = new Random();
            Start = GetStart();

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
                Employees = new List<Employee>();
                Calendars = CreateCalendars().ToList();
                VacationAppointments = new TeamAppointment[] { };
                CompanyBirthdayAppointments = new TeamAppointment[] { };
                BirthdayAppointments = new TeamAppointment[] { };
                ConferenceAppointments = new TeamAppointment[] { };
                MeetingAppointments = new TeamAppointment[] { };
                PhoneCallsAppointments = new TeamAppointment[] { };
                CarWashAppointments = new TeamAppointment[] { };
                PayBillsAppointments = new TeamAppointment[] { };
                DentistAppointments = new TeamAppointment[] { };
                RestaurantAppointments = new TeamAppointment[] { };
                AllAppointments = new TeamAppointment[] { };
                return;
            }

            
            Employees[0].BirthDate = Start.AddDays(4).AddYears(-30);

            Calendars = CreateCalendars().ToList();
            VacationAppointments = CreateVacationsAppts(Start).ToList();
            CompanyBirthdayAppointments = CreateCompanyBirthdayAppts(Start).ToList();
            BirthdayAppointments = CreateBirthdayAppts(Start).ToList();
            ConferenceAppointments = CreateConferenceAppts(Start).ToList();
            MeetingAppointments = CreateMeetingAppts(Start).ToList();
            PhoneCallsAppointments = CreatePhoneCallsAppts(Start).ToList();
            CarWashAppointments = CreateCarWashAppts(Start).ToList();
            TrainingAppointments = CreateTrainingAppts(Start).ToList();
            PayBillsAppointments = CreatePayBillsAppts(Start).ToList();
            DentistAppointments = CreateDentistAppts(Start).ToList();
            RestaurantAppointments = CreateRestaurantAppts(Start).ToList();
            AllAppointments = VacationAppointments
                .Concat(BirthdayAppointments)
                .Concat(CompanyBirthdayAppointments)
                .Concat(ConferenceAppointments)
                .Concat(MeetingAppointments)
                .Concat(PhoneCallsAppointments)
                .Concat(CarWashAppointments)
                .Concat(TrainingAppointments)
                .Concat(PayBillsAppointments)
                .Concat(DentistAppointments)
                .Concat(RestaurantAppointments)
                .ToList();
            int id = 0;
            foreach (TeamAppointment appt in AllAppointments)
                appt.Id = id++;
        }
        public static DateTime Start { get; private set; }
        public static IEnumerable<TeamCalendar> Calendars { get; private set; }
        public static TeamCalendar MyCalendar { get { return Calendars.ElementAt(0); } }
        public static TeamCalendar TeamCalendar { get { return Calendars.ElementAt(1); } }
        public static IEnumerable<TeamAppointment> AllAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> VacationAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> BirthdayAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> ConferenceAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> MeetingAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> PhoneCallsAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> CarWashAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> CompanyBirthdayAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> TrainingAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> PayBillsAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> DentistAppointments { get; private set; }
        public static IEnumerable<TeamAppointment> RestaurantAppointments { get; private set; }

        static List<Employee> Employees;
        static readonly Random Random;
        
        static DateTime GetStart() {
            DateTime today = DateTime.Today;
            DayOfWeek dayOfWeek = today.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Monday)
                return today;
            if (dayOfWeek == DayOfWeek.Sunday)
                return today.AddDays(1);
            return today.AddDays(-((int)dayOfWeek - 1));
        }
        static Employee GetRandomEmployee() {
            return Employees[Random.Next(0, Employees.Count)];
        }
        
        static IEnumerable<TeamCalendar> CreateCalendars() {
            return new TeamCalendar[] {
                new TeamCalendar() { Id = 0, Name = "My Calendar" },
                new TeamCalendar() { Id = 1, Name = "Team Calendar" },
            };
        }

        static IEnumerable<TeamAppointment> CreateBirthdayAppts(DateTime start) {
            return Employees.Select(CreateBirthdayAppt);
        }
        static TeamAppointment CreateBirthdayAppt(Employee employee) {
            if (employee.BirthDate == null) return null;
            DateTime date = employee.BirthDate.Value;
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = true,
                Start = date,
                End = date.AddDays(1),
                Subject = string.Format("{0}'s Birthday", employee.FirstName),
                Status = 0,
                Label = 8,
                ResourceId = 0,
            };
            appt.RecurrenceInfo = new RecurrenceInfo() {
                AllDay = true,
                Start = date,
                Month = date.Month,
                DayNumber = date.Day,
                Type = RecurrenceType.Yearly,
                Range = RecurrenceRange.NoEndDate
            }.ToString();
            return appt;
        }

        static IEnumerable<TeamAppointment> CreateConferenceAppts(DateTime start) {
            DateTime newStart = start;
            Tuple<string, DateTime>[] thisWeekList = new[] {
                Tuple.Create("DevExpress MVVM Framework", newStart.AddDays(1).AddHours(15)),
                Tuple.Create("New Theme Designer", newStart.AddDays(2).AddHours(14)),
                Tuple.Create("GridControl Performance Optimization", newStart.AddDays(3).AddHours(16)),
                Tuple.Create("WinForms and DirectX", newStart.AddDays(4).AddHours(16)),
            };

            newStart = start.AddDays(-7);
            Tuple<string, DateTime>[] prevWeekList = new[] {
                Tuple.Create("LOB applications", newStart.AddDays(1).AddHours(13)),
                Tuple.Create("Module Injection Framework", newStart.AddDays(2).AddHours(16)),
                Tuple.Create("Git tricks", newStart.AddDays(3).AddHours(10)),
                Tuple.Create("Machine learning", newStart.AddDays(4).AddHours(11)),
            };

            newStart = start.AddDays(7);
            Tuple<string, DateTime>[] nextWeekList = new[] {
                Tuple.Create("Azure", newStart.AddDays(1).AddHours(13)),
                Tuple.Create("WCF Services", newStart.AddDays(2).AddHours(16)),
                Tuple.Create("Docking Floating Panels", newStart.AddDays(3).AddHours(10)),
                Tuple.Create("Personal Time Management", newStart.AddDays(4).AddHours(11)),
            };

            newStart = start.AddDays(14);
            Tuple<string, DateTime>[] nextNextWeekList = new[] {
                Tuple.Create("Entity Framework Core", newStart.AddDays(1).AddHours(10)),
                Tuple.Create(".Net Core", newStart.AddDays(2).AddHours(16)),
            };

            IEnumerable<Tuple<string, DateTime>> list = thisWeekList.Concat(prevWeekList).Concat(nextWeekList).Concat(nextNextWeekList);

            List<Tuple<string, DateTime>> commonList = new List<Tuple<string, DateTime>>();
            DateTimeRange interval = new DateTimeRange(start.AddDays(-7), start.AddDays(21));
            IEnumerable<string> subjects = list.Select(x => x.Item1);
            for (int i = 0; i < 100; i++) {
                newStart = start.AddYears(-1);
                newStart = newStart.AddDays(Random.Next(2 * 365));
                newStart = newStart.AddHours(Random.Next(9, 18));
                if (interval.Start <= newStart && interval.End >= newStart)
                    continue;
                string subj = subjects.ElementAt(Random.Next(0, subjects.Count()));
                commonList.Add(Tuple.Create(subj, newStart));
            }
            return list.Concat(commonList).Select(x => CreateConferenceAppt(x.Item1, x.Item2));

        }
        static TeamAppointment CreateConferenceAppt(string subject, DateTime start) {
            Employee emp = GetRandomEmployee();
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(1.5),
                Subject = string.Format("Conference: {0}", subject),
                Description = string.Format("{0} {1} tells us about {2}.", emp.FirstName, emp.LastName, subject),
                Status = 2,
                Label = 2,
                Location = "Conference Room",
                ResourceId = 1,
            };
            return appt;
        }

        static IEnumerable<TeamAppointment> CreateMeetingAppts(DateTime start) {
            List<TeamAppointment> res = new List<TeamAppointment>() {
                CreateMeetingRecurrenceAppt("Weekly meeting", start.AddMonths(-6).Add(new TimeSpan(5, 14, 00, 0))),
                CreateLunchAppt(Employees[0], start.AddDays(1).AddHours(13)),
                CreateLunchAppt(Employees[1], start.AddDays(3).AddHours(13)),
                CreateLunchAppt(Employees[2], start.AddDays(-4).AddHours(13)),
                CreateLunchAppt(Employees[3], start.AddDays(9).AddHours(13)),
                CreateLunchAppt(Employees[4], start.AddDays(12).AddHours(13)),
            };
            DateTimeRange interval = new DateTimeRange(start.AddDays(-7), start.AddDays(21));
            List<int> days = new List<int>();
            for (int i = 0; i < 50; i++) {
                Employee emp = Employees[Random.Next(0, Employees.Count)];
                DateTime newStart = start.AddYears(-1);
                newStart = newStart.AddDays(Random.Next(365));
                if (interval.Start <= newStart && interval.End >= newStart)
                    continue;
                if (days.Contains(newStart.DayOfYear))
                    continue;
                if (VacationAppointments.Any(x => x.Start <= newStart && x.End >= newStart))
                    continue;
                days.Add(newStart.DayOfYear);
                res.Add(CreateLunchAppt(emp, newStart.AddHours(13)));
            }
            return res;
        }
        static TeamAppointment CreateMeetingRecurrenceAppt(string subject, DateTime start) {
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = false,
                Start = start,
                End = start.AddHours(1),
                Subject = subject,
                Status = 2,
                Label = 2,
                ResourceId = 1,
            };
            appt.RecurrenceInfo = new RecurrenceInfo() {
                Start = start,
                Type = RecurrenceType.Weekly,
                WeekDays = WeekDays.Friday,
                Month = 12,
                Range = RecurrenceRange.NoEndDate
            }.ToString();
            return appt;
        }
        static TeamAppointment CreateLunchAppt(Employee emp, DateTime start) {
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(1),
                Subject = string.Format("Lunch with {0}", emp.FirstName),
                Status = 3,
                Label = 3,
                ResourceId = 0,
            };
            return appt;
        }

        static IEnumerable<TeamAppointment> CreatePhoneCallsAppts(DateTime start) {
            List<TeamAppointment> res = new List<TeamAppointment>() {
                CreatePhoneCallAppt(Employees[0], start.AddDays(0).AddHours(10)),
                CreatePhoneCallAppt(Employees[1], start.AddDays(3).AddHours(11)),
                CreatePhoneCallAppt(Employees[2], start.AddDays(3).AddHours(12).AddMinutes(40), TimeSpan.FromMinutes(15)),
                CreatePhoneCallAppt(Employees[3], start.AddDays(-4).AddHours(14)),
                CreatePhoneCallAppt(Employees[4], start.AddDays(9).AddHours(15)),
                CreatePhoneCallAppt(Employees[5], start.AddDays(12).AddHours(15)),

                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(0).AddHours(16)),
                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(2).AddHours(15.6)),
                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(4).AddHours(15)),
                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(5).AddHours(10.5)),
                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(5).AddHours(16)),
                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(6).AddHours(9.7)),
                CreatePhoneCallAppt(GetRandomEmployee(), start.AddDays(6).AddHours(16.8)),
            };
            DateTimeRange interval = new DateTimeRange(start.AddDays(-7), start.AddDays(21));
            for (int i = 0; i < 50; i++) {
                Employee emp = Employees[Random.Next(0, Employees.Count)];
                DateTime newStart = start.AddYears(-1);
                newStart = newStart.AddDays(Random.Next(365));
                if (interval.Start <= newStart && interval.End >= newStart)
                    continue;
                if (VacationAppointments.Any(x => x.Start <= newStart && x.End >= newStart))
                    continue;
                res.Add(CreatePhoneCallAppt(emp, newStart.AddHours(Random.Next(9, 18))));
            }
            return res;
        }
        static TeamAppointment CreatePhoneCallAppt(Employee emp, DateTime start, TimeSpan? duration = null) {
            DateTime newStart = start.AddMinutes(Random.Next(0, 4) * 15);
            DateTime newEnd = duration != null 
                ? newStart.Add(duration.Value) 
                : newStart.AddMinutes(Random.Next(1, 6) * 5);
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = newStart,
                End = newEnd,
                Subject = string.Format("Phone Call with {0}", emp.FirstName),
                Status = 2,
                Label = 10,
                ResourceId = 0,
            };
            return appt;
        }

        static IEnumerable<TeamAppointment> CreateVacationsAppts(DateTime start) {
            return new[] {
                new TeamAppointment() {
                    AppointmentType = (int)AppointmentType.Normal,
                    AllDay = true,
                    Start = start.AddMonths(-6),
                    End = start.AddMonths(-6).AddDays(14),
                    Subject = string.Format("Vacation"),
                    Status = 0,
                    Label = 4,
                    ResourceId = 0,
                },
                new TeamAppointment() {
                    AppointmentType = (int)AppointmentType.Normal,
                    AllDay = true,
                    Start = start.AddMonths(6),
                    End = start.AddMonths(6).AddDays(14),
                    Subject = string.Format("Vacation"),
                    Status = 0,
                    Label = 4,
                    ResourceId = 0,
                },
                new TeamAppointment() {
                    AppointmentType = (int)AppointmentType.Normal,
                    AllDay = true,
                    Start = start.AddDays(4),
                    End = start.AddDays(8),
                    Subject = string.Format("Vacation"),
                    Status = 0,
                    Label = 4,
                    ResourceId = 0,
                },
            };
        }

        static IEnumerable<TeamAppointment> CreateCarWashAppts(DateTime start) {
            List<TeamAppointment> res = new List<TeamAppointment>() {
                CreateCarWashAppt(start.AddDays(1).AddHours(17)),
            };
            DateTime newStart = start.AddYears(-1);
            while(newStart < start.AddMonths(1)) {
                newStart = newStart.AddDays(Random.Next(18, 35));
                if (VacationAppointments.Any(x => x.Start <= newStart && x.End >= newStart))
                    continue;
                if (newStart >= start && newStart <= start.AddDays(7))
                    continue;
                CreateCarWashAppt(newStart);
            }
            return res;
        }
        static TeamAppointment CreateCarWashAppt(DateTime start) {
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(1),
                Subject = string.Format("Car Wash"),
                Status = 3,
                Label = 3,
                ResourceId = 0,
            };
            return appt;
        }

        static IEnumerable<TeamAppointment> CreateCompanyBirthdayAppts(DateTime start) {
            DateTime newStart = new DateTime(start.Year - 1, start.Month, start.Day);
            newStart = newStart.AddDays(5);
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = true,
                Start = newStart,
                End = newStart.AddDays(1),
                Subject = "Company Birthday Party",
                Status = 0,
                Label = 8,
                ResourceId = 1,
            };
            appt.RecurrenceInfo = new RecurrenceInfo() {
                AllDay = true,
                Start = newStart,
                Type = RecurrenceType.Yearly,
                Month = newStart.Month,
                DayNumber = newStart.Day,
                Range = RecurrenceRange.NoEndDate
            }.ToString();

            return new[] { appt };
        }

        static IEnumerable<TeamAppointment> CreateTrainingAppts(DateTime start) {
            DateTime newStart = start.AddYears(-1).AddHours(8.5);
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = false,
                Start = newStart,
                End = newStart.AddHours(1.5),
                Subject = "Sport Training",
                Status = 1,
                Label = 3,
                ResourceId = 0,
            };
            appt.RecurrenceInfo = new RecurrenceInfo() {
                AllDay = false,
                Start = newStart,
                Type = RecurrenceType.Weekly,
                WeekDays = WeekDays.Monday | WeekDays.Wednesday | WeekDays.Friday,
                Range = RecurrenceRange.NoEndDate
            }.ToString();
            return new[] { appt };
        }

        static IEnumerable<TeamAppointment> CreatePayBillsAppts(DateTime start) {
            DateTime newStart = start.AddDays(2).AddYears(-1);
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = true,
                Start = newStart,
                End = newStart.AddDays(1),
                Subject = "Pay Bills",
                Status = 0,
                Label = 3,
                ResourceId = 0,
            };
            appt.RecurrenceInfo = new RecurrenceInfo() {
                AllDay = true,
                Start = newStart,
                Type = RecurrenceType.Monthly,
                DayNumber = newStart.Day,
                Range = RecurrenceRange.NoEndDate,
            }.ToString();
            return new[] { appt };
        }

        static IEnumerable<TeamAppointment> CreateDentistAppts(DateTime start) {
            List<TeamAppointment> res = new List<TeamAppointment>() {
                CreateDentistAppt(start.AddDays(4).AddHours(17.5)),
            };
            DateTime newStart = start.AddYears(-2);
            while (newStart < start) {
                newStart = newStart.AddDays(Random.Next(365/3, 365/2));
                CreateDentistAppt(newStart);
            }
            return res;
        }
        static TeamAppointment CreateDentistAppt(DateTime start) {
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(2),
                Subject = string.Format("Dentist"),
                Status = 3,
                Label = 3,
                ResourceId = 0,
            };
            return appt;
        }

        static IEnumerable<TeamAppointment> CreateRestaurantAppts(DateTime start) {
            List<TeamAppointment> res = new List<TeamAppointment>() {
                CreateDinnerAppt(start.AddDays(2).AddHours(19)),
                CreateDinnerAppt(start.AddDays(14).AddHours(19)),
                CreateDinnerAppt(start.AddDays(18).AddHours(21)),
            };
            DateTime newStart = start.AddYears(-2);
            while (newStart < start) {
                newStart = newStart.AddDays(Random.Next(14, 42));
                res.Add(CreateDinnerAppt(newStart.AddHours(Random.Next(18, 22))));
            }
            return res;
        }
        static TeamAppointment CreateDinnerAppt(DateTime start, TimeSpan? duration = null) {
            DateTime newStart = start.AddMinutes(Random.Next(0, 4) * 15);
            DateTime newEnd = duration != null
                ? newStart.Add(duration.Value)
                : newStart.AddMinutes(Random.Next(4, 8) * 20);
            var appt = new TeamAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = newStart,
                End = newEnd,
                Subject = string.Format("Dinner"),
                Status = 0,
                Label = 5,
                ResourceId = 0,
            };
            return appt;
        }
    }
}
