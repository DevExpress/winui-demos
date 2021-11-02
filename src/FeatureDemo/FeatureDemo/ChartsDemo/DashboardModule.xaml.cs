using DevExpress.WinUI.Charts;
using FeatureDemo.Common;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI;

namespace ChartsDemo {
    public sealed partial class DashboardModule : DemoModuleView {
        public DashboardViewModel ViewModel { get; } = new DashboardViewModel();
        public DashboardModule() {
            InitializeComponent();
        }
    }

    public class DashboardViewModel : INotifyPropertyChanged {
        readonly Palette innerDoughnutPalette;
        readonly List<CountryStatisticInfo> countriesData = new List<CountryStatisticInfo>(12);
        readonly PredefinedPalette palette = new DefaultPalette();
        CountryStatisticInfo selectedCountry;

        public PredefinedPalette DashboardPalette { get { return palette; } }
        public List<CountryStatisticInfo> Countries {
            get { return countriesData; }
        }
        public List<CountryStatisticInfo> Top10LargestCountriesData {
            get { return countriesData.FindAll(country => country.Name != "Others" && country.Name != "Top 10"); }
        }
        public List<CountryStatisticInfo> Top10TogetherAndOthersCountriesData {
            get { return countriesData.FindAll(country => country.Name == "Others" || country.Name == "Top 10"); }
        }
        List<PopulationStatisticByYear> populationDynamic;
        public List<PopulationStatisticByYear> PopulationDynamic {
            get { return populationDynamic; }
            private set {
                populationDynamic = value;
                OnPropertyChanged(nameof(PopulationDynamic));
            }
        }
        Color selectedCountryColor;
        public Color SelectedCountryColor {
            get { return selectedCountryColor; }
            private set {
                selectedCountryColor = value;
                OnPropertyChanged(nameof(SelectedCountryColor));
            }
        }
        public CountryStatisticInfo SelectedCountry {
            get { return selectedCountry; }
            set {
                if(selectedCountry == value)
                    return;
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                PopulationDynamic = selectedCountry?.PopulationDynamic;
                if(selectedCountry == null) return;

                int countryIndex = Countries.IndexOf(selectedCountry);
                SelectedCountryColor = DashboardPalette[countryIndex];
            }
        }
        public Palette InnerDoughnutPalette { get { return innerDoughnutPalette; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public DashboardViewModel() {
            countriesData = CountriesInfoDataReader.Load();
            this.innerDoughnutPalette = CreateInnerDoughnutPalette();
            SelectedCountry = Countries.LastOrDefault();
        }
        void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChangedEventHendler = PropertyChanged;
            if(propertyChangedEventHendler != null)
                propertyChangedEventHendler(this, new PropertyChangedEventArgs(propertyName));
        }
        Palette CreateInnerDoughnutPalette() {
            CustomPalette palette = new CustomPalette();
            palette.Colors.Add(this.palette[10]);
            palette.Colors.Add(this.palette[11]);
            return palette;
        }
    }
    public class CountryStatisticInfo {
        readonly string name;
        readonly double area;
        readonly List<PopulationStatisticByYear> statistic;

        List<object> shapes;
        Brush brush;

        public string Name {
            get { return name; }
        }
        public double AreaMSqrKilometers {
            get { return area; }
        }
        public List<PopulationStatisticByYear> PopulationDynamic {
            get { return statistic; }
        }
        public List<object> Shapes {
            get { return shapes; }
            set {
                if(value == null)
                    throw new ArgumentNullException(nameof(value));
                shapes = value;
            }
        }
        public Brush Brush {
            get { return brush; }
            set {
                if(value == null)
                    throw new ArgumentNullException(nameof(value));
                brush = value;
            }
        }

        public CountryStatisticInfo(string name, double areaMSqrKilometers, List<PopulationStatisticByYear> statistic) {
            this.name = name;
            this.area = areaMSqrKilometers;
            this.statistic = statistic;
        }
    }
    public class PopulationStatisticByYear {
        int year;
        double populationMillionsOfPeople;
        double urbanPercent;

        public int Year {
            get { return year; }
        }
        public double PopulationMillionsOfPeople {
            get { return populationMillionsOfPeople; }
        }
        public double UrbanPercent {
            get { return urbanPercent; }
        }
        public double RuralPercent {
            get { return 100 - urbanPercent; }
        }

        public PopulationStatisticByYear(int year, double populationMillionsOfPeople, double urbanPercent) {
            SetYear(year);
            SetPopulation(populationMillionsOfPeople);
            SetUrbanPercent(urbanPercent);
        }

        void SetYear(int value) {
            if(1949 < value && value < 2051)
                this.year = value;
            else
                throw new ArgumentException("Years earlier than 1950 and later than 2050 are not considered.");
        }
        void SetUrbanPercent(double value) {
            if(0 <= value && value <= 100)
                this.urbanPercent = value;
            else
                throw new ArgumentException("Urban percentage must be greater than or equal to zero and less than or equal to 100.");
        }
        void SetPopulation(double population) {
            if(0 < population && population < 10000)
                this.populationMillionsOfPeople = population;
            else
                throw new ArgumentException("Population must be greater than zero and less than 10 billion inhabitants");
        }
    }
}