using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class CancelDelegation : System.Web.UI.Page
    {
        StationeryStoreEntities context = new StationeryStoreEntities();

        private void BindGrid()
        {          
            int DeptID = (int)HttpContext.Current.Session["DeptID"];
            
            DelegationHistoryGridView.DataSource = SupplierBizLogic.FindDelegation2(DeptID).ToList();
            DelegationHistoryGridView.DataBind();
            DelegationScheduleGridView.DataSource = SupplierBizLogic.FindDelegation(DeptID).ToList();
            DelegationScheduleGridView.DataBind();
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

        protected void DelegationScheduleGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(DelegationScheduleGridView.DataKeys[e.RowIndex].Values[0]);
            GridViewRow row = DelegationScheduleGridView.Rows[e.RowIndex];
            DateTime? start = SupplierBizLogic.findStartDate(Id);
            DateTime? end;
            if (start >= DateTime.Now.Date)
            {
                SupplierBizLogic.DeleteDelegation(Id);
                this.BindGrid();
            }
            else
            {
                end = DateTime.Now.Date.AddDays(-1);
                SupplierBizLogic.UpdateDelegation(Id, end);
            }
        }

        protected void DelegationScheduleGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != DelegationScheduleGridView.EditIndex)
            {
                (e.Row.Cells[4].Controls[0] as LinkButton).Attributes["onclick"] =
                    "return confirm('Do you want to Cancel this Delegation?');";
            }

        }

        protected void DelegationHistoryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //adin for page index
            int DeptID = (int)HttpContext.Current.Session["DeptID"];
            DelegationHistoryGridView.DataSource = SupplierBizLogic.FindDelegation2(DeptID).ToList();

            DelegationHistoryGridView.PageIndex = e.NewPageIndex;
            DelegationHistoryGridView.DataBind();
        }

        protected void DelegationScheduleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //adin for page index
            int DeptID = (int)HttpContext.Current.Session["DeptID"];
            DelegationScheduleGridView.DataSource = SupplierBizLogic.FindDelegation(DeptID).ToList();

            DelegationScheduleGridView.PageIndex = e.NewPageIndex;
            DelegationScheduleGridView.DataBind();
        }
    }
}