using Garciss.Core.Common.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Common.TestFormatters {
    [TestClass]
    public class TestOfuscacion {
        [TestMethod]
        public void TestOfuscarCorreo() {
            var ofuscando = Obfuscation.OfuscarCorreo("prueba@gmail.com");
            Assert.AreEqual("****ba@gmail.com", ofuscando);
        }

        [TestMethod]
        public void TestOfuscarTelefonoMovil() {
            var ofuscando = Obfuscation.OfuscarMovil("666555777");
            Assert.AreEqual("666*****7", ofuscando);
        }
    }
}
