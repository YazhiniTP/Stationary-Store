using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore
{
    public class VoucherListView
    {       
        public int? SerialNo { get; set; }
        public string EmployeeName { get; set; }
        public string SubmissionDate { get; set; }
        public string Status { get; set; }

        public int? VoucherID
        {
            get;set;
        }
    }
}