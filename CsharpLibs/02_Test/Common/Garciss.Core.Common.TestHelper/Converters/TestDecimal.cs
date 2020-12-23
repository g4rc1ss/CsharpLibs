using Garciss.Core.Common.Helper.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Common.TestHelper.Converters {
    [TestClass]
    public class TestDecimal {
        [TestMethod]
        public void ConvertToDecimal() {
            var numero = ConvertHelper.ToDecimal("2.000,54");
            Assert.IsTrue(numero == 2000.54m);
        }
    }
}
