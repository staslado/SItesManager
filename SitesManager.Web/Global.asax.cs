using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SitesManager.Web.Infrastructure;

namespace SitesManager.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            MapperConfig.InitializeMapper();


            // Зарегистрируем объект, в котором будет создан "фоновый поток" для оповещения клиентов о доступности сайтов
            HostingEnvironment.RegisterObject(new DetectModifiedSitesServerTimer());
        }
    }
}