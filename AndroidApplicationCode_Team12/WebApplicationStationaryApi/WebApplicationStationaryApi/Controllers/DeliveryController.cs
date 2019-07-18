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
    public class DeliveryController : ApiController
    {

        


        [HttpPost]
        [Route("api/delivery/update")]
        public string checkEmployee([FromBody] DisburseInfo disburseInfo)
        {

            int qty = (int)disburseInfo.DisbursedQty;

            int id = disburseInfo.DisbursementID;

            return new DeliverRepo().UpdateDepDisbursement(id, qty);

        }

        [HttpGet]
        [Route("api/delivery/departmentid/{id}/{status}")]
        public List<DisburseInfo> Get3(String id, String status)
        {
            int did = Convert.ToInt32(id);
            return new DeliverRepo().BindDisbursementByEmp(did, status);



        }









        [HttpGet]
        [Route("api/delivery/department")]
        public List<DeliveryDep> Get5()
        {
           
            return new DeliverRepo().DepartmentList();



        }


        [HttpPost]
        [Route("api/delivery/confirm")]
        public void confirmDeliver([FromBody] DeliveryModel deliveryModel)
        {

            int empid = (int)deliveryModel.EmployeeID;

            int depid = deliveryModel.DepartmentID;

            new DeliverRepo().ConfirmDeliveryToDep(empid, depid);

        }



    }
}
