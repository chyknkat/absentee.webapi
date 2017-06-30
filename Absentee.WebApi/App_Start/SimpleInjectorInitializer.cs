using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Absentee.WebApi.Data;
using NHibernate;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;

namespace Absentee.WebApi.App_Start
{
    public static class SimpleInjectorInitializer
    {
        public static Container Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            InitializeContainer(container);

            container.Verify();
            return container;
        }
        private static void InitializeContainer(Container container)
        {
            var sessionFactory = SessionFactory.BuildSessionFactory();
            container.RegisterSingleton(sessionFactory);
            container.Register(() => container.GetInstance<ISessionFactory>().OpenSession(), Lifestyle.Singleton);

            container.Register<IIRepository, Repository>(Lifestyle.Transient);
            
        }
    }
}