using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GTE_BL.Entities
{
    [JsonObject("RootObject")]
    public class SearchResult
    {
        public List<Contact> Data { get; set; }
        public int PageNumber { get; set; }
        public int PagesTotal { get; set; }
        public int TotalItems { get; set; }
        public bool IsSuccess { get; set; }
        public object ErrorCode { get; set; }
        public object ErrorMessage { get; set; }
        public object DisplayMessage { get; set; }
        public List<object> Links { get; set; }
        public string Href { get; set; }
    }
}
