using System;
using System.Diagnostics;
using System.IO;

namespace Core.Data.Logs {
    /// <summary>
    /// Clase para crear Logs de registro de las aplicaciones
    /// </summary>
    public class Log {
        /// <summary>
        /// Metodo para crear un archivo .log, se crea en la ruta donde esta
        /// el archivo compilado
        /// </summary>
        /// <param name="obj">recibe informacion sobre el objeto que le mandas, suele ser "this"</param>
        /// <param name="modo">
        /// recibes un enumerador para seleccionar el tipo de Log que quieres guardar
        /// </param>
        /// <param name="ex">
        /// Recibe la informacion de la excepcion que ha sido lanzada
        /// </param>
        /// <param name="mensaje">
        /// Solo se añadira al fichero LOG cuando "EX" sea null, el motivo es para
        /// que se pueda hacer uso del modo LOGGING(explicado su uso mas abajo) o DEBUG(no tan habitual)
        /// el cual no se usa con Excepciones
        /// </param>
        /// <example>
        /// <code>
        /// Log.CrearLogs(this, Modos.DEBUG);
        /// </code>
        /// </example>
        public static bool CrearLogs(object obj, Modos modo, Exception ex = null, string mensaje = null) {
            return new Log().Save(obj, modo, ex, mensaje);
        }
        private bool Save(object obj, Modos modo, Exception ex, string mensaje) {
            try {
                var fecha = System.DateTime.Now.ToString("yyyy-MM-dd");
                var hora = System.DateTime.Now.ToString("HH:mm:ss");
                string contenido = null;
                StreamWriter escribir;
                StreamReader leer;

                try {
                    leer = File.OpenText($"{fecha}.log");
                    contenido = leer.ReadToEnd();
                    leer.Close();
                } catch (Exception) {
                }

                escribir = File.CreateText($"{fecha}.log");
                var stacktrace = new StackTrace();

                escribir.Write(contenido);
                escribir.WriteLine(obj.GetType().FullName + " " + hora);
                escribir.WriteLine($"[{modo.ToString()}] {stacktrace.GetFrame(1).GetMethod().Name} - {(ex != null ? ex.Message : mensaje)}");
                escribir.WriteLine("");

                escribir.Flush();
                escribir.Close();
                return true;
            } catch (Exception) {
                return false;
            }
        }

    }

    /// <summary>
    /// Enumerador para seleccionar el modo de Log
    /// </summary>
    public enum Modos {
        /// <summary>
        /// [DEBUG] -> se usa para depurar, indicar trazas, etc
        /// </summary>
        DEBUG = 0,
        /// <summary>
        /// [ERROR] -> Se usa habitualmente cuando se lanzan excepciones en el programa
        /// </summary>
        ERROR = 1,
        /// <summary>
        /// [LOGGING] -> Se usa para registrar comportamientos de la aplicacion informativos
        /// por ejemplo, "se ha iniciado sesion en" o "se ha creado un archivo x"
        /// </summary>
        LOGGING = 2
    }
}
