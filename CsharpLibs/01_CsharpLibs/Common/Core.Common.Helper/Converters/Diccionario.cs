using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace Core.Common.Helper.Converters {
    public partial class ConvertHelper {
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
        /// convierte un NameValueCollection a un Dictionary<string, string/>
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
                    var cadenaAux = string.Empty;
                    for (var i = 0; i <= (((object[])objeto).Length - 1); i++)
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

        /// <summary>
        /// Convierte una clase cualquiera a un objeto Dictionary<string, string>
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ObjToDictionary(object objeto) {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (objeto == null)
                return dict;
            if (objeto.GetType().IsArray) {
                dict.Add("LENGHT", ((object[])objeto).Length.ToString());
                for (var i = 0; i < ((object[])objeto).Length; i++) {
                    var obj = ((object[])objeto)[i];
                    MergeObject(ref dict, ref obj, $"[{i}]");
                }
                return dict;
            }

            switch (objeto.GetType()) {
                // Si es String suponemos que es o un JSON o un XML
                case var tipoString when tipoString == typeof(string): {
                    var jsonXml = (string)objeto;
                    if (string.IsNullOrEmpty(jsonXml))
                        dict.Add("BUFFERDATA", jsonXml);
                    else
                        switch (jsonXml.Trim().Substring(0, 1)) {
                            case "{": // JSON
                            return JsonToObject<Dictionary<string, string>>(jsonXml);
                            case "<": // XML
                            return JsonToObject<Dictionary<string, string>>((string)XMLStringToJson(jsonXml));
                            default: {
                                dict.Add("BUFFERDATA", jsonXml);
                                break;
                            }
                        }
                    break;
                }
                case var tipoNameValueCollection when tipoNameValueCollection == typeof(NameValueCollection): {
                    NameValueCollectionToDictionary((NameValueCollection)objeto);
                    break;
                }
                case var tipoDiccionarioString when tipoDiccionarioString == typeof(Dictionary<string, string>): {
                    return (Dictionary<string, string>)objeto;
                }
                case var tipoDiccionarioObject when tipoDiccionarioObject == typeof(Dictionary<string, object>): {
                    foreach (var k in (Dictionary<string, object>)objeto) {
                        var value = k.Value;
                        MergeObject(ref dict, ref value, k.Key);
                    }
                    break;
                }
                default: {
                    foreach (var p in objeto.GetType().GetProperties()) {
                        if (p.CanRead) {
                            var name = p.Name.Trim().ToUpper();
                            var value = p.GetValue(objeto);
                            if (value != null) {
                                switch (value.GetType()) {
                                    case var tipoValueDecimal when tipoValueDecimal == typeof(decimal): {
                                        var numero = (decimal)value;
                                        var numberFormat = new NumberFormatInfo {
                                            NumberDecimalSeparator = ".",
                                            NumberGroupSeparator = string.Empty
                                        };
                                        var valor = numero.ToString(numberFormat);
                                        dict.Add(name, valor);
                                        break;
                                    }
                                    case var tipoValueString when tipoValueString == typeof(string): {
                                        dict.Add(name, (string)value);
                                        break;
                                    }
                                    case var tipoValueCollection when tipoValueCollection == typeof(NameValueCollection): {
                                        var valor = (NameValueCollection)value;
                                        MergeNameValueCollection(ref dict, ref valor, name);
                                        break;
                                    }
                                    case var tipoValueDictString when tipoValueDictString == typeof(Dictionary<string, string>): {
                                        var diccionarioValueString = (Dictionary<string, string>)value;
                                        MergeDictString(ref dict, ref diccionarioValueString, name);
                                        break;
                                    }
                                    case var tipoValueDictObject when tipoValueDictObject == typeof(Dictionary<string, object>): {
                                        var diccionarioValueObject = (Dictionary<string, object>)value;
                                        MergeDictObject(ref dict, ref diccionarioValueObject, name);
                                        break;
                                    }
                                    default: {
                                        if (value.GetType().IsArray) {
                                            var valorArray = ObjToDictionary((object[])value);
                                            MergeDictString(ref dict, ref valorArray, name);
                                        } else {
                                            if (value.GetType().Namespace.Equals("System"))
                                                dict.Add(name, value.ToString());
                                            else {
                                                var valorDiccionarioString = ObjToDictionary(value);
                                                MergeDictString(ref dict, ref valorDiccionarioString, name);
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
            }
            return dict;
        }

    }
}
