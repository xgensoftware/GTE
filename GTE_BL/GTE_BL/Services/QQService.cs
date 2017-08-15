using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using GTE_BL.Entities;

namespace GTE_BL.Services
{
    public class QQService
    {
        #region " Member Variables "
        string _UserName;
        string _Password;
        #endregion

        #region " Properties "
        public AuthorizationState AuthorizationToken { get; set; }
        #endregion

        #region " Constructor "
        public QQService()
        {

        }
        public QQService(string username, string password)
        {
            this._UserName = username;
            this._Password = password;

            this.AuthorizationToken = new AuthorizationState();
        }
        #endregion

        #region " Private Methods "
        private void AuthorizeClient()
        {
            var thirdPartyCreds = string.Format("{0}:{1}", ApplicationConfiguration.QQClientId, ApplicationConfiguration.QQClientSecret);
            var stringBytes = Encoding.UTF8.GetBytes(thirdPartyCreds);
            var encodedString = Convert.ToBase64String(stringBytes);
            var urlstring = new Uri(ApplicationConfiguration.QQAuthorizeURL);

            using (var loginClient = new WebClient())
            {
                loginClient.Headers.Add(HttpRequestHeader.Authorization, $"Basic {encodedString}");
                loginClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded; charset=utf-8");
                var bodyString = string.Format("username={0}&password={1}&grant_type=password", Uri.EscapeDataString(_UserName), Uri.EscapeDataString(_Password));
                var response = loginClient.UploadString(urlstring, bodyString);

                this.AuthorizationToken = response.FromJson<AuthorizationState>();
            }            
        }
        #endregion

        #region " Public Methods "
        public int SearchByPhoneNumber(string phoneNum)
        {
            int entityId = -1;

            try
            {
                if (this.AuthorizationToken == null | this.AuthorizationToken.AccessToken == null)
                    this.AuthorizeClient();

                string searchResult;

                string searchUrl = string.Format("{0}/v1/Search/Customers/BasicSearch?phone={1}", ApplicationConfiguration.QQResourceURL, phoneNum);
                using (var apiClient = new WebClient())
                {
                    apiClient.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {this.AuthorizationToken.AccessToken}");
                    searchResult = apiClient.DownloadString(searchUrl);

                }

                entityId = searchResult.FromJson<SearchResult>().Data.FirstOrDefault().EntityID;
            }
            catch
            {
                entityId = -1;
            }
            return entityId;
        }
        #endregion
    }
}
