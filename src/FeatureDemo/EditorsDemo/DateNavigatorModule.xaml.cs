using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EditorsDemo {
    public sealed partial class DateNavigatorModule : DemoModuleView {
        public DateNavigatorModule() {
            this.InitializeComponent();
            dateNavigator.CalendarCulture = Cultures.First();
            dateNavigator.MinValue = DateTime.Now.AddYears(-2);
            dateNavigator.MaxValue = DateTime.Now.AddYears(2);
            dateNavigator.SelectedDate = DateTime.Now.AddDays(-2);
        }
        public IEnumerable<CultureInfo> Cultures { get; } = CulturesProvider.GetCultures();
        public IEnumerable<CalendarSelectionMode> CalendarSelectionMode { get; } = Enum.GetValues(typeof(CalendarSelectionMode)).Cast<CalendarSelectionMode>();
        public IEnumerable<FirstDayOfWeek> FirstDayOfWeek { get; } = Enum.GetValues(typeof(FirstDayOfWeek)).Cast<FirstDayOfWeek>();
    }

    static class CulturesProvider {
        public static IEnumerable<CultureInfo> GetCultures() {
            return new List<CultureInfo> {
                        new CultureInfo("en"),
                        new CultureInfo("ar"),
                        new CultureInfo("af"),
                        new CultureInfo("sq"),
                        new CultureInfo("am"),
                        new CultureInfo("hy"),
                        new CultureInfo("as"),
                        new CultureInfo("az"),
                        new CultureInfo("eu"),
                        new CultureInfo("be"),
                        new CultureInfo("bn"),
                        new CultureInfo("bs"),
                        new CultureInfo("bg"),
                        new CultureInfo("ca"),
                        new CultureInfo("zh"),
                        new CultureInfo("hr"),
                        new CultureInfo("cs"),
                        new CultureInfo("da"),
                        new CultureInfo("prs"),
                        new CultureInfo("nl"),
                        new CultureInfo("et"),
                        new CultureInfo("fil"),
                        new CultureInfo("fi"),
                        new CultureInfo("fr"),
                        new CultureInfo("gl"),
                        new CultureInfo("ka"),
                        new CultureInfo("de"),
                        new CultureInfo("el"),
                        new CultureInfo("gu"),
                        new CultureInfo("ha"),
                        new CultureInfo( "he"),
                        new CultureInfo("hi"),
                        new CultureInfo("hu"),
                        new CultureInfo("is"),
                        new CultureInfo("id"),
                        new CultureInfo("ga"),
                        new CultureInfo("xh"),
                        new CultureInfo("zu"),
                        new CultureInfo("it"),
                        new CultureInfo("ja"),
                        new CultureInfo("kn"),
                        new CultureInfo("kk"),
                        new CultureInfo("km"),
                        new CultureInfo("rw"),
                        new CultureInfo("sw"),
                        new CultureInfo("kok"),
                        new CultureInfo("ko"),
                        new CultureInfo("lo"),
                        new CultureInfo("lv"),
                        new CultureInfo("lt"),
                        new CultureInfo("lb"),
                        new CultureInfo("mk"),
                        new CultureInfo("ms"),
                        new CultureInfo("ml"),
                        new CultureInfo("mt"),
                        new CultureInfo("mi"),
                        new CultureInfo("mr"),
                        new CultureInfo("ne"),
                        new CultureInfo("nb"),
                        new CultureInfo("or"),
                        new CultureInfo("fa"),
                        new CultureInfo("pl"),
                        new CultureInfo("pt"),
                        new CultureInfo("pa"),
                        new CultureInfo("quz"),
                        new CultureInfo("ro"),
                        new CultureInfo("ru"),
                        new CultureInfo("sr"),
                        new CultureInfo("nso"),
                        new CultureInfo("tn"),
                        new CultureInfo("si"),
                        new CultureInfo("sk"),
                        new CultureInfo("sl"),
                        new CultureInfo("es"),
                        new CultureInfo("sv"),
                        new CultureInfo("ta"),
                        new CultureInfo("te"),
                        new CultureInfo("th"),
                        new CultureInfo("ti"),
                        new CultureInfo("tr"),
                        new CultureInfo("uk"),
                        new CultureInfo("ur"),
                        new CultureInfo("uz"),
                        new CultureInfo("vi"),
                        new CultureInfo("cy"),
                        new CultureInfo("wo"),
            };
        }
    }
}
