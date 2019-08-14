using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoriosArchivos {
    /// <summary>
    /// Clase encargada de gestionar directorios y ficheros, es añadida a las ya existentes
    /// </summary>
    public class DirectoryAndFiles {

        #region Directorios

        /// <summary>
        /// Copiar directorios recursivamente(todos los archivos y subdirectorios)
        /// </summary>
        /// <param name="origen">directorio origen</param>
        /// <param name="destino">directorio destino</param>
        public static void Copy(DirectoryInfo origen, DirectoryInfo destino) {
            new DirectoryAndFiles().CopyDirectory(origen, destino);
        }
        private void CopyDirectory(DirectoryInfo origen, DirectoryInfo destino) {
            // Comprueba que el destino exista:
            if (!destino.Exists) {
                destino.Create();
            }

            // Copia todos los archivos del directorio actual:
            foreach (FileInfo archivo in origen.EnumerateFiles()) {
                archivo.CopyTo(Path.Combine(destino.FullName, archivo.Name), true);
            }

            // Procesamiento recursivo de subdirectorios:
            foreach (DirectoryInfo directorio in origen.EnumerateDirectories()) {
                // Obtención de directorio de destino:
                string directorioDestino = Path.Combine(destino.FullName, directorio.Name);

                // Invocación recursiva del método `CopiarDirectorio`:
                CopyDirectory(directorio, new DirectoryInfo(directorioDestino));
            }
        }
        #endregion

        #region Ficheros

        /// <summary>
        /// Ordenamos una lista de ficheros
        /// </summary>
        /// <returns>
        /// Devolvemos una List de tipo string con los elementos ordenados
        /// </returns>
        /// <param name="listaDesordenada">Recibimos un array tipo string</param>
        public static List<string> FicherosOrdenados(string[] listaDesordenada) {
            return new DirectoryAndFiles().OrdenarFicheros(listaDesordenada);
        }

        private List<string> OrdenarFicheros(string[] listaDesordenada) {
            var listaOrdenada = (from item in listaDesordenada
                                 orderby item
                                 select item).ToList();

            return listaOrdenada;
        }
        #endregion
    }
}

