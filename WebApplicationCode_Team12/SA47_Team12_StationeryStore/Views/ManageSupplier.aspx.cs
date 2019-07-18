using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class ManageSupplier : System.Web.UI.Page
    {
        StationeryStoreEntities context = new StationeryStoreEntities();

        private void BindGrid()
        {
            SupplierGridView.DataSource = SupplierBizLogic.ListSupplier();
            SupplierGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!this.IsPostBack)
                {
                    this.BindGrid();
                }
            }
        }

        protected void SupplierGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SupplierGridView.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void SupplierGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SupplierGridView.EditIndex = -1;
            this.BindGrid();
        }

        protected void SupplierGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = SupplierGridView.Rows[e.RowIndex];
            int SupplierID = Convert.ToInt32(SupplierGridView.DataKeys[e.RowIndex].Values[0]);

            //string name = (row.FindControl("Textbox1") as TextBox).Text;
            string address = (row.FindControl("Textbox2") as TextBox).Text;
            string phone = (row.FindControl("Textbox3") as TextBox).Text;

            SupplierBizLogic.UpdateSupplier(SupplierID, address, phone);

            SupplierGridView.EditIndex = -1;
            this.BindGrid();
        }

        protected void SupplierGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SupplierGridView.DataSource = SupplierBizLogic.ListSupplier();
            SupplierGridView.PageIndex = e.NewPageIndex;
            SupplierGridView.DataBind();
            BindGrid();
        }
    }
}