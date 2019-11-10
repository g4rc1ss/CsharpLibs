using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Core.Common.Helper.Converters {
    public partial class ConvertHelper {
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

        /// <summary>
        /// Se convierte un dataset a un json con la libreria Newtonsoft
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataSetToJson(DataSet data) {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Convierte un objeto a Json con la lib Newtonsoft
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static string ObjectToJson(object objeto) {
            return JsonConvert.SerializeObject(objeto);
        }

        /// <summary>
        /// Convierte una cadena json a un tipo de clase por Newtonsoft
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Convierte una cadena json a un tipo de clase con el parametro Settings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json, JsonSerializerSettings settings) {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Convierte una cadena json a un tipo de clase con el parametro convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json, JsonConverter[] converters) {
            return JsonConvert.DeserializeObject<T>(json, converters);
        }
    }
}
