using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class RequestRepo
    {
        public List<RequestModel> ListRequests()
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<RequestModel> requests = new List<RequestModel>();
            try
            {
                requests = entities.Requests
                     .Select<Request, RequestModel>
                 (c => new RequestModel()
                 {
                     EmployeeID = c.EmployeeID
                    
                 }).ToList<RequestModel>();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            return requests;

        }

        public List<ViewRequest> ViewPendingRequest(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<Request> r = context.Requests.Where(x => x.Employee.DepartmentID == id && x.Status == "Pending").ToList(); //RequestID is null when request is not submitted

                List<ViewRequest> req = new List<ViewRequest>();

                for (int i = 0; i < r.Count; i++)
                {
                    decimal?[] amt = new decimal?[r.Count];
                    amt[i] = 0;

                    int rid = r[i].RequestID;

                   List<RequestDetail> l = context.RequestDetails.Where(x => x.RequestID == rid).ToList();

                    for (int j = 0; j < l.Count; j++)
                    {
                        amt[i] += l[j].CatalogueInventory.UnitCost * l[j].Qty;
                    }

                    //    foreach (RequestDetail rd in l)
                    //{
                    //    amt[i] += rd.CatalogueInventory.UnitCost * rd.Qty;
                    //}

                    //decimal? d = (decimal)20.00;

                    ViewRequest rc = new ViewRequest(r[i].RequestID, r[i].Employee.Name, r[i].SubmissionDate, r[i].Status, r[i].Remarks,amt[i]);
                    req.Add(rc);
                }
                return req;
            }
        }

        public int UpdateRequest(ViewRequest req)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var q = context.Requests.Where(x => x.RequestID == req.RequestID).First();
                if (q != null)
                {
                    q.Status = req.Status;
                    q.ApprovalDate = System.DateTime.Now;
                    context.SaveChanges();
                    return 1;
                }
                return 0;
            };


            
        }

        





    }
}