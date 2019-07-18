using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class VoucherBizLogic
    {
        // Use this when clerk clicks on add item button
        public static void CreateVoucherDetail(string itemCode, int adjustedQty, string remarks, int employeeId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())       // need using... as cannot create a _entities reference in static class directly
            {
                var chosenItem = _entities.CatalogueInventory.Where(c => c.ItemID == itemCode).Single();
                var chosenItemUnitCost = chosenItem.UnitCost;

                VoucherDetail voucherDetail = new VoucherDetail
                {
                    ItemID = itemCode,
                    AdjustedQty = adjustedQty,
                    AdjustedAmt = adjustedQty * chosenItemUnitCost,
                    VoucherID = null,
                    Remarks = remarks,
                    EmployeeID = employeeId
                };
                _entities.VoucherDetail.Add(voucherDetail);
                _entities.SaveChanges();

                // for an employee with unsubmitted voucher details, 
                // can store and retrieve them later using employee id
                // once voucher raised, set their voucher id column to the voucher id that 
                // was newly generated
            }
        }

        // use this when clerk clicks on update button
        public static void UpdateVoucherDetail(int voucherDetailId, int adjustedQty, string remarks)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var voucherDetailToBeUpdated = _entities.VoucherDetail
                    .Where(v => v.AdjustmentID == voucherDetailId)
                    .Single();

                var chosenItem = _entities.CatalogueInventory
                    .Where(c => c.ItemID == voucherDetailToBeUpdated.ItemID)
                    .Single();
                var chosenItemUnitCost = chosenItem.UnitCost;

                voucherDetailToBeUpdated.AdjustedQty = adjustedQty;
                voucherDetailToBeUpdated.AdjustedAmt = adjustedQty * chosenItemUnitCost;
                voucherDetailToBeUpdated.Remarks = remarks;

                _entities.SaveChanges();
            }
        }

        // Delete voucher detail
        public static void DeleteVoucherDetail(int voucherDetailId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var voucherDetailToBeDeleted = _entities.VoucherDetail
                    .Where(v => v.AdjustmentID == voucherDetailId)
                    .Single();

                _entities.VoucherDetail.Remove(voucherDetailToBeDeleted);
                _entities.SaveChanges();
            }
        }

        // can use this when populating gridview
        public static List<VoucherDetailView> ListVoucherDetails(int employeeId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var listOfVoucherDetails = _entities.VoucherDetail
                    .Where(v => v.EmployeeID == employeeId && v.VoucherID == null)
                    .Select(v => new VoucherDetailView
                    {
                        VoucherDetailId = v.AdjustmentID,
                        ItemId = v.ItemID,
                        AdjustedQty = v.AdjustedQty,
                        Remarks = v.Remarks

                    }).ToList();

                for (int i = 0; i < listOfVoucherDetails.Count; i++)
                {
                    listOfVoucherDetails.ElementAt(i).SerialNo = i + 1;
                }
                return listOfVoucherDetails;
            }
        }

        //Use this method to list voucher Details
        public static List<VoucherDetailView> ListVoucherDetails(int employeeId, int voucherId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var listOfVoucherDetails = _entities.VoucherDetail
                    .Where(v => v.EmployeeID == employeeId && v.VoucherID == voucherId)
                    .Select(v => new VoucherDetailView
                    {
                        VoucherDetailId = v.AdjustmentID,
                        ItemId = v.ItemID,
                        AdjustedQty = v.AdjustedQty,
                        Remarks = v.Remarks

                    }).ToList();

                for (int i = 0; i < listOfVoucherDetails.Count; i++)
                {
                    listOfVoucherDetails.ElementAt(i).SerialNo = i + 1;
                }
                return listOfVoucherDetails;
            }
        }

        // use this method when clerk clicks on raise request button
        public static void CreateVoucher(int employeeId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                Voucher voucher = new Voucher
                {
                    SubmissionDate = DateTime.Now,
                    ApprovalDate = null,
                    EmployeeID = employeeId, // employee id should be shifted to voucher details or not? // employee should be able to see only his own request that he raised?
                    Status = "Pending Supervisor Approval"
                };

                var updateVoucherDetailList = _entities.VoucherDetail
                    .Where(v => v.VoucherID == null && v.EmployeeID == employeeId)
                    .ToList();

                foreach (VoucherDetail v in updateVoucherDetailList)
                {
                    v.VoucherID = voucher.VoucherID;
                }

                _entities.Voucher.Add(voucher);
                _entities.SaveChanges();
            }
        }

        // List PendingVouchers
        public static List<PendingVoucherRequest> ListPendingVoucherRequests(int employeeId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var listOfPendingVoucherRequests = _entities.Voucher
                                                    .Where(v => v.EmployeeID == employeeId && v.Status == "Pending Supervisor Approval").AsEnumerable()
                                                    .Select(v => new PendingVoucherRequest
                                                    {
                                                        VoucherId = v.VoucherID,
                                                        SubmissionDate = String.Format("{0:ddd, MMM d, yyyy}", v.SubmissionDate),
                                                        Status = v.Status

                                                    }).ToList();

                for (int i = 0; i < listOfPendingVoucherRequests.Count; i++)
                {
                    listOfPendingVoucherRequests.ElementAt(i).SerialNo = i + 1;
                }

                return listOfPendingVoucherRequests;
            }
        }

        public static void DeletePendingVoucher(int voucherId)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var voucherDetailsToBeDeleted = _entities.VoucherDetail
                    .Where(v => v.VoucherID == voucherId)
                    .ToList();

                foreach (VoucherDetail v in voucherDetailsToBeDeleted)
                {
                    _entities.VoucherDetail.Remove(v);
                }

                var voucherToBeDeleted = _entities.Voucher
                    .Where(v => v.VoucherID == voucherId)
                    .Single();

                _entities.Voucher.Remove(voucherToBeDeleted);
                _entities.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            //BizLogic.CreateVoucherDetail("C001", 10, "This is a test for 1005", 1005);
            //BizLogic.UpdateVoucherDetail(8006, 20, "This has been updated");
            //BizLogic.DeleteVoucherDetail(8009);

            //foreach (VoucherDetail v in BizLogic.ListVoucherDetails(1005))
            //{
            //    Console.Write(v.AdjustmentID + " " + v.ItemID + " " + v.AdjustedQty
            //    + " " + v.AdjustedAmt + " " + v.VoucherID + " " + v.Remarks + " " + v.EmployeeID + "\n");

            //}

            //BizLogic.CreateVoucher(1006);
        }
    }
}