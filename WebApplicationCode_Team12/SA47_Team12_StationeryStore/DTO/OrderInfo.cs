using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA47_Team12_StationeryStore
{
    //OrderInfo : new object for displaying on the "Place Order" page.
    public class OrderInfo
    {
        public string ItemID { get; set; }

        public string Description { get; set; }

        public int? OrderQty { get; set; }

        public string UnitOfMeasure { get; set; }

        public decimal? UnitCost { get; set; }

        public string FirstSupplier { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public int POID { get; set; }

        public string SubmissionDate { get; set; }
    }
}
