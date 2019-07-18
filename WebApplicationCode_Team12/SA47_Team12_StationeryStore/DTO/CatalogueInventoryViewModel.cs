using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA47_Team12_StationeryStore
{
    public class CatalogueInventoryViewModel
    {
        public int? SerialNo { get; set; }
        public string ItemID { get; set; }
        public string CategoryDescription { get; set; }
        public string ItemDescription { get; set; }
        public int? ReorderLevel { get; set; }
        public int? ReorderQty { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitCost { get; set; }
        public int? ActualQty { get; set; }

    }
}
