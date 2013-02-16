using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace Hotello.UI.Web.App_Start
{
    public class DatabaseConfig
    {
        public static void RegisterDatabase()
        {
            ConfigureAppHarbor();
        }

        /// <summary>
        /// Once Hotello is deployed to the AppHarbor cloud,
        /// several issues can occur with In Process Memory Sessions
        /// 1) It's a free service running multiple servers
        /// 2) It's going to get full quickly and wipe the memory 
        /// 3) We will lose our sessions
        /// 
        /// Resolving this issue, it's probably a good idea to use the free
        /// SQL Server instance and apply this for use with our Sessions
        /// so that they use a database not in process memory for session state. 
        /// 
        /// http://support.appharbor.com/kb/add-ons/using-sequelizer
        /// </summary>
        private static void ConfigureAppHarbor()
        {
            // TODO: Workout a Session State fix 
            // http://forums.iis.net/t/1183079.aspx/1
            // http://support.appharbor.com/kb/tips-and-tricks/using-memcached-backed-sessionprovider


            /* AppHarbor Setup
             * 
             * If you take advantage of the Sequelizer MySQL and MS SQL Server add-ons, 
             * SQLSERVER_CONNECTION_STRING and SQLSERVER_URI settings will be injected into 
             * your application appSettings when the application is deployed. 
             * SQLSERVER_CONNECTION_STRING_ALIAS will also be inserted if you add an alias. 
             * Further, the connection string will be placed in the connectionStrings element 
             * with name set to the alias if an alias is specified. Note that the connectionstring 
             * is NOT inserted into the connectionstring element if no alias is specified. 
             * If the the alias is specified, the name attribute of the inserted connectionstring 
             * element is the value of the alias you specify. Also note that connectionstrings 
             * are only replaced once your code is deployed, not when it's built. */

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SQLSERVER_URI"]))
            {
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var uriString = ConfigurationManager.AppSettings["SQLSERVER_URI"];
                var uri = new Uri(uriString);

                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = uri.Host,
                    InitialCatalog = uri.AbsolutePath.Trim('/'),
                    UserID = uri.UserInfo.Split(':').First(),
                    Password = uri.UserInfo.Split(':').Last(),
                    MultipleActiveResultSets = true
                };

                
                configuration.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString = sb.ConnectionString;
                configuration.Save();
            }
        }
    }
}