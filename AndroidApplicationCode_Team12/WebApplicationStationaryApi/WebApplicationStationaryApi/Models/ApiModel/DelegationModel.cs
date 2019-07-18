using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class DelegationModel
    {
        public int DelegationID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> DepartmentID { get; set; }



    }
}