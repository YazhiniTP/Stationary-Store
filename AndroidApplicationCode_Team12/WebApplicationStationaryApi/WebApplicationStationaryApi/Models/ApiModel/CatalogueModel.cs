using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class CatalogueModel
    {
        public string ItemID { get; set; }
        public Nullable<int> CatID { get; set; }
        public string Description { get; set; }

        public string CategoryDescription { get; set; }

        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<int> ActualQty { get; set; }

    }
}