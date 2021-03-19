using DevExpress.Mvvm;
using DevExpress.WinUI.Scheduler;
using DevExpress.WinUI.Scheduler.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Color = Windows.UI.Color;

namespace SchedulerDemo {
    public class ResourceIdToColorConverter : IValueConverter {
        Color[] colors = new Color[] { Colors.Red, Colors.Green, Colors.Blue, Colors.Magenta, Colors.Yellow, Colors.Cyan, Colors.Fuchsia, Colors.DarkOrchid, Colors.Aquamarine, Colors.Black, Colors.Gray };
        public object Convert(object value, Type targetType, object parameter, string language) {
            ResourceIdCollection c = (ResourceIdCollection)value;
            if (c != null && c.Count > 0) {
                long id = (long)c[0];
                if (id >= 0) {
                    return new SolidColorBrush(colors[id]);
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
    public class SchedulerDemoViewModel : BindableBase {
        public virtual DateTime Start { get; set; }
        public IEnumerable<WorkCalendar> Calendars { get { return WorkData.Calendars; } }

        public IEnumerable<WorkAppointment> Appointments { get; private set; }
        public IEnumerable<WorkLabel> Labels { get { return WorkData.Labels; } }

        public SchedulerGroupType GroupType {
            get { return GetProperty<SchedulerGroupType>(); }
            set { SetProperty(value); }
        }
        public AppointmentsDisplayMode AppointmentsDisplayMode {
            get { return GetProperty<AppointmentsDisplayMode>(); }
            set { SetProperty(value); }
        }

        public SchedulerDemoViewModel() {
            GroupType = SchedulerGroupType.Resource;
            AppointmentsDisplayMode = AppointmentsDisplayMode.ShowRelated;
            Init();
        }

        void Init() {
            Start = WorkData.TodayStart;
            Appointments = new ObservableCollection<WorkAppointment>(WorkData.Appointments);
        }
    }
}