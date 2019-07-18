using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore
{
    public class PendingVoucherRequest
    {
        public int? SerialNo { get; set; }
        public int? VoucherId { get; set; }
        public string SubmissionDate { get; set; }
        public string Status { get; set; }
    }
}