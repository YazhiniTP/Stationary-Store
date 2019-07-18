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
    public class DelegationController : ApiController
    {

        [HttpGet]
        [Route("api/delegation")]
        public List<DelegationModel> Get()
        {
            return new DelegationRepo().ListDelegations();
        }

        [HttpPost]
        [Route("api/delegation/add")]
        public int update([FromBody] DelegationModel e)
        {
            return new DelegationRepo().CreateDelegation(e);
        }

        [HttpPost]
        [Route("api/delegation/check")]
        public string IsDuplicate([FromBody] DelegationModel e)
        {
            int depid = Convert.ToInt32(e.DepartmentID);
            DateTime start = (DateTime)e.StartDate;
            DateTime end = (DateTime)e.StartDate;
            return new DelegationRepo().IsDuplicate(depid, start, end);
        }

    }
}
