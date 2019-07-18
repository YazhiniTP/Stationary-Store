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
    public class AspNetUserController : ApiController
    {
        [HttpGet]
        [Route("api/user")]
        public List<AspNetUser> Get()
        {
            return new AspNetUserRepo().ListUsers();
        }

        [HttpGet]
        [Route("api/user/")]
        public Employee Get(String Email)
        {
            AspNetUser l1 = new AspNetUser();
            l1 = new AspNetUserRepo().FindUserByEmail(Email);

            String id = l1.Id;

            Employee e1 = new Employee();

            e1 = new AspNetUserRepo().ListEmployeeByUserID(id);

            return e1;

        }

        [HttpGet]
        [Route("api/employee/{id}")]
        public EmployeeModel Get5(String id)
        {


            EmployeeModel e1 = new EmployeeModel();

            e1 = new AspNetUserRepo().ListEmployeeByUserID3(id);

            return e1;

        }


        [Authorize]
        [HttpGet]
        [Route("api/user/email/{email}")]
        public EmployeeModel Get2(String email)
        {
            AspNetUser l1 = new AspNetUser();
            l1 = new AspNetUserRepo().FindUserByEmail(email);

            String id = l1.Id;

            EmployeeModel e1 = new EmployeeModel();

            e1 = new AspNetUserRepo().ListEmployeeByUserID2(id);

            return e1;

        }

        [HttpPost]
        [Route("api/adjustment")]
        public EmployeeModel Get3(String email)
        {
            AspNetUser l1 = new AspNetUser();
            l1 = new AspNetUserRepo().FindUserByEmail(email);

            String id1 = l1.Id;

            EmployeeModel e1 = new EmployeeModel();

            e1 = new AspNetUserRepo().ListEmployeeByUserID2(id1);

            return e1;

        }

        [HttpPost]
        [Route("api/user/check")]
        public EmployeeModel checkEmployee([FromBody] EmployeeData e)
        {
            string email = e.Email;
            AspNetUser l1 = new AspNetUser();
            l1 = new AspNetUserRepo().FindUserByEmail(email);

            String id1 = l1.Id;

            EmployeeModel e1 = new EmployeeModel();

            e1 = new AspNetUserRepo().ListEmployeeByUserID2(id1);

            return e1;
        }
    }
}
