using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Files {
    /// <summary>
    /// Clase con metodos utiles para la gestion de archivos que no estan
    /// disponibles en las librerias habituales
    /// </summary>
    public class Archivos {

        /// <summary>
        /// Ordenamos una lista de ficheros
        /// </summary>
        /// <returns>
        /// Devolvemos una List de tipo string con los elementos ordenados
        /// </returns>
        /// <param name="listaDesordenada">Recibimos un array tipo string</param>
        /// <example>
        /// <code>
        /// string[] archivos = Directory.GetDirectories("prueba", "", SearchOption.AllDirectories);
        /// var listaOrdenada = Archivos.FicherosOrdenados(archivos);
        /// </code>
        /// </example>
        public static List<string> FicherosOrdenados(string[] listaDesordenada) {
            return new Archivos().OrdenarFicheros(listaDesordenada);
        }

        private List<string> OrdenarFicheros(string[] listaDesordenada) {
            var listaOrdenada = (from item in listaDesordenada
                                 orderby item
                                 select item).ToList();

            return listaOrdenada;
        }
    }
}
