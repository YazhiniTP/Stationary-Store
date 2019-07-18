using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationStationaryApi.Repo;

namespace WebApplicationStationaryApi.Controllers
{
    public class UserRepController : ApiController
    {
        [HttpGet]
        [Route("api/userrepcollection/departmentid/{id}")]
        public UserRepCollection Get2(String id)
        {
            UserRepCollection l1 = new UserRepCollection();
            int did = Convert.ToInt32(id);
            l1 = new UserRepRepo().ListUserRepByDeptID(did);

            return l1;

        }

        [HttpPost]
        [Route("api/userrepcollection/update")]
        public int update([FromBody] UserRepCollection e)
        {
            return new UserRepRepo().UpdateUserRep(e);
        }


    }
}
