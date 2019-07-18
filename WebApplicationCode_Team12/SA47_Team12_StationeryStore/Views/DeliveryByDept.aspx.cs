using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class DeliveryByDept : System.Web.UI.Page
    {
        static string status = "Today's Delivery";
        protected void BindGrid(string status)
        {
            HideGV();
            foreach (DepartmentInfo d in POBizLogic.DepartmentList(status))
            {
                ContentPlaceHolder placeholder;
                placeholder = (ContentPlaceHolder)this.Page.Master.FindControl("MainContent");
                if (placeholder != null)
                {
                    (placeholder.FindControl("DeliveryDepTable" + d.DepartmentID) as GridView).DataSource = POBizLogic.BindDisbursementByEmp(d.DepartmentID, status);
                    (placeholder.FindControl("DeliveryDepTable" + d.DepartmentID) as GridView).DataBind();
                    (placeholder.FindControl("DeliveryDepTable" + d.DepartmentID) as GridView).Visible = true;
                    (placeholder.FindControl("Confirm" + d.DepartmentID) as Button).Visible = true;
                    status = StatusRadioButtonList.SelectedValue;
                    if (status == "Upcoming Delivery")
                        (placeholder.FindControl("Confirm" + d.DepartmentID) as Button).Enabled = false;
                    else (placeholder.FindControl("Confirm" + d.DepartmentID) as Button).Enabled = true;
                    (placeholder.FindControl("Dep" + d.DepartmentID) as Label).Text = d.Description;
                    (placeholder.FindControl("CP" + d.DepartmentID) as Label).Text = d.Collection;
                    (placeholder.FindControl("UR" + d.DepartmentID) as Label).Text = d.UserPresentative;
                    (placeholder.FindControl("Dep" + d.DepartmentID) as Label).Visible = true;
                    (placeholder.FindControl("CP" + d.DepartmentID) as Label).Visible = true;
                    (placeholder.FindControl("UR" + d.DepartmentID) as Label).Visible = true;
                    (placeholder.FindControl("LabelDep" + d.DepartmentID) as Label).Visible = true;
                    (placeholder.FindControl("LabelCP" + d.DepartmentID) as Label).Visible = true;
                    (placeholder.FindControl("LabelUR" + d.DepartmentID) as Label).Visible = true;

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    BindGrid(status);
                }
            }
        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            status = StatusRadioButtonList.SelectedValue;
            BindGrid(status);
        }

        protected void DeliveryDepTable2001_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2001.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2001_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2001.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2001_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2001.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2001.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2001.EditIndex = -1;
            BindGrid(status);
        }

        protected void DeliveryDepTable2001_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2001.PageIndex = e.NewPageIndex;
            DeliveryDepTable2001.DataBind();
            BindGrid(status);
        }

        protected void Confirm2001_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2001);
            BindGrid(status);
        }

        protected void DeliveryDepTable2001_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }

        protected void DeliveryDepTable2002_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2002.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2002_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2002.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2002_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2002.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2002.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2002.EditIndex = -1;
            BindGrid(status);
        }

        protected void DeliveryDepTable2002_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }

        protected void DeliveryDepTable2002_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2002.PageIndex = e.NewPageIndex;
            DeliveryDepTable2002.DataBind();
            BindGrid(status);
        }

        protected void Confirm2002_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2002);
            BindGrid(status);
        }

        protected void DeliveryDepTable2003_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2003.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2003_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2003.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2003_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2003.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2003.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2003.EditIndex = -1;
            BindGrid(status);
        }

        protected void DeliveryDepTable2003_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }

        protected void DeliveryDepTable2003_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2003.PageIndex = e.NewPageIndex;
            DeliveryDepTable2003.DataBind();
            BindGrid(status);
        }

        protected void Confirm2003_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2003);
            BindGrid(status);
        }

        protected void DeliveryDepTable2004_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2004.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2004_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2004.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2004_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2004.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2004.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2004.EditIndex = -1;
            BindGrid(status);
        }
        protected void DeliveryDepTable2004_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }
        protected void DeliveryDepTable2004_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2004.PageIndex = e.NewPageIndex;
            DeliveryDepTable2004.DataBind();
            BindGrid(status);
        }

        protected void Confirm2004_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2004);
            BindGrid(status);
        }

        protected void DeliveryDepTable2005_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2005.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2005_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2005.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2005_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2005.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2005.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2005.EditIndex = -1;
            BindGrid(status);
        }
        protected void DeliveryDepTable2005_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }
        protected void DeliveryDepTable2005_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2005.PageIndex = e.NewPageIndex;
            DeliveryDepTable2005.DataBind();
            BindGrid(status);
        }

        protected void Confirm2005_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2005);
            BindGrid(status);
        }

        protected void DeliveryDepTable2006_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2006.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2006_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2006.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2006_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2006.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2006.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2006.EditIndex = -1;
            BindGrid(status);
        }
        protected void DeliveryDepTable2006_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }
        protected void DeliveryDepTable2006_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2006.PageIndex = e.NewPageIndex;
            DeliveryDepTable2006.DataBind();
            BindGrid(status);
        }

        protected void Confirm2006_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2006);
            BindGrid(status);
        }

        protected void DeliveryDepTable2007_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DeliveryDepTable2007.EditIndex = e.NewEditIndex;
            BindGrid(status);
        }

        protected void DeliveryDepTable2007_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DeliveryDepTable2007.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid(status);
        }

        protected void DeliveryDepTable2007_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = DeliveryDepTable2007.Rows[e.RowIndex];
            int DisbursementID = Convert.ToInt32(DeliveryDepTable2007.DataKeys[e.RowIndex].Values[0]);
            int OrderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            if (!POBizLogic.UpdateDepDisbursement(DisbursementID, OrderQty))
                Response.Write("<script>alert('The Qty cannot be over the original requests!');</script>");
            DeliveryDepTable2007.EditIndex = -1;
            BindGrid(status);
        }
        protected void DeliveryDepTable2007_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Redirect to AdjustVoucher Page after clicking adjustvoucher button for an item
            if (e.CommandName == "AdjustVoucher")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //string itemCode = (row.FindControl("labelItemId_TemplateField") as Label).Text;
                string ItemID = row.Cells[0].Text;
                Response.Redirect(string.Format("~/Views/RequestVoucherDetails.aspx?Id={0}", ItemID));
            }
        }
        protected void DeliveryDepTable2007_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DeliveryDepTable2007.PageIndex = e.NewPageIndex;
            DeliveryDepTable2007.DataBind();
            BindGrid(status);
        }

        protected void Confirm2007_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            POBizLogic.ConfirmDeliveryToDep(EmpID, 2007);
            BindGrid(status);
        }

        protected void HideGV()
        {
            LabelDep2001.Visible = false;
            LabelCP2001.Visible = false;
            LabelUR2001.Visible = false;
            DeliveryDepTable2001.Visible = false;
            Confirm2001.Visible = false;
            Dep2001.Visible = false;
            CP2001.Visible = false;
            UR2001.Visible = false;

            LabelDep2002.Visible = false;
            LabelCP2002.Visible = false;
            LabelUR2002.Visible = false;
            DeliveryDepTable2002.Visible = false;
            Confirm2002.Visible = false;
            Dep2002.Visible = false;
            CP2002.Visible = false;
            UR2002.Visible = false;

            LabelDep2003.Visible = false;
            LabelCP2003.Visible = false;
            LabelUR2003.Visible = false;
            DeliveryDepTable2003.Visible = false;
            Confirm2003.Visible = false;
            Dep2003.Visible = false;
            CP2003.Visible = false;
            UR2003.Visible = false;

            LabelDep2004.Visible = false;
            LabelCP2004.Visible = false;
            LabelUR2004.Visible = false;
            DeliveryDepTable2004.Visible = false;
            Confirm2004.Visible = false;
            Dep2004.Visible = false;
            CP2004.Visible = false;
            UR2004.Visible = false;

            LabelDep2005.Visible = false;
            LabelCP2005.Visible = false;
            LabelUR2005.Visible = false;
            DeliveryDepTable2005.Visible = false;
            Confirm2005.Visible = false;
            Dep2005.Visible = false;
            CP2005.Visible = false;
            UR2005.Visible = false;

            LabelDep2006.Visible = false;
            LabelCP2006.Visible = false;
            LabelUR2006.Visible = false;
            DeliveryDepTable2006.Visible = false;
            Confirm2006.Visible = false;
            Dep2006.Visible = false;
            CP2006.Visible = false;
            UR2006.Visible = false;

            LabelDep2007.Visible = false;
            LabelCP2007.Visible = false;
            LabelUR2007.Visible = false;
            DeliveryDepTable2007.Visible = false;
            Confirm2007.Visible = false;
            Dep2007.Visible = false;
            CP2007.Visible = false;
            UR2007.Visible = false;
        }
    }
}