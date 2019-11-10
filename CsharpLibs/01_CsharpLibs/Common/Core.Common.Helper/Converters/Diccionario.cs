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
            switch (objeto.GetType()) {
                case var tipoString when tipoString == typeof(string): {
                    dict.Add(name, (string)objeto);
                    break;
                }
                case var tipoNameValueCollection when tipoNameValueCollection == typeof(NameValueCollection): {
                    var nameValue = (NameValueCollection)objeto;
                    MergeNameValueCollection(ref dict, ref nameValue, name);
                    break;
                }
                case var tipoDictionaryString when tipoDictionaryString == typeof(Dictionary<string, string>): {
                    var diccionarioString = (Dictionary<string, string>)objeto;
                    MergeDictString(ref dict, ref diccionarioString, name);
                    break;
                }
                case var tipoDiccionarioObject when tipoDiccionarioObject == typeof(Dictionary<string, object>): {
                    var diccionarioObject = (Dictionary<string, object>)objeto;
                    MergeDictObject(ref dict, ref diccionarioObject, name);
                    break;
                }
                case var tipoArrayObject when tipoArrayObject == typeof(object[]): {
                    string cadenaAux = string.Empty;
                    for (int i = 0; i <= (((object[])objeto).Length - 1); i++)
                        cadenaAux += ((object[])objeto)[i].ToString();
                    dict.Add(name, cadenaAux);
                    break;
                }
                default: {
                    if (objeto.GetType().Namespace.Equals("System"))
                        dict.Add(name, objeto.ToString());
                    else {
                        var diccionario = ObjToDictionary(objeto);
                        MergeDictString(ref dict, ref diccionario, name);
                    }
                    break;
                }
            }
        }
        #endregion

        public static Dictionary<string, string> ObjToDictionary(object objeto) {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (objeto is null)
                return dict;
            switch (objeto.GetType()) {
                case var tipoArray when tipoArray.GetType().IsArray: {
                    dict.Add("LENGHT", ((object[])objeto).Length.ToString());
                    for (int i = 0; i < ((object[])objeto).Length; i++)
                        MergeObject(ref dict, ref ((object[])objeto)[i], string.Format($"[{i}]"));
                    break;
                }
                // Si es String suponemos que es o un JSON o un XML
                case var tipoString when tipoString == typeof(string): {

                    break;
                }
            }
            return dict;
        }
    }
}
