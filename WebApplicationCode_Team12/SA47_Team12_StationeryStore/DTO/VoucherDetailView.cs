using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA47_Team12_StationeryStore
{
    public class VoucherDetailView
    {   
        public int VoucherDetailId { get; set; }
        public int? SerialNo { get; set; }
        public string ItemId { get; set; }
        public int? AdjustedQty { get; set; }
        public string Remarks { get; set; }
    }
}
