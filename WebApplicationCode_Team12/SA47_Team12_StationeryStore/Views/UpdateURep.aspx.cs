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
    public partial class UpdateURep : System.Web.UI.Page
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
            Label2.Text = SupplierBizLogic.FindCollectionByDepID(DepID).EmployeeName.ToString();

            URDropDownList.DataSource = SupplierBizLogic.FindEmpByDepID(DepID);

            URDropDownList.DataTextField = "Name";
            URDropDownList.DataValueField = "EmployeeID";
            URDropDownList.DataBind();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int DepID = (int)HttpContext.Current.Session["DeptID"];
            int NewEmployeeID = Convert.ToInt32(URDropDownList.SelectedValue); //changed from CPRadioButtonList to URDropDownList
            int OldEmployeeID = Convert.ToInt32(SupplierBizLogic.FindCollectionByDepID(DepID).EmployeeID);
            UserRepCollection updatedDeptURCollection = new UserRepCollection { }; // must declare a variable here first before using it in try block or else not detectable

            try
            {
                SupplierBizLogic.UpdateNewURinEmptable(NewEmployeeID);
                SupplierBizLogic.UpdateOldURinEmptable(OldEmployeeID);
                updatedDeptURCollection = SupplierBizLogic.UpdateURcollectiontableUR(DepID, NewEmployeeID);
                Response.Write("<script>alert('User Representative changed!');</script>");
            }
            catch (Exception exp)
            {
                Response.Write(exp.ToString());
            }

            // added code below:
            try
            {
                String from = "teststationery47@gmail.com";
                List<String> toAddress = MailBizLogic.ClerkEmail();
                String subject = String.Format("[Auto Notification] Changes on User Representative for {0}", updatedDeptURCollection.Department.Description);
                String body = String.Format("New User Representative For '{0}' has been assigned to {1}." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", updatedDeptURCollection.Department.Description, updatedDeptURCollection.Employee.Name);

                foreach (String to in toAddress)
                {
                    MailBizLogic.sendMail(from, to, subject, body);
                }
                Label2.Text = SupplierBizLogic.FindCollectionByDepID(DepID).EmployeeName.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
        }
    }
}