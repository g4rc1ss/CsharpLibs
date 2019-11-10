using Core.Common.Helper.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Common.TestHelper.Formatters {
    [TestClass]
    public class TestFecha {
        [TestMethod]
        public void FormatearFecha() {
            var fechaFormateadaBarra = Format.FormatearFecha(DateTime.Now.Date);
            Assert.IsTrue(fechaFormateadaBarra.Contains("/"));
            var fechaFormateadaSinBarra = Format.FormatearFecha(DateTime.Now.Date, "ddMMyyyy");
            Assert.IsTrue(
                !fechaFormateadaSinBarra.Contains("/") &&
                fechaFormateadaSinBarra.Substring(0, 2) == DateTime.Now.Day.ToString() &&
                fechaFormateadaSinBarra.Substring(2, 2) == DateTime.Now.Month.ToString() &&
                fechaFormateadaSinBarra.Substring(4, 4) == DateTime.Now.Year.ToString()
            );
        }
    }
}
