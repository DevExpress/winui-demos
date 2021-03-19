using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo {
    public class SalesDataViewModel {
        List<SalesData> items;
        
        public List<SalesData> Items { get { return items ?? (items = new DataGenerator().CreateData(Count != 0 ? Count : 100)); } }
        public int Count { get; set; }
    }
}
