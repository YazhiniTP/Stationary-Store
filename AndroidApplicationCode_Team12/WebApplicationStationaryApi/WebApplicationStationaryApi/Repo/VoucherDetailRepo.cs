using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class VoucherDetailRepo
    {

        public String CreateVoucherDetail(VoucherDetail d)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                VoucherDetail s = new VoucherDetail
                {
                    ItemID = d.ItemID,
                    AdjustedQty = d.AdjustedQty,
                    AdjustedAmt = d.AdjustedAmt,
                    VoucherID = d.VoucherID,
                    EmployeeID = d.EmployeeID,
                    Remarks = d.Remarks


                };
                context.VoucherDetails.Add(s);
                context.SaveChanges();
                return s.AdjustmentID.ToString();
            }
        }

        public int DeleteVoucherDetail(int VoucherID)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                List<VoucherDetail> voucherDetailToBeDeleted = _entities.VoucherDetails
                    .Where(v => v.VoucherID == VoucherID)
                    .ToList();

                for(int i = 0; i < voucherDetailToBeDeleted.Count; i++)
                {
                    _entities.VoucherDetails.Remove(voucherDetailToBeDeleted[i]);
                }

                
                _entities.SaveChanges();
            }

            return 1;

        }




        public List<VoucherDetailView> ListVoucherDetails(int VoucherID)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var listOfVoucherDetails = _entities.VoucherDetails
                    .Where(v => v.VoucherID == VoucherID)
                    .Select(v => new VoucherDetailView
                    {
                        AdjustmentID = v.AdjustmentID,
                        ItemID = v.ItemID,
                        Item_Description=v.CatalogueInventory.Item_Description,
                        UnitCost=v.CatalogueInventory.UnitCost,
                        AdjustedQty = v.AdjustedQty,
                        AdjustedAmt=v.AdjustedAmt,
                        Remarks = v.Remarks

                    }).ToList();

               
                return listOfVoucherDetails;
            }
        }



    }
}