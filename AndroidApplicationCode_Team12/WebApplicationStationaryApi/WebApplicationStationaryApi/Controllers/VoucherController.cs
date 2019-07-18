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
    public class VoucherController : ApiController
    {
        [HttpPost]
        [Route("api/voucher/add")]
        public String add([FromBody] Voucher v)
        {
            return new VoucherRepo().CreateVoucher(v);
        }

        [HttpGet]
        [Route("api/voucher/employeeid/{id}")]
        public List<PendingVoucherRequest> Get2(String id)
        {
            List<PendingVoucherRequest> l1 = new List<PendingVoucherRequest>();
            int did = Convert.ToInt32(id);
            l1 = new VoucherRepo().ListPendingVoucherRequests(did);

            return l1;

        }

        [HttpPost]
        [Route("api/voucher/delete")]
        public int delete([FromBody] Voucher v)
        {

            new VoucherDetailRepo().DeleteVoucherDetail(v.VoucherID);

            new VoucherRepo().DeleteVoucher(v.VoucherID);

            

            return 1;

        }


    }
}
