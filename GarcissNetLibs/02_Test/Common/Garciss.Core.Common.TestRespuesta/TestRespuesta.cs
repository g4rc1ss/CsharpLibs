using Garciss.Core.Common.Respuestas;
using Garciss.Core.Common.TestRespuesta.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Garciss.Core.Common.TestRespuesta {
    [TestClass]
    // respuesta es una clase que recibe un objeto y lugo se trata para extraer los resultados
    // de un modo especifico
    public class TestRespuesta {
        [TestMethod]
        public void RespuestaT() {
            var resp1 = new Respuesta<DatosFake>();

            var resp2 = new Respuesta<DatosFake>(new DatosFake() {
                Nombre = "Asier",
                Apellido = "garcia",
                Fecha = DateTime.Now,
                Direccion = "alguna",
                Edad = 22,
                Salario = 1000.00
            });

            Assert.IsTrue(
                resp1.Datos == null && resp1.Mensaje == "" && resp1.Resultado == Respuesta.OK &&
                object.ReferenceEquals(resp2.Datos.GetType(), new DatosFake().GetType()) &&
                resp2.Mensaje == string.Empty && resp2.Resultado == Respuesta.OK
            );
        }
    }
}
