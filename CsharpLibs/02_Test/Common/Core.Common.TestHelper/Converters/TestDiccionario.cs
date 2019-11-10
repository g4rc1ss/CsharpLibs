using Core.Common.Helper.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Common.TestHelper.Converters {
    [TestClass]
    public class TestDiccionario {

        [TestMethod]
        public void ConvertObjToDictionary() {
            var claseToConvert = new ClaseParaConvertirToDictionary {
                ArrayString = new string[] { "1", "2", "3" },
                Date = DateTime.Now.Date,
                Diccionario = new System.Collections.Generic.Dictionary<string, string>() {
                    { "Clave01", "Valor01" },
                    { "Clave02", "Valor02" }
                },
                Edad = 22,
                NameValue = new System.Collections.Specialized.NameValueCollection() {
                    { "Nombre", "Valor" }
                },
                Nombre = "Test de conversion",
                Salario = 2000.00m
            };
            var diccionario = ConvertHelper.ObjToDictionary(claseToConvert);

            Assert.IsTrue(
                diccionario["NOMBRE"] == "Test de conversion" &&
                diccionario["SALARIO"] == "2000.00" &&
                diccionario["EDAD"] == "22" &&
                diccionario["DATE"] == DateTime.Now.Date.ToString() &&
                diccionario["DICCIONARIO.CLAVE01"] == "Valor01" &&
                diccionario["DICCIONARIO.CLAVE02"] == "Valor02" &&
                diccionario["ARRAYSTRING.LENGHT"] == "3" &&
                diccionario["ARRAYSTRING.[0]"] == "1" &&
                diccionario["ARRAYSTRING.[1]"] == "2" &&
                diccionario["ARRAYSTRING.[2]"] == "3" &&
                diccionario["NAMEVALUE.NOMBRE"] == "Valor" 
            );
        }
    }
}
