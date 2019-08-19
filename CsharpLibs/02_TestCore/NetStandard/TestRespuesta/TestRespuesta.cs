using Microsoft.VisualStudio.TestTools.UnitTesting;
using Respuestas;
using System;

namespace TestRespuesta {
    [TestClass]
    // respuesta es una clase que recibe un objeto y lugo se trata para extraer los resultados
    // de un modo especifico
    public class TestRespuestaCore {
        [TestMethod]
        public void RespuestaConstrutores() {
            Respuesta resp1 = null, resp2 = null, resp3 = null;
            Exception ex = null;
            try {
                resp1 = new Respuesta();
                resp2 = new Respuesta(new Datos() {
                    nombre = "Asier",
                    apellido = "garcia",
                    fecha = DateTime.Now,
                    direccion = "alguna",
                    edad = 22,
                    salario = 1000.00
                });
                int x = 0; int y = 1; int z = y / x;
            } catch (Exception e) {
                resp3 = new Respuesta(e, Errores.ZeroDivision);
                ex = e;
            }

            Assert.IsTrue(
                resp1.datos == null && resp1.mensaje == "" && resp1.resultado == 0 &&

                object.ReferenceEquals(resp2.datos.GetType(), new Datos().GetType()) && 
                resp2.mensaje == string.Empty && resp2.resultado == 0 &&

                resp3.mensaje == ex.Message && resp3.resultado != Errores.sinError
                );
        }
    }

    // Me creo una clase de datos que es lo que le voy a mandar a respuesta
    internal class Datos {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha { get; set; }
        public string direccion { get; set; }
        public int edad { get; set; }
        public double salario { get; set; }
    }
    // Me creo una estructura de errores paraa pasarlos
    // por resultado, por convencion seria bueno crear esto, puesto
    // que un proyecto solitario es normal que tenga sus propios errores internos
    // y hay que reflegarlos
    public struct Errores {
        public const int sinError = 0;
        public const int general = 9999;
        public const int ZeroDivision = 1;
    }
}
