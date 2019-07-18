using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class DisbursementModel
    {
        public int DisbursementID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string ItemID { get; set; }
        public Nullable<int> DisbursedQty { get; set; }
        public Nullable<int> DeliveryID { get; set; }

    }
}