using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class PlaceOrder : System.Web.UI.Page
    {
        private void BindGrid()
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            PlaceOrderGridView.DataSource = POBizLogic.DataBind(EmpID);
            PlaceOrderGridView.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    int EmpID = (int)HttpContext.Current.Session["EmpID"];
                    PlaceOrderGridView.DataSource = POBizLogic.InitialPO(EmpID);
                    PlaceOrderGridView.DataBind();
                    if (PlaceOrderGridView.Rows.Count == 0)
                    {
                        ConfirmButton.Visible = false;
                    }
                }
            }
        }

        protected void PlaceOrderGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PlaceOrderGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void PlaceOrderGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PlaceOrderGridView.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid();
        }

        protected void PlaceOrderGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            GridViewRow row = PlaceOrderGridView.Rows[e.RowIndex];
            string ItemID = PlaceOrderGridView.DataKeys[e.RowIndex].Values[0].ToString();
            int SupplierID = Convert.ToInt32((row.FindControl("DropDownList1") as DropDownList).SelectedValue);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            POBizLogic.UpdateOrderItem(ItemID, OrderQty, SupplierID, EmpID);

            PlaceOrderGridView.EditIndex = -1;
            BindGrid();
        }

        protected void PlaceOrderGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            string ItemID = PlaceOrderGridView.DataKeys[e.RowIndex].Values[0].ToString();
            POBizLogic.DeleteOrderItem(ItemID, EmpID);
            BindGrid();
        }

        protected void PlaceOrderGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            PlaceOrderGridView.DataSource = POBizLogic.DataBind(EmpID);
            PlaceOrderGridView.PageIndex = e.NewPageIndex;
            PlaceOrderGridView.DataBind();
            BindGrid();
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.PlaceOrder(EmpID);
            BindGrid();
            Response.Write("<script>alert('Order Placed !');</script>");
        }

        protected List<Supplier> GetSupplier(string ItemID)
        {
            return POBizLogic.ListSup(ItemID);
        }

        protected void PlaceOrderGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dropdownCodes = e.Row.Cells[5].FindControl("DropDownList1") as DropDownList;
                if (null != dropdownCodes)
                {
                    // bind it
                    dropdownCodes.DataSource = POBizLogic.ListSup(e.Row.Cells[0].Text);
                    dropdownCodes.DataBind();
                }
            }
        }
    }
}