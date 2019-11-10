using Core.Data.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Core.Data.TestLogs {
    [TestClass]
    public class TestFicheroLog {

        [TestMethod]
        public void Logs() {
            var fecha = System.DateTime.Now.ToString("yyyy-MM-dd");

            try {
                Log.CrearLogs(this, Modos.DEBUG);
                Log.CrearLogs(this, Modos.LOGGING, mensaje: "Hola, este es un mensaje del logging");

                try {
                    var x = 0; var y = 1;
                    var z = y / x;
                } catch (DivideByZeroException e) {
                    Log.CrearLogs(this, Modos.ERROR, e);
                }
                Assert.IsTrue(File.Exists($"{fecha}.log"));
                //$"{fecha}.log"
                using (var read = new StreamReader($"{fecha}.log")) {
                    string[] modosParaComprobar = { "[DEBUG]", "[LOGGING]", "[ERROR]" };

                    var linea = read.ReadToEnd();
                    Assert.IsTrue(
                        linea.Contains("[DEBUG]") &&
                        linea.Contains("[LOGGING]") &&
                        linea.Contains("[ERROR]")
                        );
                }
            } finally {
                File.Delete($"{fecha}.log");
            }
        }
    }
}
