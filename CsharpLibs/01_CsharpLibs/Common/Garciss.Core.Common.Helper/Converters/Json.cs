using Newtonsoft.Json;

namespace Garciss.Core.Common.Helper.Converters {
    public partial class ConvertHelper {

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
        public static T JsonToObject<T>(string json, params JsonConverter[] converters) {
            return JsonConvert.DeserializeObject<T>(json, converters);
        }
    }
}
