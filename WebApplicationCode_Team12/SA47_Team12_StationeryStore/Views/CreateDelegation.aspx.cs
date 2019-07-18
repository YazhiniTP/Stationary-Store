using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class CreateDelegation : System.Web.UI.Page
    {
        static DateTime d;
        static DateTime d2;
        static int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!this.IsPostBack)
                {
                    int EmpID = (int)HttpContext.Current.Session["EmpID"];
                    int DeptID = (int)HttpContext.Current.Session["DeptID"];
                    SelectStaffDropDownList.DataSource = SupplierBizLogic.FindEmpByDepID2(DeptID, EmpID);
                    SelectStaffDropDownList.DataTextField = "Name";
                    SelectStaffDropDownList.DataValueField = "EmployeeID";
                    SelectStaffDropDownList.DataBind();
                }
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (DateRadioButtonList.SelectedValue == "Select Start Date")
            {

                if (count == 0)
                {
                    if (Calendar1.SelectedDate >= DateTime.Now.Date)
                    {
                        Label1.Text = String.Format("{0:ddd, MMM d, yyyy}", Calendar1.SelectedDate);
                        d = Calendar1.SelectedDate;
                        Label3.Text = "Your Selected Start Date : ";
                    }
                    else
                    {
                        Label3.Text = null;
                        Label1.Text = "Invalid Selection";
                    }
                }
                if (count > 0)
                {
                    if (Calendar1.SelectedDate > d2)
                    {
                        Label1.Text = "Invalid Selection";
                        Label3.Text = null;
                    }
                    else
                    {
                        if (Calendar1.SelectedDate <= d2 && Calendar1.SelectedDate >= DateTime.Now.Date)
                        {
                            Label1.Text = String.Format("{0:ddd, MMM d, yyyy}", Calendar1.SelectedDate);
                            
                            d = Calendar1.SelectedDate;
                            Label3.Text = "Your Selected Start Date : ";
                        }
                        else
                        {
                            Label3.Text = null;
                            Label1.Text = "Invalid Selection";
                        }
                    }
                }
            }
            if (DateRadioButtonList.SelectedValue == "Select End Date")
            {

                if (Calendar1.SelectedDate >=d && Calendar1.SelectedDate>DateTime.Today)
                {
                    Label2.Text = String.Format("{0:ddd, MMM d, yyyy}", Calendar1.SelectedDate);
                    Label4.Text = "Your Selected End Date : ";
                    d2 = Calendar1.SelectedDate;
                    count++;
                }
                else
                {
                    Label2.Text = "Invalid Selection";
                    Label4.Text = null;
                }
            }
        }

        protected void SelectStaffDropDownList_DataBound(object sender, EventArgs e)
        {
            DropDownList list = sender as DropDownList;
            if (list != null)
                list.Items.Insert(0, "--Select--");
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Label1.Text != "Invalid Selection" && Label2.Text != "Invalid Selection")
            {                
                int DeptID = (int)HttpContext.Current.Session["DeptID"];
                
                int EmpID = Convert.ToInt32(SelectStaffDropDownList.SelectedValue);
                DateTime start = Convert.ToDateTime(Label1.Text);
                DateTime end = Convert.ToDateTime(Label2.Text);
                if (SupplierBizLogic.IsDuplicate(DeptID, start, end))
                {
                    Response.Write("<script>alert('The selected dates already assigned !');</script>");
                    Label1.Text = "";
                    Label2.Text = "";
                    Label3.Text = "";
                    Label4.Text = "";
                    Calendar1.SelectedDate = DateTime.Now.Date;
                    SelectStaffDropDownList.SelectedIndex = 0;
                    DateRadioButtonList.SelectedIndex = 0;
                    count = 0;
                }
                else
                {
                    SupplierBizLogic.CreateDelegation(EmpID, start, end, DeptID);
                    Response.Write("<script>alert('Job Delegated !');</script>");
                    Label1.Text = "";
                    Label2.Text = "";
                    Label3.Text = "";
                    Label4.Text = "";
                    Calendar1.SelectedDate = DateTime.Now.Date;
                    SelectStaffDropDownList.SelectedIndex = 0;
                    DateRadioButtonList.SelectedIndex = 0;

                    
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Selection');</script>");
            }
        }
    }
}