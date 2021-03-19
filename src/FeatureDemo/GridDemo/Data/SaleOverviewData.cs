using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class SaleOverviewData {
        public SaleOverviewData(string state, double sales, double salesVsTarget, double profit, double customersSatisfaction, double markerShare) {
            this.State = state;
            this.Sales = sales;
            this.Profit = profit;
            this.SalesVsTarget = salesVsTarget;
            this.CustomersSatisfaction = customersSatisfaction;
            this.MarketShare = markerShare;
        }
        public string State { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }
        public double SalesVsTarget { get; set; }
        public double MarketShare { get; set; }
        public double CustomersSatisfaction { get; set; }
    }
    public static class SaleOverviewDataGenerator {
        public static SaleOverviewData[] Sales {
            get {
                return new SaleOverviewData[] {
                new SaleOverviewData("California", 467949484.42, 0.027697392, 44000000, 4.6, .34),
                new SaleOverviewData("South Dakota", 458113868.36, 0.048206741, 27000000, 4.8, .32),
                new SaleOverviewData("Ohio", 250772304.63, -0.028834951, -15000000, 3.4, .29),
                new SaleOverviewData("Wisconsin", 182660621.53, 0.059826898, 14000000, 4.1, .22),
                new SaleOverviewData("New Hampshire", 158710257.91, 0.01329949, 10000000, 4.0, .19),
                new SaleOverviewData("Maine", 156032947.24, -0.099670575, -9000000, 2.9, .14),
                new SaleOverviewData("Utah", 131496479.72, 0.044533577, 18000000, 4.7, .27),
                new SaleOverviewData("Idaho", 119199535.50, 0.006545225, 14000000, 3.9, .30),
                new SaleOverviewData("Illinois", 102480457.66, -0.003925837, 9000000, 4.2, .17),
                new SaleOverviewData("Washington", 102301309.53, 0.024232883, 18000000, 4.6, .26),
                new SaleOverviewData("Wyoming", 98012761.36, 0.00213708, 4000000, 3.7, .31),
                new SaleOverviewData("Indiana", 95976655.67, 0.024917714, -7000000, 2.5, .17),
                new SaleOverviewData("Nevada", 91535057.56, 0.028420047, 23000000, 4.5, .22),
                new SaleOverviewData("Massachusetts", 90602516.28, 0.013988886, 12000000, 4.0, .24),
                new SaleOverviewData("Rhode Island", 90548513.43, 0.013798557, 21000000, 4.4, .29),
                new SaleOverviewData("Montana", 89977272.49, -0.004951279, -5000000, 2.0, .13),
                new SaleOverviewData("Alabama", 88237187.77, -0.005941003, 6000000, 2.0, .15),
                new SaleOverviewData("Georgia", 88175277.25, 0.004525271, 11000000, 4.2, .24),
                new SaleOverviewData("New York", 87774749.80, 0.011495413, 15000000, 3.9, .30),
                new SaleOverviewData("Texas", 84291394.13, -0.009302101, -20000000, 2.9, .13),
                new SaleOverviewData("Virginia", 83582665.33, -0.005796166, 8000000, 3.9, .10),
                new SaleOverviewData("South Carolina", 83391264.21, 0.038427198, 6000000, 4.4, .28),
                new SaleOverviewData("Missouri", 82106438.59, -0.014408108, 2000000, 3.2, .18),
                new SaleOverviewData("Florida", 81751131.33, 0.011509847, 7000000, 4.1, .24),
                new SaleOverviewData("Mississippi", 81203810.96, 0.033294143, 6000000, 4.4, .31),
                new SaleOverviewData("New Mexico", 80452595.66, 0.012225542, 13000000, 4.9, .23),
                new SaleOverviewData("Kentucky", 79995544.60, 0.040305647, 10000000, 4.6, .27),
                new SaleOverviewData("Connecticut", 73220631.05, 0.033769282, 8000000, 2.9, .24),
                new SaleOverviewData("Arizona", 72989023.31, -0.009417676, -4000000, 3.6, .11),
                new SaleOverviewData("Tennessee", 72877959.11, 0.029150876, 14000000, 4.9, .25),
                new SaleOverviewData("Colorado", 71391979.17, 0.003629055, 5000000, 4.1, .28),
                new SaleOverviewData("Oregon", 70908060.11, 0.004207476, 6000000, 4.5, .23),
                new SaleOverviewData("North Carolina", 70896926.22, -0.011178166, -17000000, 3.0, .12),
                new SaleOverviewData("Michigan", 69466608.80, -0.008368982, 3000000, 4.0, .20),
                new SaleOverviewData("Minnesota", 66473670.35, 0.009992279, 8000000, 4.2, .19),
            };
            }
        }
    }
}