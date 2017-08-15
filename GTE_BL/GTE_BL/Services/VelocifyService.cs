using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using com.Xgensoftware.Core;
using GTE_BL.Entities;

namespace GTE_BL.Services
{
    public class VelocifyService
    {
        #region " Member Variables "
        string _UserName;
        string _Password;
        #endregion


        #region " Constructor "
        public VelocifyService(string username, string password)
        {
            this._UserName = username;
            this._Password = password;
        }

        #endregion

        #region " Public Methods "
        public int GetContactIdByPhone(string phoneNum)
        {
            VelocifyAPIService.ClientServiceSoapClient client = new VelocifyAPIService.ClientServiceSoapClient("ClientServiceSoap");
            int id = -1;
            try
            {
                var result = client.GetLeadsByPhone(this._UserName, this._Password, phoneNum);
                id = result.SelectSingleNode("Lead").Attributes["Id"].Value.Parse<int>();
            }
            catch
            {
                id = -1;
            }

            return id;
        }
        #endregion
    }
}
