using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class UpdateItemSupplier : System.Web.UI.Page
    {
        StationeryStoreEntities context = new StationeryStoreEntities();
        string ItemID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");      
        }

        protected void ItemDropDownList_DataBound(object sender, EventArgs e)
        {
            DropDownList list = sender as DropDownList;
            if (list != null)
                list.Items.Insert(0, "--Select--");
        }

        protected void GoButton_Click(object sender, EventArgs e)
        {
            ItemID = ItemDropDownList.SelectedItem.Value;
            Label1.Text = "Current 1st Priorty Supplier : " + SupplierBizLogic.FindItemSupplierByID(ItemID).P1Supplier;
            Label2.Text = "Current 2nd Priorty Supplier : " + SupplierBizLogic.FindItemSupplierByID(ItemID).P2Supplier;
            Label3.Text = "Current 3rd Priorty Supplier : " + SupplierBizLogic.FindItemSupplierByID(ItemID).P3Supplier;
            Label4.Text = "Select for your updates :";
            Label5.Text = "Select for your updates :";
            Label6.Text = "Select for your updates :";
            Priority1DropDownList.Visible = true;
            Priority2DropDownList.Visible = true;
            Priority3DropDownList.Visible = true;
            UpdateButton.Visible = true;
        }

        protected void Priority1DropDownList_DataBound(object sender, EventArgs e)
        {
            DropDownList list = sender as DropDownList;
            if (list != null)
                list.Items.Insert(0, "--Select 1st Priority--");
        }

        protected void Priority2DropDownList_DataBound(object sender, EventArgs e)
        {
            DropDownList list = sender as DropDownList;
            if (list != null)
                list.Items.Insert(0, "--Select 2nd Priority--");
        }

        protected void Priority3DropDownList_DataBound(object sender, EventArgs e)
        {
            DropDownList list = sender as DropDownList;
            if (list != null)
                list.Items.Insert(0, "--Select 3rd Priority--");
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int Priority1 = Convert.ToInt32(Priority1DropDownList.SelectedItem.Value);
            int Priority2 = Convert.ToInt32(Priority2DropDownList.SelectedItem.Value);
            int Priority3 = Convert.ToInt32(Priority3DropDownList.SelectedItem.Value);

            try
            {
                ItemID = ItemDropDownList.SelectedItem.Value;
                SupplierBizLogic.UpdateItemSupplier(ItemID, Priority1, Priority2, Priority3);
                //Response.Redirect("Default2.aspx?username=" + user);
                Response.Write("<script>alert('Suppliers Priority has been updated !');</script>");
                Label1.Text = "";
                Label2.Text = "";
                Label3.Text = "";
                Label4.Text = "";
                Label5.Text = "";
                Label6.Text = "";
                ItemDropDownList.SelectedIndex = 0;
                Priority1DropDownList.Visible = false;
                Priority2DropDownList.Visible = false;
                Priority3DropDownList.Visible = false;

                //send mail to clerk
                String from = "teststationery47@gmail.com";
                List<String> toAddress = MailBizLogic.ClerkEmail();
                String subject = "[Auto Notification] Item supplier update";
                String body = String.Format("Supplier's priority has been changed for item {0}. Check website for further details." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", ItemID);

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