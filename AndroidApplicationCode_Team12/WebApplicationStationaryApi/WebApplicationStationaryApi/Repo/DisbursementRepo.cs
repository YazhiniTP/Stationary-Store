using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class DisbursementRepo
    {
        public static List<Disbursement> GenerateDisbursement()
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {

                DateTime today = DateTime.Now;
                var requestdetaillist = context.RequestDetails.Where(x => x.Request.Status == "Approved" && x.Request.ApprovalDate < today).ToList();
                var list = requestdetaillist.GroupBy(h => new { h.Request.Employee.DepartmentID, h.ItemID })
                                               .Select(group => new
                                               {
                                                   DepartmentID = group.Key.DepartmentID,
                                                   ItemID = group.Key.ItemID,
                                                   DisbursedQty = group.Sum(s => s.Qty),
                                               });
                List<Disbursement> disburse = new List<Disbursement>();
                foreach (var q in list)
                {
                    Disbursement d = new Disbursement();
                    d.DepartmentID = q.DepartmentID;
                    d.DisbursedQty = q.DisbursedQty;
                    d.ItemID = q.ItemID;
                    disburse.Add(d);
                    context.Disbursements.Add(d);
                }
                var requestids = requestdetaillist.Select(x => x.RequestID).ToList().Distinct();
                foreach (var q in requestids)
                {
                    Request r = context.Requests.Where(x => x.RequestID == q).FirstOrDefault();
                    r.Status = "Processed";
                }
                context.SaveChanges();
                return disburse;
            }
        }
        //Disbursement add in outstanding items, already add into database
        public List<DisbursementModel> GenerateDisbursementwithOuts()
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {

                List<Outstanding> outsList = context.Outstandings.Where(x => x.Status == "Pending").ToList<Outstanding>();
                GenerateDisbursement();
                List<Disbursement> disList = context.Disbursements.ToList<Disbursement>();
                foreach (Outstanding o in outsList)
                {
                    Disbursement d = context.Disbursements.Where(x => x.DepartmentID == o.DepartmentID && x.ItemID == o.ItemID && x.Delivery.Status == null).ToList<Disbursement>().FirstOrDefault();
                    if (d != null)
                    {
                        d.DisbursedQty += o.Qty;
                    }
                    else
                    {
                        Disbursement e = new Disbursement();
                        e.DepartmentID = o.DepartmentID;
                        e.ItemID = o.ItemID;
                        e.DisbursedQty = o.Qty;
                        context.Disbursements.Add(e);
                    }
                    o.Status = "Processed";
                    //context.Outstanding.Remove(o);
                }
                context.SaveChanges();

                List<DisbursementModel> dis = new List<DisbursementModel>();

                dis = context.Disbursements
                   .Select<Disbursement, DisbursementModel>
               (c => new DisbursementModel()
               {

                   DisbursementID = c.DisbursementID,
                   DepartmentID = c.DepartmentID,
                   ItemID = c.ItemID,
                   DisbursedQty = c.DisbursedQty,
                   DeliveryID = c.DeliveryID

               }).ToList<DisbursementModel>();



                return dis;
            }




        }




        public List<DisburseInfo> BindDisbursement()
        {
            GenerateDisbursementwithOuts();

            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<DisburseInfo> list = context.Disbursements.Where(x => x.DeliveryID == null).GroupBy(y => new { y.ItemID }).Select(group => new DisburseInfo()
                {
                    ItemID = group.Key.ItemID,
                    ItemDes = group.Where(x => x.ItemID == group.Key.ItemID).Select(x => x.CatalogueInventory.Item_Description).ToList<String>().FirstOrDefault(),
                    DisbursedQty = group.Sum(s => s.DisbursedQty)
                }).ToList<DisburseInfo>();
                return list;
            }
        }







        public static List<int> LastDepartment(string ItemID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<RequestDetail> rlist = context.RequestDetails.Where(x => x.ItemID == ItemID && x.Request.Status == "Processed" && x.Qty > 0)
                                        .OrderByDescending(y => y.Request.ApprovalDate).ToList<RequestDetail>();
                List<int> dIdlist = new List<int>();
                foreach (RequestDetail r in rlist)
                {
                    int dId = (int)r.Request.Employee.DepartmentID;
                    dIdlist.Add(dId);
                }

                List<Outstanding> olist = context.Outstandings.Where(x => x.ItemID == ItemID && x.Status == "Processed" && x.Qty > 0)
                                        .ToList<Outstanding>();
                foreach (Outstanding o in olist)
                {
                    int oId = (int)o.DepartmentID;
                    dIdlist.Add(oId);
                }

                return dIdlist;
            }
        }
        //validation : newly input qty should be <= (request + outstanding)
        public static int RequestQty(string ItemID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                int itemQty = 0;
                List<Outstanding> outsList = context.Outstandings.Where(x => x.Status == "Processed" && x.ItemID == ItemID).ToList<Outstanding>();
                foreach (Outstanding o in outsList)
                {
                    itemQty += (int)o.Qty;
                }

                List<RequestDetail> requestDetails = context.RequestDetails.Where(x => x.ItemID == ItemID && x.Request.Status == "Processed").ToList<RequestDetail>();
                if (requestDetails != null)
                    itemQty += (int)requestDetails.Sum(y => y.Qty);
                return itemQty;

                //itemQty += (int)context.RequestDetails.Where(x => x.ItemID == ItemID && x.Request.Status == "Processed").Sum(y => y.Qty);
                //return itemQty;
            }
        }


        //Can only allow to decrease Qty and can only change Qty one time.
        public string UpdateDisbursement(string ItemID, int newQty)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                //delete the temporary outstandings related to this ItemID
                //List<Outstanding> outslist = context.Outstandings.Where(x => x.Status == null && x.ItemID == ItemID).ToList<Outstanding>();
                //foreach (Outstanding o in outslist)
                //{
                //    Disbursement d = context.Disbursements.Where(x => x.ItemID == o.ItemID && x.DepartmentID == o.DepartmentID).ToList<Disbursement>().FirstOrDefault();
                //    d.DisbursedQty += o.Qty;
                //    context.Outstandings.Remove(o);
                //}

                List<Outstanding> outslist = context.Outstandings.Where(x => x.Status == null && x.ItemID == ItemID).ToList<Outstanding>();
                foreach (Outstanding o in outslist)
                {
                    Disbursement d = context.Disbursements.Where(x => x.ItemID == o.ItemID && x.DepartmentID == o.DepartmentID && x.DeliveryID == null).ToList<Disbursement>().FirstOrDefault();
                    d.DisbursedQty += o.Qty;
                    context.Outstandings.Remove(o);
                }
                //context.SaveChanges();

                context.SaveChanges();
                //restore the request qty
                int outQty = RequestQty(ItemID) - newQty;
                int count = 0;
                //if the newQty >= the total request Qty, cannot accept
                if (outQty < 0) return "notok";
                else
                {
                    while (outQty > 0)
                    {
                        int lastDep = LastDepartment(ItemID)[count];
                        Disbursement b = context.Disbursements.Where(x => x.ItemID == ItemID && x.DepartmentID == lastDep && x.DeliveryID == null).ToList<Disbursement>().First();
                        int newDisbursedQty = (int)b.DisbursedQty - outQty;
                        Outstanding o = new Outstanding();
                        o.DepartmentID = b.DepartmentID;
                        o.ItemID = b.ItemID;
                        if (newDisbursedQty >= 0)
                        {
                            b.DisbursedQty = newDisbursedQty;
                            o.Qty = outQty;
                            outQty = 0;
                        }
                        else
                        {
                            o.Qty = b.DisbursedQty;
                            b.DisbursedQty = 0;
                            outQty = -newDisbursedQty;
                        }
                        context.Outstandings.Add(o);
                        context.SaveChanges();
                        count++;
                    }
                    return "ok";
                }
            }
        }

        public void ScheduleDelivery(int EmployeeID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<int> list = context.Disbursements.Where(x => x.DeliveryID == null).Select<Disbursement, int>(x => (int)x.DepartmentID).Distinct().ToList<int>();

                foreach (int DepId in list)
                {
                    ScheduleDepDelivery(EmployeeID, DepId);
                }
            }
        }

        //add one delivery to each department
        public static void ScheduleDepDelivery(int EmployeeID, int DepartmentID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                //create a delivery and set "Delivered"
                Delivery d = new Delivery();
                d.DepartmentID = DepartmentID;
                d.EmployeeID = EmployeeID;
                //d.DeliveryDate = DateTime.Now.Date; //??need to set the next Monday
                /*set delivery date to next monday */
                int timespan = 0;
                int today = (int)DateTime.Now.DayOfWeek;
                switch (today)
                {
                    case 0:
                        timespan = 1;
                        break;
                    case 1:
                        timespan = 7;
                        break;
                    case 2:
                        timespan = 6;
                        break;
                    case 3:
                        timespan = 5;
                        break;
                    case 4:
                        timespan = 4;
                        break;

                    case 5:
                        timespan = 3;
                        break;
                    case 6:
                        timespan = 2;
                        break;
                }
                d.DeliveryDate = DateTime.Now.Date.AddDays(timespan);
                /*end of set delivery date to next monday */

                d.Status = "Scheduled";
                context.Deliveries.Add(d);

                //assign disbursement to this delivery
                List<Disbursement> disbursements = context.Disbursements.Where(x => x.DepartmentID == DepartmentID && x.DeliveryID == null).ToList<Disbursement>();
                foreach (Disbursement dis in disbursements)
                {
                    if (dis.DisbursedQty == 0)
                    {
                        context.Disbursements.Remove(dis);
                    }
                    dis.DeliveryID = d.DeliveryID;
                }

                List<Request> requests = context.Requests.Where(x => x.Employee.DepartmentID == DepartmentID && x.Status == "Processed").ToList<Request>();
                foreach (Request r in requests)
                {
                    r.Status = "Scheduled";
                }
                context.SaveChanges();
            }
        }


        //public void ScheduleDelivery(int EmployeeID)
        //{
        //    using (StationeryStoreEntities context = new StationeryStoreEntities())
        //    {
        //        List<int> list = context.Disbursements.Where(x => x.DeliveryID == null).Select<Disbursement, int>(x => (int)x.DepartmentID).Distinct().ToList<int>();
        //        foreach (int DepId in list)
        //        {
        //            ScheduleDepDelivery(EmployeeID, DepId);
        //        }
        //    }
        //}
        ////add one delivery to each department
        //public static void ScheduleDepDelivery(int EmployeeID, int DepartmentID)
        //{
        //    using (StationeryStoreEntities context = new StationeryStoreEntities())
        //    {
        //        //create a delivery and set "Delivered"
        //        Delivery d = new Delivery();
        //        d.DepartmentID = DepartmentID;
        //        d.EmployeeID = EmployeeID;
        //        d.DeliveryDate = DateTime.Now.Date; //??need to set the next Monday
        //        d.Status = "Scheduled";
        //        context.Deliveries.Add(d);
        //        //assign disbursement to this delivery
        //        List<Disbursement> disbursements = context.Disbursements.Where(x => x.DepartmentID == DepartmentID && x.DeliveryID == null).ToList<Disbursement>();
        //        foreach (Disbursement dis in disbursements)
        //        {
        //            dis.DeliveryID = d.DeliveryID;
        //        }
        //        List<Request> requests = context.Requests.Where(x => x.Employee.DepartmentID == DepartmentID && x.Status == "Processed").ToList<Request>();
        //        foreach (Request r in requests)
        //        {
        //            r.Status = "Scheduled";
        //        }
        //        context.SaveChanges();
        //    }
        //}
















    }
}