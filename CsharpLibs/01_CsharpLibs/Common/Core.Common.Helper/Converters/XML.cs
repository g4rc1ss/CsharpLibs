using System.Data;
using System.IO;

namespace Core.Common.Helper.Converters {
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
