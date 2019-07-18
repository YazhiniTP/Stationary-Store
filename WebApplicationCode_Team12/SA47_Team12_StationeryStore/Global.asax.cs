using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using SA47_Team12_StationeryStore.BizLogic;
using Microsoft.AspNet.Identity;

namespace SA47_Team12_StationeryStore
{
    public class Global : HttpApplication
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        "Default",                                              // Route name
        //        "{controller}/{action}/{id}",                           // URL with parameters
        //        new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
        //    );

        //}
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["User"] = "" ;
            

            //String id = HttpContext.Current.User.Identity.GetUserId();

            //StationeryStoreEntities context = new StationeryStoreEntities();
            //Employee emp = context.Employee.Where(x => x.Id == id).ToList().FirstOrDefault();
            //int empId = emp.EmployeeID;
            //int deptID = (int)emp.DepartmentID;
            //Session["EmpID"] = empId;
            //Session["DeptID"] = deptID;

            // maybe can include here a condition to see if it is a clerk starting the session, if yes, run the code

            // Code that runs when a new session is started
            System.Timers.Timer myTimer = new System.Timers.Timer();

            myTimer.AutoReset = false;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
        }

        public void myTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            var listOfLowStockItems = CatalogueBizLogic.ListLowStockItems();

            ScheduleEmail objScheduleMail = new ScheduleEmail();
            objScheduleMail.SendScheduleMail(listOfLowStockItems);
        }
    }
}