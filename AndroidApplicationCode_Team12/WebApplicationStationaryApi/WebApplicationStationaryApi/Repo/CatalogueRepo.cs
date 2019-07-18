using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class CatalogueRepo
    {

        public List<CatalogueModel> ListCatalogues()
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
        List<CatalogueModel> employees = new List<CatalogueModel>();
            try
            {
                employees = entities.CatalogueInventories.Select<CatalogueInventory, CatalogueModel>
                (c => new CatalogueModel()
        {
            ItemID = c.ItemID,
                    CatID = c.CatID,
                    Description = c.Item_Description,
                    UnitCost = c.UnitCost,
                    ActualQty = c.ActualQty


                }).ToList<CatalogueModel>();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }


            return employees;


        }
    }
}