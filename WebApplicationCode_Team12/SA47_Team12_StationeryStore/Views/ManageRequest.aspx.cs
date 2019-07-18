using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class ManageRequest : System.Web.UI.Page
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

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            RequestDetailsGridView.Visible = false;
            RejectButton.Visible = false;
            ApproveButton.Visible = false;
            RemarksTextBox.Visible = false;
            Label3.Visible = false;
            string status = StatusRadioButtonList.SelectedValue;
            ViewState["status"] = status;

            RequestGridView.DataSource = RequestBizLogic.ListRequests(status, EmpID);

            if (StatusRadioButtonList.SelectedValue == "Approved" || StatusRadioButtonList.SelectedValue == "Rejected")
            {
                RequestGridView.Columns[5].Visible = true;
                //GridView1.Columns[6].Visible = true;
            }
            else if (StatusRadioButtonList.SelectedValue == "Pending")
            {
                RequestGridView.Columns[5].Visible = false;
                //GridView1.Columns[6].Visible = false;
            }
            RequestGridView.DataBind();
            if (RequestGridView.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Requests');window.location ='ManageRequest.aspx';", true);
            }
        }

        protected void RequestGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //find which requestID has been clicked
            int RequestID = (int)RequestGridView.SelectedDataKey.Value;
            ViewState["rid"] = (int)RequestID;
            RequestDetailsGridView.Visible = true;
            RequestDetailsGridView.DataSource = RequestBizLogic.ListRequestDetails(RequestID);
            RequestDetailsGridView.DataBind();
            if (StatusRadioButtonList.SelectedValue == "Approved" || StatusRadioButtonList.SelectedValue == "Rejected")
            {
                RejectButton.Visible = false;
                ApproveButton.Visible = false;
            }
            else if (StatusRadioButtonList.SelectedValue == "Pending")
            {
                RejectButton.Visible = true;
                ApproveButton.Visible = true;
                RemarksTextBox.Visible = true;
                Label3.Visible = true;
            }
        }

        protected void RequestGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            string status = (string) ViewState["status"];
            RequestGridView.DataSource = RequestBizLogic.ListRequests(status, EmpID);

            RequestGridView.PageIndex = e.NewPageIndex;
            RequestGridView.DataBind();
        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            string remarks = RemarksTextBox.Text;
            int count = 0;
            foreach (GridViewRow row in RequestDetailsGridView.Rows)
            {
                int RequestId = (int)RequestDetailsGridView.DataKeys[row.RowIndex].Values["Id"];

                DateTime dt = DateTime.Now;
                RequestBizLogic.RejectRequest(RequestId, dt, remarks,count);
                count++;
            }

            //to generate popup page and then refresh page again
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Rejected');window.location ='ManageRequest.aspx';", true);
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            string remarks = RemarksTextBox.Text;
            int count = 0;
            foreach (GridViewRow row in RequestDetailsGridView.Rows)
            {
                int RequestId = (int)RequestDetailsGridView.DataKeys[row.RowIndex].Values["Id"];
                DateTime dt = DateTime.Now;

                RequestBizLogic.ApproveRequest(RequestId, dt, remarks,count);
                count++;
            }

            //to generate popup page and then refresh page again
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Approved');window.location ='ManageRequest.aspx';", true);
        }

        protected void RequestDetailsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int RequestID=(int)ViewState["rid"];
            RequestDetailsGridView.DataSource = RequestBizLogic.ListRequestDetails(RequestID);
            RequestDetailsGridView.PageIndex = e.NewPageIndex;
            RequestDetailsGridView.DataBind();
        }
    }
}