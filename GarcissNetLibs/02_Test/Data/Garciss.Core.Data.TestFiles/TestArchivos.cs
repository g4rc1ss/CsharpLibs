using System.IO;
using Garciss.Core.Data.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Data.TestFiles {
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
