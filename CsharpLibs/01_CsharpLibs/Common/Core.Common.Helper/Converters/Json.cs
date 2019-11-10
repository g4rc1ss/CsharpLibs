using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.Common.Helper.Converters {
    public partial class Converter {
        /// <summary>
        /// Convierte un json a un Dictionary<string, string>
        /// </summary>
        /// <param name="json">json a convertir</param>
        /// <returns></returns>
        public static Dictionary<string, string> JsonToDictionary(string json) {
            try {
                if (string.IsNullOrEmpty(json))
                    return null;
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            } catch (Exception) {
                return ObjToDictionary(JsonConvert.DeserializeObject<object>(json));
            }
        }
    }
}
