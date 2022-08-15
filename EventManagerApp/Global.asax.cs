using Autofac;
using Autofac.Integration.Mvc;
using EventManagerLibrary.Models;
using EventManagerLibrary.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EventManagerApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            builder.RegisterType<TicketService>().As<ITicketService>();
            builder.RegisterType<EventService>().As<IEventService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
