using Garciss.Core.Common.Converter;
using Garciss.Core.Common.MockConverter.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Common.TestConverter {
    [TestClass]
    public class TestConvertXml {
        readonly string jsonToCompare = @"{
""Book"": [
{
""Author"": ""Garghentini, Davide"",
""Title"": ""XML Developer's Guide"",
""Genre"": ""Computer"",
""Price"": ""44.95"",
""PublishDate"": ""2000-10-01"",
""Description"": ""An in-depth look at creating applications with XML."",
""id"": ""bk101""
},
{
""Author"": ""Garcia, Debra"",
""Title"": ""Midnight Rain"",
""Genre"": ""Fantasy"",
""Price"": ""5.95"",
""PublishDate"": ""2000-12-16"",
""Description"": ""A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world."",
""id"": ""bk102""
}
]
}".Replace("\r\n", string.Empty).Replace(": ", ":");

        [TestMethod]
        public void TestXMLToJson() {
            var json = ConvertXml.XMLStringToJson(XmlMock.XML);
            Assert.IsTrue(json.Equals(jsonToCompare));
        }

        [TestMethod]
        public void TestXmlFileToJSon() {
            var json = ConvertXml.XMLFileToJson(XmlMock.XML_PATH);
            Assert.IsTrue(json.Equals(jsonToCompare));
        }

    }
}
