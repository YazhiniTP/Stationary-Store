using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class Disbursement : System.Web.UI.Page
    {
        private void BindGrid()
        {
            DisbursementGridView.DataSource = POBizLogic.BindDisbursement();
            DisbursementGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    POBizLogic.GenerateDisbursementwithOuts();
                    BindGrid();
                    if (DisbursementGridView.Rows.Count == 0)
                    {
                        ProceedToDeliverButton.Visible = false;
                    }
                }
            }
        }

        protected void DisbursementGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DisbursementGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void DisbursementGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DisbursementGridView.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid();
        }

        protected void DisbursementGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DisbursementGridView.Rows[e.RowIndex];
            string ItemID = DisbursementGridView.DataKeys[e.RowIndex].Values[0].ToString();
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDisbursement(ItemID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");

            DisbursementGridView.EditIndex = -1;
            BindGrid();
        }

        protected void DisbursementGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                string ItemID = row.Cells[0].Text;
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }

        protected void DisbursementGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DisbursementGridView.DataSource = POBizLogic.BindDisbursement();
            DisbursementGridView.PageIndex = e.NewPageIndex;
            DisbursementGridView.DataBind();
            BindGrid();
        }

        protected void ProceedToDeliverButton_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ScheduleDelivery(EmpID);
            BindGrid();
            Response.Redirect("~/Views/DeliveryByDept.aspx");
        }
    }
}