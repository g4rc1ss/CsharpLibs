using System;
using Garciss.Core.Common.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Common.TestFormatters {
    [TestClass]
    public class TestDateTimeFormatter {
        [TestMethod]
        public void FormatearFecha() {
            var fechaFormateadaBarra = DateTimeFormatter.FormatearFecha(DateTime.Now.Date);
            Assert.IsTrue(fechaFormateadaBarra.Contains("/"));
            var fechaFormateadaSinBarra = DateTimeFormatter.FormatearFecha(DateTime.Now.Date, "ddMMyyyy");
            Assert.IsTrue(
                !fechaFormateadaSinBarra.Contains("/") &&
                fechaFormateadaSinBarra.Substring(0, 2).Contains(DateTime.Now.Day.ToString()) &&
                fechaFormateadaSinBarra.Substring(2, 2).Contains(DateTime.Now.Month.ToString()) &&
                fechaFormateadaSinBarra.Substring(4, 4).Contains(DateTime.Now.Year.ToString())
            );
        }

        [TestMethod]
        public void FormatFechaJuliana() {
            var fecha = new DateTime(2019, 11, 10);
            var fechaFormateada = DateTimeFormatter.FormatToFechaJuliana(fecha);
            Assert.AreEqual("9314", fechaFormateada);
        }

        [TestMethod]
        public void ObtenerMilisegundosDiferenciaDates() {
            var fechaDesde = new DateTime(2016, 11, 10);
            var fechaHasta = new DateTime(2019, 11, 10);
            var milisegundosFechaDesdeHasta = DateTimeFormatter.DateDiffMilliSecond(fechaDesde, fechaHasta);
            _ = DateTimeFormatter.DateDiffMilliSecond(fechaDesde);
            Assert.IsTrue(94608000000 == milisegundosFechaDesdeHasta);
        }
    }
}
