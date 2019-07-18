using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class PendingPO : System.Web.UI.Page
    {
        private void BindGrid()
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            PendingOrderGridView.DataSource = POBizLogic.ListPendingPOByDate(EmpID);
            PendingOrderGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
        }

        private void BindGridBySup(int Supplier)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            PendingOrderGridView.DataSource = POBizLogic.ListPendingPOBySupplier(EmpID, Supplier);
            PendingOrderGridView.DataBind();
        }

        protected void PendingOrderGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
            PendingOrderGridView.PageIndex = e.NewPageIndex;
            PendingOrderGridView.DataBind();
        }

        protected void AllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Supplier1_CheckedChanged(object sender, EventArgs e)
        {
            BindGridBySup(13001);
        }

        protected void Supplier2_CheckedChanged(object sender, EventArgs e)
        {
            BindGridBySup(13002);
        }

        protected void Supplier3_CheckedChanged(object sender, EventArgs e)
        {
            BindGridBySup(13003);
        }

        protected void Supplier4_CheckedChanged(object sender, EventArgs e)
        {
            BindGridBySup(13004);
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            int POID = Convert.ToInt32(PendingOrderGridView.DataKeys[gvr.RowIndex].Values[0]);
            POBizLogic.ConfirmDeliveryFromSupplier(EmpID, POID);
            if (AllSupplier.Checked)
                BindGrid();
            else if (Supplier1.Checked)
                BindGridBySup(13001);
            else if (Supplier2.Checked)
                BindGridBySup(13002);
            else if (Supplier3.Checked)
                BindGridBySup(13003);
            else BindGridBySup(13004);

            Response.Write("<script>alert('Sent delivery confirmation!');</script>");
        }

        protected void SelectSupButton_Click(object sender, EventArgs e)
        {
            if (AllSupplier.Checked)
                BindGrid();
            else if (Supplier1.Checked)
                BindGridBySup(13001);
            else if (Supplier2.Checked)
                BindGridBySup(13002);
            else if (Supplier3.Checked)
                BindGridBySup(13003);
            else BindGridBySup(13004);
        }
    }
}