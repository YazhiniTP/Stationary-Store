using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Repo
{
    public class UserRepRepo
    {
        public UserRepCollection ListUserRepByDeptID(int Did)
        {
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<UserRepCollection> userrepcoll = new List<UserRepCollection>();
            try
            {
                userrepcoll = entities.UserRepCollections.Where(a => a.DepartmentID == Did)
                    .ToList<UserRepCollection>();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            if (userrepcoll.Count > 0)
                return userrepcoll[0];
            else return null;

        }


        public int UpdateUserRep(UserRepCollection req)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var q = context.UserRepCollections.Where(x => x.URCollectionID == req.URCollectionID).First();
                if (q != null)
                {
                    q.EmployeeID = req.EmployeeID;
                    q.CollectionID = req.CollectionID;
                    context.SaveChanges();
                    return 1;
                }
                return 0;
            };



        }


    }
}