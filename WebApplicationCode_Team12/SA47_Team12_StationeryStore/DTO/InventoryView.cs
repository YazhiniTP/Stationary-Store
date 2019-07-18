using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA47_Team12_StationeryStore
{
    public class InventoryView
    {
        public int? SerialNo { get; set; }
        public string ItemId {get; set;}
        public string CategoryDescription { get; set; }
        public string ItemDescription { get; set; }
        public int? ActualQty { get; set; }
        public string UnitOfMeasure { get; set; }

    }
}
