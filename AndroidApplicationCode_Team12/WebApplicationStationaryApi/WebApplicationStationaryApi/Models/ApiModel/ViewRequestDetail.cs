using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class ViewRequestDetail
    {
        public int RequestDetailID { get; set; }
        public string RequestProductDescription { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> EmployeeID { get; set; }

        public virtual CatalogueInventory CatalogueInventory { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Request Request { get; set; }

        public Nullable<decimal> UnitCost { get; set; }

        public Nullable<decimal> TotalCost { get; set; }


        public ViewRequestDetail(int RequestDetailID,string RequestProductDescription,int? Qty,decimal? UnitCost, decimal? TotalCost)
        {
            this.RequestDetailID = RequestDetailID;
            this.RequestProductDescription = RequestProductDescription;
            this.Qty = Qty;
            this.UnitCost = UnitCost;
            this.TotalCost = TotalCost;
            
        }

    }
}