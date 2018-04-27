namespace Fanex.BetList.Example.MVC
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Fanex.RemotingRef;
    using NLog;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RefDataTypeContainer.ConnectTo(new Uri(ConfigurationManager.AppSettings["Fanex.RemotingRef.Service.Url"]));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            logger.Error(CultureInfo.InvariantCulture, Server.GetLastError());
        }
    }
}