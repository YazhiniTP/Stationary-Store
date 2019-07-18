using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore
{
    public class ViewRequest
    {
        int id;
        string empName;
        decimal? amount;
        string submittedTime;
        string approvedTime;
        string status;
        string remarks;

        public ViewRequest()
        {

        }

        public ViewRequest(int id, string name, string submissionDate, string approvalDate, decimal? amt, string status, string remarks)
        {
            this.RequestId = id;
            this.EmployeeName = name;
            this.Amount = amt;
            this.SubmittedTime = submissionDate;
            this.ApprovedTime = approvalDate;
            this.Status = status;
            this.Remarks = remarks;
        }

        public int RequestId { get => id; set => id = value; }
        public string EmployeeName { get => empName; set => empName = value; }
        public decimal? Amount { get => amount; set => amount = value; }
        public string SubmittedTime { get => submittedTime; set => submittedTime = value; }
        public string ApprovedTime { get => approvedTime; set => approvedTime = value; }
        public string Status { get => status; set => status = value; }
        public string Remarks { get => remarks; set => remarks = value; }

        //public string Remarks { get; set; }
    }
}