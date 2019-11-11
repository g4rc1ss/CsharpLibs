using Core.Common.Respuestas;
using Core.Common.TestRespuesta.Fake;
using Core.Data.Databases.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.IO;

namespace Core.Common.TestRespuesta {
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
                resp1.Datos == null && resp1.Mensaje == "" && resp1.Resultado == 0 &&
                object.ReferenceEquals(resp2.Datos.GetType(), new DatosFake().GetType()) &&
                resp2.Mensaje == string.Empty && resp2.Resultado == resp2.OK
            );
        }

        [TestMethod]
        public void RespuestaT_ExceptionLocal() {
            var nombreFichero = $"{DateTime.Now.ToString("yyy-MM-dd")}.log";
            var nombreBaseDatos = $"Log-{DateTime.Now.ToString("ddMMyyy")}.db";
            try {
                Respuesta<DatosFake> resp = null, resp2 = null;
                Exception ex = null;
                try {
                    var x = 0; var y = 1; var z = y / x;
                } catch (Exception e) {
                    resp = new Respuesta<DatosFake>(e, "RespuestaT_ExceptionLocal");
                    ex = e;
                }
                Assert.IsTrue(File.Exists(nombreFichero));
                using (var leer = File.OpenText(nombreFichero)) {
                    var leido = leer.ReadToEnd();
                    Assert.IsTrue(
                        leido.Contains(resp.Mensaje) &&
                        resp.Mensaje == ex.Message &&
                        resp.Resultado == ex.HResult
                    );
                }

                try {
                    var x = 0; var y = 1; var z = y / x;
                } catch (Exception e) {
                    resp2 = new Respuesta<DatosFake>(e, "RespuestaT_ExceptionLocal", true, DondeGuardar.BaseDatosLocal);
                }

                var sqlite = new SQLiteDB() { DBName = nombreBaseDatos };
                Assert.IsTrue(sqlite.IsCreateDatabase());

                var respuestaSelect = sqlite.Select("SELECT * FROM LOG");
                var row = respuestaSelect.Rows[0];

                var codError = row.Field<string>("CODIGOERROR");
                var mensaje = row.Field<string>("MENSAJE");

                Assert.IsTrue(
                    codError == resp2.Resultado.ToString() &&
                    mensaje == resp2.Mensaje
                 );

            } finally {
                File.Delete(nombreBaseDatos);
                File.Delete(nombreFichero);
            }
        }

        [TestMethod]
        public void RespuestaT_ExceptionMSSQL() {

        }

    }
}
