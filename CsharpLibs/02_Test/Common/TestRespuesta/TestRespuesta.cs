using Microsoft.VisualStudio.TestTools.UnitTesting;
using Respuestas;
using System;

namespace TestRespuesta {
    [TestClass]
    // respuesta es una clase que recibe un objeto y lugo se trata para extraer los resultados
    // de un modo especifico
    public class TestRespuesta {
        [TestMethod]
        public void RespuestaConstrutores() {
            Respuesta resp1 = null, resp2 = null, resp3 = null;
            Exception ex = null;
            try {
                resp1 = new Respuesta();
                resp2 = new Respuesta(new Datos() {
                    Nombre = "Asier",
                    Apellido = "garcia",
                    Fecha = DateTime.Now,
                    Direccion = "alguna",
                    Edad = 22,
                    Salario = 1000.00
                });
                int x = 0; int y = 1; int z = y / x;
            } catch (Exception e) {
                resp3 = new Respuesta(e, Errores.ZERODIVISION);
                ex = e;
            }

            Assert.IsTrue(
                resp1.Datos == null && resp1.Mensaje == "" && resp1.Resultado == 0 &&

                object.ReferenceEquals(resp2.Datos.GetType(), new Datos().GetType()) &&
                resp2.Mensaje == string.Empty && resp2.Resultado == 0 &&

                resp3.Mensaje == ex.Message && resp3.Resultado != Errores.SINERROR
                );
        }
    }

    // Me creo una clase de datos que es lo que le voy a mandar a respuesta
    internal class Datos {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public double Salario { get; set; }
    }
    // Me creo una estructura de errores paraa pasarlos
    // por resultado, por convencion seria bueno crear esto, puesto
    // que un proyecto solitario es normal que tenga sus propios errores internos
    // y hay que reflegarlos
    public struct Errores {
        public const int SINERROR = 0;
        public const int GENERAL = 9999;
        public const int ZERODIVISION = 1;
    }
}
