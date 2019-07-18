using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationStationaryApi.Models.ApiModel;
using WebApplicationStationaryApi.Repo;

namespace WebApplicationStationaryApi.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("api/employee/departmentid/{id}")]
        public List<EmployeeModel> Get2(String id)
        {
            List<EmployeeModel> l1 = new List<EmployeeModel>();
            int did = Convert.ToInt32(id);
            l1 = new EmployeeRepo().ListEmployeeByDepartmentID(did);

            return l1;

        }

        [HttpGet]
        [Route("api/employee/detaildepartmentid/{id}")]
        public List<EmployeeModel> Get1(String id)
        {
            List<EmployeeModel> l1 = new List<EmployeeModel>();
            int did = Convert.ToInt32(id);
            l1 = new EmployeeRepo().ListEmployeeByDepartmentID2(did);

            return l1;

        }

    }
}
