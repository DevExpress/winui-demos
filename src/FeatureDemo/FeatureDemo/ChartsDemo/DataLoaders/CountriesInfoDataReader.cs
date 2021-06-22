using FeatureDemo.Common;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ChartsDemo {

    class CountriesInfoDataReader {
        const string UriToXmlFile = @"Data\Top10LagerstCountriesInfo.xml";

        static void LoadStatistic(List<PopulationStatisticByYear> list, XElement populationDynamic) {
            foreach(XElement populationDynamicItem in populationDynamic.Elements("PopulationStatisticByYear")) {
                int year = int.Parse(populationDynamicItem.Element("Year").Value);
                long population = long.Parse(populationDynamicItem.Element("Population").Value);
                double urbanPercent = double.Parse(populationDynamicItem.Element("UrbanPercent").Value);
                PopulationStatisticByYear popDynamicItem = new PopulationStatisticByYear(year, population / 1000000.0, urbanPercent);
                list.Add(popDynamicItem);
            }
        }

        public static List<CountryStatisticInfo> Load() {
            List<CountryStatisticInfo> data = new List<CountryStatisticInfo>(12);
            try {

                XDocument Top10LagerstCountriesInfo_xml = XDocument.Parse(DemoDataLoader.GetFileContentFromResource(UriToXmlFile), LoadOptions.None);
                foreach(XElement countryInfo in Top10LagerstCountriesInfo_xml.Root.Elements("CountryInfo")) {
                    string name = countryInfo.Element("Name").Value;
                    double areaSqKm = uint.Parse(countryInfo.Element("AreaSqrKilometers").Value);
                    List<PopulationStatisticByYear> statistic = new List<PopulationStatisticByYear>(11);
                    LoadStatistic(statistic, countryInfo.Element("Statistic"));
                    CountryStatisticInfo countryInfoInstance = new CountryStatisticInfo(name, areaSqKm/1000000, statistic);
                    data.Add(countryInfoInstance);
                }
            }
            catch {
            }
            return data;
        }
    }

}