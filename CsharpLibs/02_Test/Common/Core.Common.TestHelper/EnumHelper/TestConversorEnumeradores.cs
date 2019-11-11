using Core.Common.Helper.EnumHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.TestEnumHelper.EnumHelper {
    [TestClass]
    public class TestConversorEnumeradores {
        [TestMethod]
        public void TestEnumItemFromKeyEnvioClave() {
            var enumeradorPrueba = EnumFactory.EnumItemFromKey<EnumTestFake>("10");
            Assert.IsTrue(enumeradorPrueba == EnumTestFake.testNumUno);
        }

        [TestMethod]
        public void TestEnumItemFromKeyEnvioArray() {
            string[] clavesPropias = { "Cero", "Uno", "Dos", "Tres" };
            var enumeradorPrueba = EnumFactory.EnumItemFromKey<EnumTestFakeSinClaves>(clavesPropias, "Dos");
            Assert.IsTrue(enumeradorPrueba == EnumTestFakeSinClaves.enumSinClave2);
        }

        [TestMethod]
        public void TestKeyFromEnumItemEnvioEnumerador() {
            var clavePrueba = EnumFactory.KeyFromEnumItem(EnumTestFake.testNumDiez);
            Assert.IsTrue(clavePrueba == "hgfjrhguor");
        }

        [TestMethod]
        public void TestKeyFromEnumItemEnvioArray() {
            string[] clavesPropias = { "Cero", "Uno", "Dos", "Tres" };
            var clavePrueba = EnumFactory.KeyFromEnumItem(clavesPropias, EnumTestFakeSinClaves.enumSinClave3);
            Assert.IsTrue(clavePrueba == clavesPropias[3]);
        }

        [TestMethod]
        public void TestParseStringToEnum() {
            var enumeradorConvertido = EnumFactory.ParseEnum<EnumTestFakeSinClaves>("enumSinClave3");
            Assert.IsTrue(enumeradorConvertido == EnumTestFakeSinClaves.enumSinClave3);
        }

        [TestMethod]
        public void TestParseStringEnumNullable() {
            var enumeradorConvertido = EnumFactory.ParseEnumNullable<EnumTestFakeSinClaves>(null);
            Assert.IsTrue(enumeradorConvertido == null);
        }

        [TestMethod]
        public void TestParseStringToEnumValue() {
            var enumeradorConvertido = EnumFactory.ParseEnumValue<EnumTestFake>("unknow");
            Assert.IsTrue(enumeradorConvertido == EnumTestFake.unknow);
        }
    }
}
