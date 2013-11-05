using System;
using System.Linq;
using System.Web.Http;
using Owin;
using BadgeService.Ioc;
using BadgeService.Controller;
using Services;
using BadgeService.Storage;
using BadgeService.Service;
using Microsoft.Practices.Unity;


namespace BadgeService
{
    class Startup
    {



        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var unity = new UnityContainer();
            unity.RegisterType<BadgesController>();
            unity.RegisterType<IBadgeStorageService, BadgeSqlStorageDriver>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IBadgeGenerator, BadgeGenerator>(
                new HierarchicalLifetimeManager());
            config.DependencyResolver = new IoCContainer(unity);
            
         

            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            app.UseWebApi(config);

           

        }

       

    }
}
