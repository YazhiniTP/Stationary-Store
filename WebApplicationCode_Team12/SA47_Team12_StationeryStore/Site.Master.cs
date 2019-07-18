using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Linq;


namespace SA47_Team12_StationeryStore
{
    
    public partial class SiteMaster : MasterPage
    {
        
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.Session["EmpID"] == null)
                {
                    // Response.Redirect("/Account/Login.aspx");
                }
                else
                {
                    String id = HttpContext.Current.User.Identity.GetUserId();

                    StationeryStoreEntities context = new StationeryStoreEntities();
                    Employee emp = context.Employee.Where(x => x.Id == id).ToList().FirstOrDefault();
                    int empId = emp.EmployeeID;
                    int deptID = (int)emp.DepartmentID;
                    

                    if (HttpContext.Current.User.IsInRole("Dept Head"))
                    {
                        if (deptID == 2007)
                        {
                            PurchasingDHTreeView.Visible = true;
                        }
                        else
                        {
                            DHTreeView.Visible = true;
                        }
                    }
                    else if (HttpContext.Current.User.IsInRole("Dept Staff"))
                    {
                        DateTime today = DateTime.Now.Date;
                        Delegation d = context.Delegation.Where(x => x.EmployeeID == empId && (x.StartDate <= today && x.EndDate >= today)).FirstOrDefault<Delegation>();
                        if (d != null)
                        {
                            DelStaffTreeView.Visible = true;
                        }
                        else
                        {
                            if (emp.isUserRep == 1)
                            {
                                URDeptStaffTreeView.Visible = true;
                            }
                            else
                            {
                                DeptStaffTreeView.Visible = true;
                            }
                        }
                    }
                    else if (HttpContext.Current.User.IsInRole("Store Manager"))
                    {
                        ManagerTreeView.Visible = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("Store Supervisor"))
                    {
                        SupervisorTreeView.Visible = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("Store Clerk"))
                    {
                        ClerkTreeView.Visible = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("Admin"))
                    {
                        AdminTreeView.Visible = true;
                    }
                }
            }
            else
            {
                //Label1.Text = "Not logged in";
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

    }

}