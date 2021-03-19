using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.WinUI.Editors;
using DevExpress.WinUI.Editors.Internal.Calendar;
using FeatureDemo.Data;

namespace EditorsDemo {
    public class DateNavigatorSpecialDatesViewModel : DevExpress.Mvvm.BindableBase {
        #region Props
        public CustomDatesCollection SpecialDates { get; private set; }
        public DateTime DisplayDate { get; private set; }
        public DateTime DisplayDateStart { get; private set; }
        public DateTime DisplayDateEnd { get; private set; }
        #endregion

        public DateNavigatorSpecialDatesViewModel() {
            SpecialDates = new CustomDatesCollection();
            var dataStorage = new DataStorage();
            SpecialDates.AddRange(dataStorage.Appointments.Select<Appointment, ICustomDateEvent>(e => new CustomDateEvent(e.Date, e)).ToList());
            IEnumerable<DateTime> dates = dataStorage.Appointments.Select(e => e.Date).OrderBy(e => e);
            DisplayDate = SpecialDates[0].Date;
            DisplayDateStart = dates.First().AddMonths(-2);
            DisplayDateEnd = dates.Last().AddMonths(2);
        }
    }
}