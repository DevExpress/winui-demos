using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Media;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Scheduler;
using Microsoft.UI;
using Color = Windows.UI.Color;

namespace SchedulerDemo {
    static class ColorExtensions {
        public static Color FromRgb(byte r, byte g, byte b) {
            return Color.FromArgb(255, r, g, b);
        }
    }
    public class GymData {

        static class TrainingKind {
            public const int Bodybuilding = 0;
            public const int Yoga = 1;
            public const int Zumba = 2;
            public const int Boxing = 3;
            public const int Cxworx = 4;
            public const int BodyPump = 5;
            public const int Pilates = 6;
            public const int Abl = 7;
            public const int Tabata = 8;
            public const int PrivateTraining = 9;
        }

        static Random rnd = new Random();

        static readonly Training[] kindOfTraining = {
            new Training() { Id = TrainingKind.Bodybuilding, Name = "Bodybuilding", Color = ColorExtensions.FromRgb(0xC1, 0xF4, 0x9C)},
            new Training() { Id = TrainingKind.Yoga, Name = "YOGA" , Color = ColorExtensions.FromRgb(0xFF, 0xC2, 0xBE),
                Description = "Yoga is a group of physical, mental, and spiritual practices or disciplines."
            },
            new Training() { Id = TrainingKind.Zumba, Name = "Zumba", Color = ColorExtensions.FromRgb(0xA8, 0xD5, 0xFF),
                Description = "Zumba involves dance and aerobic movements performed to energetic music."
            },
            new Training() { Id = TrainingKind.Boxing, Name = "Boxing", Color = ColorExtensions.FromRgb(0x8D, 0xE9, 0xDF),
                Description = "Boxing is a combat sport in which two people, wearing protective gloves, throw punches at each other for a predetermined set of time in a boxing ring."}
            ,
            new Training() { Id = TrainingKind.Cxworx, Name = "CXWORX", Color = ColorExtensions.FromRgb(0xF3, 0xE4, 0xC7),
                Description = "Exercising muscles around the core."
            },
            new Training() { Id = TrainingKind.BodyPump, Name = "BodyPump", Color = ColorExtensions.FromRgb(0xF4, 0xCE, 0x93),
                Description = "BodyPump is a barbell workout for anyone looking to get lean, toned and fit – fast."
            },
            new Training() { Id = TrainingKind.Pilates, Name = "Pilates", Color = ColorExtensions.FromRgb(0xC7, 0xF4, 0xFF),
                Description = "Pilates classes build strength, flexibility and lean muscle tone with an emphasis on lengthening the body and aligning the spine."
            },
            new Training() { Id = TrainingKind.Abl, Name = "ABL", Color = ColorExtensions.FromRgb(0xCF, 0xDB, 0x98),
                Description = "ABL program is specially designed for women. Strength training aims to develop the muscles of the abdomen, buttocks and legs."
            },
            new Training() { Id = TrainingKind.Tabata, Name = "Tabata", Color = ColorExtensions.FromRgb(0xE0, 0xCF, 0xE9),
                Description = "Tabata is a form of high intensity interval training designed to get your heart rate up in that very hard anaerobic zone for short periods of time."
            },
            new Training() { Id = TrainingKind.PrivateTraining, Name = "Private Training", Color = ColorExtensions.FromRgb(0xFF, 0xF7, 0xA5) },
        };
        static readonly Trainer[] trainerNames = {
            new Trainer() { Id = TrainingKind.Bodybuilding, Name = "Mark Oliver" },
            new Trainer() { Id = TrainingKind.Yoga, Name = "Greta Sims" },
            new Trainer() { Id = TrainingKind.Zumba, Name = "Cindy Stanwick" },
            new Trainer() { Id = TrainingKind.Boxing, Name = "Robert Reagan" },
            new Trainer() { Id = TrainingKind.Cxworx, Name = "Andrew Fuller" },
            new Trainer() { Id = TrainingKind.BodyPump, Name = "Sammy Hill" },
            new Trainer() { Id = TrainingKind.Pilates, Name = "Laura Callahan" },
            new Trainer() { Id = TrainingKind.Abl, Name = "Terry Bradley" },
            new Trainer() { Id = TrainingKind.Tabata, Name = "Sandy Bright" },
            new Trainer() { Id = TrainingKind.PrivateTraining, Name = "Lucas Smith" },
        };

        static ObservableCollection<GymEvent> GenerateEvents(int dayCount, DateTime startDate) {
            ObservableCollection<GymEvent> result = GetEvents(0, startDate, 14);
            result.Add(GetBodybuildingEvent(result.Count, startDate, 14));
            return result;
        }
        static string recurrenceInfoFormat = @"<RecurrenceInfo Start=""{0}"" End=""{1}"" WeekDays=""{2}"" OccurrenceCount=""{3}"" Range=""{4}"" Type=""{5}"" Id=""{6}""/>";

        static GymEvent GetBodybuildingEvent(int id, DateTime date, int dayCount) {
            DateTime firstDate = new DateTime(date.Year, date.Month, date.Day);
            Training training = kindOfTraining[TrainingKind.Bodybuilding];
            GymEvent pattern = GetGymEvent(id, firstDate.AddHours(8), firstDate.AddHours(22), training.Id, training.Id);
            pattern.AllDay = true;
            pattern.Type = (int)AppointmentType.Pattern;
            RecurrenceInfo recInfo = new RecurrenceInfo();
            recInfo.Start = pattern.StartTime;
            recInfo.AllDay = true;
            recInfo.End = pattern.EndTime.AddDays(dayCount);
            recInfo.WeekDays = WeekDays.EveryDay;
            recInfo.Range = RecurrenceRange.EndByDate;
            recInfo.Type = RecurrenceType.Daily;
            pattern.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, recurrenceInfoFormat,
                recInfo.Start, recInfo.End, (int)recInfo.WeekDays, recInfo.OccurrenceCount, (int)recInfo.Range, (int)recInfo.Type, recInfo.Id.ToString());
            return pattern;
        }
        static ObservableCollection<GymEvent> GetEvents(int id, DateTime date, int dayCount) {
            ObservableCollection<GymEvent> result = new ObservableCollection<GymEvent>();
            DateTime firstDate = new DateTime(date.Year, date.Month, date.Day);
            DateTime lastDate = firstDate.AddDays(dayCount);

            for (DateTime dateTime = firstDate; dateTime < lastDate; dateTime += TimeSpan.FromDays(1)) {
                DateTime startDate = dateTime.AddHours(rnd.Next(9, 12));
                DateTime endDate = dateTime.AddHours(22);

                int trainingId = rnd.Next(1, kindOfTraining.Length - 1);

                for (DateTime startTime = startDate; startTime < endDate; startTime += new TimeSpan(rnd.Next(1, 4), 0, 0)) {
                    GymEvent gymEvent = GetGymEvent(id, startTime, startTime.AddHours(1), trainingId, trainingId);
                    result.Add(gymEvent);
                    id++;
                    trainingId++;
                    if (trainingId >= kindOfTraining.Length)
                        trainingId = 1;
                }
            }
            return result;
        }

        static GymEvent GetGymEvent(int id, DateTime start, DateTime end, int trainingId, int location) {
            Training training = kindOfTraining[trainingId];
            var res = new GymEvent();
            res.Id = id;
            res.StartTime = start;
            res.EndTime = end;
            res.TrainerId = trainingId;
            res.TrainingId = trainingId;
            res.Caption = training.Name;
            res.Description = training.Description;
            res.Room = (location + 1).ToString();
            res.Type = 0;
            return res;
        }

        public GymData(int dayCount) {
            Trainers = new ObservableCollection<Trainer>(trainerNames);
            Trainings = new ObservableCollection<Training>(kindOfTraining);
            GymEvents = GenerateEvents(dayCount, DateTime.Today);
        }

        public ObservableCollection<Trainer> Trainers { get; private set; }
        public ObservableCollection<Training> Trainings { get; private set; }
        public ObservableCollection<GymEvent> GymEvents { get; private set; }
    }

    public class Training {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public string Description { get; set; }
    }

    public class Trainer {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GymEvent {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Caption { get; set; }
        public int TrainerId { get; set; }
        public int TrainingId { get; set; }
        public int? Type { get; set; }
        public bool AllDay { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
    }
}
