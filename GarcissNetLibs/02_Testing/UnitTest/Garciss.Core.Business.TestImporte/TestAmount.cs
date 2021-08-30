using Garciss.Core.Business.Importe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Business.TestImporte {
    [TestClass]
    public class TestAmount {
        [TestMethod]
        public void ImporteTest() {
            var importe1 = new Amount(2000);
            var importe2 = new Amount(3000, "EUR");
            var importe3 = new Amount(null, null);
            var importe4 = new Amount(null);
            var importe5 = new Amount(3000.05M, "USA");
            var importe6 = new Amount("3000.67");
            var importe7 = new Amount("4589,56");

            var suma = importe1 + importe2;
            var resta = importe2 - importe1;
            var multiplicacion = importe1 * importe2;
            var division = importe1 / importe2;

            Assert.IsTrue(suma.Cantidad == 5000 && suma.Moneda.Equals("EUR") 
                && resta.Cantidad == 1000 && resta.Moneda.Equals("EUR")
                && multiplicacion.Cantidad == 6_000_000M && multiplicacion.Moneda.Equals("EUR")
                && division.Cantidad == 0.6666666666666666666666666667M && division.Moneda.Equals("EUR")
                && importe3.Cantidad is null && importe3.Moneda is null
                && importe4.Cantidad is null && importe4.Moneda is "EUR"
                && importe5.Cantidad == 3000.05M && importe5.Moneda is "USA"
                && importe6.Cantidad == 3000.67m
                && importe7.Cantidad == 4589.56m);
        }
    }
}
