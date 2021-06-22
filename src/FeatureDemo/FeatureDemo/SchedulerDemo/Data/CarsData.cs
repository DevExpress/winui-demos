using DevExpress.WinUI.Scheduler;
using DevExpress.WinUI.Scheduler.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace SchedulerDemo {
    public class Car {
        public Car(int carId, string caption) {
            Id = carId;
            Caption = caption;
        }

        public int Id { get; set; }
        public string Caption { get; set; }
    }
    public class CarAppointment {
        public int Id { get; set; }
        public bool AllDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Label { get; set; }
        public int EventType { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public int? CarId { get; set; }
        public double Price { get; set; }
        public ImageSource Image { get; set; }
    }
    public class CarsData {
        static class CarBrand {
            public const int MercedesBenz = 1;
            public const int Audi = 2;
            public const int Chevrolet = 3;
            public const int Lexus = 4;
            public const int Toyota = 5;
            public const int Nissan = 6;
            public const int Ford = 7;
        }
        static class CarDescription {
            public const string Rent = "Rent this car";
            public const string RentAllDay = "Rent this car for the all day";
            public const string Repair = "Scheduled repair of this car";
            public const string CheckUp = "Check up after maintenance";
            public const string Wash = "Wash this car in the garage";
        }
        static class CarLabel {
            public const int None = 0;
            public const int Important = 1;
            public const int Business = 2;
            public const int Personal = 3;
            public const int Vacation = 4;
            public const int MustAttend = 5;
        }
        static class CarStatus {
            public const int Free = 0;
            public const int Tentative = 1;
            public const int Busy = 2;
            public const int OutOfOffice = 3;
            public const int WorkingElsewhere = 4;
        }
        static class CarLocation {
            public const string Garage = "Garage";
            public const string ServiceCenter = "Service Center";
            public const string City = "City";
            public const string OutOfTown = "Out-Of-Town";
        }
        static class CarImages {
            public static readonly ImageSource CheckUp;
            public static readonly ImageSource Free;
            public static readonly ImageSource Maintance;
            public static readonly ImageSource Rent;
            public static readonly ImageSource Wash;
            static CarImages() {
                CheckUp = GetImage("CheckUp");
                Free = GetImage("Free");
                Maintance = GetImage("Maintance");
                Rent = GetImage("Rent");
                Wash = GetImage("Wash");
            }
            static ImageSource GetImage(string imageName) {
                
                
                
                
                return null;
            }
        }

        static List<Car> CreateCars() {
            List<Car> cars = new List<Car>();
            cars.Add(new Car(CarBrand.MercedesBenz, "Mercedes-Benz Slk350"));
            cars.Add(new Car(CarBrand.Chevrolet, "Chevrolet Camaro"));
            cars.Add(new Car(CarBrand.Audi, "Audi S8"));
            cars.Add(new Car(CarBrand.Lexus, "Lexus IS 350"));
            cars.Add(new Car(CarBrand.Toyota, "Toyota Tundra 4x4 Reg Cab"));
            cars.Add(new Car(CarBrand.Nissan, "Nissan Murano"));
            cars.Add(new Car(CarBrand.Ford, "Ford Mustang GT Coupe"));
            return cars;
        }
        static List<CarAppointment> CreateAppointment(DateTime startDate) {
            string recurrenceFormat = @"<RecurrenceInfo Start=""{0}"" End=""{1}"" WeekDays=""{2}"" Month=""{3}"" OccurrenceCount=""{4}"" Range=""{5}"" Type=""{6}"" Id=""{7}""/>";
            string changedOccurrenceFormat = @"<RecurrenceInfo Id=""{0}"" Index=""{1}""/>";
            List<CarAppointment> appts = new List<CarAppointment>();

            CarAppointment apt1 = new CarAppointment();
            apt1.EventType = (int)AppointmentType.Normal;
            apt1.StartTime = startDate.Add(new TimeSpan(3, 08, 15, 00));
            apt1.EndTime = startDate.Add(new TimeSpan(5, 16, 40, 00));
            apt1.Description = CarDescription.Repair;
            apt1.Label = CarLabel.Vacation;
            apt1.Location = CarLocation.ServiceCenter;
            apt1.CarId = CarBrand.Audi;
            apt1.Status = CarStatus.Tentative;
            apt1.Subject = "Repair";
            apt1.Price = 90;
            apt1.Image = CarImages.Maintance;
            appts.Add(apt1);

            CarAppointment apt2 = new CarAppointment();
            apt2.EventType = (int)AppointmentType.Normal;
            apt2.StartTime = startDate.Add(new TimeSpan(10, 00, 00));
            apt2.EndTime = startDate.Add(new TimeSpan(2, 11, 45, 00));
            apt2.Description = CarDescription.Rent;
            apt2.Label = CarLabel.MustAttend;
            apt2.Location = CarLocation.OutOfTown;
            apt2.CarId = CarBrand.Chevrolet;
            apt2.Status = CarStatus.Tentative;
            apt2.Subject = "Mrs.Black";
            apt2.Price = 5;
            apt2.Image = CarImages.Rent;
            appts.Add(apt2);

            CarAppointment apt3 = new CarAppointment();
            apt3.EventType = (int)AppointmentType.Normal;
            apt3.StartTime = startDate.Add(new TimeSpan(1, 13, 00, 00));
            apt3.EndTime = startDate.Add(new TimeSpan(1, 14, 30, 00));
            apt3.Description = CarDescription.Rent;
            apt3.Label = CarLabel.Personal;
            apt3.Location = CarLocation.OutOfTown;
            apt3.CarId = CarBrand.Chevrolet;
            apt3.Status = CarStatus.OutOfOffice;
            apt3.Subject = "Mrs.Black";
            apt3.Price = 6;
            apt3.Image = CarImages.Rent;
            appts.Add(apt3);

            CarAppointment apt4 = new CarAppointment();
            apt4.EventType = (int)AppointmentType.Normal;
            apt4.StartTime = startDate.Add(new TimeSpan(2, 15, 30, 00));
            apt4.EndTime = startDate.Add(new TimeSpan(3, 14, 00, 00));
            apt4.Description = CarDescription.Rent;
            apt4.Label = CarLabel.Personal;
            apt4.Location = CarLocation.OutOfTown;
            apt4.CarId = CarBrand.Chevrolet;
            apt4.Status = CarStatus.OutOfOffice;
            apt4.Subject = "Mrs.Black";
            apt4.Price = 4;
            apt4.Image = CarImages.Rent;
            appts.Add(apt4);

            CarAppointment apt5 = new CarAppointment();
            apt5.EventType = (int)AppointmentType.Pattern;
            apt5.StartTime = startDate.Add(new TimeSpan(07, 30, 00));
            apt5.EndTime = startDate.Add(new TimeSpan(08, 45, 00));
            apt5.Description = CarDescription.Wash;
            apt5.Label = CarLabel.Business;
            apt5.Location = CarLocation.Garage;
            apt5.CarId = CarBrand.Chevrolet;
            apt5.Status = CarStatus.Tentative;
            apt5.Price = 4;
            apt5.Image = CarImages.Wash;
            apt5.Subject = "Wash";


            RecurrenceInfo recInfo = new RecurrenceInfo();
            recInfo.Start = startDate.Add(new TimeSpan(07, 00, 00));
            recInfo.End = startDate.Add(new TimeSpan(44, 01, 00, 00));
            recInfo.Type = RecurrenceType.Weekly;
            recInfo.WeekDays = WeekDays.Monday | WeekDays.Wednesday | WeekDays.Friday;
            recInfo.Month = 12;
            recInfo.OccurrenceCount = 1;
            recInfo.Range = RecurrenceRange.EndByDate;
            apt5.RecurrenceInfo = String.Format(
                CultureInfo.InvariantCulture, 
                recurrenceFormat, 
                startDate.Add(new TimeSpan(07, 00, 00)), 
                startDate.Add(new TimeSpan(44, 01, 00, 00)), 
                (int)recInfo.WeekDays, 
                recInfo.Month, 
                recInfo.OccurrenceCount, 
                (int)recInfo.Range, 
                (int)recInfo.Type, 
                recInfo.Id.ToString());
            appts.Add(apt5);

            CarAppointment apt6 = new CarAppointment();
            apt6.EventType = (int)AppointmentType.ChangedOccurrence;
            apt6.StartTime = startDate.Add(new TimeSpan(8, 01, 30, 00));
            apt6.EndTime = startDate.Add(new TimeSpan(8, 09, 00, 00));
            apt6.Description = CarDescription.Wash;
            apt6.Label = CarLabel.Business;
            apt6.Location = CarLocation.Garage;
            apt6.CarId = CarBrand.Chevrolet;
            apt6.Status = CarStatus.Tentative;
            apt6.Subject = "Wash";
            apt6.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo.Id.ToString(), 4);
            apt6.Price = 6;
            apt6.Image = CarImages.Wash;
            appts.Add(apt6);

            CarAppointment apt7 = new CarAppointment();
            apt7.EventType = (int)AppointmentType.Normal;
            apt7.StartTime = startDate;
            apt7.EndTime = startDate.AddDays(1);
            apt7.AllDay = true;
            apt7.Description = CarDescription.RentAllDay;
            apt7.Label = CarLabel.Personal;
            apt7.Location = CarLocation.City;
            apt7.CarId = CarBrand.MercedesBenz;
            apt7.Status = CarStatus.Busy;
            apt7.Subject = "Mr.Green";
            apt7.Price = 6;
            apt7.Image = CarImages.Rent;
            appts.Add(apt7);

            CarAppointment apt8 = new CarAppointment();
            apt8.EventType = (int)AppointmentType.Normal;
            apt8.StartTime = startDate.Add(new TimeSpan(1, 11, 05, 00));
            apt8.EndTime = startDate.Add(new TimeSpan(1, 14, 30, 00));
            apt8.Description = CarDescription.Rent;
            apt8.Label = CarLabel.Business;
            apt8.Location = CarLocation.City;
            apt8.CarId = CarBrand.MercedesBenz;
            apt8.Status = CarStatus.OutOfOffice;
            apt8.Subject = "Mr.Brown";
            apt8.Price = 8;
            apt8.Image = CarImages.Rent;
            appts.Add(apt8);

            CarAppointment apt9 = new CarAppointment();
            apt9.EventType = (int)AppointmentType.Normal;
            apt9.StartTime = startDate.Add(new TimeSpan(2, 10, 00, 00));
            apt9.EndTime = startDate.Add(new TimeSpan(4, 17, 00, 00));
            apt9.Description = CarDescription.Rent;
            apt9.Label = CarLabel.Personal;
            apt9.Location = CarLocation.City;
            apt9.CarId = CarBrand.MercedesBenz;
            apt9.Status = CarStatus.OutOfOffice;
            apt9.Subject = "Mr.White";
            apt9.Price = 10;
            apt9.Image = CarImages.Rent;
            appts.Add(apt9);

            CarAppointment apt10 = new CarAppointment();
            apt10.EventType = (int)AppointmentType.Normal;
            apt10.StartTime = startDate.Add(new TimeSpan(4, 19, 45, 00));
            apt10.EndTime = startDate.Add(new TimeSpan(4, 22, 45, 00));
            apt10.Description = CarDescription.CheckUp;
            apt10.Label = CarLabel.MustAttend;
            apt10.Location = CarLocation.ServiceCenter;
            apt10.CarId = CarBrand.MercedesBenz;
            apt10.Status = CarStatus.WorkingElsewhere;
            apt10.Subject = "Check up";
            apt10.Price = 45;
            apt10.Image = CarImages.CheckUp;
            appts.Add(apt10);

            CarAppointment apt11 = new CarAppointment();
            apt11.EventType = (int)AppointmentType.Pattern;
            apt11.StartTime = startDate.Add(new TimeSpan(-6, 16, 40, 00));
            apt11.EndTime = startDate.Add(new TimeSpan(-6, 17, 50, 00));
            apt11.Description = CarDescription.Wash;
            apt11.Label = CarLabel.Important;
            apt11.Location = CarLocation.Garage;
            apt11.CarId = CarBrand.MercedesBenz;
            apt11.Status = CarStatus.Tentative;
            apt11.Subject = "Wash";
            apt11.Image = CarImages.Wash;
            apt11.Price = 7;

            RecurrenceInfo recInfo1 = new RecurrenceInfo();
            recInfo1.Start = startDate.Add(new TimeSpan(-6, 16, 30, 00));
            recInfo1.End = startDate.Add(new TimeSpan(21, 00, 00, 00));
            recInfo1.Type = RecurrenceType.Weekly;
            recInfo1.WeekDays = WeekDays.WorkDays;
            recInfo1.Month = 12;
            recInfo1.OccurrenceCount = 1;
            recInfo1.Range = RecurrenceRange.EndByDate;
            apt11.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, recurrenceFormat, recInfo1.Start, recInfo1.End, (int)recInfo1.WeekDays, recInfo1.Month, recInfo1.OccurrenceCount, (int)recInfo1.Range, (int)recInfo1.Type, recInfo1.Id.ToString());

            appts.Add(apt11);

            CarAppointment apt12 = new CarAppointment();
            apt12.EventType = (int)AppointmentType.ChangedOccurrence;
            apt12.StartTime = startDate.Add(new TimeSpan(2, 18, 30, 00));
            apt12.EndTime = startDate.Add(new TimeSpan(2, 20, 00, 00));
            apt12.Description = CarDescription.Wash;
            apt12.Label = CarLabel.Important;
            apt12.Location = CarLocation.Garage;
            apt12.CarId = CarBrand.MercedesBenz;
            apt12.Status = CarStatus.Tentative;
            apt12.Subject = "Wash";
            apt12.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 6);
            apt12.Price = 5;
            apt12.Image = CarImages.Wash;
            appts.Add(apt12);

            CarAppointment apt13 = new CarAppointment();
            apt13.EventType = (int)AppointmentType.DeletedOccurrence;
            apt13.StartTime = startDate.Add(new TimeSpan(5, 16, 20, 00));
            apt13.EndTime = startDate.Add(new TimeSpan(5, 17, 40, 00));
            apt13.Description = CarDescription.Wash;
            apt13.Label = CarLabel.Important;
            apt13.Location = CarLocation.Garage;
            apt13.CarId = CarBrand.MercedesBenz;
            apt13.Status = CarStatus.Tentative;
            apt13.Subject = "Wash";
            apt13.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 7);
            apt13.Price = 5;
            apt13.Image = CarImages.Wash;
            appts.Add(apt13);

            CarAppointment apt14 = new CarAppointment();
            apt14.EventType = (int)AppointmentType.ChangedOccurrence;
            apt14.StartTime = startDate.Add(new TimeSpan(9, 15, 00, 00));
            apt14.EndTime = startDate.Add(new TimeSpan(9, 16, 30, 00));
            apt14.Description = CarDescription.Wash;
            apt14.Label = CarLabel.Important;
            apt14.Location = CarLocation.Garage;
            apt14.CarId = CarBrand.MercedesBenz;
            apt14.Status = CarStatus.Tentative;
            apt14.Subject = "Wash";
            apt14.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 11);
            apt14.Price = 5;
            apt14.Image = CarImages.Wash;
            appts.Add(apt14);

            CarAppointment apt15 = new CarAppointment();
            apt15.EventType = (int)AppointmentType.ChangedOccurrence;
            apt15.StartTime = startDate.Add(new TimeSpan(13, 16, 30, 00));
            apt15.EndTime = startDate.Add(new TimeSpan(13, 17, 00, 00));
            apt15.Description = CarDescription.Wash;
            apt15.Label = CarLabel.Important;
            apt15.Location = CarLocation.Garage;
            apt15.CarId = CarBrand.MercedesBenz;
            apt15.Status = CarStatus.Tentative;
            apt15.Subject = "Wash";
            apt15.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 13);
            apt15.Price = 5;
            apt15.Image = CarImages.Wash;
            appts.Add(apt15);

            CarAppointment apt16 = new CarAppointment();
            apt16.EventType = (int)AppointmentType.DeletedOccurrence;
            apt16.StartTime = startDate.Add(new TimeSpan(2, 16, 25, 00));
            apt16.EndTime = startDate.Add(new TimeSpan(2, 17, 45, 00));
            apt16.Description = CarDescription.Wash;
            apt16.Label = CarLabel.Important;
            apt16.Location = CarLocation.Garage;
            apt16.CarId = CarBrand.MercedesBenz;
            apt16.Status = CarStatus.Tentative;
            apt16.Subject = "Wash";
            apt16.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, changedOccurrenceFormat, recInfo1.Id.ToString(), 4);
            apt16.Price = 6;
            apt16.Image = CarImages.Wash;
            appts.Add(apt16);

            return appts;
        }

        public List<Car> Cars { get; private set; }
        public List<CarAppointment> Events { get; private set; }

        public CarsData() {
            Cars = CreateCars();
            Events = CreateAppointment(DateTime.Today);
        }
    }
}
