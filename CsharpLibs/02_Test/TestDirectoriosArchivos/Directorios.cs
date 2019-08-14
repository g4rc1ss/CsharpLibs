using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoriosArchivos;
using System.IO;

namespace TestDirectoriosArchivos {
    [TestClass]
    public class Directorios {

        [TestMethod]
        public void Copiar() {
            try {
                DirectoryAndFiles.Copy(
                    new DirectoryInfo("prueba"),
                    new DirectoryInfo("copia")
                );
                Assert.IsTrue(Directory.Exists("prueba") && Directory.Exists("copia"));
            } finally {
                Directory.Delete("copia", true);
            }
        }


    }
}
