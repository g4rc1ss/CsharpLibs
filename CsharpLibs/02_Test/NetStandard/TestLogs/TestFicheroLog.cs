using Logs;
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
                Log.crearLogs(this, modos.DEBUG);
                Log.crearLogs(this, modos.LOGGING, mensaje: "Hola, este es un mensaje del logging");

                try {
                    int x = 0; int y = 1;
                    int z = y / x;
                } catch (DivideByZeroException e) {
                    Log.crearLogs(this, modos.ERROR, e);
                }
                Assert.IsTrue(File.Exists($"{fecha}.log"));
                //$"{fecha}.log"
                using (StreamReader read = new StreamReader($"{fecha}.log")) {
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
