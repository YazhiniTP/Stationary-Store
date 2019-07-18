using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class DeliverRepo
    {
        public List<DeliveryDep> DepartmentList()
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {

                List<int> list = context.Disbursements.Where(x => x.Delivery.Status == "Scheduled").Select<Disbursement, int>(x => (int)x.DepartmentID).Distinct().ToList<int>();
                List<DeliveryDep> dlist = new List<DeliveryDep>();
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i];

                    List<DeliveryDep> dlist1 = context.Departments.Where(x => x.DepartmentID == id).Select<Department, DeliveryDep>
                (c => new DeliveryDep()
                {
                    DepartmentID = c.DepartmentID,
                    Description = c.Description,
                    Location = context.UserRepCollections.Where(x => x.DepartmentID == c.DepartmentID).Select(y => y.Collection.Location).FirstOrDefault(),
                    Name = context.UserRepCollections.Where(x => x.DepartmentID == c.DepartmentID).Select(y => y.Employee.Name).FirstOrDefault()

                }).ToList<DeliveryDep>();

                    dlist.Add(dlist1[0]);
                }
                return dlist;


            }
        }

        //after schedule, list items and Qty by dep
        //public  List<DisburseInfo> BindDisbursementByEmp(int DepartmentID)
        //{
        //    using (StationeryStoreEntities context = new StationeryStoreEntities())
        //    {
        //        DateTime today = DateTime.Now.Date;
        //        List<DisburseInfo> d = context.Disbursements.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate == today).Select(y => new DisburseInfo()
        //        {
        //            DisbursementID = y.DisbursementID,
        //            DepartmentDes = y.Department.Description,
        //            DisbursedQty = y.DisbursedQty,
        //            ItemID=y.CatalogueInventory.ItemID,
        //            ItemDes = y.CatalogueInventory.Item_Description
        //        }).ToList<DisburseInfo>();
        //        return d;
        //    }
        //}
        public List<DisburseInfo> BindDisbursementByEmp(int DepartmentID, String status)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                DateTime today = DateTime.Now.Date;


                if (status == "Today's Delivery")
                {
                    List<DisburseInfo> d = context.Disbursements.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate == today).Select(y => new DisburseInfo()
                    {
                        DisbursementID = y.DisbursementID,
                        DepartmentDes = y.Department.Description,
                        DisbursedQty = y.DisbursedQty,
                        ItemID = y.CatalogueInventory.ItemID,
                        ItemDes = y.CatalogueInventory.Item_Description
                    }).ToList<DisburseInfo>();
                    return d;
                }
                else if (status == "Outstanding Delivery")
                {
                    List<DisburseInfo> d = context.Disbursements.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate < today).Select(y => new DisburseInfo()
                    {
                        DisbursementID = y.DisbursementID,
                        DepartmentDes = y.Department.Description,
                        DisbursedQty = y.DisbursedQty,
                        ItemID = y.CatalogueInventory.ItemID,
                        ItemDes = y.CatalogueInventory.Item_Description
                    }).ToList<DisburseInfo>();
                    return d;
                }
                else
                {
                    List<DisburseInfo> d = context.Disbursements.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate > today).Select(y => new DisburseInfo()
                    {
                        DisbursementID = y.DisbursementID,
                        DepartmentDes = y.Department.Description,
                        DisbursedQty = y.DisbursedQty,
                        ItemID = y.CatalogueInventory.ItemID,
                        ItemDes = y.CatalogueInventory.Item_Description
                    }).ToList<DisburseInfo>();
                    return d;
                }
            }
        }

        public static int RequestQty(string ItemID, int DepartmentID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                int itemQty = 0;
                List<Outstanding> outsList = context.Outstandings.Where(x => x.Status == "Processed" && x.ItemID == ItemID && x.DepartmentID == DepartmentID).ToList<Outstanding>();
                foreach (Outstanding o in outsList)
                {
                    itemQty += (int)o.Qty;
                }
                List<RequestDetail> requestDetails = context.RequestDetails.Where(x => x.ItemID == ItemID && x.Request.Status == "Scheduled" && x.Employee.DepartmentID == DepartmentID).ToList<RequestDetail>();
                if (requestDetails != null)
                    itemQty += (int)requestDetails.Sum(y => y.Qty);
                //itemQty += (int)context.RequestDetail.Where(x => x.ItemID == ItemID && x.Request.Status == "Scheduled" && x.Employee.DepartmentID == DepartmentID).Sum(y => y.Qty);
                return itemQty;
            }
        }
        //during delivery
        public string UpdateDepDisbursement(int DisbursementID, int Qty)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                //select the request with last ApprovalDate
                Disbursement b = context.Disbursements.Where(x => x.DisbursementID == DisbursementID).ToList<Disbursement>().First();
                Outstanding outs = context.Outstandings.Where(x => x.Status == null && x.DepartmentID == b.DepartmentID && x.ItemID == b.ItemID).ToList<Outstanding>().FirstOrDefault();
                if (Qty > RequestQty(b.ItemID, (int)b.DepartmentID)) return "notok";
                else if (Qty < RequestQty(b.ItemID, (int)b.DepartmentID))
                {
                    int outsQty = 0;
                    if (outs == null)
                    {
                        outsQty = (int)b.DisbursedQty - Qty;
                        Outstanding o = new Outstanding();
                        o.DepartmentID = b.DepartmentID;
                        o.ItemID = b.ItemID;
                        o.Qty = outsQty;
                        context.Outstandings.Add(o);
                        b.DisbursedQty = Qty;
                        context.SaveChanges();
                        return "ok";
                    }
                    else
                    {
                        outsQty = (int)outs.Qty + (int)b.DisbursedQty - Qty;
                        if (outsQty == 0) context.Outstandings.Remove(outs);
                        else outs.Qty = outsQty;
                        b.DisbursedQty = Qty;
                        context.SaveChanges();
                        return "ok";
                    }
                }
                else
                {
                    b.DisbursedQty = RequestQty(b.ItemID, (int)b.DepartmentID);
                    context.Outstandings.Remove(outs);
                    context.SaveChanges();
                    return "ok";
                }
            }
        }

        //Confirm delivery
        public void ConfirmDeliveryToDep(int EmployeeID, int DepartmentID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                //select today's delivery and set "Delivered"
                //select today's delivery and set "Delivered"
                DateTime today = DateTime.Now.Date;
                List<Delivery> deliveryList = context.Deliveries.Where(x => x.DepartmentID == DepartmentID && x.DeliveryDate <= today).ToList<Delivery>();
                foreach (Delivery d in deliveryList)
                {
                    d.Status = "Delivered";
                    List<Disbursement> clist = context.Disbursements.Where(x => x.DeliveryID == d.DeliveryID).ToList<Disbursement>();
                    foreach (Disbursement dis in clist)
                    {
                        //update catalogueInventory
                        CatalogueInventory c = context.CatalogueInventories.Where(x => x.ItemID == dis.ItemID).FirstOrDefault<CatalogueInventory>();
                        c.ActualQty = c.ActualQty - dis.DisbursedQty;
                        //update stockCard
                        StockCard sc = new StockCard();
                        sc.ItemID = dis.ItemID;
                        sc.SCCatID = 18002;
                        sc.Description = dis.Department.Description;
                        sc.AdjustedQty = -dis.DisbursedQty;
                        sc.TransactionDate = DateTime.Now.Date;
                        context.StockCards.Add(sc);
                    }
                }

                List<Request> requests = context.Requests.Where(x => x.Employee.DepartmentID == DepartmentID && x.Status == "Scheduled").ToList<Request>();
                foreach (Request r in requests)
                {
                    r.Status = "Delivered";
                }


                List<Outstanding> outsList = context.Outstandings.Where(x => x.DepartmentID == DepartmentID).ToList<Outstanding>();
                foreach (Outstanding o in outsList)
                {
                    if (o.Status == "Processed")
                    {
                        context.Outstandings.Remove(o);
                    }
                    else if (o.Status == null)
                    {
                        o.Status = "Pending";
                    }
                }
                context.SaveChanges();
            }
        } 
            
        




    }
}