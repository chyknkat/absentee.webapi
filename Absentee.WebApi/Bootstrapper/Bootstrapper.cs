using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Absentee.WebApi.Bootstrapper
{
    public class Bootstrapper
    {
        public Bootstrapper() { }

        public static void StartService()
        {
            SimpleInjectorWebApiInitializer.Initialize();
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute("AbsenteeDefault"
                , "{controller}/{action}/{id}/{user}"
                , new { id = RouteParameter.Optional, user = RouteParameter.Optional });

            routes.MapHttpRoute("AbsenteePostBasic"
                , "{controller}/{action}/{post}"
                , null);
        }
    }
}