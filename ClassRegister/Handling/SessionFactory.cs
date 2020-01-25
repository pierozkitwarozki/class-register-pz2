using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister
{
    public class SessionFactory
    {
        //Klasa odpowiadająca za tworzenie całej sesji, mapowanie, łączenie z bazą i inne chuje, muje, dzikie węże
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new Object();
        public static ISession session;

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            iSessionFactory = BuildSessionFactory();
                        }
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.AppSettings["connection_string"];
                return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<App>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(testc => testc.Namespace == "ClassRegister.Model");
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {

        }

        public static void Configuration()
        {
            //Uruchomienie sesji, łączenie z bazą danych
            if (session != null && session.IsOpen)
            {
                session.Close();
            }
            session = SessionFactory.OpenSession;
        }
    }
}
