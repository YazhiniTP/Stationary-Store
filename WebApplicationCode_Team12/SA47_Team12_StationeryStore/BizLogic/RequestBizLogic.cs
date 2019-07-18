using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class RequestBizLogic
    {//Hardcoding RequestID Not submitted- 9010 (line 22,44)
        //Hardcoding RequestStatus - Pending (line 82)

        //User Functions

        //1.Update qty for RequestDetail --tested
        public static void UpdateRequestDetail(int id, int qty)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var q = context.RequestDetail.Where(x => x.RequestDetailID == id).First();
                q.Qty = qty; //save change to request detail table
                context.SaveChanges();
            };
        }

        //2. Add Item to not submitted cart
        public static void AddRequestDetail(string id, int empid, int qty)  //change to catalogue-inventory
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                bool flag = false;
                List<RequestDetail> reqList = context.RequestDetail.Where(x => x.EmployeeID == empid && x.RequestID == null).ToList();
                foreach (RequestDetail r in reqList)
                {
                    if (r.CatalogueInventory.ItemID == id)
                    {
                        r.Qty += qty;
                        flag = true;
                        context.SaveChanges();
                    }
                }

                if (flag == false)
                {
                    RequestDetail obj = new RequestDetail();
                    obj.ItemID = id;
                    obj.Qty = qty;
                    obj.RequestID = null;
                    obj.EmployeeID = empid;
                    context.RequestDetail.Add(obj); //save change to request detail table
                    context.SaveChanges();
                }
            };
        }

        //3. Delete item from not submitted cart --tested
        public static void DeleteRequestDetail(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var q = context.RequestDetail.Where(x => x.RequestDetailID == id).First();
                context.RequestDetail.Remove(q); //save change to request detail table
                context.SaveChanges();
            };
        }

        //4. View not submitted list -- tested
        public static List<ViewRequestDetail> ViewNotSubmittedRequestDetail(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                //int id = e.EmployeeID;
                List<RequestDetail> r = context.RequestDetail.Where(x => x.EmployeeID == id && x.RequestID == null).ToList(); //RequestID is null when request is not submitted
                List<ViewRequestDetail> rc = new List<ViewRequestDetail>();
                for (int i = 0; i < r.Count; i++)
                {
                    ViewRequestDetail rdc = new ViewRequestDetail(r[i].RequestDetailID, r[i].CatalogueInventory.Item_Description, r[i].Qty, r[i].CatalogueInventory.UnitOfMeasure, r[i].CatalogueInventory.UnitCost, r[i].CatalogueInventory.UnitCost * r[i].Qty);
                    rc.Add(rdc);
                }
                return rc;
            }
        }

        //5. Submit a request --tested
        public static void SubmitRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var rdObject = context.RequestDetail.Where(x => x.EmployeeID == id && x.RequestID == null).First();//First RequestDetail
                Request r = new Request();
                r.EmployeeID = rdObject.EmployeeID;
                r.SubmissionDate = DateTime.Now;
                r.ApprovalDate = null;
                r.Status = "Pending";
                r.Remarks = null;
                context.Request.Add(r);
                context.SaveChanges();

                var rdList = context.RequestDetail.Where(x => x.EmployeeID == id && x.RequestID == null).ToList();//List of RequestDetails
                foreach (RequestDetail rd in rdList)
                {
                    rd.RequestID = r.RequestID;
                }
                context.SaveChanges();
            }
        }

        //6. View approved list of request
        public static List<ViewRequest> ViewApprovedRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<Request> r = context.Request.Where(x => x.EmployeeID == id && (x.Status == "Approved" || x.Status == "Scheduled" || x.Status == "Delivered" || x.Status == "Processed")).ToList(); //RequestID is null when request is not submitted

                List<ViewRequest> req = new List<ViewRequest>();

                HashSet<RequestDetail> l = new HashSet<RequestDetail>();

                for (int i = 0; i < r.Count; i++)
                {
                    decimal?[] qty = new decimal?[r.Count];
                    qty[i] = 0;

                    string[] desc = new string[r.Count];
                    desc[i] = null;
                    l = (HashSet<RequestDetail>)r[i].RequestDetails;
                    foreach (RequestDetail rd in l)
                    {
                        desc[i] = rd.CatalogueInventory.Item_Description;
                        qty[i] = rd.Qty;
                    }
                    ViewRequest rc = new ViewRequest(r[i].RequestID, desc[i], String.Format("{0:ddd, MMM d, yyyy}", r[i].SubmissionDate), null, qty[i], r[i].Status, r[i].Remarks);
                    req.Add(rc);
                }
                return req;
            }
        }

        //7. View rejected list of request
        public static List<ViewRequest> ViewRejectedRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<Request> r = context.Request.Where(x => x.EmployeeID == id && x.Status == "Rejected").ToList(); //RequestID is null when request is not submitted

                List<ViewRequest> req = new List<ViewRequest>();

                HashSet<RequestDetail> l = new HashSet<RequestDetail>();

                for (int i = 0; i < r.Count; i++)
                {
                    decimal?[] qty = new decimal?[r.Count];
                    qty[i] = 0;

                    string[] desc = new string[r.Count];
                    desc[i] = null;
                    l = (HashSet<RequestDetail>)r[i].RequestDetails;
                    foreach (RequestDetail rd in l)
                    {
                        desc[i] = rd.CatalogueInventory.Item_Description;
                        qty[i] = rd.Qty;
                    }
                    ViewRequest rc = new ViewRequest(r[i].RequestID, desc[i], String.Format("{0:ddd, MMM d, yyyy}", r[i].SubmissionDate), null, qty[i], null, r[i].Remarks);
                    req.Add(rc);
                }
                return req;
            }
        }

        //9. View approved list of request
        //8. View delivering list of request
        public static List<ViewRequest> ViewPendingRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<Request> r = context.Request.Where(x => x.EmployeeID == id && x.Status == "Pending").ToList(); //RequestID is null when request is not submitted

                List<ViewRequest> req = new List<ViewRequest>();
                for (int i = 0; i < r.Count; i++)
                {
                    decimal?[] amt = new decimal?[r.Count];
                    amt[i] = 0;

                    HashSet<RequestDetail> l = (HashSet<RequestDetail>)r[i].RequestDetails;

                    foreach (RequestDetail rd in l)
                    {
                        amt[i] += rd.CatalogueInventory.UnitCost * rd.Qty;
                    }

                    ViewRequest rc = new ViewRequest(r[i].RequestID, null, String.Format("{0:ddd, MMM d, yyyy}", r[i].SubmissionDate), null, amt[i], null, null);
                    req.Add(rc);
                }
                return req;
            }
        }

        //10. Delete Pending Request(Not approved yet by DH)
        public static void DeletePendingRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<Request> obj = context.Request.Where(x => x.EmployeeID == id && x.Status == "Pending").ToList();
                foreach (Request i in obj)
                {
                    List<RequestDetail> list = context.RequestDetail.Where(x => x.RequestID == i.RequestID).ToList();
                    foreach (RequestDetail r in list)
                    {
                        context.RequestDetail.Remove(r);   //remove co-oresponding record from request details table
                    }
                    context.SaveChanges();
                    context.Request.Remove(i); //remove from request table
                    context.SaveChanges();
                }
            };
        }

        //11.Delete Pending Request(int requestId)
        public static void DeleteSelectedPendingRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Request obj = context.Request.Where(x => x.RequestID == id).First();

                List<RequestDetail> list = context.RequestDetail.Where(x => x.RequestID == obj.RequestID).ToList();
                foreach (RequestDetail r in list)
                {
                    context.RequestDetail.Remove(r);   //remove co-oresponding record from request details table
                }
                context.SaveChanges();
                context.Request.Remove(obj); //remove from request table
                context.SaveChanges();
            };
        }

        public static List<string> statusList()
        {
            List<String> status = new List<string>();
            status.Add("Pending");
            status.Add("Approved");
            status.Add("Rejected");
            return status;
        }

        //11.Search Catalogue Inventory
        public static List<CatalogueInventory> SearchCatalogue(string name)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<CatalogueInventory> list = context.CatalogueInventory.Where(x => x.Item_Description.Contains(name)).ToList();
                return list;
            }

        }

        public static List<String> ValueCatalogue(string id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                CatalogueInventory item = context.CatalogueInventory.Where(x => x.ItemID == id).First();
                string itemId = item.ItemID;
                string desc = item.Item_Description;
                string cat = item.Category.Category_Description;
                string uom = item.UnitOfMeasure;
                List<String> list = new List<String>();
                list.Add(desc);
                list.Add(cat);
                list.Add(uom);
                list.Add(itemId);
                return list;
            }
        }

        public static List<CatalogueInventory> ViewAll()
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<CatalogueInventory> list = context.CatalogueInventory.ToList();
                return list;
            }

        }

        //5. View name for Request--tested
        public static string ViewEmpName(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                string name;
                Request r = context.Request.Where(x => x.RequestID == id).First(); //View List of RequestDetail
                return name = r.Employee.Name;
            };
        }


        //Manage Request
        public static List<ViewRequest> ListRequests(string status, int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Employee emp = context.Employee.Where(e => e.EmployeeID == id).First();
                int? deptId = emp.DepartmentID;
                List<Request> r;

                if (status == "Pending" || status == "Rejected")
                {
                    r = context.Request.Where(rq => rq.Status.Contains(status) && rq.Employee.DepartmentID == deptId).ToList();
                }
                else
                {
                    r = context.Request.Where(rq => (rq.Status == "Approved" || rq.Status == "Delivered" || rq.Status == "Processed" || rq.Status == "Scheduled") && rq.Employee.DepartmentID == deptId).ToList();
                }

                List<ViewRequest> req = new List<ViewRequest>();

                for (int i = 0; i < r.Count; i++)
                {
                    decimal?[] amt = new decimal?[r.Count];
                    amt[i] = 0;

                    HashSet<RequestDetail> l = (HashSet<RequestDetail>)r[i].RequestDetails;

                    foreach (RequestDetail rd in l)
                    {
                        amt[i] += rd.CatalogueInventory.UnitCost * rd.Qty;
                    }

                    ViewRequest rc = new ViewRequest(r[i].RequestID, r[i].Employee.Name, String.Format("{0:ddd, MMM d, yyyy}", r[i].SubmissionDate), String.Format("{0:ddd, MMM d, yyyy}", r[i].ApprovalDate), amt[i], r[i].Status, null);
                    req.Add(rc);
                }
                return req;
            }
        }

        public static List<ViewRequestDetail> ListRequestDetails(int id)
        {

            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var requestdetail = context.RequestDetail.Where(rq => rq.RequestID == id)
                    .Select(rq => new ViewRequestDetail //Include(vd => vd.CatalogueInventory).
                    {
                        Id = (int)rq.RequestID,
                        Description = rq.CatalogueInventory.Item_Description,
                        Qty = rq.Qty,
                        UnitOfMeasure = rq.CatalogueInventory.UnitOfMeasure,
                        UnitCost = rq.CatalogueInventory.UnitCost,
                        TotalPrice = rq.CatalogueInventory.UnitCost * rq.Qty,
                        //Remarks = rq.Request.Remarks,

                    }).ToList();

                return requestdetail;
            }
        }

        public static void ApproveRequest(int RequestId, DateTime datetime, string remarks, int flag)
        {
            string to;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {

                var item = context.Request.Where(c => c.RequestID == RequestId).Single();
                item.Status = "Approved";
                item.ApprovalDate = datetime;
                item.Remarks = remarks;
                context.SaveChanges();
                to = item.Employee.Email;
            }
            if (flag == 0)
            {
                String from = "teststationery47@gmail.com";
                String subject = "[Auto Notification] Request Status";
                String body = String.Format("Your request {0} has been approved." +
                    "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                    "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                    "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                    "nor disclose its contents to any other person.\n\nThank you.", RequestId);
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }
        public static void RejectRequest(int RequestId, DateTime datetime, string remarks, int flag)
        {
            string to;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var item = context.Request.Where(c => c.RequestID == RequestId).Single();
                item.Status = "Rejected";
                item.ApprovalDate = datetime;
                item.Remarks = remarks;
                context.SaveChanges();
                to = item.Employee.Email;
            }
            if (flag == 0)
            {
                String from = "teststationery47@gmail.com";
                String subject = "[Auto Notification] Request Status";
                String body = String.Format("Sorry! Your request {0} has been rejected" +
                    ".\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                    "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                    "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                    "nor disclose its contents to any other person.\n\nThank you.", RequestId);
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }
    }
}