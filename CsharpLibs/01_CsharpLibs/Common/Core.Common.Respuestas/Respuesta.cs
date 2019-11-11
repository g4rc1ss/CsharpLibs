using Core.Data.Databases.SQLite;
using System;
using System.Diagnostics;
using System.IO;

namespace Core.Common.Respuestas {
    /// <summary>
    /// Clase que contiene un objeto para gestionar resultados y excepciones
    /// principalemente en aplicaciones Web, hace que se devuelva los
    /// resultados en un formato parecido o igual
    /// </summary>
    public class Respuesta {

        public int OK { get; } = 0;
        /// <summary>
        /// Propiedad que contiene el codigo de un error
        /// </summary>
        public int Resultado { get; private set; }

        /// <summary>
        /// Propiedad para enseñar un mensaje de error, advertencia, etc
        /// </summary>
        public string Mensaje { get; private set; }

        /// <summary>
        /// Propiedad para señalizar la funcionalidad
        /// </summary>
        public string Funcionalidad { get; private set; }

        /// <summary>
        /// Se usa para no retornar datos
        /// </summary>
        /// <example>
        /// <code>
        /// Respuesta resp1 = new Respuesta();
        /// </code>
        /// </example>
        public Respuesta() {
            Resultado = 0;
            Mensaje = string.Empty;
            Funcionalidad = string.Empty;
        }

        /// <summary>
        /// Se inicializa la respuesta con un resultado, mensaje y quiza, funcionalidad
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="mensaje"></param>
        /// <param name="funcionalidad"></param>
        public Respuesta(int resultado, string mensaje, string funcionalidad = "") {
            Resultado = resultado;
            Mensaje = mensaje;
            Funcionalidad = funcionalidad;
        }

        /// <summary>
        /// Se agrega una excepcion y se retorna
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="funcionalidad"></param>
        /// <param name="guardarLog"></param>
        public Respuesta(Exception ex, string funcionalidad = "", bool guardarLog = true, DondeGuardar donde = DondeGuardar.ArchivoTexto) {
            var stackTrace = new StackTrace(2, true);

            Resultado = ex.HResult;
            Mensaje = ex.Message;
            Funcionalidad = funcionalidad;

            if (guardarLog) {
                switch (donde) {
                    case DondeGuardar.ArchivoTexto: {
                        GuardarTrazaArchivoTexto(ex, stackTrace);
                        break;
                    }
                    case DondeGuardar.BaseDatosLocal: {
                        GuardarTrazaBaseDatosLocal(ex, stackTrace);
                        break;
                    }
                    case DondeGuardar.BaseDatosMSSQL: {
                        GuardarTrazaMSSQL();
                        break;
                    }
                }
            }
        }

        private void GuardarTrazaArchivoTexto(Exception ex, StackTrace stackTrace) {
            try {
                var fecha = DateTime.Now.ToString("yyyy-MM-dd");
                var hora = DateTime.Now.ToString("HH:mm:ss");
                var nombreFichero = $"{fecha}.log";
                var contenido = string.Empty;

                if (File.Exists(nombreFichero)) {
                    using (var leer = File.OpenText(nombreFichero)) {
                        contenido = leer.ReadToEnd();
                    }
                }
                using (var escribir = File.CreateText(nombreFichero)) {
                    escribir.Write($"{contenido} \n" +
                        $"[{fecha}] ~ [{hora}] {stackTrace.GetFrame(1).GetMethod().Name} - {Mensaje} \n");
                }
            } catch (Exception) {
                try {
                    GuardarTrazaBaseDatosLocal(ex, stackTrace);
                } catch (Exception) {
                    throw new Exception("Se ha producido un error al guardar el log en un archivo de texto y en una base de datos local");
                }
            }
        }

        private void GuardarTrazaBaseDatosLocal(Exception ex, StackTrace stackTrace) {
            var sqlite = new SQLiteDB() { DBName = $"Log-{DateTime.Now.ToString("ddMMyyy")}.db" };
            sqlite.CreateDatabase("CREATE TABLE LOG(" +
                    "ID             INT       PRIMARY KEY      NOT NULL," +
                    "CODIGOERROR    TEXT                       NOT NULL," +
                    "MENSAJE        TEXT                       NOT NULL)");
            sqlite.UpdateOrInsert($"INSERT INTO LOG (ID, CODIGOERROR, MENSAJE) " +
                $"VALUES ({sqlite.MaxID("ID", "LOG")}, '{Resultado.ToString()}', '{Mensaje}')");
        }

        private void GuardarTrazaMSSQL() {
            //TODO
        }
    }

    public enum DondeGuardar {
        Desconocido = -1,
        ArchivoTexto = 0,
        BaseDatosLocal = 1,
        BaseDatosMSSQL = 2
    }
}
