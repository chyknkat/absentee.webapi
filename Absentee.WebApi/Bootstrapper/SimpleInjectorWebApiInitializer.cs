using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Absentee.WebApi.Data;
using NHibernate;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Absentee.WebApi.Bootstrapper
{
    public static class SimpleInjectorWebApiInitializer
    {
        private static Container Container { get; set; }

        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            //var container = new Container();
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            InitializeContainer(Container);

            Container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            Container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Container);
        }

        private static void InitializeContainer(Container container)
        {
            var sessionFactory = SessionFactory.BuildSessionFactory();
            container.Register<IIRepository, Repository>(Lifestyle.Scoped);
            container.RegisterSingleton(sessionFactory);
            container.Register(() => container.GetInstance<ISessionFactory>().OpenSession(), Lifestyle.Scoped);
        }
    }
}