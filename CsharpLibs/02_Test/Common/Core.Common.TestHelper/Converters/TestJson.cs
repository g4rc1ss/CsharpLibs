using Core.Common.Helper.Converters;
using Core.Common.TestHelper.Converters.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Core.Common.TestHelper.Converters {
    [TestClass]
    public class TestJson {
        [TestMethod]
        public void TestJsonToObject() {
            var dict = new Dictionary<string, string>() {
                { "Nombre", "namememememe" },
                { "Edad", "22" }
            };
            var json = ConvertHelper.ObjectToJson(dict);
            Assert.IsTrue(
                json == @"{""Nombre"":""namememememe"",""Edad"":""22""}"
            );
        }

        [TestMethod]
        public void ObjectToJson() {
            var json = @"{""Nombre"":""namememememe"",""Edad"":""22""}";
            var diccionario = ConvertHelper.JsonToObject<Dictionary<string, string>>(json);
            Assert.IsTrue(
                diccionario["Nombre"] == "namememememe" &&
                diccionario["Edad"] == "22"
            );
        }

        [TestMethod]
        public void TestJsonToObjectWithIsoDateTimeConverter() {
            var json = @"{""Nombre"":""namememememe"",""Edad"":22,""Date"":""10112019""}";
            var clase = ConvertHelper.JsonToObject<OtherClass>(json, new IsoDateTimeConverter { DateTimeFormat = "ddMMyyyy" });
            Assert.IsTrue(
                clase.Nombre.Equals("namememememe") &&
                clase.Edad == 22 &&
                clase.Date == Convert.ToDateTime("10/11/2019")
            );
        }

        [TestMethod]
        public void TestJsonToObjectWitihSettings() {
            var json = @"{""Nombre"":""namememememe"",""Edad"":22,""Date"":""10/11/2019""}";
            var clase = ConvertHelper.JsonToObject<OtherClass>(json,
                new Newtonsoft.Json.JsonSerializerSettings {
                    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat
                });
            Assert.IsTrue(
                clase.Nombre == "namememememe" &&
                clase.Edad == 22 &&
                clase.Date == Convert.ToDateTime("11/10/2019")
            );
        }
    }
}
