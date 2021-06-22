using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.WinUI.Scheduler;
using FeatureDemo.Common;

using Windows.Storage;

namespace SchedulerDemo {
    public class WorkCalendar {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public string Group { get; set; }
    }
    public class WorkAppointment {
        public long Id { get; set; }
        public int AppointmentType { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Label { get; set; }
        public string Location { get; set; }
        public ObservableCollection<long> ResourceId { get; protected set; } = new ObservableCollection<long>();
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }

        public bool IsPrivate { get; set; }

        public string PatternId { get; set; }
    }
    public class WorkLabel {
        public int Id { get; set; }
        public string Caption { get; set; }
    }
    public static class WorkDataStrings {
        public static string AptSubj_Dinner = "Dinner";
        public static string AptSubj_Dentist = "Dentist";
        public static string AptSubj_Gym = "Gym";
        public static string AptSubj_CarWash = "Car wash";
        public static string AptSubj_Lunch = "Lunch";
        public static string AptSubj_PhoneCall = "Phone call with {0}";
        public static string AptSubj_FrenchLesson = "French lesson";
        public static string AptSubj_GermanLesson = "German lesson";
        public static string AptSubj_Training = "Training (Customer Service)";
        public static string AptSubj_Sick = "Sick Leave";
        public static string AptSubj_SecondShift = "Second shift";
        public static string AptSubj_HalfDay = "Half day off";
        public static string AptSubj_Day = "Day off";
        public static string AptSubj_Vacation = "Vacation";
        public static string AptSubj_Birthday = "Birthday";
        public static string AptSubj_CompanyMeeting = "Company meeting";
        public static string AptSubj_FridayParty = "Friday party";
        public static string AptSubj_TripConFooCA = "ConFoo.CA";
        public static string AptSubj_TripDevnexus = "Devnexus";
        public static string AptSubj_TripSmashingConf = "SmashingConf";
        public static string AptSubj_DeveloperWeek = "DeveloperWeek";
        public static string AptSubj_Conference = "Conference: {0}";

        public static string AptDesc_FrenchLesson = "If we are to have any chance in France, salespeople must learn french. Practice makes perfect and without constant repetition, learning a new language is impossible.";
        public static string AptDesc_GermanLesson = "If we are to have any chance in Germany, salespeople must learn german. Practice makes perfect and without constant repetition, learning a new language is impossible.";
        public static string AptDesc_Training = "Jack Howell from Worldwide Consulting will pay us a visit to give us pointers on how to improve our customer service processes.";
        public static string AptDesc_CompanyMeeting = "Review rules that govern workplace behavior. Discuss impact of changes to the law and how it will impact DevAV in the upcoming 12 months.";
        public static string AptDesc_FridayParty = "Rest, relax and get ready for next week.";
        public static string AptDesc_TripConFooCA = "ConFoo focuses on pragmatic solutions for web developers. The conference usually features 150+ presentations and is trying to attract more AI developers.";
        public static string AptDesc_TripDevnexus = "Conference organizers say their goal is to make Devnexus a forum for connecting developers from all over the world and promoting open-source values. While the conference caters to all developers, there is a slightly larger focus on Java topics, and JavaScript after that.";
        public static string AptDesc_TripSmashingConf = "Considered one of the top conferences for designers, SmashingConf, which is affiliated with Smashing magazine, is aimed at seasoned professionals searching for a competitive edge in the design world.";
        public static string AptDesc_DeveloperWeek = "DeveloperWeek is a series of conferences held in the United States for developers and technology managers. The Oakland DeveloperWeek has more than 100 talks, with speakers from the biggest companies in tech. Some of the events include DevExec World, a get-together for developer managers and executives, and a BlockChain Dev conference to discuss the latest developments in blockchain and ethereum technologies.";
        public static string AptDesc_Conference = "{0} {1} tells us about {2}.";

        public static string AptLoc_TrainingRoom = "Training Room";
        public static string AptLoc_ConferenceRoom = "Conference Room 1001";
        public static string AptLoc_TripConFooCA = "Montreal, Quebec, Canada";
        public static string AptLoc_TripDevnexus = "Atlanta, Georgia, USA";
        public static string AptLoc_TripSmashingConf = "London, United Kingdom";
        public static string AptLoc_DeveloperWeek = "Oakland, California, USA";

        public static string Conf1 = "DevExpress MVVM Framework";
        public static string Conf2 = "New Theme Designer";
        public static string Conf3 = "New Theme Designer";
        public static string Conf4 = "WinForms and DirectX";
        public static string Conf5 = "LOB applications";
        public static string Conf6 = "Module Injection Framework";
        public static string Conf7 = "Git tricks";
        public static string Conf8 = "Machine learning";
        public static string Conf9 = "Azure";
        public static string Conf10 = "WCF Services";
        public static string Conf11 = "Docking Floating Panels";
        public static string Conf12 = "Personal Time Management";
        public static string Conf13 = "Entity Framework Core";
        public static string Conf14 = ".Net Core";

        public static string Team_Support = "Support Team";
        public static string Team_Development = "Development Team";
        public static string MyCalendar = "My Calendar";

        public static string GetCustomerInfo(Customer customer) {
            string res = "";
            res += string.Format("Contact Name: {0}", customer.ContactName) + Environment.NewLine;
            res += string.Format("Country: {0}", customer.Country) + Environment.NewLine;
            res += string.Format("City: {0}", customer.City) + Environment.NewLine;
            res += string.Format("Fax: {0}", customer.Fax) + Environment.NewLine;
            res += string.Format("Phone: {0}", customer.Phone);
            return res;
        }
    }
    public static class WorkData {
        public static DateTime TodayStart { get; private set; }
        public static DateTime Start { get; private set; }
        public static ObservableCollection<Employee> Employees { get; private set; }
        public static ObservableCollection<Customer> Customers { get; private set; }
        
        public static ObservableCollection<Employee> Employees_Support { get; private set; }
        public static ObservableCollection<Employee> Employees_Development { get; private set; }
        public static ObservableCollection<WorkAppointment> Appointments { get; private set; }
        public static ObservableCollection<WorkCalendar> Calendars { get; private set; }
        public static WorkCalendar MyCalendar { get; private set; }
        public static ObservableCollection<WorkCalendar> Calendars_Support { get; private set; }
        public static ObservableCollection<WorkCalendar> Calendars_Development { get; private set; }
        public static ObservableCollection<WorkLabel> Labels { get; private set; }

        static Employee Nancy { get; set; }
        static Employee Andrew { get; set; }
        static Employee Janet { get; set; }
        static Employee Margaret { get; set; }
        static Employee Steven { get; set; }
        static Employee Michael { get; set; }
        static Employee Robert { get; set; }
        static Employee Laura { get; set; }
        static Employee Anne { get; set; }

        static int StatusFree = 0;
        static int StatusTentative = 1;
        static int StatusBusy = 2;
        
        static int StatusWorkingElsewhere = 4;

        static WorkLabel LabelNone = new WorkLabel() { Id = 0, Caption = "None" };
        static WorkLabel LabelImportant = new WorkLabel() { Id = 1, Caption = "Important" };
        static WorkLabel LabelPersonal = new WorkLabel() { Id = 2, Caption = "Personal" };
        static WorkLabel LabelBirthday = new WorkLabel() { Id = 3, Caption = "Birthday" };
        static WorkLabel LabelVacation = new WorkLabel() { Id = 4, Caption = "Vacation" };
        static WorkLabel LabelJourney = new WorkLabel() { Id = 5, Caption = "Journey" };
        static WorkLabel LabelSecondShift = new WorkLabel() { Id = 6, Caption = "Second Shift" };
        static WorkLabel LabelTraining = new WorkLabel() { Id = 7, Caption = "Training" };
        static WorkLabel LabelPhoneCall = new WorkLabel() { Id = 8, Caption = "Phone Call" };

        static Random random;

        static WorkData() {
            random = new Random();
            Start = GetMonday(DateTime.Today);
            TodayStart = GetWorkDay(DateTime.Today, true);
            Employees = new ObservableCollection<Employee>();
            Customers = new ObservableCollection<Customer>();
            Appointments = new ObservableCollection<WorkAppointment>();
            Calendars = new ObservableCollection<WorkCalendar>();
            Calendars_Support = new ObservableCollection<WorkCalendar>();
            Calendars_Development = new ObservableCollection<WorkCalendar>();
            MyCalendar = new WorkCalendar();
            Labels = new ObservableCollection<WorkLabel>();
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;

            InitEmployees();
            InitCalendars();
            InitLabels();
            InitAppts();
        }
        static void LoadFromXml() {
            XDocument document = XDocument.Parse(DemoDataLoader.GetFileContentFromResource("Data/SchedulerDemoData/Employees.xml"), LoadOptions.None);
            if(document != null) {
                List<Employee> employees = new List<Employee>();
                foreach(XElement element in document.Element("ArrayOfEmployee").Elements()) {
                    Employee employee = new Employee();
                    employees.Add(employee);
                    if(element.Element("EmployeeID") != null) 
                        employee.EmployeeID = long.Parse(element.Element("EmployeeID").Value);
                    if(element.Element("LastName") != null)
                        employee.LastName = element.Element("LastName").Value;
                    if(element.Element("FirstName") != null)
                        employee.FirstName = element.Element("FirstName").Value;
                    if(element.Element("Title") != null)
                        employee.Title = element.Element("Title").Value;
                    if(element.Element("TitleOfCourtesy") != null)
                        employee.TitleOfCourtesy = element.Element("TitleOfCourtesy").Value;
                    if(element.Element("BirthDate") != null)
                        employee.BirthDate = DateTime.Parse(element.Element("BirthDate").Value);
                    if(element.Element("HireDate") != null)
                        employee.HireDate = DateTime.Parse(element.Element("HireDate").Value);
                    if(element.Element("Address") != null)
                        employee.Address = element.Element("Address").Value;
                    if(element.Element("Region") != null)
                        employee.Region = element.Element("Region").Value;
                    if(element.Element("City") != null)
                        employee.City = element.Element("City").Value;
                    if(element.Element("PostalCode") != null)
                        employee.PostalCode = element.Element("PostalCode").Value;
                    if(element.Element("Country") != null)
                        employee.Country = element.Element("Country").Value;
                    if(element.Element("HomePhone") != null)
                        employee.HomePhone = element.Element("HomePhone").Value;
                    if(element.Element("Extension") != null)
                        employee.Extension = element.Element("Extension").Value;
                    if(element.Element("Photo") != null)
                        employee.Photo = System.Text.Encoding.ASCII.GetBytes(element.Element("Photo").Value);
                    if(element.Element("Notes") != null)
                        employee.Notes = element.Element("Notes").Value;

                    XElement ReportsTo = element.Element("ReportsTo");
                    employee.ReportsTo = ReportsTo == null || ReportsTo.Value == "" ? (long?)null : long.Parse(ReportsTo.Value);
                    if(element.Element("Email") != null)
                        employee.Email = element.Element("Email").Value;
                    if(element.Element("GroupName") != null)
                        employee.GroupName = element.Element("GroupName").Value;
                    XElement customerEmployees = element.Element("CustomerEmployee");
                    if(customerEmployees != null) {
                        employee.CustomerEmployee = new List<CustomerEmployee>();
                        foreach(XElement customerEmployee in customerEmployees.Elements()) {
                            string CustomerID = customerEmployee.Element("CustomerID").Value;
                            string EmployeeId = customerEmployee.Element("EmployeeId").Value;
                            CustomerEmployee ce = new CustomerEmployee();
                            ce.EmployeeId = long.Parse(EmployeeId);
                            ce.CustomerID = CustomerID;
                            employee.CustomerEmployee.Add(ce);
                        }
                    }
                }
                Employees = new ObservableCollection<Employee>(employees);
            }
            document = XDocument.Parse(DemoDataLoader.GetFileContentFromResource("Data/SchedulerDemoData/Customers.xml"), LoadOptions.None);
            if(document != null) {
                List<Customer> customers = new List<Customer>();
                foreach(XElement element in document.Element("ArrayOfCustomer").Elements()) {
                    Customer customer = new Customer();
                    customers.Add(customer);
                    if(element.Element("CustomerID") != null)
                        customer.CustomerID = element.Element("CustomerID").Value;
                    if(element.Element("CompanyName") != null)
                        customer.CompanyName = element.Element("CompanyName").Value;
                    if(element.Element("ContactName") != null)
                        customer.ContactName = element.Element("ContactName").Value;
                    if(element.Element("ContactTitle") != null)
                        customer.ContactTitle = element.Element("ContactTitle").Value;
                    if(element.Element("Address") != null)
                        customer.Address = element.Element("Address").Value;
                    if(element.Element("City") != null)
                        customer.City = element.Element("City").Value;
                    if(element.Element("PostalCode") != null)
                        customer.PostalCode = element.Element("PostalCode").Value;
                    if(element.Element("Country") != null)
                        customer.Country = element.Element("Country").Value;
                    if(element.Element("Phone") != null)
                        customer.Phone = element.Element("Phone").Value;
                    if(element.Element("Fax") != null)
                        customer.Fax = element.Element("Fax").Value;
                    XElement customerEmployees = element.Element("CustomerEmployee");
                    if(customerEmployees != null) {
                        customer.CustomerEmployee = new List<CustomerEmployee>();
                        foreach(XElement customerEmployee in customerEmployees.Elements()) {
                            string CustomerID = customerEmployee.Element("CustomerID").Value;
                            string EmployeeId = customerEmployee.Element("EmployeeId").Value;
                            CustomerEmployee ce = new CustomerEmployee();
                            ce.EmployeeId = long.Parse(EmployeeId);
                            ce.CustomerID = CustomerID;
                            customer.CustomerEmployee.Add(ce);
                        }
                    }
                }
                Customers = new ObservableCollection<Customer>(customers);
                foreach(var employee in Employees)
                    if(employee.CustomerEmployee != null)
                        foreach(var ce in employee.CustomerEmployee) {
                            ce.Employee = Employees.Where(e => e.EmployeeID == ce.EmployeeId).FirstOrDefault();
                            ce.Customer = Customers.Where(c => c.CustomerID == ce.CustomerID).FirstOrDefault();
                        }
                foreach(var customer in Customers)
                    if(customer.CustomerEmployee != null)
                        foreach(var ce in customer.CustomerEmployee) {
                            ce.Employee = Employees.Where(e => e.EmployeeID == ce.EmployeeId).FirstOrDefault();
                            ce.Customer = Customers.Where(c => c.CustomerID == ce.CustomerID).FirstOrDefault();
                        }
            }
        }
        static void InitEmployees() {
            LoadFromXml();
            Employees_Support = new ObservableCollection<Employee>(Employees.Take(4));
            Employees_Development = new ObservableCollection<Employee>(Employees.Skip(4));
            
            Nancy = Employees_Support[0];
            Andrew = Employees_Support[1];
            Janet = Employees_Support[2];
            Margaret = Employees_Support[3];

            Steven = Employees_Development[0];
            Michael = Employees_Development[1];
            Robert = Employees_Development[2];
            Laura = Employees_Development[3];
            Anne = Employees_Development[4];

            Nancy.BirthDate = TodayStart.AddYears(-GetOld(Nancy));
            Margaret.BirthDate = Start.AddYears(-GetOld(Margaret));
            Steven.BirthDate = Start.AddYears(-GetOld(Steven));
        }
        static void InitCalendars() {
            MyCalendar = new WorkCalendar() {
                Id = -1,
                Name = WorkDataStrings.MyCalendar,
                IsVisible = true,
            };
            Calendars_Support = new ObservableCollection<WorkCalendar>(
                Employees_Support
                .Select(x => new WorkCalendar() {
                    Id = (int)x.EmployeeID,
                    Name = x.FullName,
                    IsVisible = true,
                }));
            Calendars_Development = new ObservableCollection<WorkCalendar>(
                Employees_Development
                .Select(x => new WorkCalendar() {
                    Id = (int)x.EmployeeID,
                    Name = x.FullName,
                    IsVisible = false,
                }));


            Calendars.Add(MyCalendar);
            Calendars_Support.ToList().ForEach(x => Calendars.Add(x));
            Calendars_Development.ToList().ForEach(x => Calendars.Add(x));
        }
        static void InitLabels() {
            Labels.Add(LabelNone);
            Labels.Add(LabelImportant);
            Labels.Add(LabelPersonal);
            Labels.Add(LabelTraining);
            Labels.Add(LabelPhoneCall);
            Labels.Add(LabelBirthday);
            Labels.Add(LabelVacation);
            Labels.Add(LabelJourney);
            Labels.Add(LabelSecondShift);
        }
        static void InitAppts() {
            Employees_Support
                .SelectMany(x => GenerateEmployeesApts(x.EmployeeID, true, false))
                .ToList()
                .ForEach(x => Appointments.Add(x));
            Employees_Development
                .SelectMany(x => GenerateEmployeesApts(x.EmployeeID, false, true))
                .ToList()
                .ForEach(x => Appointments.Add(x));

            AddThisWeekNancy();
            AddThisWeekAndrew();
            AddThisWeekJanet();
            AddThisWeekMargaret();

            AddThisWeekSteven();
            AddThisWeekMichael();
            AddThisWeekRobert();
            AddThisWeekLaura();
            AddThisWeekAnne();

            AddMyAppts();
            AddBirthdayApts();
            AddConferences();
        }

        static void AddMyAppts() {
            List<WorkAppointment> res = new List<WorkAppointment>();
            res.AddRange(GenerateEmployeesApts(-1, true, false));
            res.Add(Gym(Start, -1));
            
            res.Add(Lunch(Start.AddHours(13), -1));
            res.Add(Lunch(Start.AddDays(1).AddHours(13), -1));
            res.Add(Lunch(Start.AddDays(2).AddHours(13), -1));
            res.Add(Lunch(Start.AddDays(3).AddHours(12), -1));
            res.Add(Lunch(Start.AddDays(4).AddHours(12), -1));

            DateTime newStart = Start.AddYears(-1);
            while (newStart < Start.AddMonths(1)) {
                newStart = newStart.AddDays(random.Next(18, 35));
                if (res.Any(x => x.Start <= newStart && x.End >= newStart))
                    continue;
                if (newStart >= Start && newStart <= Start.AddDays(7))
                    continue;
                AddIfNoIntersect(res, 1, () => CarWash(newStart, -1));
            }
            AddIfNoIntersect(res, 1, () => CarWash(Start.AddDays(1).AddHours(17), -1));

            newStart = Start.AddYears(-2);
            while (newStart < Start) {
                newStart = newStart.AddDays(random.Next(365 / 3, 365 / 2));
                AddIfNoIntersect(res, 1, () => Dentist(newStart, -1, false));
            }
            AddIfNoIntersect(res, 1, () => Dentist(Start.AddHours(17), -1, true));

            newStart = Start.AddMonths(-5);
            while (newStart < Start) {
                newStart = newStart.AddDays(random.Next(14, 42));
                AddIfNoIntersect(res, 1, () => Dinner(newStart.AddHours(random.Next(20, 21)), -1));
            }
            AddIfNoIntersect(res, 1, () => Dinner(TodayStart.AddDays(2).AddHours(20.5), -1));
            AddIfNoIntersect(res, 1, () => Dinner(Start.AddDays(14).AddHours(19), -1));
            AddIfNoIntersect(res, 1, () => Dinner(Start.AddDays(18).AddHours(21), -1));

            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy), 
                Start.AddHours(10).AddMinutes(10), 
                Start.AddHours(10).AddMinutes(25), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy),
                Start.AddHours(16),
                Start.AddHours(16).AddMinutes(25), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy), 
                Start.AddDays(1).AddHours(11).AddMinutes(5),
                Start.AddDays(1).AddHours(11).AddMinutes(15), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy), 
                Start.AddDays(1).AddHours(15).AddMinutes(20),
                Start.AddDays(1).AddHours(15).AddMinutes(35), - 1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy), 
                Start.AddDays(1).AddHours(16),
                Start.AddDays(1).AddHours(16).AddMinutes(10), - 1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy),
                Start.AddDays(2).AddHours(15).AddMinutes(10),
                Start.AddDays(2).AddHours(15).AddMinutes(20), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy), 
                Start.AddDays(3).AddHours(11).AddMinutes(5),
                Start.AddDays(3).AddHours(11).AddMinutes(15), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy),
                Start.AddDays(3).AddHours(16).AddMinutes(5),
                Start.AddDays(3).AddHours(16).AddMinutes(35), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy),
                Start.AddDays(4).AddHours(11).AddMinutes(0),
                Start.AddDays(4).AddHours(11).AddMinutes(5), -1));
            AddIfNoIntersect(res, 1, () => PhoneCall(GetRandomCustomer(Nancy),
                Start.AddDays(4).AddHours(15).AddMinutes(10),
                Start.AddDays(4).AddHours(15).AddMinutes(25), -1));

            res.ForEach(x => Appointments.Add(x));
        }
        static void AddBirthdayApts() {
            Employees
                .Select(x => Birthday(x, x.EmployeeID))
                .ToList()
                .ForEach(x => Appointments.Add(x));
        }
        static void AddConferences() {
            Appointments.Add(CompanyMeeting(Start.AddDays(2)));
            Appointments.Add(FridayParty(Start));

            DateTime start = Start;
            Appointments.Add(ConferenceGridControlPerformanceOptimization(start.AddDays(1).AddHours(16), true));
            Appointments.Add(ConferenceThemeDesigner(start.AddDays(3).AddHours(13), true));
            Appointments.Add(ConferenceWinFormsAndDirectX(start.AddDays(4).AddHours(16), true));

            start = start.AddDays(7);
            AddIfNoIntersect(Appointments, 1, () => ConferenceWCFServices(start.AddDays(4), false));

            start = start.AddDays(7);
            AddIfNoIntersect(Appointments, 1, () => ConferenceModuleInjectionFramework(start.AddDays(1), false));
            AddIfNoIntersect(Appointments, 1, () => ConferenceGitTricks(start.AddDays(2), false));

            start = start.AddDays(7);
            AddIfNoIntersect(Appointments, 1, () => ConferenceLOBApplications(start, false));
            AddIfNoIntersect(Appointments, 1, () => ConferenceAzure(start.AddDays(3), false));

            start = Start.AddDays(-7);
            AddIfNoIntersect(Appointments, 1, () => ConferenceDockingFloatingPanels(start.AddDays(1), false));
            AddIfNoIntersect(Appointments, 1, () => ConferencePersonalTimeManagement(start.AddDays(2), false));
            AddIfNoIntersect(Appointments, 1, () => ConferenceEntityFrameworkCore(start.AddDays(4), false));

            start = Start.AddDays(-7);
            AddIfNoIntersect(Appointments, 1, () => ConferenceNetCore(start.AddDays(3), false));

            start = Start.AddDays(-7);
            AddIfNoIntersect(Appointments, 1, () => ConferenceMVVM(start, false));
            AddIfNoIntersect(Appointments, 1, () => ConferenceMachineLearning(start.AddDays(1), false));

            AddIfNoIntersect(Appointments, 4,
                   () => FridayParty(Start.AddDays(random.Next(-30 * 5, -7))));
            AddIfNoIntersect(Appointments, 4,
                    () => FridayParty(Start.AddDays(random.Next(14, 60))));

        }

        static void AddThisWeekNancy() {
            Appointments.Add(Training_FrenchLesson(Start, Nancy.EmployeeID));
            Appointments.Add(Training_CustomerService(Start, Nancy.EmployeeID));

            Appointments.Add(Lunch(Start.AddHours(13), Nancy.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(1).AddHours(13), Nancy.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Nancy.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(12), Nancy.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(4).AddHours(13), Nancy.EmployeeID));

            Appointments.Add(PhoneCall(Nancy,
                Start.AddDays(1).AddHours(11).AddMinutes(30),
                Start.AddDays(1).AddHours(11).AddMinutes(45)));
            Appointments.Add(PhoneCall(Nancy,
                Start.AddDays(2).AddHours(16).AddMinutes(30),
                Start.AddDays(2).AddHours(16).AddMinutes(45)));
            Appointments.Add(PhoneCall(Nancy,
                Start.AddDays(3).AddHours(16).AddMinutes(30),
                Start.AddDays(3).AddHours(16).AddMinutes(45)));
            Appointments.Add(PhoneCall(Nancy,
                Start.AddDays(4).AddHours(16).AddMinutes(30),
                Start.AddDays(4).AddHours(16).AddMinutes(45)));
        }
        static void AddThisWeekAndrew() {
            Appointments.Add(SecondShift(Start, Andrew.EmployeeID));
            Appointments.Add(HalfDayVacation(Start, false, Andrew.EmployeeID));

            Appointments.Add(Lunch(Start.AddDays(1).AddHours(18), Andrew.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(18), Andrew.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(18), Andrew.EmployeeID));

            Appointments.Add(PhoneCall(Andrew,
                Start.AddHours(20).AddMinutes(30),
                Start.AddHours(20).AddMinutes(45)));
            Appointments.Add(PhoneCall(Andrew,
                Start.AddDays(1).AddHours(17).AddMinutes(10),
                Start.AddDays(1).AddHours(17).AddMinutes(25)));
            Appointments.Add(PhoneCall(Andrew,
                Start.AddDays(2).AddHours(16).AddMinutes(10),
                Start.AddDays(2).AddHours(16).AddMinutes(25)));
            Appointments.Add(PhoneCall(Andrew,
                Start.AddDays(3).AddHours(17).AddMinutes(45),
                Start.AddDays(3).AddHours(17).AddMinutes(50)));
            Appointments.Add(PhoneCall(Andrew,
                Start.AddDays(3).AddHours(21).AddMinutes(45),
                Start.AddDays(3).AddHours(21).AddMinutes(50)));
            Appointments.Add(PhoneCall(Andrew,
                Start.AddDays(4).AddHours(22).AddMinutes(30),
                Start.AddDays(4).AddHours(22).AddMinutes(40)));
        }
        static void AddThisWeekJanet() {
            Appointments.Add(Training_GermanLesson(Start, Janet.EmployeeID));
            Appointments.Add(DayVacation(Start, Janet.EmployeeID, 2));

            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Janet.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(12), Janet.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(4).AddHours(13), Janet.EmployeeID));

            Appointments.Add(PhoneCall(Janet,
               Start.AddDays(2).AddHours(10).AddMinutes(30),
               Start.AddDays(2).AddHours(10).AddMinutes(45)));
            Appointments.Add(PhoneCall(Janet,
               Start.AddDays(2).AddHours(14).AddMinutes(30),
               Start.AddDays(2).AddHours(14).AddMinutes(45)));
            Appointments.Add(PhoneCall(Janet,
               Start.AddDays(2).AddHours(16).AddMinutes(30),
               Start.AddDays(2).AddHours(16).AddMinutes(35)));

            Appointments.Add(PhoneCall(Janet,
               Start.AddDays(4).AddHours(15).AddMinutes(30),
               Start.AddDays(4).AddHours(15).AddMinutes(35)));
        }
        static void AddThisWeekMargaret() {
            Appointments.Add(Training_GermanLesson(Start, Margaret.EmployeeID));
            Appointments.Add(DayVacation(Start, Margaret.EmployeeID));
            Appointments.Add(DayVacation(Start.AddDays(2), Margaret.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(1).AddHours(13), Margaret.EmployeeID));
            Appointments.Add(TripSmashingConf(Start.AddDays(3), Margaret.EmployeeID));
        }
        static void AddThisWeekSteven() {
            Appointments.Add(Training_GermanLesson(Start, Steven.EmployeeID));
            Appointments.Add(TripDevnexus(Start.AddDays(14), Steven.EmployeeID));
            Appointments.Add(Lunch(Start.AddHours(13), Steven.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(1).AddHours(13), Steven.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Steven.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(12), Steven.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(4).AddHours(14), Steven.EmployeeID));
        }
        static void AddThisWeekMichael() {
            Appointments.Add(TripDeveloperWeek(Start.AddDays(-6), Michael.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Michael.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(12), Michael.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(4).AddHours(14), Michael.EmployeeID));
        }
        static void AddThisWeekRobert() {
            Appointments.Add(HalfDayVacation(Start, false, Robert.EmployeeID));
            Appointments.Add(HalfDayVacation(Start.AddDays(1), false, Robert.EmployeeID));
            Appointments.Add(TripConFooCA(Start.AddDays(3), Robert.EmployeeID));
            Appointments.Add(Lunch(Start.AddHours(13), Robert.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(1).AddHours(13), Robert.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Robert.EmployeeID));
        }
        static void AddThisWeekLaura() {
            Appointments.Add(HalfDayVacation(Start.AddDays(1), true, Laura.EmployeeID));
            Appointments.Add(HalfDayVacation(Start.AddDays(3), true, Laura.EmployeeID));
            AddIfNoIntersect(Appointments, 1, () => TripDevnexus(Start.AddDays(-30), Laura.EmployeeID));
            Appointments.Add(Lunch(Start.AddHours(13), Laura.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Laura.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(12), Laura.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(4).AddHours(14), Laura.EmployeeID));
        }
        static void AddThisWeekAnne() {
            Appointments.Add(Training_FrenchLesson(Start, Anne.EmployeeID));
            Appointments.Add(Lunch(Start.AddHours(13), Anne.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(1).AddHours(13), Anne.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(2).AddHours(13), Anne.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(3).AddHours(12), Anne.EmployeeID));
            Appointments.Add(Lunch(Start.AddDays(4).AddHours(14), Anne.EmployeeID));
        }
        static IEnumerable<WorkAppointment> GenerateEmployeesApts(long employeeId, bool isSupport, bool isDevelopment) {
            List<WorkAppointment> res = new List<WorkAppointment>();
            res.Add(LongVacation(
                Start.AddDays(random.Next(-10 * 30, -30)), employeeId));
            res.Add(LongVacation(
                Start.AddDays(random.Next(30, 10 * 30)), employeeId));

            if (isSupport) {
                AddIfNoIntersect(res, 6,
                    () => SecondShift(Start.AddDays(random.Next(-30 * 12, -1)), employeeId));
                AddIfNoIntersect(res, 1,
                    () => SecondShift(Start.AddDays(random.Next(7, 30)), employeeId));
            }
            AddIfNoIntersect(res, 8,
                () => DayVacation(Start.AddDays(random.Next(-30 * 6, -4)), employeeId));
            AddIfNoIntersect(res, 1,
                () => DayVacation(Start.AddDays(random.Next(7, 30)), employeeId));
            AddIfNoIntersect(res, 8,
                () => HalfDayVacation(Start.AddDays(random.Next(-30 * 6, -1)), RandomBool(), employeeId));
            AddIfNoIntersect(res, 3,
                () => HalfDayVacation(Start.AddDays(random.Next(7, 30)), RandomBool(), employeeId));
            AddIfNoIntersect(res, 2,
                () => Sick(Start.AddDays(random.Next(-30 * 10, -14)), employeeId));

            if (isSupport) {
                var emp = Employees.FirstOrDefault(x => x.EmployeeID == employeeId);
                if (emp != null && emp.CustomerEmployee != null && emp.CustomerEmployee.Any()) {
                    AddIfNoIntersect(res, 50,
                        () => PhoneCall_RandomTime(emp, Start.AddDays(random.Next(-30 * 10, -3))));
                    AddIfNoIntersect(res, 2,
                        () => PhoneCall_RandomTime(emp, Start.AddDays(random.Next(7, 21))));
                }
            }

            if (isSupport && RandomBool()) {
                AddIfNoIntersect(res, 1,
                    () => Training_CustomerService(Start.AddDays(random.Next(-30 * 10, -30 * 6)), employeeId));
                AddIfNoIntersect(res, 1,
                    () => Training_CustomerService(Start.AddDays(random.Next(-30 * 5, -7)), employeeId));
            }
            if (isSupport && RandomBool()) {
                AddIfNoIntersect(res, 1,
                    () => Training_CustomerService(Start.AddDays(random.Next(14, 30 * 2)), employeeId));
            }
            if (RandomBool()) {
                AddIfNoIntersect(res, 1,
                    () => Training_GermanLesson(Start.AddDays(random.Next(-30 * 10, -30 * 6)), employeeId));
                AddIfNoIntersect(res, 1,
                    () => Training_GermanLesson(Start.AddDays(random.Next(-30 * 5, -7 * 6)), employeeId));
            }
            if (RandomBool()) {
                AddIfNoIntersect(res, 1,
                    () => Training_GermanLesson(Start.AddDays(random.Next(7 * 6, 30 * 2)), employeeId));
            }
            if (RandomBool()) {
                AddIfNoIntersect(res, 1,
                    () => Training_FrenchLesson(Start.AddDays(random.Next(-30 * 10, -30 * 6)), employeeId));
                AddIfNoIntersect(res, 1,
                    () => Training_FrenchLesson(Start.AddDays(random.Next(-30 * 5, -7 * 4)), employeeId));
            }
            if (RandomBool()) {
                AddIfNoIntersect(res, 1,
                    () => Training_FrenchLesson(Start.AddDays(random.Next(7*4, 30 * 2)), employeeId));
            }

            return res;
        }

        static WorkAppointment ConferenceMVVM(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf1, start, exactTime, true, true);
        }
        static WorkAppointment ConferenceThemeDesigner(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf2, start, exactTime, true, true);
        }
        static WorkAppointment ConferenceGridControlPerformanceOptimization(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf3, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceWinFormsAndDirectX(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf4, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceLOBApplications(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf5, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceModuleInjectionFramework(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf6, start, exactTime, true, true);
        }
        static WorkAppointment ConferenceGitTricks(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf7, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceMachineLearning(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf8, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceAzure(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf9, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceWCFServices(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf10, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceDockingFloatingPanels(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf11, start, exactTime, true, true);
        }
        static WorkAppointment ConferencePersonalTimeManagement(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf12, start, exactTime, true, false);
        }
        static WorkAppointment ConferenceEntityFrameworkCore(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf13, start, exactTime, false, true);
        }
        static WorkAppointment ConferenceNetCore(DateTime start, bool exactTime) {
            return Conference(WorkDataStrings.Conf14, start, exactTime, false, true);
        }
        static WorkAppointment Conference(string subject, DateTime start, bool exactTime, bool ownerIsSupport, bool ownerIsDevelopment) {
            DateTime newStart = exactTime ? start : GetWorkDay(start).AddHours(random.Next(10, 17));
            DateTime newEnd = exactTime ? start.AddHours(1.5) : newStart.AddMinutes(random.Next(4, 8) * 15);
            Employee emp = GetRandomEmployee(ownerIsSupport, ownerIsDevelopment);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = newStart,
                End = newEnd,
                Subject = string.Format(WorkDataStrings.AptSubj_Conference, subject),
                Description = string.Format(WorkDataStrings.AptDesc_Conference, emp.FirstName, emp.LastName, subject),
                Status = StatusWorkingElsewhere,
                Label = LabelTraining.Id,
                Location = WorkDataStrings.AptLoc_ConferenceRoom,
            };
            appt.ResourceId.Add(GetAllResourceIds(ownerIsSupport, ownerIsDevelopment).First());
            return appt;
        }

        static WorkAppointment TripDeveloperWeek(DateTime start, long resourceId) {
            start = GetWorkDay(start).AddHours(random.Next(10, 17));
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(7),
                Subject = WorkDataStrings.AptSubj_DeveloperWeek,
                Description = WorkDataStrings.AptDesc_DeveloperWeek,
                Status = StatusWorkingElsewhere,
                Label = LabelJourney.Id,
                Location = WorkDataStrings.AptLoc_DeveloperWeek,
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment TripSmashingConf(DateTime start, long resourceId) {
            start = GetWorkDay(start).AddHours(random.Next(10, 17));
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(10),
                Subject = WorkDataStrings.AptSubj_TripSmashingConf,
                Description = WorkDataStrings.AptDesc_TripSmashingConf,
                Status = StatusWorkingElsewhere,
                Label = LabelJourney.Id,
                Location = WorkDataStrings.AptLoc_TripSmashingConf,
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment TripDevnexus(DateTime start, long resourceId) {
            start = GetWorkDay(start).AddHours(random.Next(10, 17));
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(5),
                Subject = WorkDataStrings.AptSubj_TripDevnexus,
                Description = WorkDataStrings.AptDesc_TripDevnexus,
                Status = StatusWorkingElsewhere,
                Label = LabelJourney.Id,
                Location = WorkDataStrings.AptLoc_TripDevnexus,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment TripConFooCA(DateTime start, long resourceId) {
            start = GetWorkDay(start).AddHours(random.Next(10, 17));
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(7),
                Subject = WorkDataStrings.AptSubj_TripConFooCA,
                Description = WorkDataStrings.AptDesc_TripConFooCA,
                Status = StatusWorkingElsewhere,
                Label = LabelJourney.Id,
                Location = WorkDataStrings.AptLoc_TripConFooCA,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }

        static WorkAppointment FridayParty(DateTime start) {
            start = GetMonday(start).AddDays(4).AddHours(18);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(2),
                Subject = WorkDataStrings.AptSubj_FridayParty,
                Description = WorkDataStrings.AptDesc_FridayParty,
                Status = StatusFree,
                Label = LabelNone.Id,
                Location = WorkDataStrings.AptLoc_ConferenceRoom,
                
            };
            appt.ResourceId.Add(GetAllResourceIds().First());
            return appt;
        }
        static WorkAppointment CompanyMeeting(DateTime start) {
            start = GetWorkDay(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start.AddHours(12),
                End = start.AddHours(13),
                Subject = WorkDataStrings.AptSubj_CompanyMeeting,
                Status = StatusWorkingElsewhere,
                Label = LabelImportant.Id,
                Location = WorkDataStrings.AptLoc_ConferenceRoom,
                Description = WorkDataStrings.AptDesc_CompanyMeeting,
                
            };
            appt.ResourceId.Add(GetAllResourceIds().First());
            return appt;
        }
        static WorkAppointment Birthday(Employee employee, long resourceId) {
            if (employee.BirthDate == null) return null;
            DateTime date = employee.BirthDate.Value;
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = true,
                Start = date,
                End = date.AddDays(1),
                Subject = WorkDataStrings.AptSubj_Birthday,
                Status = StatusFree,
                Label = LabelBirthday.Id,
                
                PatternId = Guid.NewGuid().ToString()
            };
            appt.ResourceId.Add(resourceId);
            appt.RecurrenceInfo = RecurrenceRule.New().Yearly().InMonth((uint)date.Month).OnDay((uint)date.Day);
            return appt;
        }
        static WorkAppointment LongVacation(DateTime start, long resourceId) {
            DateTime newStart = GetMonday(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = newStart,
                End = newStart.AddDays(14),
                Subject = WorkDataStrings.AptSubj_Vacation,
                Status = StatusFree,
                Label = LabelVacation.Id,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment DayVacation(DateTime start, long resourceId, int dayCount = 1) {
            start = GetWorkDay(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(dayCount),
                Subject = WorkDataStrings.AptSubj_Day,
                Status = StatusFree,
                Label = LabelVacation.Id,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment HalfDayVacation(DateTime start, bool isFirstHalf, long resourceId) {
            start = GetWorkDay(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = isFirstHalf ? start.AddHours(9) : start.AddHours(14),
                End = isFirstHalf ? start.AddHours(14) : start.AddHours(18),
                Subject = WorkDataStrings.AptSubj_HalfDay,
                Status = StatusFree,
                Label = LabelVacation.Id,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment SecondShift(DateTime start, long resourceId) {
            start = GetMonday(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(5),
                Subject = WorkDataStrings.AptSubj_SecondShift,
                Status = StatusBusy,
                Label = LabelSecondShift.Id,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment Sick(DateTime start, long resourceId) {
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = true,
                Start = start,
                End = start.AddDays(random.Next(3, 10)),
                Subject = WorkDataStrings.AptSubj_Sick,
                Status = StatusFree,
                Label = LabelVacation.Id,
                
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment Training_CustomerService(DateTime start, long resourceId) {
            start = GetMonday(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = false,
                Start = start.AddHours(14),
                End = start.AddHours(17),
                Subject = WorkDataStrings.AptSubj_Training,
                Status = StatusWorkingElsewhere,
                Label = LabelTraining.Id,
                Location = WorkDataStrings.AptLoc_TrainingRoom,
                Description = WorkDataStrings.AptDesc_Training,
                
                PatternId = Guid.NewGuid().ToString()
            };
            appt.ResourceId.Add(resourceId);
            appt.RecurrenceInfo = RecurrenceRule.New().Until(start.AddDays(7 * 3)).Weekly().OnDays(Days.Monday | Days.Wednesday | Days.Friday);
            return appt;
        }
        static WorkAppointment Training_GermanLesson(DateTime start, long resourceId) {
            start = GetMonday(start).AddDays(1);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = false,
                Start = start.AddHours(15),
                End = start.AddHours(16.5),
                Subject = WorkDataStrings.AptSubj_GermanLesson,
                Status = StatusWorkingElsewhere,
                Label = LabelTraining.Id,
                Location = WorkDataStrings.AptLoc_TrainingRoom,
                Description = WorkDataStrings.AptDesc_GermanLesson,
                
                PatternId = Guid.NewGuid().ToString()
            };
            appt.ResourceId.Add(resourceId);
            appt.RecurrenceInfo = RecurrenceRule.New().Until(start.AddDays(7 * 12)).Weekly().OnDays(Days.Tuesday | Days.Thursday);
            return appt;
        }
        static WorkAppointment Training_FrenchLesson(DateTime start, long resourceId) {
            start = GetMonday(start);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = false,
                Start = start.AddHours(9),
                End = start.AddHours(10.5),
                Subject = WorkDataStrings.AptSubj_FrenchLesson,
                Status = StatusWorkingElsewhere,
                Label = LabelTraining.Id,
                Location = WorkDataStrings.AptLoc_TrainingRoom,
                Description = WorkDataStrings.AptDesc_FrenchLesson,
                
                PatternId = Guid.NewGuid().ToString()
            };
            appt.ResourceId.Add(resourceId);
            appt.RecurrenceInfo = RecurrenceRule.New().Until(start.AddDays(7 * 12)).Weekly().OnDays(Days.Monday | Days.Wednesday | Days.Friday);
            return appt;
        }
        static WorkAppointment PhoneCall_RandomTime(Employee emp, DateTime start) {
            var customer = GetRandomCustomer(emp);
            return PhoneCall_RandomTime(customer, start, emp.EmployeeID);
        }
        static WorkAppointment PhoneCall(Employee emp, DateTime start, DateTime end) {
            var customer = GetRandomCustomer(emp);
            return PhoneCall(customer, start, end, emp.EmployeeID);
        }
        static WorkAppointment PhoneCall_RandomTime(Customer customer, DateTime start, long resourceId) {
            DateTime newStart = start.AddHours(random.Next(10, 17)).AddMinutes(random.Next(0, 5) * 5);
            DateTime newEnd = newStart.AddMinutes(random.Next(1, 6) * 5);
            return PhoneCall(customer, newStart, newEnd, resourceId);
        }
        static WorkAppointment PhoneCall(Customer customer, DateTime start, DateTime end, long resourceId) {
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = end,
                Subject = string.Format(WorkDataStrings.AptSubj_PhoneCall, customer.ContactName),
                Status = StatusBusy,
                Label = LabelPhoneCall.Id,
                
                Description = WorkDataStrings.GetCustomerInfo(customer),
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }

        static WorkAppointment Lunch(DateTime exactStart, long resourceId) {
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = exactStart,
                End = exactStart.AddHours(1),
                Subject = WorkDataStrings.AptSubj_Lunch,
                Status = StatusTentative,
                Label = LabelNone.Id,
                
                IsPrivate = true,
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment CarWash(DateTime start, long resourceId) {
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(1),
                Subject = WorkDataStrings.AptSubj_CarWash,
                Status = StatusTentative,
                Label = LabelPersonal.Id,
                
                IsPrivate = true,
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment Gym(DateTime start, long resourceId) {
            DateTime newStart = start.AddYears(-1).AddHours(8.5);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Pattern,
                AllDay = false,
                Start = newStart,
                End = newStart.AddHours(1.5),
                Subject = WorkDataStrings.AptSubj_Gym,
                Status = StatusTentative,
                Label = LabelPersonal.Id,
                
                IsPrivate = true,
                PatternId = Guid.NewGuid().ToString()
            };
            appt.ResourceId.Add(resourceId);
            appt.RecurrenceInfo = RecurrenceRule.New().Weekly().OnDays(Days.Monday | Days.Wednesday | Days.Friday);
            return appt;
        }
        static WorkAppointment Dentist(DateTime start, long resourceId, bool exactTime) {
            if (!exactTime)
                start = start.AddHours(random.Next(10, 18));
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = start,
                End = start.AddHours(2),
                Subject = WorkDataStrings.AptSubj_Dentist,
                Status = StatusFree,
                Label = LabelPersonal.Id,
                
                IsPrivate = true,
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }
        static WorkAppointment Dinner(DateTime start, long resourceId) {
            DateTime newStart = start.AddMinutes(random.Next(0, 4) * 15);
            DateTime newEnd = newStart.AddMinutes(random.Next(4, 8) * 20);
            var appt = new WorkAppointment() {
                AppointmentType = (int)AppointmentType.Normal,
                AllDay = false,
                Start = newStart,
                End = newEnd,
                Subject = WorkDataStrings.AptSubj_Dinner,
                Status = StatusFree,
                Label = LabelPersonal.Id,
                
                IsPrivate = true,
            };
            appt.ResourceId.Add(resourceId);
            return appt;
        }


        static DateTime GetMonday(DateTime date) {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Monday)
                return date.Date;
            if (dayOfWeek == DayOfWeek.Saturday)
                return date.Date.AddDays(2);
            if (dayOfWeek == DayOfWeek.Sunday)
                return date.Date.AddDays(1);
            return date.Date.AddDays(-((int)dayOfWeek - 1));
        }
        static DateTime GetWorkDay(DateTime date, bool decrease = false) {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return decrease ? date.AddDays(-1) : date.AddDays(2);
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return decrease ? date.AddDays(-2) : date.AddDays(1);
            return date;
        }
        static DateTimeRange GetInterval(WorkAppointment x) {
            return new DateTimeRange(x.Start, x.End);
        }
        static bool Intersect(DateTimeRange x, DateTimeRange y) {
            return x.Intersect(y) != DateTimeRange.Empty;
        }
        static bool Intersect(WorkAppointment x, WorkAppointment y) {
            return GetInterval(x).Intersect(GetInterval(y)) != DateTimeRange.Empty;
        }
        static bool RandomBool() {
            return random.Next(0, 2) == 0 ? false : true;
        }
        static void AddIfNoIntersect(IList<WorkAppointment> list, int count, Func<WorkAppointment> generate) {
            for (int i = 0; i < count; i++) {
                var a = generate();
                if (list.Any(x => Intersect(x, a))) continue;
                list.Add(a);
            }
        }
        static long[] GetAllResourceIds(bool isSupport = true, bool isDevelopment = true) {
            IEnumerable<Employee> employees;
            if (isSupport && isDevelopment)
                employees = Employees;
            else employees = isSupport ? Employees_Support : Employees_Development;
            var res = employees.Select(x => x.EmployeeID);
            if (isSupport)
                res = res.Concat(new long[] { -1 });
            return res.ToArray();
        }
        static Employee GetRandomEmployee(bool isSupport = true, bool isDevelopment = true) {
            IEnumerable<Employee> employees;
            if (isSupport && isDevelopment)
                employees = Employees;
            else employees = isSupport ? Employees_Support : Employees_Development;
            return employees.ElementAt(random.Next(0, employees.Count()));
        }
        static Customer GetRandomCustomer(Employee emp) {
            if (emp != null && emp.CustomerEmployee != null) {
                int index = random.Next(0, emp.CustomerEmployee.Count() - 1);
                CustomerEmployee ce = emp.CustomerEmployee.ElementAt(index);
                return ce.Customer;
            }
            return null;
        }
        static int GetOld(Employee emp) {
            if (emp.BirthDate == null)
                throw new InvalidOperationException();
            return DateTime.Today.Year - emp.BirthDate.Value.Year;
        }
    }
}
