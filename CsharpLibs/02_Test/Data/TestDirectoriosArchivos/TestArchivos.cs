using Core.Data.DirectoriosArchivos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestDirectoriosArchivos {
    [TestClass]
    public class TestArchivos {

        [TestMethod]
        public void Ordenar() {
            string[] archivos = Directory.GetDirectories("prueba", "", SearchOption.AllDirectories);

            var listaOrdenada = Archivos.FicherosOrdenados(archivos);

            Assert.IsTrue(listaOrdenada.Count == archivos.Length);
        }
    }
}
