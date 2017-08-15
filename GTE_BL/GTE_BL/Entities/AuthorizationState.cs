using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GTE_BL.Entities
{
    [DataContract]
    public class AuthorizationState
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "expires_in")]
        public long ExpiresIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
