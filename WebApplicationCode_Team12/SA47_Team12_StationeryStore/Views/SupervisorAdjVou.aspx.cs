using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class SupervisorAdjVou : System.Web.UI.Page
    {
        public decimal totalamt = 0;
        int VoucherId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            ApproveButton.Visible = false;
            ManagerAppButton.Visible = false;
            RejectButton.Visible = false;
            Label3.Visible = false;
            RemarkTextBox.Visible = false;
            AdjVouDetailsGridView.Visible = false;
            string status = StatusRadioButtonList.SelectedValue;
            AdjVouGridView.DataSource = AdjustmentBizLogic.ListVouchers(status);
            ViewState["status"] = status;

            AdjVouGridView.DataBind();
            if (AdjVouGridView.Rows.Count == 0)
            {
                //Label4.Visible = true;
                //Label4.Text = "No pending requests";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Requests');window.location ='SupervisorAdjVou.aspx';", true);
            }
        }

        protected void AdjVouGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //find which orderID has been clicked
            VoucherId = (int)AdjVouGridView.SelectedDataKey.Value;
            ViewState["vid"] = VoucherId;
            AdjVouDetailsGridView.Visible = true;
            Label3.Visible = true;
            RemarkTextBox.Visible = true;
            AdjVouDetailsGridView.DataSource = AdjustmentBizLogic.ListVoucherDetails(VoucherId);
            AdjVouDetailsGridView.DataBind();
            //ModalPopupExtender.Show();

            if (StatusRadioButtonList.SelectedValue == "Approved")
            {
                ApproveButton.Visible = false;
                ManagerAppButton.Visible = false;
                RejectButton.Visible = false;
                Label3.Visible = false;
                RemarkTextBox.Visible = false;
            }
            else if (StatusRadioButtonList.SelectedValue == "Pending Supervisor Approval")
            {
                ApproveButton.Visible = true;
                ManagerAppButton.Visible = true;
                RejectButton.Visible = true;
                Label3.Visible = true;
                RemarkTextBox.Visible = true;
                AdjVouDetailsGridView.Columns[5].Visible = false;
            }
        }

        protected void AdjVouGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string status = (string)ViewState["status"];
            AdjVouGridView.DataSource = AdjustmentBizLogic.ListVouchers(status);

            AdjVouGridView.PageIndex = e.NewPageIndex;
            AdjVouGridView.DataBind();
            //BindGrid();
        }

        protected void AdjVouDetailsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalamt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AdjAmt"));

                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[4].Text = String.Format("{0}", totalamt);
                }

                bool found = false;
                foreach (GridViewRow row in AdjVouDetailsGridView.Rows)
                {   //loop through each row in the GridView 
                    //for any row where adjamt > 250, will have option to route to manager for approval
                    decimal adjamt = (decimal)AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["AdjAmt"];
                    if (Math.Abs(adjamt) >= 250)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == true)
                {
                    ApproveButton.Enabled = false;
                    ManagerAppButton.Enabled = true;
                    RejectButton.Enabled = false;
                    RemarkTextBox.Enabled = false;
                }
                if (found == false)
                {
                    ApproveButton.Enabled = true;
                    ManagerAppButton.Enabled = false;
                    RejectButton.Enabled = true;
                    RemarkTextBox.Enabled = true;
                }
            }
        }           

        protected void AdjVouDetailsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int VoucherId =(int) ViewState["vid"];
            AdjVouDetailsGridView.DataSource = AdjustmentBizLogic.ListVoucherDetails(VoucherId);

            AdjVouDetailsGridView.PageIndex = e.NewPageIndex;
            AdjVouDetailsGridView.DataBind();
            //BindGrid();
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
        {
                int count = 0;
                string remarks = RemarkTextBox.Text;
                foreach (GridViewRow row in AdjVouDetailsGridView.Rows)
                {
                    string itemId = AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["ItemID"].ToString();
                    int actualqty = (int)AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["ActualQty"];
                    int adjqty = (int)AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["AdjQty"];
                    int voucherId = (int)AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["VoucherID"];
                    DateTime dt = DateTime.Now;
                    actualqty = actualqty + adjqty;
                    AdjustmentBizLogic.ApproveVoucher(itemId, actualqty, voucherId, dt, remarks, count);
                    count++;
                }
            //to generate popup page and then refresh page again
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Voucher Approved');window.location ='SupervisorAdjVou.aspx';", true);
            }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            string remarks = RemarkTextBox.Text;
            foreach (GridViewRow row in AdjVouDetailsGridView.Rows)
            {
                int voucherId = (int)AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["VoucherID"];
                //string remarks = GridView2.DataKeys[row.RowIndex].Values["Remarks"].ToString();
                DateTime dt = DateTime.Now;
                AdjustmentBizLogic.RejectVoucher(voucherId, dt, remarks, count);
                count++;
            }            

            //to generate popup page and then refresh page again
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Voucher Rejected');window.location ='SupervisorAdjVou.aspx';", true);
        }

        protected void ManagerAppButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in AdjVouDetailsGridView.Rows)
            {
                int voucherId = (int)AdjVouDetailsGridView.DataKeys[row.RowIndex].Values["VoucherID"];
                AdjustmentBizLogic.UpdateStatus(voucherId, count);
                count++;
            }

            //mail to manager
            String from = "teststationery47@gmail.com";
            List<String> toAddress = MailBizLogic.ManagerEmail();
            String subject = "[Auto Notification] Voucher Status";
            String body = String.Format("Adjustment Voucher has been moved to your notice, as the amount of item is more than $250." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.");

            foreach (String to in toAddress)
            {
                MailBizLogic.sendMail(from, to, subject, body);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Routed to Manager for approval');window.location ='SupervisorAdjVou.aspx';", true);
        }        
    }
}