using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Linq;
using System;

namespace Core.Common.Helper.Converters {
    public partial class Converter {
        #region "Funciones Privadas"
        /// <summary>
        /// Convierte un objeto tipo NameValueCollection a Dictoionary<string, string/>
        /// </summary>
        /// <param name="nameValue"></param>
        /// <returns>objecto Dictionary<string, string/></returns>
        private static Dictionary<string, string> NameValueCollectionToDictionary(NameValueCollection nameValue) {
            return nameValue.Cast<string>().ToDictionary(key => key, valor => nameValue[valor]);
        }
        #endregion

        #region "Merge de datos de distintos tipos de objeto a un Dictionary<string, string>"
        /// <summary>
        /// Agrega el segundo diccionario al primero
        /// </summary>
        /// <param name="mergeDict">Diccionario que se va a modificar, sera el resultado</param>
        /// <param name="dictToMerge">Diccionario que se recibe para ser fusionado</param>
        /// <param name="prefijo">Clave de la que proviene el diccionario a juntar</param>
        private static void MergeDictString(ref Dictionary<string, string> mergeDict, ref Dictionary<string, string> dictToMerge, string prefijo) {
            prefijo = prefijo.ToUpper() + ".";
            foreach (var k in dictToMerge)
                mergeDict.Add(prefijo + k.Key.Trim().ToUpper(), k.Value);
        }

        /// <summary>
        /// Agrega el segundo diccionario al primero
        /// </summary>
        /// <param name="mergeDict">Diccionario que se va a modificar, sera el resultado</param>
        /// <param name="dictToMerge">Diccionario que se recibe para ser fusionado</param>
        /// <param name="prefijo">Clave de la que proviene el diccionario a juntar</param>
        private static void MergeDictObject(ref Dictionary<string, string> dictA, ref Dictionary<string, object> dictB, string prefijo) {
            prefijo = prefijo.Trim().ToUpper() + ".";
            foreach (var k in dictB) {
                var value = k.Value;
                MergeObject(ref dictA, ref value, prefijo + k.Key.Trim().ToUpper());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="nameValue"></param>
        /// <param name="prefijo"></param>
        private static void MergeNameValueCollection(ref Dictionary<string, string> dict, ref NameValueCollection nameValue, string prefijo) {
            prefijo = prefijo.Trim().ToUpper() + ".";
            foreach (var k in nameValue)
                dict.Add(prefijo + k.ToString().Trim().ToUpper(), nameValue[(string)k]);
        }

        /// <summary>
        /// añade un objeto al diccionario
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="objeto"></param>
        /// <param name="name">clave que proviene del objeto</param>
        private static void MergeObject(ref Dictionary<string, string> dict, ref object objeto, string name) {
            var tipo = objeto.GetType();
            if (tipo.Equals(typeof(string))) {
                dict.Add(name, (string)objeto);
            } else if (objeto is NameValueCollection) {
                MergeNameValueCollection(ref dict, ref (NameValueCollection)objeto, name);
            } else if (objeto is Dictionary<string, string>) {
                MergeDictString(ref dict, ref (Dictionary<string, string>)objeto, name);
            } else if (objeto is Dictionary<string, object>) {
                MergeDictObject(ref dict, ref (Dictionary<string, object>)objeto, name);
            } else if (objeto is object[]) {
                string cadenaAux = string.Empty;
                for (var i = 0; i < ((object[])objeto).Length; i++)
                    cadenaAux += i.ToString();
                dict.Add(name, cadenaAux);
            } else {
                if (objeto.GetType().Namespace.Equals("System"))
                    dict.Add(name, objeto.ToString());
                else
                    MergeDictString(dict, objToDictionary(objeto), name);
            }
        }
        #endregion
    }
}
