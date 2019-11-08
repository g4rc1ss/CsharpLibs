using Core.Data.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TestLogs {
    [TestClass]
    public class TestFicheroLog {

        [TestMethod]
        public void Logs() {
            string fecha = System.DateTime.Now.ToString("yyyy-MM-dd");

            try {
                Log.CrearLogs(this, Modos.DEBUG);
                Log.CrearLogs(this, Modos.LOGGING, mensaje: "Hola, este es un mensaje del logging");

                try {
                    int x = 0; int y = 1;
                    int z = y / x;
                } catch (DivideByZeroException e) {
                    Log.CrearLogs(this, Modos.ERROR, e);
                }
                Assert.IsTrue(File.Exists($"{fecha}.log"));
                //$"{fecha}.log"
                using (var read = new StreamReader($"{fecha}.log")) {
                    string[] modosParaComprobar = { "[DEBUG]", "[LOGGING]", "[ERROR]" };

                    string linea = read.ReadToEnd();
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
