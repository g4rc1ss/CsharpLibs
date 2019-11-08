using Core.Common.EnumHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.TestEnumHelper {
    [TestClass]
    public class TestConversorEnumeradores {
        [TestMethod]
        public void TestEnumItemFromKey() {
            var enumeradorPrueba = EnumFactory.EnumItemFromKey<EnumTestFake>("02");
        }

        [TestMethod]
        public void TestKeyFromEnumItem() {
            var clavePrueba = EnumFactory.KeyFromEnumItem<EnumTestFake>(EnumTestFake.unknow);
        }
    }
}
