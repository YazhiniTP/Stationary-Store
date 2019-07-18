using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class SupervisorReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
        }

        protected void ReportButton_Click(object sender, EventArgs e)
        {
            if (SupReportsDropDownList.SelectedValue == "Item Stock")
            {
                Response.Redirect("~/Views/Clerk_ItemStockRpt.aspx");
            }

            else if (SupReportsDropDownList.SelectedValue == "Item Ordered")
            {
                Response.Redirect("~/Views/ClerkSup_ItemOrderedRpt.aspx");
            }

            else if (SupReportsDropDownList.SelectedValue == "Item Requests (Across Dept)")
            {
                Response.Redirect("~/Views/ClerkSup_ItemReqQtyBreakdownRpt.aspx");
            }

            else if (SupReportsDropDownList.SelectedValue == "Item Requests (By Dept)")
            {
                Response.Redirect("~/Views/ClerkSup_ItemReqBreakdownByDeptRpt.aspx");
            }
        }
    }
}