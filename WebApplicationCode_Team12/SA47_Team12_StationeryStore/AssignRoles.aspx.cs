using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;//new reference
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;//new reference
using SA47_Team12_StationeryStore.Models;

namespace SA47_Team12_StationeryStore
{
    public partial class AssignRoles : System.Web.UI.Page
    {
        void Init_Roles() //a method to populate the roles in EF
        {
            ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>(); //get the EF context in (Models/IdentityModels.cs)
            if (DbContext.Roles.ToList<IdentityRole>().Count == 0) //There is no roles in the list
            {
                string[] roles = { "Admin", "Dept Head", "Dept Staff", "Store Manager", "Store Supervisor", "Store Clerk" };
                foreach (string r in roles)
                {
                    DbContext.Roles.Add(new IdentityRole() { Name = r }); // add the role Owner and User in the roles array into the EF
                }
                DbContext.SaveChanges();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init_Roles(); //populate roles in EF

                ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>();//get the EF context in (Models/IdentityModels.cs)
                var l1 = DbContext.Users.ToList<IdentityUser>(); //populate dropdownlist1
                UserName.DataSource = l1;
                UserName.DataTextField = "UserName";
                UserName.DataValueField = "UserName";
                UserName.DataBind();
                if (l1.Count > 0)
                {
                    UserName.SelectedIndex = 0;
                }

                var r1 = DbContext.Roles.ToList<IdentityRole>();//populate dropdownlist2
                AssignRolesRadioButtonList.DataSource = r1;
                AssignRolesRadioButtonList.DataTextField = "Name";
                AssignRolesRadioButtonList.DataValueField = "Name";
                AssignRolesRadioButtonList.DataBind();
                if (r1.Count > 0)
                {
                    AssignRolesRadioButtonList.SelectedIndex = 0;
                }
            }
        }

        protected void AssignRolesButton_Click(object sender, EventArgs e)
        {
            ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>(); //create EF object
            string username = UserName.SelectedValue;
            string rolename = AssignRolesRadioButtonList.SelectedValue;
            IdentityUser user = DbContext.Users.FirstOrDefault
                                   (u => u.UserName.Equals(username,
                                    StringComparison.CurrentCultureIgnoreCase));//allows case insensitive input in the name
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); //create manager object
            manager.AddToRole(user.Id, rolename); //add the rolename to the user
        }

    }
}