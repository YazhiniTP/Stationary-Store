using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore
{
    public class StockCardView
    {
        public int? SerialNo {get; set;}
        public string TransactionDate { get; set; }
        public string StockCardDescription { get; set; }    
        public int? AdjustedQty { get; set; }
    }
}