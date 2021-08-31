using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace Garciss.Core.Common.Converter {
    /// <summary>
    /// Clase estatica en la que se ubican metodos para realizar conversiones y tratamiento de objetos
    /// Como por ejemlo: Convertir una clase a un diccionario, un XML a un Json, etc
    /// </summary>
    public static class ConvertXml {
        /// <summary>
        /// Convierte un xml a un Json
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static string XMLStringToJson(string xmlString) {
            var dataSet = new DataSet();
            dataSet.ReadXml(new StringReader(xmlString));
            return JsonConvert.SerializeObject(dataSet);
        }

        /// <summary>
        /// Convierte un XMLFile a un Json
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static string XMLFileToJson(string xmlFilePath) {
            var dataSet = new DataSet();
            dataSet.ReadXml(xmlFilePath);
            return JsonConvert.SerializeObject(dataSet);
        }
    }
}
