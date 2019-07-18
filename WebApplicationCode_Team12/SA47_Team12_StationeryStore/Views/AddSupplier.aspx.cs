using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class AddSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
        }

        protected void AddNewSupButton_Click(object sender, EventArgs e)
        {
            string name = SupplierName.Text;
            string add = SupplierAdd.Text;
            string phone = SupplierPhone.Text;

            {
                try
                {
                    SupplierBizLogic.AddSupplier(name, add, phone);
                    //Response.Redirect("Default2.aspx?username=" + user);
                    Response.Write("<script>alert('New Supplier has been Added!');</script>");
                    SupplierName.Text = "";
                    SupplierAdd.Text = "";
                    SupplierPhone.Text = "";

                    //send mail to clerk
                    String from = "teststationery47@gmail.com";
                    List<String> toAddress = MailBizLogic.ClerkEmail();
                    String subject = "[Auto Notification] Added new supplier";
                    String body = String.Format("New supplier {0} has been added. Check website for further details." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", name);

                    foreach (String to in toAddress)
                    {
                        MailBizLogic.sendMail(from, to, subject, body);
                    }
                }
                catch (Exception exp)
                {
                    Response.Write(exp.ToString());
                }
            }
        }
    }
}