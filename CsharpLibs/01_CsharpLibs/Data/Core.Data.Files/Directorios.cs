using System.IO;
using System.Threading.Tasks;

namespace Core.Data.Files {
    /// <summary>
    /// Clase para tratamiento de directorios que no estan disponibles en las
    /// librerias habituales
    /// </summary>
    public class Directorios {

        /// <summary>
        /// Copiar directorios recursivamente(todos los archivos y subdirectorios)
        /// </summary>
        /// <param name="origen">directorio origen</param>
        /// <param name="destino">directorio destino</param>
        public static void Copy(DirectoryInfo origen, DirectoryInfo destino) {
            new Directorios().CopyDirectory(origen, destino);
        }
        private void CopyDirectory(DirectoryInfo origen, DirectoryInfo destino) {
            // Comprueba que el destino exista:
            if (!destino.Exists) {
                destino.Create();
            }

            // Copia todos los archivos del directorio actual:
            Parallel.ForEach(origen.EnumerateFiles(), (archivo) => {
                archivo.CopyTo(Path.Combine(destino.FullName, archivo.Name), true);
            });

            // Procesamiento recursivo de subdirectorios:
            Parallel.ForEach(origen.EnumerateDirectories(), (directorio) => {
                // Obtención de directorio de destino:
                string directorioDestino = Path.Combine(destino.FullName, directorio.Name);

                // Invocación recursiva del método `CopiarDirectorio`:
                CopyDirectory(directorio, new DirectoryInfo(directorioDestino));
            });
        }
    }
}

