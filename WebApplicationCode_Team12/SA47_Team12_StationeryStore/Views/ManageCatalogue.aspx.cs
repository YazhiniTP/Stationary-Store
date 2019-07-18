using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore.Views
{
    public partial class ManageCatalogue : System.Web.UI.Page
    {
        static string searchString ="";

        private void BindGrid()
        {
            ManageCatalogueGridView.DataSource = CatalogueBizLogic.ListCatalogue(searchString);
            ManageCatalogueGridView.DataBind();
        }

        private void BindGrid1()
        {
            ManageCatalogueGridView.DataSource = CatalogueBizLogic.ListCatalogue();
            ManageCatalogueGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["EmpID"] == null)
                Response.Redirect("/Account/Login.aspx");
            else if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            searchString = SearchTextBox.Text;
            ManageCatalogueGridView.DataSource = CatalogueBizLogic.ListCatalogue(SearchTextBox.Text);
            ManageCatalogueGridView.DataBind();
        }

        protected void ManageCatalogueGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ManageCatalogueGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void ManageCatalogueGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ManageCatalogueGridView.EditIndex = -1; // return editindex to no selection of any rows for editing
            BindGrid();
        }

        protected void ManageCatalogueGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = ManageCatalogueGridView.Rows[e.RowIndex];
            string itemNumber = ManageCatalogueGridView.DataKeys[e.RowIndex].Values[0].ToString();
            int reorderLevel = Convert.ToInt32((row.FindControl("TextBox1") as TextBox).Text);
            int reorderQty = Convert.ToInt32((row.FindControl("TextBox2") as TextBox).Text);
            CatalogueBizLogic.UpdateItem(itemNumber, reorderLevel, reorderQty);

            ManageCatalogueGridView.EditIndex = -1;
            BindGrid();
        }

        protected void ManageCatalogueGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ManageCatalogueGridView.DataSource = CatalogueBizLogic.ListCatalogue(searchString);
            ManageCatalogueGridView.PageIndex = e.NewPageIndex;
            ManageCatalogueGridView.DataBind();
            BindGrid1();
        }
    }
}