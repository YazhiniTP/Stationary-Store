using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class ScheduleEmail
    {
        private List<CatalogueInventoryViewModel> listOfLowStockItems;

        public ScheduleEmail()
        { }

        public void SendScheduleMail(List<CatalogueInventoryViewModel> list) // argument: List<CatalogueInventoryViewModel> list
        {
            //configure smtpClient
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "teststationery47@gmail.com",
                Password = "rFEq8D6UnwV9"
            };

            foreach (CatalogueInventoryViewModel c in list)
            {
                List<String> toAddress = MailBizLogic.ClerkEmail();
                foreach (String to in toAddress)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(to);
                    mailMessage.From = new MailAddress("teststationery47@gmail.com");
                    mailMessage.Subject = String.Format("[Auto Notification] Item {0} is low in stock - {1}", c.ItemID, c.ItemDescription);
                    mailMessage.Body = String.Format(" Item {0} is low in stock - {1}" +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", c.ItemID, c.ItemDescription);

                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }                
            }
        }

        public void SendScheduleMail(List<UserRepCollection> list) // argument: List<CatalogueInventoryViewModel> list
        {
            //configure smtpClient
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "teststationery47@gmail.com",
                Password = "rFEq8D6UnwV9"
            };

            //foreach (CatalogueInventoryViewModel c in list)
            //{
            //    MailMessage mailMessage = new MailMessage();
            //    mailMessage.To.Add("teststationery47@gmail.com");
            //    mailMessage.From = new MailAddress("teststationery47@gmail.com");
            //    mailMessage.Subject = String.Format("Item {0} is low in stock - {1}", c.ItemNo, c.ItemDescription);
            //    mailMessage.Body = String.Format(" Item {0} is low in stock - {1}", c.ItemNo, c.ItemDescription);

            //    smtpClient.EnableSsl = true;
            //    smtpClient.Send(mailMessage);
            //}
        }
    }
}