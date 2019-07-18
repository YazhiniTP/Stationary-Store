using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class DisburseInfo
    {
        public int DisbursementID { get; set; }
        public string DepartmentDes { get; set; }

        public string ItemID { get; set; }
        public string ItemDes { get; set; }

        public int? DisbursedQty { get; set; }

    }
}