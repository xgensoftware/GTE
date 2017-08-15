using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.Xgensoftware.Core;
using com.Xgensoftware.DAL;
using GTE_BL.Entities;

namespace GTE_BL.Repository
{
    public class ClientRepository : BaseRepository
    {
        public ClientRepository():base()
        {

        }

        public void SaveRegistration(Registration r)
        {
            List<SQLParam> sqlParms = new List<SQLParam>();
            sqlParms.Add(SQLParam.GetParam("@id", r.Id));
            sqlParms.Add(SQLParam.GetParam("@companyname", r.CompanyName));
            sqlParms.Add(SQLParam.GetParam("@qqusername", r.APIUserName));
            sqlParms.Add(SQLParam.GetParam("@qqpassword", r.APIPassword));
            sqlParms.Add(SQLParam.GetParam("@apiprovider", r.ApiProvider.ToString()));
            sqlParms.Add(SQLParam.GetParam("@isActive", r.IsActive));

            this._dbProvider.ExecuteNonQuery("Save_Registration", sqlParms);
        }

        public DataRow GetBy(string companyName, string apiProvider)
        {
            List<SQLParam> sqlParms = new List<SQLParam>();
            sqlParms.Add(SQLParam.GetParam("@companyName", companyName));
            sqlParms.Add(SQLParam.GetParam("@api", apiProvider));

            return this._dbProvider.ExecuteDataSet("Get_ByCompanyName", sqlParms).Tables[0].Rows[0];
        }
    }
}
