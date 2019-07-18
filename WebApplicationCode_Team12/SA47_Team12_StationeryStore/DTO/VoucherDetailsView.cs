using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore
{
    public class VoucherDetailsView
    {       
        public int? VoucherID { get; set; }
        public string ItemID { get; set; }
        public int? ActualQty { get; set; }
        public string ItemName { get; set; }
        public decimal? AdjAmt { get; set; }
        public int? AdjQty { get; set; }
        public string Remarks { get; set; }
        public string Reasons { get; set; }
        public string Status { get; set; }
        public string ApprovalDate{ get; set; }
    }
}