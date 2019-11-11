using System.Data;
using System.IO;

namespace Core.Common.Helper.Converters {
    /// <summary>
    /// Clase estatica en la que se ubican metodos para realizar conversiones y tratamiento de objetos
    /// Como por ejemlo: Convertir una clase a un diccionario, un XML a un Json, etc
    /// </summary>
    public partial class ConvertHelper {
        /// <summary>
        /// Convierte un xml a un Json
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static object XMLStringToJson(string xmlString) {
            var dataSet = new DataSet();
            dataSet.ReadXml(new StringReader(xmlString));
            return ObjectToJson(dataSet);
        }

        /// <summary>
        /// Convierte un XMLFile a un Json
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static object XMLFileToJson(string xmlFile) {
            var dataSet = new DataSet();
            dataSet.ReadXml(xmlFile);
            return ObjectToJson(dataSet);
        }
    }
}
