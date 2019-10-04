using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoriosArchivos;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace TestDirectoriosArchivos {
    [TestClass]
    public class TestDirectorios {

        [TestMethod]
        public void Copiar() {
            try {
                Directorios.Copy(
                    new DirectoryInfo("prueba"),
                    new DirectoryInfo("copia")
                );

                var archivosOrigen = new List<FileInfo>(new DirectoryInfo("prueba").GetFiles("*", SearchOption.AllDirectories));
                var nombresOrigen = (from arch in archivosOrigen
                                     select arch.Name).ToList();

                var archivosDestino = new List<FileInfo>(new DirectoryInfo("copia").GetFiles("*", SearchOption.AllDirectories));
                var nombresDestino = (from dest in archivosDestino
                                      select dest.Name).ToList();
                archivosOrigen = null;
                archivosDestino = null;

                Assert.IsTrue(Directory.Exists("prueba") && Directory.Exists("copia"));
                Assert.IsTrue(nombresOrigen.Count == nombresDestino.Count);

                foreach(string origen in nombresOrigen)
                    Assert.IsTrue(nombresOrigen.Contains(origen));

            } finally {
                Directory.Delete("copia", true);
            }
        }
    }
}
