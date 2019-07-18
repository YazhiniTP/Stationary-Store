using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationStationaryApi.Repo
{
    public class Bizlogic
    {

        public string todo(HttpContextBase context)
        {
            if(context.User.IsInRole("Admin"))
            {
                return "Admin";
            }
            else if (context.User.IsInRole("Dept Head"))
            {
                return "Dept Head";
            }
            else if (context.User.IsInRole("Dept Staff"))
            {
                return "Dept Staff";
            }
            else if (context.User.IsInRole("Store Clerk"))
            {
                return "Store Clerk";
            }
            else if (context.User.IsInRole("Store Manager"))
            {
                return "Store Manager";
            }
            else if (context.User.IsInRole("Store Supervisor"))
            {
                return "Store Supervisor";
            }
            else 
            {
                return "No Member";
            }


        }
    }
}