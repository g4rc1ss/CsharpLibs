using Core.Common.Helper.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.TestHelper.Converters {
    [TestClass]
    public class TestXML {
        [TestMethod]
        public void TestXMLToJson() {
            var xml = @"
<Catalog>
   <Book id=""bk101"">
      <Author>Garghentini, Davide</Author>
      <Title>XML Developer's Guide</Title>
      <Genre>Computer</Genre>
      <Price>44.95</Price>
      <PublishDate>2000-10-01</PublishDate>
      <Description>An in-depth look at creating applications
      with XML.</Description>
   </Book>
   <Book id=""bk102"">
      <Author>Garcia, Debra</Author>
      <Title>Midnight Rain</Title>
      <Genre>Fantasy</Genre>
      <Price>5.95</Price>
      <PublishDate>2000-12-16</PublishDate>
      <Description>A former architect battles corporate zombies,
      an evil sorceress, and her own childhood to become queen
      of the world.</Description>
   </Book>
</Catalog>";
            var json = ConvertHelper.XMLStringToJson(xml);
            Assert.IsTrue(json.Equals("{\"Book\":[{\"Author\":\"Garghentini, Davide\",\"Title\":\"XML Developer's Guide\",\"Genre\":\"Computer\"," +
                    "\"Price\":\"44.95\",\"PublishDate\":\"2000-10-01\"," +
                    "\"Description\":\"An in-depth look at creating applications\\r\\n" +
                    "      with XML.\",\"id\":\"bk101\"}," +

                "{\"Author\":\"Garcia, Debra\",\"Title\":\"Midnight Rain\",\"Genre\":\"Fantasy\"," +
                    "\"Price\":\"5.95\",\"PublishDate\":\"2000-12-16\",\"Description\":\"A former architect battles corporate zombies,\\r\\n" +
                    "      an evil sorceress, and her own childhood to become queen\\r\\n      of the world.\",\"id\":\"bk102\"}]}")

            );
        }

        [TestMethod]
        public void TestXmlFileToJSon() {
            var json = ConvertHelper.XMLFileToJson(@"Converters\Fake\TestXml.xml");
            var jsonToCompare = "{\"Book\":[{\"Author\":\"Garghentini, Davide\",\"Title\":\"XML Developer's Guide\",\"Genre\":\"Computer\"," +
                "\"Price\":\"44.95\",\"PublishDate\":\"2000-10-01\",\"Description\":\"An in-depth look at creating applications\\r\\n" +
                "      with XML.\",\"id\":\"bk101\"}," +
                "{\"Author\":\"Garcia, Debra\",\"Title\":\"Midnight Rain\",\"Genre\":\"Fantasy\",\"Price\":\"5.95\"," +
                "\"PublishDate\":\"2000-12-16\"," +
                "\"Description\":\"A former architect battles corporate zombies,\\r\\n      an evil sorceress, and her own childhood to become queen\\r\\n" +
                "      of the world.\",\"id\":\"bk102\"}]}";
            Assert.IsTrue(((string)json).Equals(jsonToCompare));
        }

    }
}
