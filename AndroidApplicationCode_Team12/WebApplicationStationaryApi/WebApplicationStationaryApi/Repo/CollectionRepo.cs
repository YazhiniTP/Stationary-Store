using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Repo
{
    public class CollectionRepo
    {
        public List<Collection> ListCollections()
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<Collection> requests = new List<Collection>();
            try
            {
                requests = entities.Collections.ToList();

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

    }
}