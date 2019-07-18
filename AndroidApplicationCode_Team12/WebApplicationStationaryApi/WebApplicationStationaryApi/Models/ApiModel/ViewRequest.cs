using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class ViewRequest
    {

        public int RequestID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public decimal? Amt { get; set; }

        public ViewRequest(int RequestID, string EmployeeName,DateTime? SubmissionDate,string Status, string Remarks, decimal? Amt)
        {
            this.RequestID = RequestID;
            this.EmployeeName = EmployeeName;
            this.SubmissionDate = SubmissionDate;
            this.Status = Status;
            this.Remarks = Remarks;
            this.Amt = Amt;
           
        }
        


    }
}