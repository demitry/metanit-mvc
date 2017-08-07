using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcLifecycle
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            BeginRequest += (src, args) => AddEvent("BeginRequest");
            AuthenticateRequest += (src, args) => AddEvent("AuthentucateRequest");
            PreRequestHandlerExecute += (src, args) => AddEvent("PreRequestHandlerExecute");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Write("Application_PostRequestHandlerExecute");
        }

        /*
        protected void Application_BeginRequest()
        {
            AddEvent("BeginRequest");
        }

        protected void Application_AuthenticateRequest()
        {
            AddEvent("AuthenticateRequest");
        }

        protected void Application_PreRequestHandlerExecute()
        {
            AddEvent("PreRequestHandlerExecute");
        }
        */

        private void AddEvent(string name)
        {
            List<string> eventList = Application["events"] as List<string>;
            if (eventList == null)
            {
                Application["events"] = eventList = new List<string>();
            }
            eventList.Add(name);
        }

    }
}
