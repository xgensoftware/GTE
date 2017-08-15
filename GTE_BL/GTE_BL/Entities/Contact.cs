using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GTE_BL.Entities
{
    [JsonObject("Datum")]
    public class Contact
    {
        public int EntityID { get; set; }
        public string DisplayName { get; set; }
        public string TypeDisplay { get; set; }
        public string ContactSubTypeDisplay { get; set; }
        public string AgentName { get; set; }
        public string StatusDisplay { get; set; }
        public string ContactTypeDisplay { get; set; }
        public string PhoneType { get; set; }
        public string PrimaryContact { get; set; }
        public string DateLastModified { get; set; }
        public string CustomerNo { get; set; }
        public string ContactSubType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public object County { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public int LocationID { get; set; }
        public string Status { get; set; }
        public string ContactType { get; set; }
        public object BusinessName { get; set; }
    }
}
