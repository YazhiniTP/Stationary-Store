using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class VoucherRepo
    {

        public String CreateVoucher(Voucher d)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Voucher s = new Voucher
                {
                    EmployeeID = d.EmployeeID,
                    SubmissionDate = System.DateTime.Now,
                    Status = "Pending Supervisor Approval",

                };
                context.Vouchers.Add(s);
                context.SaveChanges();
                return s.VoucherID.ToString();
            }
        }


        public int DeleteVoucher(int VoucherID)
        {
            using (StationeryStoreEntities _entities = new StationeryStoreEntities())
            {
                var voucherDetailToBeDeleted = _entities.Vouchers
                    .Where(v => v.VoucherID == VoucherID)
                    .Single();

                _entities.Vouchers.Remove(voucherDetailToBeDeleted);
                _entities.SaveChanges();
            }

            return 1;
        }



        public List<PendingVoucherRequest> ListPendingVoucherRequests(int employeeId)
        {
            using (StationeryStoreEntities entities = new StationeryStoreEntities())
            {
                List<PendingVoucherRequest> listOfPendingVoucherRequests = entities.Vouchers
                                                    .Where(v => v.EmployeeID == employeeId && v.Status == "Pending Supervisor Approval")
                                                    .Select(v => new PendingVoucherRequest
                                                    {
                                                        VoucherID = v.VoucherID,
                                                        SubmissionDate = v.SubmissionDate,
                                                        Name=v.Employee.Name,
                                                        Status = v.Status

                                                    }).ToList();

                

                return listOfPendingVoucherRequests;
            }
        }



    }
}