using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
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

        public static void ToCsvFile<T>(this IEnumerable<T> items, string separator, string fileName)
                where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();

            string header = String.Join(separator, properties.Select(f => f.Name).ToArray());
            using (FileStream file = File.Create(fileName))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine(header);

                    foreach (var o in items)
                    {
                        string line = string.Join(separator, properties.Select(p => p.GetValue(o, null).ToCsvValue()).ToArray());
                        writer.WriteLine(line);
                    }
                }
            }
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }

        /*
        private static string ToCSVFields(string separator, FieldInfo[] fields, object o)
        {
            StringBuilder data = new StringBuilder();
            foreach (var f in fields)
            {
                if (data.Length > 0)
                    data.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    data.Append(x.ToString());
            }
            return data.ToString();
        }

        public static void ToCSV<T>(this IEnumerable<T> objectList, string separator, string fileName)
        {
            var type = typeof(T);
            var fields = type.GetFields();
            string header = String.Join(separator, fields.Select(f => f.Name).ToArray());
            using (FileStream file = File.Create(fileName))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine(header);

                    foreach (var o in objectList)
                    {
                        writer.WriteLine(ToCSVFields(separator, fields, o));
                    }
                }
            }
        }
        */
    }
}
