using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class InventoryBizLogic
    {
        //list all Catalogue-Inventory items
        public static List<InventoryView> ListInventoryItem()
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var listOfInventoryItems = _entities.CatalogueInventory
                                            .Select(c => new InventoryView
                                            {
                                                ItemId = c.ItemID,
                                                CategoryDescription = c.Category.Category_Description,
                                                ItemDescription = c.Item_Description,
                                                ActualQty = c.ActualQty,
                                                UnitOfMeasure = c.UnitOfMeasure

                                            }).ToList();

                for (int i = 0; i < listOfInventoryItems.Count; i++)
                {
                    listOfInventoryItems.ElementAt(i).SerialNo = i + 1;
                }
                return listOfInventoryItems;
            }
        }

        /*Change: 7-2-19 */
        public static int? GetInventoryItemQty(string itemId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var selectedInventoryItem = _entities.CatalogueInventory.Where(c => c.ItemID == itemId).First();

                int? selectedInventoryItemQty = selectedInventoryItem.ActualQty;

                return selectedInventoryItemQty;
            }
        }
       
        // after clicking on an item "details" button, page will load gridview with the list of stockcards for item
        public static List<StockCardView> GetStockCardsForItem(string itemId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var listofItemStockCards = _entities.StockCard
                                            //.Include(s => s.SCCategory)
                                            .Where(s => s.ItemID == itemId).AsEnumerable()
                                            .Select(s => new StockCardView
                                            {
                                                TransactionDate = String.Format("{0:ddd, MMM d, yyyy}", s.TransactionDate),
                                                StockCardDescription = s.Description,
                                                AdjustedQty = s.AdjustedQty

                                            }).ToList();

                for (int i = 0; i < listofItemStockCards.Count; i++)
                {
                    listofItemStockCards.ElementAt(i).SerialNo = i + 1;
                }
                return listofItemStockCards;
            }
        }

        // GetCatalogueItem and its suppliers; used to display item description and top 3 suppliers
        public static List<ItemDetailsView> GetCatalogueItem(string itemId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var CatalogueItemAndSuppliers = _entities.CatalogueInventory
                                                .Where(c => c.ItemID == itemId)
                                                .Select(c => new ItemDetailsView
                                                {
                                                    ItemId = c.ItemID,
                                                    ItemDescription = c.Item_Description,
                                                    UnitOfMeasure = c.UnitOfMeasure,
                                                    FirstSupplier = c.Supplier.Name,
                                                    SecondSupplier = c.Supplier1.Name,
                                                    ThirdSupplier = c.Supplier2.Name

                                                }).ToList();

                //.Include(c => c.Supplier)
                //.Include(c => c.Supplier1)
                //.Include(c => c.Supplier2)
                //.Single();

                return CatalogueItemAndSuppliers;
            }
        }

        public static void Main(string[] args)
        {
            /* Test GetCatalogueItem */
            //var temp = GetCatalogueItem("C001");
            //Console.WriteLine(temp.ItemID + " " + temp.Description + " " + temp.UnitOfMeasure 
            //    + " " + temp.Supplier.Name + " " + temp.Supplier1.Name + " " + temp.Supplier2.Name);

            /* Test ListInventory */
            //foreach (CatalogueInventory i in InventoryBizLogic.ListInventoryByItemCode("C"))
            //{
            //    Console.Write(i.ItemID + " " + i.Category.Description + " " + i.Description
            //    + " " + i.ReorderLevel + " " + i.ReorderQty + " " + i.UnitOfMeasure
            //    + " " + i.UnitCost + " " + i.ActualQty + "\n");
            //}

            //Console.WriteLine("\n");

            //foreach (CatalogueInventory i in InventoryBizLogic.ListInventoryByItemCategory("ENv"))
            //{
            //    Console.Write(i.ItemID + " " + i.Category.Description + " " + i.Description
            //    + " " + i.ReorderLevel + " " + i.ReorderQty + " " + i.UnitOfMeasure
            //    + " " + i.UnitCost + " " + i.ActualQty + "\n");
            //}

            //using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            //{
            //    var count = _entities.CatalogueInventories.Count();
            //    Console.WriteLine(count);
            //}

            /* Test GetInventoryItemStockCards */
            //foreach (StoreCard s in GetInventoryItemStockCards("E035"))
            //{
            //    Console.WriteLine(s.TransactionDate + " " + s.SCCategory.Description + " " + s.Description + " " + s.AdjustedQty);
            //}

        }
    }
}