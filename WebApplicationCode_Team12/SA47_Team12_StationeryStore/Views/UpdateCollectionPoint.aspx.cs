using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;
using System.Net.Mail;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class UpdateCollectionPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                int DepID = (int)HttpContext.Current.Session["DeptID"];
                if (!this.IsPostBack)
                {
                    BindData(DepID);
                }
            }
        }

        protected bool IsDelegated()
        {
            int empID = (int)HttpContext.Current.Session["EmpID"];
            DateTime today = DateTime.Now.Date;
            StationeryStoreEntities context = new StationeryStoreEntities();

            Employee emp = context.Employee.Where(x => x.EmployeeID == empID).ToList().FirstOrDefault();
            Delegation d = context.Delegation.Where(x => x.DepartmentID == emp.DepartmentID && (x.StartDate <= today && x.EndDate >= today)).FirstOrDefault();
            if (d != null) return true;
            else return false;
        }

        protected void BindData(int DepID)
        {
            Label1.Text = SupplierBizLogic.FindCollectionByDepID(DepID).CollectionPoint.ToString();
            CPRadioButtonList.SelectedValue = SupplierBizLogic.FindCollectionByDepID(DepID).CollectionPointID.ToString();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //int DepID = 2001;
            int DepID = (int)HttpContext.Current.Session["DeptID"];
            int NewCollectionID = Convert.ToInt32(CPRadioButtonList.SelectedValue);

            UserRepCollection updatedDeptURCollection = new UserRepCollection { }; // must declare a variable here first before using it in try block or else not detectable

            try
            {
                updatedDeptURCollection = SupplierBizLogic.UpdateURcollectiontableCollection(DepID, NewCollectionID); // changed this to return a userrepcollection object
                Response.Write("<script>alert('Collection Point updated!');</script>");
            }
            catch (Exception exp)
            {
                Response.Write(exp.ToString());
            }

            // added code below:
            try
            {
                //to clerk
                string oldCollection = Label1.Text;
                String from = "teststationery47@gmail.com";
                List<String> toAddress = MailBizLogic.ClerkEmail();
                String subject = String.Format("[Auto Notification] Changes on Collection Point for {0}", updatedDeptURCollection.Department.Description);
                String body = String.Format("Collection Point For '{0}' has been changed from '{1}' to '{2}'." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", updatedDeptURCollection.Department.Description, oldCollection, updatedDeptURCollection.Collection.Location);

                foreach (String to in toAddress)
                {
                    MailBizLogic.sendMail(from, to, subject, body);
                }
                BindData(DepID);
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
        }
    }
}