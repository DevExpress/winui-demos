using System;

namespace SchedulerDemo {
    [Flags]
    public enum WeekDays {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        WorkDays = 62,
        Saturday = 64,
        WeekendDays = 65,
        EveryDay = 127
    }
    public enum RecurrenceType {
        Daily = 0,
        Weekly = 1,
        Monthly = 2,
        Yearly = 3,
        Minutely = 4,
        Hourly = 5
    }
    public enum RecurrenceRange {
        NoEndDate = 0,
        OccurrenceCount = 1,
        EndByDate = 2
    }
    internal class RecurrenceInfo {
        public DateTime Start { get; internal set; }
        public DateTime End { get; internal set; }
        public RecurrenceType Type { get; internal set; }
        public WeekDays WeekDays { get; internal set; }
        public int Month { get; internal set; }
        public int OccurrenceCount { get; internal set; }
        public RecurrenceRange Range { get; internal set; }
        public int Id { get; internal set; }
        public bool AllDay { get; internal set; }
        public int DayNumber { get; internal set; }

        public override string ToString() {
            return "Default";
        }
    }
}