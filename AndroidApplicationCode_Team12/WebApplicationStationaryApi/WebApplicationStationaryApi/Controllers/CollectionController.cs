using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationStationaryApi.Repo;

namespace WebApplicationStationaryApi.Controllers
{
    public class CollectionController : ApiController
    {
        [HttpGet]
        [Route("api/collection")]
        public List<Collection> Get()
        {
            return new CollectionRepo().ListCollections();
        }


    }
}
