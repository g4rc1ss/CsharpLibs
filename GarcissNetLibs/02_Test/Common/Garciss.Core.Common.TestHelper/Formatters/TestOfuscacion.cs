using Garciss.Core.Common.Helper.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Common.TestHelper.Formatters {
    [TestClass]
    public class TestOfuscacion {
        [TestMethod]
        public void TestOfuscarCorreo() {
            var ofuscando = Format.OfuscarCorreo("prueba@gmail.com");
            Assert.AreEqual("****ba@gmail.com", ofuscando);
        }

        [TestMethod]
        public void TestOfuscarTelefonoMovil() {
            var ofuscando = Format.OfuscarMovil("666555777");
            Assert.AreEqual("666*****7", ofuscando);
        }
    }
}
