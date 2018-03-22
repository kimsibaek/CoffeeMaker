using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoffeeMaker.Common
{
    public static class JsonExtention
    {
        public static string Serialize<T>(T o)
        {
            if (o == null)
            {
                return string.Empty;
            }

            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T Deserialize<T>(string s) where T : new()
        {
            if (string.IsNullOrEmpty(s))
            {
                return new T();
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(s);

            }
            catch
            {
                return new T();
            }
        }
    }
}
