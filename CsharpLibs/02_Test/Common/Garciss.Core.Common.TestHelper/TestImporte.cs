using Core.Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.TestHelper {
    [TestClass]
    public class TestImporte {
        [TestMethod]
        public void ImporteTest() {
            var importe1 = new Importe(2000, "EUR");
            var importe2 = new Importe(3000);

            var suma = importe1 + importe2;
            var resta = importe2 - importe1;

            Assert.IsTrue(suma.Cantidad == 5000 && suma.Moneda.Equals("EUR") && resta.Cantidad == 1000);
        }
    }
}
