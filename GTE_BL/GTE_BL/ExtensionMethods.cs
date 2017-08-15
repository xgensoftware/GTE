using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Text;
using System.Text;
using System.Threading.Tasks;

namespace GTE_BL
{
    public static class ExtensionMethods
    {
        #region " Serialization "

        public static T FromJson<T>(this string value)
        {
            T returnObject = default(T);

            try
            {
                //DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
                //using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(value)))
                //{
                //    returnObject = (T)ds.ReadObject(ms);
                //}
                returnObject = JsonConvert.DeserializeObject<T>(value);
            }
            catch { }

            return returnObject;
        }

        public static string ToJson<T>(this T value)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(JsonConvert.SerializeObject(value, Formatting.Indented));
            }
            catch { }

            return sb.ToString();
        }
        #endregion
    }
}
