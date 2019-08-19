using System;
using System.Diagnostics;
using System.IO;

namespace Logs {
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
        public static bool crearLogs(object obj, modos modo, Exception ex = null, string mensaje = null) {
            return new Log().save(obj, modo, ex, mensaje);
        }
        private bool save(object obj, modos modo, Exception ex, string mensaje) {
            try {
                string fecha = System.DateTime.Now.ToString("yyyy-MM-dd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
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
                StackTrace stacktrace = new StackTrace();

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
    public enum modos {
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
