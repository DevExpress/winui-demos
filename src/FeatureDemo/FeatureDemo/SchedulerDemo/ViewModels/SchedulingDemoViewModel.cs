using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SchedulerDemo {
    public class SchedulerDemoViewModel : BindableBase {
        public virtual DateTime Start { get; set; }
        public IEnumerable<WorkCalendar> Calendars { get { return WorkData.Calendars; } }

        public IEnumerable<WorkAppointment> Appointments { get; private set; }
        public IEnumerable<WorkLabel> Labels { get { return WorkData.Labels; } }

        public SchedulerDemoViewModel() {
            Init();
        }

        void Init() {
            Start = WorkData.TodayStart;
            Appointments = new ObservableCollection<WorkAppointment>(WorkData.Appointments);
        }
    }
}