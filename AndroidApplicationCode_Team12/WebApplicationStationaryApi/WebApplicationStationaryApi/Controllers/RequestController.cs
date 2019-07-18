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
    public class RequestController : ApiController
    {
        [HttpGet]
        [Route("api/request")]
        public List<RequestModel> Get()
        {
            return new RequestRepo().ListRequests();
        }

        


        
        [HttpGet]
        [Route("api/request/departmentid/{id}")]
        public List<ViewRequest> Get2(String id)
        {
            List<ViewRequest> l1 = new List<ViewRequest>();
            int did = Convert.ToInt32(id);
            l1 = new RequestRepo().ViewPendingRequest(did);
            
            return l1;

        }

        [HttpPost]
        [Route("api/request/apprej/update")]
        public int update([FromBody] ViewRequest e)
        {
            return new RequestRepo().UpdateRequest(e);
        }

    }
}
