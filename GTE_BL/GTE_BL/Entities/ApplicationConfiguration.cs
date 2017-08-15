using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTE_BL.Entities
{
    public class ApplicationConfiguration
    {
        public static string QQClientId
        {
            get { return ConfigurationManager.AppSettings["QQClientId"].ToString(); }
        }

        public static string QQClientSecret
        {
            get { return ConfigurationManager.AppSettings["QQClientSecret"].ToString(); }
        }

        public static string QQRedirectURL
        {
            get { return ConfigurationManager.AppSettings["QQRedirectURL"].ToString(); }
        }

        public static string QQAuthorizeURL
        {
            get { return ConfigurationManager.AppSettings["QQAuthorizeURL"].ToString(); }
        }

        public static string QQResourceURL
        {
            get { return ConfigurationManager.AppSettings["QQResourceURL"].ToString(); }
        }

        public static string DBConnection
        {
            get { return ConfigurationManager.AppSettings["DBConnection"].ToString(); }
        }
    }
}
