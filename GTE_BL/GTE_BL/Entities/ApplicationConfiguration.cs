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

        public static string QBPAPIUrl
        {
            get { return ConfigurationManager.AppSettings["QBPAPIUrl"].ToString(); }
        }
        public static string QBPAPIKey
        {
            get { return ConfigurationManager.AppSettings["QBPAPIKey"].ToString(); }
        }

        public static string QBPOutputFile
        {
            get { return ConfigurationManager.AppSettings["QBPOutputFile"].ToString(); }
        }

        public static string LogFile
        {
            get { return ConfigurationManager.AppSettings["LogFile"].ToString(); }
        }

        public static string ProductImageURL
        {
            get { return ConfigurationManager.AppSettings["ProductImageURL"].ToString(); }
        }

    }
}
