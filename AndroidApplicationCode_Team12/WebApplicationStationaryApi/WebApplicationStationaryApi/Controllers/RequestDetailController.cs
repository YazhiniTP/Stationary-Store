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
    public class RequestDetailController : ApiController
    {

        [HttpGet]
        [Route("api/requestdetail")]
        public List<RequestDetail> Get()
        {
            return new RequestDetailRepo().ListRequests();
        }





        [HttpGet]
        [Route("api/requestdetail/requestid/{id}")]
        public List<ViewRequestDetail> Get2(String id)
        {
            List<ViewRequestDetail> l1 = new List<ViewRequestDetail>();
            int did = Convert.ToInt32(id);

            l1 = new RequestDetailRepo().ViewRequestDetail(did);

            return l1;

        }
    }
}
