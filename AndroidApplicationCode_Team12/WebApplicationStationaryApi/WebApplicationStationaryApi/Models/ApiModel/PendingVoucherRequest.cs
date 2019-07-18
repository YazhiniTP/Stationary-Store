using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class PendingVoucherRequest
    {
        public int VoucherID { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
    }
}