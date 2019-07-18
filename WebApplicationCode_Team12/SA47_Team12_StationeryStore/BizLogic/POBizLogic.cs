using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SA47_Team12_StationeryStore.BizLogic
{
    public class POBizLogic
    {
        static StationeryStoreEntities context = new StationeryStoreEntities();

        /* 1. Place 
         * Order*/
        public static List<PO> ListCart(int EmployeeID)
        {
            List<PO> list = context.PO.Where(x => x.EmployeeID == EmployeeID && x.Status == null).ToList<PO>();
            return list;

        }

        //DataBind() -- return a PO list
        public static List<OrderInfo> DataBind(int EmployeeID)
        {
            List<OrderInfo> OIlist = ListCart(EmployeeID).Select(z => new OrderInfo()
            {
                ItemID = z.ItemID,
                Description = z.CatalogueInventory.Item_Description,
                FirstSupplier = z.Supplier.Name,
                OrderQty = z.Qty,
                UnitCost = z.CatalogueInventory.UnitCost,
                UnitOfMeasure = z.CatalogueInventory.UnitOfMeasure,
                Suppliers = ListSup(z.ItemID)
            }).ToList<OrderInfo>();
            return OIlist;

        }


        //ListSup(): return a supplier list List<Supplier>
        public static List<Supplier> ListSup(string ItemID)
        {
            CatalogueInventory c = context.CatalogueInventory.Where(x => x.ItemID == ItemID).FirstOrDefault<CatalogueInventory>();
            if (c != null)
            {
                List<Supplier> slist = new List<Supplier>();
                slist.Add(c.Supplier);
                slist.Add(c.Supplier1);
                slist.Add(c.Supplier2);
                return slist;
            }
            else return null;
            //return context.Supplier.ToList<Supplier>();
        }

        //InitialPO(): Initial PO. List items which stocks are in reorder level
        //OrderInfo object will get these information from CatalogueInventory
        //: ItemID, Description, OrderQty(ReorderQty), FirstSupplier, UnitCost, UnitOfMeasure
        //return a List<OrderInfo>
        public static List<OrderInfo> InitialPO(int EmployeeID)//need to add one parameter "EmployeeID" to differentiate each user
        {
            //Delete the invalid data 
            List<PO> polist = context.PO.Where(d => d.SubmissionDate == null).ToList<PO>();
            foreach (PO p in polist)
            {
                context.PO.Remove(p);
            }

            //Filter the ordered but not delivered items
            List<String> AlreadyOrdered = context.PO.Where(d => d.Status == "Pending").Select<PO, String>
                                       (c => c.ItemID).ToList<String>();

            List<CatalogueInventory> SelectedItems = context.CatalogueInventory
                                                     .Where(x => x.ActualQty <= x.ReorderLevel && !AlreadyOrdered.Contains(x.ItemID))
                                                     .ToList<CatalogueInventory>();

            List<PO> WaitedPODlist = SelectedItems.Select(y => new PO()
            {
                ItemID = y.ItemID,
                SupplierID = y.Priority1,
                Qty = y.ReorderQty,
                EmployeeID = EmployeeID, //hardcode, later add one parameter in this method
            }).ToList<PO>();
            //Add into PO database
            foreach (PO p in WaitedPODlist)
            {
                context.PO.Add(p);
                context.SaveChanges();
                Console.WriteLine(p.ItemID + "" + p.Qty);
            }

            //This list will display on the page, bind data 
            List<OrderInfo> OIlist = SelectedItems.Select(o => new OrderInfo()
            {
                ItemID = o.ItemID,
                Description = o.Item_Description,
                FirstSupplier = o.Supplier.Name,
                OrderQty = o.ReorderQty,
                UnitCost = o.UnitCost,
                UnitOfMeasure = o.UnitOfMeasure
            }).ToList<OrderInfo>();
            return OIlist;
        }


        //UpdateOrderItem():
        //Update Qty and supplier changes of the parameter object
        public static void UpdateOrderItem(String itemID, int Qty, int SupplierID, int EmployeeID)
        {
            PO p = context.PO.Where(x => x.ItemID == itemID && x.EmployeeID == EmployeeID && x.Status == null).ToList().First();
            p.Qty = Qty;
            p.SupplierID = SupplierID;
            context.SaveChanges();
        }

        //Delete one item
        public static void DeleteOrderItem(String itemID, int EmployeeID)
        {
            PO p = context.PO.Where(x => x.ItemID == itemID && x.EmployeeID == EmployeeID && x.Status == null).ToList().First();
            context.PO.Remove(p);
            context.SaveChanges();
        }

        //Raise a PO
        public static void PlaceOrder(int EmployeeID)
        {
            foreach (PO p in ListCart(EmployeeID))
            {
                p.SubmissionDate = DateTime.Now.Date;
                p.Status = "Pending";
                context.SaveChanges();
                //Console.WriteLine(p.POID);
            }
        }


        /* 2. Check status and
         * Confirm delivery*/

        //List pending PO , can without EmployeeID?, because other employee can also confirm
        public static List<OrderInfo> ListPendingPOBySupplier(int EmployeeID, int SupplierID)
        {
            List<OrderInfo> OIlist = context.PO.Where(x => x.SupplierID == SupplierID && x.Status == "Pending").AsEnumerable().OrderByDescending(y => y.SubmissionDate).Select(z => new OrderInfo()
            {

                POID = z.POID,
                Description = z.CatalogueInventory.Item_Description,
                FirstSupplier = z.Supplier.Name,
                OrderQty = z.Qty,
                SubmissionDate = String.Format("{0:ddd, MMM d, yyyy}", (DateTime)z.SubmissionDate)
            }).ToList<OrderInfo>();
            return OIlist;

        }
        public static List<OrderInfo> ListPendingPOByDate(int EmployeeID)
        {
            List<OrderInfo> OIlist = context.PO.Where(x => x.Status == "Pending").AsEnumerable().OrderByDescending(y => y.SubmissionDate).Select(z => new OrderInfo()
            {
                POID = z.POID,
                Description = z.CatalogueInventory.Item_Description,
                FirstSupplier = z.Supplier.Name,
                OrderQty = z.Qty,
                SubmissionDate = String.Format("{0:ddd, MMM d, yyyy}", (DateTime)z.SubmissionDate)
            }).ToList<OrderInfo>();
            return OIlist;
        }

        //List Delivered PO
        public static List<PO> ListDeliveredPO(int EmployeeID)
        {
            return context.PO.Where(x => x.Status == "Delivered").ToList<PO>();
        }

        //Confirm Delivery
        public static void ConfirmDeliveryFromSupplier(int EmployeeID, int POID)
        {
            //update PO
            PO p = context.PO.Where(f => f.POID == POID).ToList().First();
            p.DeliveryDate = DateTime.Now.Date;
            p.Status = "Delivered";
            
            //update Inventory
            CatalogueInventory c = context.CatalogueInventory.Where(x => x.ItemID == p.ItemID).ToList<CatalogueInventory>().FirstOrDefault();
            c.ActualQty += p.Qty;

            //update Stockcard
            StockCard sc = new StockCard();
            sc.ItemID = p.ItemID;
            sc.SCCatID = 18003;
            sc.Description = p.Supplier.Name;
            sc.AdjustedQty = p.Qty;
            sc.TransactionDate = DateTime.Now.Date;
            context.StockCard.Add(sc);
            context.SaveChanges();
        }

        /*3. Disbursement */
        //Disbursement only with request items
        public static List<Disbursement> GenerateDisbursement()
        {


            DateTime today = DateTime.Now;

            var requestdetaillist = context.RequestDetail.Where(x => x.Request.Status == "Approved" && x.Request.ApprovalDate < today).ToList();

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
                context.Disbursement.Add(d);

            }

            var requestids = requestdetaillist.Select(x => x.RequestID).ToList().Distinct();

            foreach (var q in requestids)
            {
                Request r = context.Request.Where(x => x.RequestID == q).FirstOrDefault();
                r.Status = "Processed";
            }
            context.SaveChanges();
            return disburse;

        }

        //Disbursement add in outstanding items, already add into database
        public static List<Disbursement> GenerateDisbursementwithOuts()
        {
            List<Outstanding> outsList = context.Outstanding.Where(x => x.Status == "Pending").ToList<Outstanding>();
            GenerateDisbursement();
            List<Disbursement> disList = context.Disbursement.ToList<Disbursement>();

            foreach (Outstanding o in outsList)
            {
                Disbursement d = context.Disbursement.Where(x => x.DepartmentID == o.DepartmentID && x.ItemID == o.ItemID && x.Delivery.Status == null).ToList<Disbursement>().FirstOrDefault();

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
                    context.Disbursement.Add(e);
                }
                o.Status = "Processed";
                //context.Outstanding.Remove(o);
            }
            context.SaveChanges();

            return context.Disbursement.ToList<Disbursement>();
        }

        //Display disbursement list by Item using DisburseInfo
        public static List<DisburseInfo> BindDisbursement()
        {
            List<DisburseInfo> list = context.Disbursement.Where(x => x.DeliveryID == null).GroupBy(y => new { y.ItemID }).Select(group => new DisburseInfo()
            {
                ItemID = group.Key.ItemID,
                ItemDes = group.Where(x => x.ItemID == group.Key.ItemID).Select(x => x.CatalogueInventory.Item_Description).ToList<String>().FirstOrDefault(),
                DisbursedQty = group.Sum(s => s.DisbursedQty)
            }).ToList<DisburseInfo>();
            return list;
        }

        //before delivery
        //select the request with last ApprovalDate
        public static List<int> LastDepartment(string ItemID)
        {
            List<RequestDetail> rlist = context.RequestDetail.Where(x => x.ItemID == ItemID && x.Request.Status == "Processed" && x.Qty > 0)
                                        .OrderByDescending(y => y.Request.ApprovalDate).ToList<RequestDetail>();
            List<int> dIdlist = new List<int>();
            foreach (RequestDetail r in rlist)
            {
                int dId = (int)r.Request.Employee.DepartmentID;
                dIdlist.Add(dId);
            }
            List<Outstanding> olist = context.Outstanding.Where(x => x.ItemID == ItemID && x.Status == "Processed" && x.Qty > 0)
                                        .ToList<Outstanding>();
            foreach (Outstanding o in olist)
            {
                int oId = (int)o.DepartmentID;
                dIdlist.Add(oId);
            }
            return dIdlist;
        }

        //validation : newly input qty should be <= (request + outstanding)
        public static int RequestQty(string ItemID)
        {
            int itemQty = 0;
            List<Outstanding> outsList = context.Outstanding.Where(x => x.Status == "Processed" && x.ItemID == ItemID).ToList<Outstanding>();
            foreach (Outstanding o in outsList)
            {
                itemQty += (int)o.Qty;
            }
            List<RequestDetail> requestDetails = context.RequestDetail.Where(x => x.ItemID == ItemID && x.Request.Status == "Processed").ToList<RequestDetail>();
            if (requestDetails != null)
                itemQty += (int)requestDetails.Sum(y => y.Qty);
            return itemQty;
        }

        //Can only allow to decrease Qty and can only change Qty one time.
        public static bool UpdateDisbursement(string ItemID, int newQty)
        {
            //delete the temporary outstandings related to this ItemID
            List<Outstanding> outslist = context.Outstanding.Where(x => x.Status == null && x.ItemID == ItemID).ToList<Outstanding>();
            foreach (Outstanding o in outslist)
            {
                Disbursement d = context.Disbursement.Where(x => x.ItemID == o.ItemID && x.DepartmentID == o.DepartmentID && x.DeliveryID == null).ToList<Disbursement>().FirstOrDefault();
                d.DisbursedQty += o.Qty;
                context.Outstanding.Remove(o);
            }
            context.SaveChanges();

            //restore the request qty
            int outQty = RequestQty(ItemID) - newQty;
            int count = 0;
            //if the newQty >= the total request Qty, cannot accept
            if (outQty < 0) return false;
            else
            {
                while (outQty > 0)
                {
                    int lastDep = LastDepartment(ItemID)[count];
                    Disbursement b = context.Disbursement.Where(x => x.ItemID == ItemID && x.DepartmentID == lastDep && x.DeliveryID == null).ToList<Disbursement>().First();
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
                    context.Outstanding.Add(o);
                    context.SaveChanges();
                    count++;
                }

                return true;
            }
        }

        //Schedule delivery
        public static void ScheduleDelivery(int EmployeeID)
        {
            List<int> list = context.Disbursement.Where(x => x.DeliveryID == null).Select<Disbursement, int>(x => (int)x.DepartmentID).Distinct().ToList<int>();

            foreach (int DepId in list)
            {
                ScheduleDepDelivery(EmployeeID, DepId);
            }
        }

        //add one delivery to each department
        public static void ScheduleDepDelivery(int EmployeeID, int DepartmentID)
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
            context.Delivery.Add(d);

            //assign disbursement to this delivery
            List<Disbursement> disbursements = context.Disbursement.Where(x => x.DepartmentID == DepartmentID && x.DeliveryID == null).ToList<Disbursement>();
            foreach (Disbursement dis in disbursements)
            {
                if (dis.DisbursedQty == 0)
                    context.Disbursement.Remove(dis);
                dis.DeliveryID = d.DeliveryID;
            }

            List<Request> requests = context.Request.Where(x => x.Employee.DepartmentID == DepartmentID && x.Status == "Processed").ToList<Request>();
            foreach (Request r in requests)
            {
                r.Status = "Scheduled";
            }
            context.SaveChanges();
        }
        //after schedule, deplist who has scheduled disbursement
        public static List<DepartmentInfo> DepartmentList(string status)
        {
            DateTime today = DateTime.Now.Date;
            List<DepartmentInfo> list = new List<DepartmentInfo>();
            if (status == "Today's Delivery")
            {
                List<DepartmentInfo> d = context.Disbursement.Where(x => x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate == today).Select(y => new DepartmentInfo()
                {
                    DepartmentID = (int)y.DepartmentID,
                    Description = y.Department.Description,
                    Collection = context.UserRepCollection.Where(z => z.DepartmentID == y.DepartmentID).Select(a => a.Collection.Location).FirstOrDefault(),
                    UserPresentative = context.UserRepCollection.Where(z => z.DepartmentID == y.DepartmentID).Select(a => a.Employee.Name).FirstOrDefault()
                }).ToList<DepartmentInfo>();
                return d;
            }
            else if (status == "Outstanding Delivery")
            {
                List<DepartmentInfo> d = context.Disbursement.Where(x => x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate < today).Select(y => new DepartmentInfo()
                {
                    DepartmentID = (int)y.DepartmentID,
                    Description = y.Department.Description,
                    Collection = context.UserRepCollection.Where(z => z.DepartmentID == y.DepartmentID).Select(a => a.Collection.Location).FirstOrDefault(),
                    UserPresentative = context.UserRepCollection.Where(z => z.DepartmentID == y.DepartmentID).Select(a => a.Employee.Name).FirstOrDefault()
                }).ToList<DepartmentInfo>();
                return d;
            }
            else
            {
                List<DepartmentInfo> d = context.Disbursement.Where(x => x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate > today).Select(y => new DepartmentInfo()
                {
                    DepartmentID = (int)y.DepartmentID,
                    Description = y.Department.Description,
                    Collection = context.UserRepCollection.Where(z => z.DepartmentID == y.DepartmentID).Select(a => a.Collection.Location).FirstOrDefault(),
                    UserPresentative = context.UserRepCollection.Where(z => z.DepartmentID == y.DepartmentID).Select(a => a.Employee.Name).FirstOrDefault()
                }).ToList<DepartmentInfo>();
                return d;
            }
        }

        //after schedule, list items and Qty by dep
        public static List<DisburseInfo> BindDisbursementByEmp(int DepartmentID, string status)
        {
            DateTime today = DateTime.Now.Date;


            if (status == "Today's Delivery")
            {
                List<DisburseInfo> d = context.Disbursement.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate == today).Select(y => new DisburseInfo()
                {
                    DisbursementID = y.DisbursementID,
                    DepartmentDes = y.Department.Description,
                    DisbursedQty = y.DisbursedQty,
                    ItemID = y.ItemID,
                    ItemDes = y.CatalogueInventory.Item_Description
                }).ToList<DisburseInfo>();
                return d;
            }
            else if (status == "Outstanding Delivery")
            {
                List<DisburseInfo> d = context.Disbursement.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate < today).Select(y => new DisburseInfo()
                {
                    DisbursementID = y.DisbursementID,
                    DepartmentDes = y.Department.Description,
                    DisbursedQty = y.DisbursedQty,
                    ItemID = y.ItemID,
                    ItemDes = y.CatalogueInventory.Item_Description
                }).ToList<DisburseInfo>();
                return d;
            }
            else
            {
                List<DisburseInfo> d = context.Disbursement.Where(x => x.DepartmentID == DepartmentID && x.Delivery.Status == "Scheduled" && x.Delivery.DeliveryDate > today).Select(y => new DisburseInfo()
                {
                    DisbursementID = y.DisbursementID,
                    DepartmentDes = y.Department.Description,
                    DisbursedQty = y.DisbursedQty,
                    ItemID = y.ItemID,
                    ItemDes = y.CatalogueInventory.Item_Description
                }).ToList<DisburseInfo>();
                return d;
            }
        }


        public static int RequestQty(string ItemID, int DepartmentID)
        {
            int itemQty = 0;
            List<Outstanding> outsList = context.Outstanding.Where(x => x.Status == "Processed" && x.ItemID == ItemID && x.DepartmentID == DepartmentID).ToList<Outstanding>();
            foreach (Outstanding o in outsList)
            {
                itemQty += (int)o.Qty;
            }
            List<RequestDetail> requestDetails = context.RequestDetail.Where(x => x.ItemID == ItemID && x.Request.Status == "Scheduled" && x.Employee.DepartmentID == DepartmentID).ToList<RequestDetail>();
            if (requestDetails != null)
                itemQty += (int)requestDetails.Sum(y => y.Qty);
            //itemQty += (int)context.RequestDetail.Where(x => x.ItemID == ItemID && x.Request.Status == "Scheduled" && x.Employee.DepartmentID == DepartmentID).Sum(y => y.Qty);
            return itemQty;
        }

        //during delivery
        public static bool UpdateDepDisbursement(int DisbursementID, int Qty)
        {
            //select the request with last ApprovalDate
            Disbursement b = context.Disbursement.Where(x => x.DisbursementID == DisbursementID).ToList<Disbursement>().FirstOrDefault();
            Outstanding outs = context.Outstanding.Where(x => x.Status == null && x.DepartmentID == b.DepartmentID && x.ItemID == b.ItemID).ToList<Outstanding>().FirstOrDefault();
            if (Qty > RequestQty(b.ItemID, (int)b.DepartmentID)) return false;
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
                    context.Outstanding.Add(o);
                    b.DisbursedQty = Qty;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    outsQty = (int)outs.Qty + (int)b.DisbursedQty - Qty;
                    if (outsQty == 0) context.Outstanding.Remove(outs);
                    else outs.Qty = outsQty;
                    b.DisbursedQty = Qty;
                    context.SaveChanges();
                    return true;
                }
            }
            else
            {
                b.DisbursedQty = RequestQty(b.ItemID, (int)b.DepartmentID);
                context.Outstanding.Remove(outs);
                context.SaveChanges();
                return true;
            }

        }



        //Confirm delivery
        public static void ConfirmDeliveryToDep(int EmployeeID, int DepartmentID)
        {
            //select today's delivery and set "Delivered"
            DateTime today = DateTime.Now.Date;
            List<Delivery> deliveryList = context.Delivery.Where(x => x.DepartmentID == DepartmentID && x.DeliveryDate <= today).ToList<Delivery>();
            foreach (Delivery d in deliveryList)
            {
                d.Status = "Delivered";
                List<Disbursement> clist = context.Disbursement.Where(x => x.DeliveryID == d.DeliveryID).ToList<Disbursement>();
                foreach (Disbursement dis in clist)
                {
                    //update catalogueInventory
                    CatalogueInventory c = context.CatalogueInventory.Where(x => x.ItemID == dis.ItemID).FirstOrDefault<CatalogueInventory>();
                    c.ActualQty = c.ActualQty - dis.DisbursedQty;
                    //update stockCard
                    StockCard sc = new StockCard();
                    sc.ItemID = dis.ItemID;
                    sc.SCCatID = 18002;
                    sc.Description = dis.Department.Description;
                    sc.AdjustedQty = -dis.DisbursedQty;
                    sc.TransactionDate = DateTime.Now.Date;
                    context.StockCard.Add(sc);
                }
            }         

            List<Request> requests = context.Request.Where(x => x.Employee.DepartmentID == DepartmentID && x.Status == "Scheduled").ToList<Request>();
            foreach (Request r in requests)
            {
                r.Status = "Delivered";
            }


            List<Outstanding> outsList = context.Outstanding.Where(x => x.DepartmentID == DepartmentID).ToList<Outstanding>();
            foreach (Outstanding o in outsList)
            {
                if (o.Status == "Processed")
                {
                    context.Outstanding.Remove(o);
                }
                else if (o.Status == null)
                {
                    o.Status = "Pending";
                }
            }
            context.SaveChanges();
        }

        static void Main(string[] args)
        {
            //1.List<OrderInfo> list = InitialPO(1006);
            //2.UpdateOrderItem("C001", 10, 13002, 1006);
            //3.DeleteOrderItem("C001", 1006);
            //4.PlaceOrder(1006);
            //5.ConfirmDeliveryFromSupplier(1006, 11005);
            //6.List<PO> list = ListPendingPOBySupplier(1006,13001);
            //7.List<PO> list = ListDeliveredPO(1006);

            //8.GenerateDisbursement();
            //9.GenerateDisbursementwithOuts();
            //10.List<DisburseInfo> list = BindDisbursement();
            //if(UpdateDisbursement("C001", 54) == 1)
            //{
            //    List<DisburseInfo> list = BindDisbursement();
            //    foreach (DisburseInfo p in list)
            //    {
            //        Console.WriteLine(p.ItemID + " " + p.DisbursedQty);
            //    }
            //}
            //ScheduleDelivery(1006);

            Console.ReadLine();
        }
    }
}