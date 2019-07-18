using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Models.ApiModel
{
    public class DeliveryDep
    {
        public int DepartmentID { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}