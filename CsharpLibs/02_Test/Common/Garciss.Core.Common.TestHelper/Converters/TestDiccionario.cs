using Garciss.Core.Common.Helper.Converters;
using Garciss.Core.Common.TestHelper.Converters.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Garciss.Core.Common.TestHelper.Converters {
    [TestClass]
    public class TestDiccionario {

        [TestMethod]
        public void ConvertObjToDictionary() {
            var claseToConvert = new ClaseParaConvertirToDictionary {
                ArrayString = new string[] { "1", "2", "3" },
                Date = new DateTime(2018, 06, 21),
                Diccionario = new System.Collections.Generic.Dictionary<string, string>() {
                    { "Clave01", "Valor01" },
                    { "Clave02", "Valor02" }
                },
                Edad = 22,
                NameValue = new System.Collections.Specialized.NameValueCollection() {
                    { "Nombre", "Valor" }
                },
                Nombre = "Test de conversion",
                Salario = 2000.00m,
                ListaValoresInt = new System.Collections.Generic.List<int> {
                    1,
                    2
                },
                ListaValoresString = new System.Collections.Generic.List<string> {
                    "ListaValoresString1",
                    "ListaValoresString2"
                },
                ListaValoresOtroObj = new System.Collections.Generic.List<OtherClass> {
                    new OtherClass {
                        Nombre = "Prueba Test",
                        Date = new DateTime(2016, 12, 22).Date,
                        Edad = 22
                    }
                }
            };
            var diccionario = ConvertHelper.ObjToDictionary(claseToConvert);

            Assert.IsTrue(
                diccionario["NOMBRE"] == "Test de conversion" &&
                diccionario["SALARIO"] == "2000.00" &&
                diccionario["EDAD"] == "22" &&
                diccionario["DATE"].Contains("2018") &&
                diccionario["DICCIONARIO.CLAVE01"] == "Valor01" &&
                diccionario["DICCIONARIO.CLAVE02"] == "Valor02" &&
                diccionario["ARRAYSTRING.LENGTH"] == "3" &&
                diccionario["ARRAYSTRING.[0]"] == "1" &&
                diccionario["ARRAYSTRING.[1]"] == "2" &&
                diccionario["ARRAYSTRING.[2]"] == "3" &&
                diccionario["NAMEVALUE.NOMBRE"] == "Valor" &&
                diccionario["LISTAVALORESINT.LENGTH"] == "2" &&
                diccionario["LISTAVALORESINT.[0]"] == "1" &&
                diccionario["LISTAVALORESINT.[1]"] == "2" &&
                diccionario["LISTAVALORESSTRING.LENGTH"] == "2" &&
                diccionario["LISTAVALORESSTRING.[0]"] == "ListaValoresString1" &&
                diccionario["LISTAVALORESSTRING.[1]"] == "ListaValoresString2" &&
                diccionario["LISTAVALORESOTROOBJ.LENGTH"] == "1" &&
                diccionario["LISTAVALORESOTROOBJ.NOMBRE"] == "Prueba Test" &&
                diccionario["LISTAVALORESOTROOBJ.EDAD"] == "22" &&
                diccionario["LISTAVALORESOTROOBJ.DATE"].Contains("2016")
            );
        }
    }
}
