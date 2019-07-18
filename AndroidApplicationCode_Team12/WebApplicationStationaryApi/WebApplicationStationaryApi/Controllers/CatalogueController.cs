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
    public class CatalogueController : ApiController
    {
        [HttpGet]
        [Route("api/catalogue")]
        public List<CatalogueModel> Get2()
        {
            List<CatalogueModel> l1 = new List<CatalogueModel>();

            l1 = new CatalogueRepo().ListCatalogues();

            return l1;

        }

    }
}
