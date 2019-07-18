using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class VoucherDetailView
    {

        public int AdjustmentID { get; set; }
        public string ItemID { get; set; }

        public string Item_Description { get; set; }

        public Nullable<decimal> UnitCost { get; set; }

        public Nullable<int> AdjustedQty { get; set; }
        public Nullable<decimal> AdjustedAmt { get; set; }
        public Nullable<int> VoucherID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string Remarks { get; set; }
    }
}