using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SA47_Team12_StationeryStore.BizLogic
{
    public class AdjustmentBizLogic
    {
        public static List<VoucherListView> ListVouchers(string status)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var voucherlist = context.Voucher.Where(vc => vc.Status.Contains(status)).AsEnumerable()
                .Select(vc => new VoucherListView //.Include(vc => vc.Employee)
                {
                    VoucherID = vc.VoucherID,
                    EmployeeName = vc.Employee.Name,
                    SubmissionDate = String.Format("{0:ddd, MMM d, yyyy}", vc.SubmissionDate),
                    Status = vc.Status,

                }).ToList();

                return voucherlist;
            }
        }

        public static List<VoucherDetailsView> ListVoucherDetails(int id)
        {
            decimal totalamt = 0;

            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var voucherdetail = context.VoucherDetail.Where(vd => vd.VoucherID == id)
                    .Select(vd => new VoucherDetailsView //Include(vd => vd.CatalogueInventory).
                    {
                    VoucherID = vd.VoucherID,
                    ItemName = vd.CatalogueInventory.Item_Description,
                    AdjQty = vd.AdjustedQty,
                    AdjAmt = vd.AdjustedAmt,
                    ItemID = vd.ItemID,
                    ActualQty = vd.CatalogueInventory.ActualQty,
                    Status = vd.Voucher.Status,
                    Reasons = vd.Remarks,
                    Remarks = vd.Voucher.Remarks
                }).ToList();

                return voucherdetail;
            }
        }

        public static void ApproveVoucher(string itemId, int actualqty, int voucherId, DateTime datetime, string remarks, int count)
        {
            string to;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var item = context.CatalogueInventory.Where(c => c.ItemID == itemId).Single();

                item.ActualQty = actualqty;

                var item2 = context.Voucher.Where(c => c.VoucherID == voucherId).Single();
                item2.Status = "Approved";
                item2.ApprovalDate = datetime;
                item2.Remarks = remarks;
                to = item2.Employee.Email;
                
                //update Stockcard
                StockCard sc = new StockCard();
                sc.ItemID = itemId;
                sc.SCCatID = 18001;
                sc.Description = context.Voucher.Where(x => x.VoucherID == voucherId).Select(y => y.Employee.Name).FirstOrDefault();
                //sc.AdjustedQty = actualqty;
                sc.AdjustedQty = context.VoucherDetail.Where(x => x.VoucherID == voucherId && x.ItemID==itemId).Select(y => y.AdjustedQty).FirstOrDefault();
                sc.TransactionDate = DateTime.Now.Date;
                context.StockCard.Add(sc);

                context.SaveChanges();
            }
            if (count == 0)
            {
                //mail to employee who raised request
                String from = "teststationery47@gmail.com";
                String subject = "[Auto Notification] Voucher Status";
                String body = String.Format("Your Voucher {0} has been approved." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", voucherId);
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }
        public static void RejectVoucher(int voucherId, DateTime datetime, string remarks, int count)
        {
            string to;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var item = context.Voucher.Where(c => c.VoucherID == voucherId).Single();
                item.Status = "Rejected";
                item.ApprovalDate = datetime;
                item.Remarks = remarks;
                context.SaveChanges();
                to = item.Employee.Email;
            }

            if (count == 0)
            {
                //mail to employee who raised request
                String from = "teststationery47@gmail.com";
                String subject = "[Auto Notification] Voucher Status";
                String body = String.Format("Sorry! Your Voucher {0} has been rejected." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", voucherId);
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }
        public static void UpdateStatus(int voucherId, int count)
        {
            string to;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                var item = context.Voucher.Where(c => c.VoucherID == voucherId).Single();

                item.Status = "Pending Manager Approval";

                context.SaveChanges();
                to = item.Employee.Email;
            }
            if (count == 0)
            {
                //mail to employee who raised request
                String from = "teststationery47@gmail.com";
                String subject = "[Auto Notification] Voucher Status";
                String body = String.Format("Your voucher {0} has been moved to Manager notice, as the amount of item is more than $250." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", voucherId);
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }
    }
}