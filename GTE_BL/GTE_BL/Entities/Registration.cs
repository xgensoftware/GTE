using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.Xgensoftware.Core;
using GTE_BL.Repository;

namespace GTE_BL.Entities
{
    public enum APIProvider
    {
        QQSOLUTION,
        VELOCIFY
    }

    public class Registration : BaseEntity, IEntity
    {
        #region " Member Variables "
        string _companyName;
        APIProvider _apiProvider = APIProvider.QQSOLUTION;
        #endregion

        #region " Properties "
        public int Id { get; set; }

        [Required(ErrorMessage ="*")]
        public string CompanyName {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value.Replace(" ", string.Empty);
            }
        }

        [Required(ErrorMessage ="*")]
        public string APIUserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        public string APIPassword { get; set; }

        public APIProvider ApiProvider
        {
            get { return _apiProvider; }
            set { _apiProvider = value; }
        }

        public bool IsActive { get; set; }
        #endregion

        #region " Constructor "
        public Registration()
        {
            this.Id = -1;
        }

        public Registration(string companyName, string userName, string password)
        {
            this.Id = -1;
            this.CompanyName = companyName;
            this.APIUserName = userName;
            this.APIPassword = password;
            this.IsActive = true;
        }
        #endregion

        #region " Interface Implementation "
        public void LoadFrom(DataRow dr)
        {
            if (dr != null)
            {
                this.Id = dr["Id"].Parse<int>();
                this.CompanyName = dr["CompanyName"].ToString();
                this.APIUserName = dr["QQUsername"].ToString();
                this.APIPassword = dr["QQPassword"].ToString();
                this._apiProvider = (APIProvider) Enum.Parse(typeof(APIProvider), dr["APIProvider"].ToString());
                this.IsActive = dr["IsActive"].Parse<bool>();
            }
        }
        #endregion

        #region " Public Methods "
        public override void Save()
        {
            
            using (ClientRepository cr = new ClientRepository())
            {
                cr.SaveRegistration(this);
            }
        }

        public void GetBy(string companyName, string apiProvider)
        {
            using (ClientRepository cr = new ClientRepository())
            {
                DataRow dr = cr.GetBy(companyName,apiProvider);
                this.LoadFrom(dr);
            }
        }
        #endregion
    }
}
