using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DirectoriosArchivos;

namespace TestDirectoriosArchivos {
    [TestClass]
    public class Archivos {
        [TestMethod]
        public void Ordenar() {
            var archivos = Directory.GetDirectories("prueba", "", SearchOption.AllDirectories);

            var listaOrdenada = DirectoryAndFiles.FicherosOrdenados(archivos);

            Assert.IsTrue(listaOrdenada.Count == archivos.Length);
        }
    }
}
