using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DirectoriosArchivos;

namespace TestDirectoriosArchivosCore {
    [TestClass]
    public class TestArchivos {

        [TestMethod]
        public void Ordenar() {
            var archivos = Directory.GetDirectories("prueba", "", SearchOption.AllDirectories);

            var listaOrdenada = Archivos.FicherosOrdenados(archivos);

            Assert.IsTrue(listaOrdenada.Count == archivos.Length);
        }
    }
}
