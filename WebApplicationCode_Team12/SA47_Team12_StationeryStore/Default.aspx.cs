using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using SA47_Team12_StationeryStore.BizLogic;

namespace SA47_Team12_StationeryStore
{
    public partial class _Default : Page
    {
        static string searchString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmpID"] != null)
            {
                String id = HttpContext.Current.User.Identity.GetUserId();

                StationeryStoreEntities context = new StationeryStoreEntities();
                Employee emp = context.Employee.Where(x => x.Id == id).ToList().FirstOrDefault();
                if (emp != null)
                {
                    int empId = emp.EmployeeID;
                    int deptID = (int)emp.DepartmentID;
                    String email = emp.Email;
                    Session["EmpID"] = empId;
                    Session["DeptID"] = deptID;
                    Session["Email"] = email;
                }               
            }
        }
        
        private void BindGrid()
        {
            ItemListGridView.DataSource = RequestBizLogic.SearchCatalogue(searchString);
            ItemListGridView.DataBind();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            searchString = SearchTextBox.Text;
            ItemListGridView.DataSource = RequestBizLogic.SearchCatalogue(searchString);
            ItemListGridView.DataBind();
            BindGrid();
        }

        protected void ItemListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ItemListGridView.PageIndex = e.NewPageIndex;
            ItemListGridView.DataBind();
            BindGrid();
        }
    }
}