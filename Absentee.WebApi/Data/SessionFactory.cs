using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.SqlAzure;
using NHibernate.Cfg;

namespace Absentee.WebApi.Data
{
    public class SessionFactory
    {
        public static ISessionFactory BuildSessionFactory()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Absentee;Integrated Security=True";
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).Driver<SqlAzureClientDriver>().IsolationLevel(IsolationLevel.ReadUncommitted))
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<Repository>();
                })
                .ExposeConfiguration(cfg => cfg.SetProperty(Environment.CurrentSessionContextClass, "web"))
                .BuildSessionFactory();
            return sessionFactory;
        }
    }
    
}