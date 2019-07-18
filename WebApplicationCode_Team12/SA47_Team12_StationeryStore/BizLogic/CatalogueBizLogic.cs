using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class CatalogueBizLogic
    {
        public static List<CatalogueInventoryViewModel> ListCatalogue()
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var catalogueList = _entities.CatalogueInventory                                    
                                    .Select(c => new CatalogueInventoryViewModel
                                    {
                                        ItemID = c.ItemID,
                                        CategoryDescription = c.Category.Category_Description,
                                        ItemDescription = c.Item_Description,
                                        ReorderLevel = c.ReorderLevel,
                                        ReorderQty = c.ReorderQty,
                                        UnitOfMeasure = c.UnitOfMeasure,
                                        UnitCost = c.UnitCost,
                                        ActualQty = c.ActualQty

                                    }).ToList();

                for (int i = 0; i < catalogueList.Count; i++)
                {
                    catalogueList.ElementAt(i).SerialNo = i + 1;
                }

                return catalogueList;
            }
        }


        //assume clerk is searching by item category
        public static List<CatalogueInventoryViewModel> ListCatalogue(string searchString)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var catalogueList = _entities.CatalogueInventory
                                    .Where(c => c.Item_Description.Contains(searchString))
                                    .Select(c => new CatalogueInventoryViewModel
                                    {
                                        ItemID = c.ItemID,
                                        CategoryDescription = c.Category.Category_Description,
                                        ItemDescription = c.Item_Description,
                                        ReorderLevel = c.ReorderLevel,
                                        ReorderQty = c.ReorderQty, 
                                        UnitOfMeasure = c.UnitOfMeasure,
                                        UnitCost = c.UnitCost,
                                        ActualQty = c.ActualQty

                                    }).ToList();

                for (int i = 0; i < catalogueList.Count; i++)
                {
                    catalogueList.ElementAt(i).SerialNo = i + 1;
                }
                return catalogueList;
            }
        }


        public static bool CheckIfItemExists(string itemId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var itemToBeChecked = _entities.CatalogueInventory
                                        .Where(c => c.ItemID == itemId)
                                        .SingleOrDefault();

                if (itemToBeChecked == null)
                {
                    return false;
                }

                else
                {
                    return true;
                }

            }
        }

        // clerk is updating reorder level and reorder qty
        public static void UpdateItem(string itemId, int reorderLevel, int reorderQty)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var item = _entities.CatalogueInventory
                            .Where(c => c.ItemID == itemId)
                            .Single();

                item.ReorderLevel = reorderLevel;
                item.ReorderQty = reorderQty;

                _entities.SaveChanges();
            }
        }

        // system generated notification calls this method to check if actual qty is below reorder qty
        public static List<CatalogueInventoryViewModel> ListLowStockItems()
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var allCatalogueItems = CatalogueBizLogic.ListCatalogue("");

                List<CatalogueInventoryViewModel> listOfLowStockItems = new List<CatalogueInventoryViewModel>();
                foreach (CatalogueInventoryViewModel c in allCatalogueItems)
                {
                    if (c.ActualQty < c.ReorderLevel)
                    {
                        listOfLowStockItems.Add(c);
                    }
                }
                return listOfLowStockItems;
            }
        }

        public static void Main(string[] args)
        {
            /*Test ListCatalogue*/
            //foreach (CatalogueInventory c in CatalogueBizLogic.ListCatalogue("Clips"))
            //{
            //    Console.WriteLine(c.ItemID + " " + c.Category.Description + " " + c.Description
            //                        + " " + c.ReorderLevel + " " + c.ReorderQty + " " + c.UnitOfMeasure);
            //}

            /*Test UpdateCatalogue Item - use C001 as test, original reorderlevel = 50, reorder qty = 30*/
            //CatalogueBizLogic.UpdateItem("C001", 50, 30);
        }
    }
}