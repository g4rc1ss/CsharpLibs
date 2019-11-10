using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Xml;

namespace Core.Common.Helper.Converters {
    public partial class ConvertHelper {
        /// <summary>
        /// Convierte un xml a un Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object XMLStringToJson(string json) {
            var dataSet = new DataSet();
            dataSet.ReadXml(new StringReader(json));
            return DataSetToJson(dataSet);
        }

        /// <summary>
        /// Convierte un XMLFile a un Json
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static object XMLFileToJson(string xmlFile) {
            var dataSet = new DataSet();
            dataSet.ReadXml(xmlFile);
            return DataSetToJson(dataSet);
        }

        /// <summary>
        /// Convierte un XML a un objeto NameValueCollection
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static NameValueCollection XmlToNameValue(string tag, string xml) {
            try {
                var outXML = default(XmlDocument);
                var outNameValue = new NameValueCollection();
                outXML.LoadXml(xml);

                var xmlCabecera = outXML.SelectSingleNode(tag);

                foreach (XmlAttribute nodo in xmlCabecera.Attributes)
                    outNameValue.Add(nodo.Name, nodo.Value);
                return outNameValue;
            } catch (Exception) {
                return null;
            }
        }

        /// <summary>
        /// Convierte un nodo de XML a Json
        /// </summary>
        /// <param name="nodo"></param>
        /// <returns></returns>
        public static string XmlNodeToJson(XmlNode nodo) {
            return JsonConvert.SerializeXmlNode(nodo, Newtonsoft.Json.Formatting.None, true);
        }

        /// <summary>
        /// optimiza el proceso de Trim a las conversiones de nodo xml a Json
        /// </summary>
        /// <param name="nodo"></param>
        /// <returns></returns>
        public static string XmlNodeToJsonOptimizedWithTrim(XmlNode nodo) {
            var aux = XmlNodeToJson(nodo).Replace(@""",""@", @""",""");

            aux = aux.Replace(@"{""@", @"{""");
            aux = aux.Replace(@"        ", @" ");
            aux = aux.Replace(@"       ", @" ");
            aux = aux.Replace(@"      ", @" ");
            aux = aux.Replace(@"     ", @" ");
            aux = aux.Replace(@"    ", @" ");
            aux = aux.Replace(@"   ", @" ");
            aux = aux.Replace(@"  ", @" ");
            aux = aux.Replace(@"  ", @" ");
            aux = aux.Replace(@" """, @"""");
            return aux;
        }
    }
}
