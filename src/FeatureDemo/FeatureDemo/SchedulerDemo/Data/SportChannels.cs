using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.WinUI.Scheduler;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Color = Windows.UI.Color;

namespace SchedulerDemo {
    public class SportEvent {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Caption { get; set; }
        public int ChannelId { get; set; }
        public int SportId { get; set; }
        public int? Type { get; set; }
        public bool AllDay { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public string CustomText { get { return String.Format("[{0}] - {1}", Location, Caption); } set { } }
    }
    public class SportChannel {
        public int Id { get; set; }
        public string Caption { get; set; }
        public bool HasNews { get { return !NewsTimeRange.IsZero; } }
        public bool IsVisible { get; set; }
        public TimeSpanRange NewsTimeRange { get; set; }
    }
    public class SportGroup {
        public int Id { get; set; }
        public string Caption { get; set; }
        public Color Color { get; set; }
        public ImageSource Image { get; set; }
    }
    public class SportChannelsData {
        public static readonly SportGroup[] SportGroups = {
            new SportGroup() { Id = 0, Caption = "SportNews", Color = Colors.White },
            new SportGroup() { Id = 1, Caption = "Basketball", Color = ColorExtensions.FromRgb(0xFF, 0xC2, 0xBE), Image = CreateSportGroupImageSource("Basketball") },
            new SportGroup() { Id = 2, Caption = "Boxing", Color = ColorExtensions.FromRgb(0xA8, 0xD5, 0xFF), Image = CreateSportGroupImageSource("Boxing") },
            new SportGroup() { Id = 3, Caption = "Tennis", Color = ColorExtensions.FromRgb(0xC1, 0xF4, 0x9C), Image = CreateSportGroupImageSource("Tennis") },
            new SportGroup() { Id = 4, Caption = "Soccer", Color = ColorExtensions.FromRgb(0xF3, 0xE4, 0xC7), Image = CreateSportGroupImageSource("Soccer") },
            new SportGroup() { Id = 5, Caption = "Artistic Gymnastics", Color = ColorExtensions.FromRgb(0xF4, 0xCE, 0x93), Image = CreateSportGroupImageSource("Artistic Gymnastics") },
            new SportGroup() { Id = 6, Caption = "Canoe", Color = ColorExtensions.FromRgb(0xC7, 0xF4, 0xFF), Image = CreateSportGroupImageSource("Canoe") },
            new SportGroup() { Id = 7, Caption = "Kayak", Color = ColorExtensions.FromRgb(0xCF, 0xDB, 0x98), Image = CreateSportGroupImageSource("Kayak") },
            new SportGroup() { Id = 8, Caption = "Sailing", Color = ColorExtensions.FromRgb(0xE0, 0xCF, 0xE9), Image = CreateSportGroupImageSource("Sailing") },
            new SportGroup() { Id = 9, Caption = "Swimming", Color = ColorExtensions.FromRgb(0x8D, 0xE9, 0xDF), Image = CreateSportGroupImageSource("Swimming") },
            new SportGroup() { Id = 10, Caption = "Shooting", Color = ColorExtensions.FromRgb(0xFF, 0xF7, 0xA5), Image = CreateSportGroupImageSource("Shooting") },
        };

        static ImageSource CreateSportGroupImageSource(string caption) {
            
            
            
            
            return null;
        }
        static ObservableCollection<SportEvent> GenerateEvents(int dayCount) {
            ObservableCollection<SportEvent> result = new ObservableCollection<SportEvent>();
            int aptId = 0;
            
            foreach (SportChannel ch in sportChannels) {
                DateTime start = DateTime.Today - TimeSpan.FromDays(dayCount);
                DateTime end = DateTime.Today + TimeSpan.FromDays(dayCount);
                if (ch.HasNews)
                    result.Add(GetRandomRecurrenceSportEvent(aptId++, start, ch, (dayCount * 2) - 1));
                for (DateTime i = start; i < end; i += TimeSpan.FromDays(1)) {
                    if (i.DayOfWeek == DateTime.Today.DayOfWeek && ch.Id == 0) {
                        result.Add(GetRandomSportEvent(aptId++, i, i + TimeSpan.FromDays(1), ch.Id));
                        continue;
                    }
                    DateTime airtimeStart = new DateTime(i.Year, i.Month, i.Day, 6, 0, 0);
                    DateTime airtimeEnd = new DateTime(i.Year, i.Month, i.Day, 21, 0, 0);
                    DateTime aptStartTime = airtimeStart;
                    while (aptStartTime < airtimeEnd) {
                        DateTime aptEndTime;
                        TimeSpan duration = TVProgramDurations[rnd.Next(0, TVProgramDurations.Length - 1)];
                        TimeSpanRange newsTimeRange = ch.NewsTimeRange;
                        if (ch.HasNews) {
                            long timeBeforeNews = (newsTimeRange.Start - aptStartTime.TimeOfDay).Ticks;
                            long minTicks = TimeSpan.FromMinutes(30).Ticks;
                            if (timeBeforeNews > 0 && (timeBeforeNews < minTicks || (timeBeforeNews - duration.Ticks) < minTicks)) {
                                    aptEndTime = aptStartTime.Date + newsTimeRange.Start;
                                result.Add(GetRandomSportEvent(aptId++, aptStartTime, aptEndTime, ch.Id));
                                aptStartTime = aptEndTime.Date + newsTimeRange.End;
                                continue;
                            }
                        }
                        aptEndTime = aptStartTime + duration;
                        result.Add(GetRandomSportEvent(aptId++, aptStartTime, aptEndTime, ch.Id));
                        aptStartTime = aptEndTime;
                    }
                }
            }
            return result;
        }
        static string recurrenceInfoFormat = @"<RecurrenceInfo Start=""{0}"" End=""{1}"" WeekDays=""{2}"" OccurrenceCount=""{3}"" Range=""{4}"" Type=""{5}"" Id=""{6}""/>";
        
        static SportEvent GetRandomRecurrenceSportEvent(int id, DateTime startDate, SportChannel channel, int dayCount) {
            SportEvent pattern = new SportEvent();
            pattern.Id = id;
            TimeSpanRange newsTimeRange = channel.NewsTimeRange;
            pattern.StartTime = startDate + newsTimeRange.Start;
            pattern.EndTime = startDate + newsTimeRange.End;
            pattern.SportId = 0;
            pattern.ChannelId = channel.Id;
            pattern.Caption = "Sport News";
            pattern.Location = "New York City, USA";
            pattern.Type = (int)AppointmentType.Pattern;
            RecurrenceInfo recInfo = new RecurrenceInfo();
            recInfo.Id = id;
            recInfo.Start = pattern.StartTime;
            recInfo.End = pattern.EndTime.AddDays(dayCount);
            recInfo.WeekDays = WeekDays.WorkDays;
            recInfo.Range = RecurrenceRange.EndByDate;
            recInfo.Type = RecurrenceType.Daily;
            pattern.RecurrenceInfo = String.Format(CultureInfo.InvariantCulture, recurrenceInfoFormat,
                recInfo.Start, recInfo.End, (int)recInfo.WeekDays, recInfo.OccurrenceCount, (int)recInfo.Range, (int)recInfo.Type, recInfo.Id.ToString());
            
            return pattern;
        }
        static SportEvent GetRandomSportEvent(int id, DateTime start, DateTime end, int channelId) {
            var res = new SportEvent();
            res.Id = id;
            res.StartTime = start;
            res.EndTime = end;
            res.SportId = rnd.Next(1, 10);
            res.ChannelId = channelId;
            res.Caption = GetRandomString(GetEvents(res.SportId));
            res.Location = GetRandomString(GetLocations(res.SportId));
            res.Description = GetRandomString(GetDescriptions(res.SportId));
            res.Type = 0;
            return res;
        }
        static string GetRandomString(string[] events) {
            return events[rnd.Next(0, events.Count())];
        }
        static string[] GetEvents(int sportId) {
            switch(sportId) {
                case 1: return basketballEvents;
                case 2: return boxingEvents;
                case 3: return tennisEvents;
                case 4: return soccerEvents;
                case 5: return artisticGymnasticsEvents;
                case 6: return canoeEvents;
                case 7: return kayakEvents;
                case 8: return sailingEvents;
                case 9: return swimmingEvents;
                case 10: return shootingEvents;
            }
            return null;
        }
        static string[] GetLocations(int sportId) {
            switch(sportId) {
                case 1: return basketballLocations;
                case 2: return boxingLocations;
                case 3: return tennisLocations;
                case 4: return soccerLocations;
                case 5: return artisticGymnasticsLocations;
                case 6: return canoeLocations;
                case 7: return kayakLocations;
                case 8: return sailingLocations;
                case 9: return swimmingLocations;
                case 10:return shootingLocations;
            }
            return null;
        }
        static string[] GetDescriptions(int sportId) {
            switch(sportId) {
                case 1: return basketballDescriptions;
                case 2: return boxingDescriptions;
                case 3: return tennisDescriptions;
                case 4: return soccerDescriptions;
                case 5: return artisticGymnasticsDescriptions;
                case 6: return canoeDescriptions;
                case 7: return kayakDescriptions;
                case 8: return sailingDescriptions;
                case 9: return swimmingDescriptions;
                case 10: return shootingDescriptions;
            }
            return null;
        }
        static Random rnd = new Random();

        static TimeSpanRange GetRandomNewsTimeRange(TimeSpan start) {
            return new TimeSpanRange(start, start + TVProgramDurations[rnd.Next(0, 3)]);
        }
        #region Sample Data
        static readonly SportChannel[] sportChannels;
        static readonly TimeSpan[] TVProgramDurations;

        static SportChannelsData() {
            int step = 10;
            int start = 30;
            int stop = 180;
            int count = (stop - start) / step;
            TVProgramDurations = new TimeSpan[count];
            TimeSpan duration = TimeSpan.FromMinutes(start);
            TimeSpan durationStep = TimeSpan.FromMinutes(step);
            for(int i = 0; i < count; i++) {
                TVProgramDurations[i] = duration;
                duration = duration + durationStep;
            }
            sportChannels = new SportChannel[] {
                    new SportChannel() { Id = 0, Caption = "SPORT TV 1", NewsTimeRange = GetRandomNewsTimeRange(TimeSpan.FromHours(11)) },
                    new SportChannel() { Id = 1, Caption = "SPORT TV 2", NewsTimeRange = GetRandomNewsTimeRange(TimeSpan.FromHours(19)) },
                    new SportChannel() { Id = 2, Caption = "SPORT TV 3" },
                    new SportChannel() { Id = 3, Caption = "SPORT TV 4" },
                    new SportChannel() { Id = 4, Caption = "TV 5" },
                    new SportChannel() { Id = 5, Caption = "TV 6" },
                    new SportChannel() { Id = 6, Caption = "TV 7" },
                    new SportChannel() { Id = 7, Caption = "TV 8" },
                };
        }

        static readonly string[] basketballEvents = {
            "Basketball First Group Phase - Men",
            "Basketball First Group Phase - Women"
        };
        static readonly string[] boxingEvents = {
            "Boxing - ((Showtime) Jeff Lacy (20-0) vs. Joe Calzaghe (39-0) (IBF, IBO and WBO super middleweight belts)",
            "Boxing - ((WBC and WBO lightweight belts) (PPV) Carlos Hernandez vs. Bobby Pacquiao",
            "Boxing - (Danilo Haussler (25-3) vs. TBA",
            "Boxing - (PPV) Antonio Tarver (23-3) vs. Roy Jones (49-3) (IBO light heavyweight belt)"
        };
        static readonly string[] tennisEvents = {
            "Tennis - BNP Paribas Masters",
            "Tennis - Tennis Masters Hamburg",
            "Tennis - Tennis Masters Monte Carlo",
            "Tennis - Australian Open"
        };
        static readonly string[] soccerEvents = {
            "Soccer Quarter final - *Women's Quarterfinal - Women",
            "Soccer 1st Round - *Men's Preliminaries - Men",
            "Soccer places 3/4 - *Women's Bronze Medal Match - Women",
        };
        static readonly string[] artisticGymnasticsEvents = {
            "Artistic Gymnastics - Vault - Women - Final",
            "Artistic Gymnastics - Floor Exercise - Men - Final",
        };
        static readonly string[] canoeEvents = {
            "Canoe - Slalom C2 - Men - Heats",
            "Canoe - Slalom C1 - Men - Heats",
        };
        static readonly string[] kayakEvents = {
            "Kayak - Slalom K2 - Men - Heats",
            "Kayak - Slalom K1 - Women - Semi-finals",
        };
        static readonly string[] sailingEvents = {
            "Sailing - Finn - Race 1",
            "Sailing - Women's Mistral - Race 01",
        };
        static readonly string[] swimmingEvents = {
            "Swimming - Men's 100m Breaststroke - Heat 2",
            "Swimming - Women's 400m Individual Medley - Heat 2",
            "Swimming - Women's 100m Butterfly - Heat 1",
        };
        static readonly string[] shootingEvents = {
            "Shooting - Men's 50m Pistol Qualification",
            "Shooting - Women's 25m Pistol Final",
            "Shooting - Men's 10m Air Pistol Qualification",
        };
        static readonly string[] basketballLocations = {
            "Brooklyn, NY, USA",
            "Charlottesville, VA, USA"
        };
        static readonly string[] basketballDescriptions = {
            "Baseline Leaners vs Rimshots",
            "Lady Mustangs vs Houston Rockettes"
        };
        static readonly string[] boxingLocations = {
            "Hamburg, Germany",
            "Chattanooga, Tennessee, USA",
            "Salt Lake City, Utah, USA",
            "Albuquerque, New Mexico, USA",
        };
        static readonly string[] boxingDescriptions = {
            "IBF, IBO and WBO super middleweight belts",
            "WBC and WBO lightweight belts",
            "WBC super middleweight belt",
            "IBO light heavyweight belt",
        };
        static readonly string[] tennisLocations = {
            "Paris, France",
            "Hamburg, Germany",
            "Monte Carlo, Monaco",
            "Melbourne, Victoria, Australia"
        };
        static readonly string[] tennisDescriptions = {
            "One Hit Wonders vs Big Hitters",
            "Love Hurts vs Queens of the Court",
            "Alley Gators vs String Nation",
            "Tennis the Menace vs Heavy Duty Felt"
        };
        static readonly string[] soccerLocations = {
            "Philadelphia, PA, USA",
            "San Francisco, CA, USA",
            "New York City, USA",
        };
        static readonly string[] soccerDescriptions = {
            "Cheetahs vs Lady Eagles",
            "Amigos vs Terminators",
            "Spitting Cobras vs Red Dragons",
        };
        static readonly string[] artisticGymnasticsLocations = {
            "Hong Kong Open Championships",
            "World Artistic Gymnastics Championships Glasgo, UK",
        };
        static readonly string[] artisticGymnasticsDescriptions = {
            "Complicated vaults in different body positions, such as tucked, piked or stretched.",
            "Express the personalities through their music choice and choreography.",
        };
        static readonly string[] canoeLocations = {
            "Račice, Czech republic",
            "Milan, Italy",
        };
        static readonly string[] canoeDescriptions = {
            "Row hard or row home.",
            "Recovery is not an option.",
        };
        static readonly string[] kayakLocations = {
            "Puerto Deportivo el Puntal, Spain",
            "Danson Lake, Bexleyheath, UK",
        };
        static readonly string[] kayakDescriptions = {
            "After the Gold Rush vs Humpbacks",
            "Drag-On vs Shark Week",
        };
        static readonly string[] sailingLocations = {
            "Marseille, France",
            "Savannah, Georgia, United States",
        };
        static readonly string[] sailingDescriptions = {
            "Grab life by the tail, set sail",
            "Do or do not.  There is no try.",
        };
        static readonly string[] swimmingLocations = {
            "Budapest, Hungary",
            "Kazan, Russia",
            "Barcelona, Spain",
        };
        static readonly string[] swimmingDescriptions = {
            "Dashing Dolphins and Krazy Krakens",
            "Golden Gators and Stingrays",
            "Wave Makers and Murray Marlins",
        };
        static readonly string[] shootingLocations = {
            "Munich, Germany",
            "Zagreb, Croatia",
            "Lahti, Finland",
        };
        static readonly string[] shootingDescriptions = {
            "Shoot over a distance of 50 meters / 54.68 yards in standing position, using a 5.6 millimeters / 0.22 inches caliber pistol with no weight restriction.",
            "Shoot over a distance of 25 meters / 27.34 yards in standing position, using a 5.6 millimeters / 0.22 inches caliber pistol with a maximum weight of 1.4 kilograms / 3.09 libbers.",
            "Shoot over a distance of 10 meters / 10.94 yards in standing position, using a 4.5 millimeters / 0.177 inches caliber air pistol with a maximum weight of 1.5 kilograms / 3,31 libbers.",
        };
        #endregion

        public SportChannelsData(int dayCount) {
            Groups = new ObservableCollection<SportGroup>(SportGroups);
            Channels = new ObservableCollection<SportChannel>(sportChannels);
            Events = GenerateEvents(dayCount);
        }

        public ObservableCollection<SportGroup> Groups { get; private set; }
        public ObservableCollection<SportChannel> Channels { get; private set; }
        public ObservableCollection<SportEvent> Events { get; private set; }


    }
}
