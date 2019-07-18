using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class DHReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
        }

        protected void ReportButton_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1003 )
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_Commerce.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1005)
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_Zoology.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1001)
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_English.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1016)
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_Purchasing.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1004)
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_Registrar.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1002)
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_CompSc.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Frequency" && EmpID == 1018)
            {
                Response.Redirect("~/Views/DH_ReqFreqRpt_Store.aspx");
            }

            //this is to geenrate ReqAmtRpt
            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1003)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_Commerce.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1005)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_Zoology.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1001)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_English.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1016)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_Purchasing.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1004)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_Registrar.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1002)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_CompSc.aspx");
            }

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Total Amt" && EmpID == 1018)
            {
                Response.Redirect("~/Views/DH_ReqAmtRpt_Store.aspx");
            }
            //for report sort by date

            else if (DHReportsDropDownList.SelectedValue == "Requests Sort by Date" && EmpID == 1001)
            {
                Response.Redirect("~/Views/DH_ReqSortByDateRpt.aspx");
            }
           
        }
    }
}