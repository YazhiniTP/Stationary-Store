using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class RaiseRequest : System.Web.UI.Page
    {
        static string searchBy = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else
            {
                int EmpID = (int)HttpContext.Current.Session["EmpID"];

                ViewState["qty"] = Convert.ToInt32(TextBoxQty.Text);
                if (!IsPostBack)
                {
                    HideButton();
                }
                if (searchBy != "") ;

                else
                {
                    ItemListGridView.DataSource = RequestBizLogic.ViewAll();
                    ItemListGridView.DataBind();
                }
                ViewGV();
                HideGVCart();
            }
        }

        protected void ItemListGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = (string)ItemListGridView.SelectedValue; //ItemListGridView.SelectedDataKey.Value; //ItemListGridView.DataKeys[ItemListGridView.SelectedRow.RowIndex].Values[0].ToString();
                                                                //ViewState["Itemid"] = id; 
                                                                //string id = Requestid.ToString();
                                                                //Response.Redirect("DHViewDetails.aspx?id=" + id);
            HideGV();
            List<String> list = RequestBizLogic.ValueCatalogue(id);
            LabelDescDisplay.Text = list[0];
            LabelCatDisplay.Text = list[1];
            LabelUoMDisplay.Text = list[2];
            ViewState["itemid"] = list[3];
            ViewButton();
            TextBoxQty.Text = 1.ToString();
        }

        protected void ItemListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ItemListGridView.DataSource = RequestBizLogic.SearchCatalogue(searchBy);
            ItemListGridView.PageIndex = e.NewPageIndex;
            ItemListGridView.DataBind();
        }

        protected void CartGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            HideGV();
            ButtonDeleteRequest.Visible = false;
            ButtonSubmitRequest.Visible = false;

            CartGridView.EditIndex = -1;
            GridViewCartBindGrid();
        }

        protected void CartGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void CartGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(CartGridView.DataKeys[e.RowIndex].Values[0]);
            RequestBizLogic.DeleteRequestDetail(id);
            GridViewCartBindGrid();
            ViewGVCart();
            HideGV();

            if (CartGridView.Rows.Count == 0)
            {
                ButtonDeleteRequest.Visible = false;
                ButtonSubmitRequest.Visible = false;
            }
        }

        protected void CartGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            HideGV();
            ButtonDeleteRequest.Visible = false;
            ButtonSubmitRequest.Visible = false;

            CartGridView.EditIndex = e.NewEditIndex;
            GridViewCartBindGrid();

            GridViewRow row = CartGridView.Rows[e.NewEditIndex];
            TextBox tb = ((TextBox)row.Cells[0].Controls[0]) as TextBox; // original 1
            tb.ReadOnly = true;
        }

        protected void CartGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (CartGridView.EditIndex == e.Row.RowIndex)
                {
                    //TextBox tb = ((TextBox)e.Row.Cells[2].Controls[0]) as TextBox;
                    TextBox tb = ((TextBox)e.Row.Cells[2].Controls[0]) as TextBox; // get reference to your Control to validate (you specify cell and control indeces)
                    tb.ID = "reg_" + e.Row.RowIndex; // give your Control to validate an ID

                    RangeValidator rv = new RangeValidator(); // Create validator and configure
                    rv.MaximumValue = "500";
                    rv.MinimumValue = "0";
                    rv.Type = ValidationDataType.Integer;
                    rv.Display = ValidatorDisplay.Dynamic;
                    rv.ErrorMessage = "<br/>Qty should be between 1-500 pcs";
                    rv.ForeColor = System.Drawing.Color.Red;
                    rv.ControlToValidate = tb.ID;
                    e.Row.Cells[2].Controls.Add(rv); // Add validator to GridView cell
                }
            }
        }

        protected void CartGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HideGV();
            ButtonDeleteRequest.Visible = false;
            ButtonSubmitRequest.Visible = false;

            GridViewRow row = CartGridView.Rows[e.RowIndex];

            string rid = ((TextBox)row.Cells[0].Controls[0]).Text;
            int id = Convert.ToInt32(rid);
            TextBox txQty = ((TextBox)row.Cells[2].Controls[0]) as TextBox;
            string qty = ((TextBox)row.Cells[2].Controls[0]).Text;
            int quantity = Convert.ToInt32(qty);

            RequestBizLogic.UpdateRequestDetail(id, quantity);

            CartGridView.EditIndex = -1;
            GridViewCartBindGrid();
        }

        protected void CartGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            CartGridView.DataSource = RequestBizLogic.ViewNotSubmittedRequestDetail(EmpID);//employeeId

            CartGridView.PageIndex = e.NewPageIndex;
            CartGridView.DataBind();
            //BindGrid();
        }

        protected void ButtonDeleteRequest_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            RequestBizLogic.DeletePendingRequest(EmpID);
            Response.Write("<script>alert('Request Deleted!');</script>");
        }

        protected void ButtonSubmitRequest_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            int DeptID = (int)HttpContext.Current.Session["DeptID"];
            RequestBizLogic.SubmitRequest(EmpID);//hardcord for testing
            GridViewCartBindGrid();
            Response.Write("<script>alert('Request Submitted Successfully !');</script>");

            //send mail to DH (String)HttpContext.Current.Session["Email"];
            String from = "teststationery47@gmail.com";
            String to = MailBizLogic.DeptHeadEmail(DeptID);
            String subject = "[Auto Notification] New Request";
            String body = String.Format("New Request by {0} has been raised. Check website for further deatils." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", EmpID);

            MailBizLogic.sendMail(from, to, subject, body);            
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            searchBy = TextBoxSearch.Text.ToString();
            ItemListGridView.DataSource = RequestBizLogic.SearchCatalogue(searchBy);
            ItemListGridView.DataBind();
            ViewGV();
            HideButton();
            HideGVCart();
            ButtonDecreaseQty.Visible = false;
            ButtonIncreaseQty.Visible = false;
        }

        protected void ButtonDecreaseQty_Click(object sender, EventArgs e)
        {
            HideGV();
            int qty = (int)ViewState["qty"];
            if (qty > 1)
            {
                TextBoxQty.Text = (qty - 1).ToString();
            }
            else
            {
                TextBoxQty.Text = qty.ToString();
            }
        }

        protected void ButtonIncreaseQty_Click(object sender, EventArgs e)
        {
            HideGV();
            int qty = (int)ViewState["qty"];
            TextBoxQty.Text = (qty + 1).ToString();
        }

        protected void ButtonAddtoCart_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(TextBoxQty.Text);
            string itemId = (string)ViewState["itemid"];
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            RequestBizLogic.AddRequestDetail(itemId, EmpID, qty);
            ItemListGridView.DataSource = RequestBizLogic.ViewAll();
            ItemListGridView.DataBind();
            ViewGV();
            HideButton();
            ButtonDecreaseQty.Visible = false;
            ButtonIncreaseQty.Visible = false;
        }

        protected void ButtonViewCart_Click(object sender, EventArgs e)
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            GridViewCartBindGrid();
            ViewGVCart();
            HideGV();
            CartGridView.EmptyDataText = "There are no items to display.";
            if (CartGridView.Rows.Count == 0)
            {
                ButtonDeleteRequest.Visible = false;
                ButtonSubmitRequest.Visible = false;
            }
            ButtonViewCart.Visible = false;
        }

        private void HideButton()
        {
            LabelDesc.Visible = false;
            LabelDescDisplay.Visible = false;

            LabelCat.Visible = false;
            LabelCatDisplay.Visible = false;

            LabelUom.Visible = false;
            LabelUoMDisplay.Visible = false;

            LabelQty.Visible = false;
            TextBoxQty.Visible = false;
            ButtonAddtoCart.Visible = false;
        }

        private void HideGVCart()
        {
            CartGridView.Visible = false;
            ButtonDeleteRequest.Visible = false;
            ButtonSubmitRequest.Visible = false;
        }

        private void ViewGVCart()
        {
            CartGridView.Visible = true;
            ButtonDeleteRequest.Visible = true;
            ButtonSubmitRequest.Visible = true;
        }

        private void HideGV()
        {
            ItemListGridView.Visible = false;
        }

        private void ViewGV()
        {
            ItemListGridView.Visible = true;
        }

        private void ViewButton()
        {
            LabelDesc.Visible = true;
            LabelDescDisplay.Visible = true;

            LabelCat.Visible = true;
            LabelCatDisplay.Visible = true;

            LabelUom.Visible = true;
            LabelUoMDisplay.Visible = true;

            LabelQty.Visible = true;
            TextBoxQty.Visible = true;
            ButtonDecreaseQty.Visible = true;
            ButtonIncreaseQty.Visible = true;
            ButtonAddtoCart.Visible = true;
        }

        private void GridViewCartBindGrid()
        {
            int EmpID = (int)HttpContext.Current.Session["EmpID"];
            CartGridView.DataSource = RequestBizLogic.ViewNotSubmittedRequestDetail(EmpID);//employeeId
            CartGridView.DataBind();
            CartGridView.Visible = true;

            ButtonSubmitRequest.Visible = true;
            ButtonDeleteRequest.Visible = true;            
        }
    }
}