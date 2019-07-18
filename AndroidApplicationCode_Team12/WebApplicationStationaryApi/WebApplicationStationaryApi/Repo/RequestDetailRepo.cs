using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class RequestDetailRepo
    {

        public List<RequestDetail> ListRequests()
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<RequestDetail> requests = new List<RequestDetail>();
            try
            {
                requests = entities.RequestDetails.ToList();

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


        public List<ViewRequestDetail> ViewRequestDetail(int id)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<RequestDetail> r = context.RequestDetails.Where(x => x.RequestID == id).ToList(); //View List of RequestDetail
                List<ViewRequestDetail> rc = new List<ViewRequestDetail>();
                for (int i = 0; i < r.Count; i++)
                {
                    ViewRequestDetail rdc = new ViewRequestDetail(r[i].RequestDetailID, r[i].CatalogueInventory.Item_Description, r[i].Qty, r[i].CatalogueInventory.UnitCost, r[i].CatalogueInventory.UnitCost * r[i].Qty);
                    rc.Add(rdc);
                }
                return rc;
            };
        }

    }
}