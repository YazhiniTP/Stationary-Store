using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public String Name { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int? DepartmentID { get; set; }

        public int? RoleID { get; set; }
    }
}