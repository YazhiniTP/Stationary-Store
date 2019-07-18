using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class ManageOwnRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    StatusRadioButtonList.DataSource = RequestBizLogic.statusList();
                    StatusRadioButtonList.DataBind();
                }
            }
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            string value = StatusRadioButtonList.SelectedValue;
            ViewState["value"] = value;
            if (value == "Pending")
            {
                PendingRequestGridView.DataSource = RequestBizLogic.ViewPendingRequest(EmpID);//hardcord employeeId for testing
                PendingRequestGridView.DataBind();
                RequestGridView.Columns[3].Visible = false;
                RequestGridView.Columns[4].Visible = false;
                PendingRequestGridView.Visible = true;
                RequestGridView.Visible = false;
                RequestDetailsGridView.Visible = false;
                //DeleteButton.Visible = true;
            }
            if (value == "Approved")
            {
                RequestGridView.DataSource = RequestBizLogic.ViewApprovedRequest(EmpID);//hardcord employeeId for testing
                RequestGridView.DataBind();
                RequestGridView.Visible = true;
                PendingRequestGridView.Visible = false;
                RequestDetailsGridView.Visible = false;
                //DeleteButton.Visible = false;
            }
            if (value == "Rejected")
            {
                RequestGridView.DataSource = RequestBizLogic.ViewRejectedRequest(EmpID);//hardcord employeeId for testing
                RequestGridView.DataBind();
                RequestGridView.Columns[3].Visible = false;
                RequestGridView.Visible = true;
                PendingRequestGridView.Visible = false;
                RequestDetailsGridView.Visible = false;
                //DeleteButton.Visible = false;
            }
        }

        protected void RequestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false; //hiding RequestId
            //e.Row.Cells[1].Visible = false; //hiding EmployeeName
            //e.Row.Cells[4].Visible = false; //hiding ApprovalDate
        }

        protected void RequestGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            string value = (string)ViewState["value"];
            if (value == "Approved")
            {
                RequestGridView.DataSource = RequestBizLogic.ViewApprovedRequest(EmpID);//hardcord employeeId for testing
            }
            if (value == "Rejected")
            {
                RequestGridView.DataSource = RequestBizLogic.ViewRejectedRequest(EmpID);//hardcord employeeId for testing
            }
            RequestGridView.PageIndex = e.NewPageIndex;
            RequestGridView.DataBind();
        }

        protected void ButtonDeleteRequest_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            RequestBizLogic.DeletePendingRequest(EmpID);//hardcord for testing
            RequestGridView.Visible = false;
        }

        protected void RequestGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

            int RequestID = (int)RequestGridView.SelectedDataKey.Value;
            ViewState["rid"] = (int)RequestID;
            RequestDetailsGridView.Visible = true;
            RequestDetailsGridView.DataSource = RequestBizLogic.ListRequestDetails(RequestID);
            RequestDetailsGridView.DataBind();
        }

        protected void PendingRequestGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RequestID = (int)PendingRequestGridView.SelectedDataKey.Value;
            ViewState["rid"] = (int)RequestID;
            RequestDetailsGridView.Visible = true;
            RequestDetailsGridView.DataSource = RequestBizLogic.ListRequestDetails(RequestID);
            RequestDetailsGridView.DataBind();
        }

        protected void PendingRequestGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];

            int requestID = Convert.ToInt32(PendingRequestGridView.DataKeys[e.RowIndex].Values[0]);
            RequestBizLogic.DeleteSelectedPendingRequest(requestID);
            PendingRequestGridView.DataSource = RequestBizLogic.ViewPendingRequest(EmpID);
            PendingRequestGridView.DataBind();
            RequestDetailsGridView.Visible = false;
        }

        protected void PendingRequestGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            PendingRequestGridView.DataSource = RequestBizLogic.ViewPendingRequest(EmpID);
            PendingRequestGridView.PageIndex = e.NewPageIndex;
            PendingRequestGridView.DataBind();
        }

        protected void RequestDetailsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int requestID = (int)ViewState["rid"];
            RequestDetailsGridView.DataSource = RequestBizLogic.ListRequestDetails(requestID);
            RequestDetailsGridView.PageIndex = e.NewPageIndex;
            RequestDetailsGridView.DataBind();
        }
    }
}