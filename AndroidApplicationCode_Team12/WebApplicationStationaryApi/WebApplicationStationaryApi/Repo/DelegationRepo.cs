using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class DelegationRepo
    {

        public List<DelegationModel> ListDelegations()
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<DelegationModel> requests = new List<DelegationModel>();
            try
            {
                requests = entities.Delegations.Select<Delegation, DelegationModel>
                (c => new DelegationModel()
                {
                    EmployeeID = c.EmployeeID,
                    DelegationID = c.DelegationID,
                    DepartmentID = c.DepartmentID,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                }).ToList<DelegationModel>();

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

        public int CreateDelegation(DelegationModel d)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Delegation s = new Delegation
                {
                    EmployeeID = d.EmployeeID,
                    DepartmentID = d.DepartmentID,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,

                };
                context.Delegations.Add(s);
                context.SaveChanges();
                return 1;
            }
        }

        public string IsDuplicate(int DepID, DateTime start, DateTime end)
        {

            DateTime today = DateTime.Now.Date;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Delegation s = context.Delegations.Where(p => p.DepartmentID == DepID && ((p.StartDate <= end && p.EndDate >= end) || (p.StartDate <= start && p.EndDate >= start))).FirstOrDefault<Delegation>();
                if (s != null) return "notok";
                else return "ok";
            }
        }


    }
}