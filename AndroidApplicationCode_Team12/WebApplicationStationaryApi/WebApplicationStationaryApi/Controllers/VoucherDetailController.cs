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
    public class VoucherDetailController : ApiController
    {

        [HttpPost]
        [Route("api/voucherdetail/add")]
        public String add([FromBody] VoucherDetail v)
        {
            return new VoucherDetailRepo().CreateVoucherDetail(v);
        }

        [HttpGet]
        [Route("api/voucherdetail/voucherid/{id}")]
        public List<VoucherDetailView> Get2(String id)
        {
            List<VoucherDetailView> l1 = new List<VoucherDetailView>();
            int did = Convert.ToInt32(id);
            l1 = new VoucherDetailRepo().ListVoucherDetails(did);

            return l1;

        }

    }
}
