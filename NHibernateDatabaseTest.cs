using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Ecs.Eclipse.Model.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Cf = NHibernate.Cfg;

namespace Ecs.Eclipse.Data.Tests
{
    
    public class NHibernateDatabaseTest : IDisposable
    {
        private static  FluentConfiguration Configuration;
        private static ISessionFactory SessionFactory;
        protected ISession session;
       
        public NHibernateDatabaseTest()
        {
            if (Configuration == null)
            {

         
                Configuration = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigurationManager.ConnectionStrings["Default"].ConnectionString).ShowSql())
                    .Mappings(m =>
                              m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
                               .Mappings(m => m.HbmMappings.AddClasses(typeof(CandidateMap)));

                SessionFactory = Configuration.BuildSessionFactory();
            }
            
             session = SessionFactory.OpenSession();
            
           
        }
        [SetUp]
        public void SetUp()
        {
            session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            session.Transaction.Rollback();
        }
        public void Dispose()
        {
            session.Dispose();
        }
    }
}
