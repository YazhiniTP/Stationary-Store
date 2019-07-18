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
    public class DisbursementController : ApiController
    {
        [HttpGet]
        [Route("api/disbursement/generate")]
        public List<DisbursementModel> Get()
        {
            return new DisbursementRepo().GenerateDisbursementwithOuts();
        }


        [HttpGet]
        [Route("api/disbursement/get")]
        public List<DisburseInfo> Get2()
        {
            return new DisbursementRepo().BindDisbursement();
        }


        [HttpPost]
        [Route("api/disbursement/update")]
        public string checkEmployee([FromBody] DisburseInfo disburseInfo)
        {
           
            int qty=(int)disburseInfo.DisbursedQty;

            return new DisbursementRepo().UpdateDisbursement(disburseInfo.ItemID,qty);

        }

        [HttpGet]
        [Route("api/disbursement/scheduledelivery/{id}")]
        public void Get3(String id)
        {
            int did = Convert.ToInt32(id);
            new DisbursementRepo().ScheduleDelivery(did);
        }


    }
}
