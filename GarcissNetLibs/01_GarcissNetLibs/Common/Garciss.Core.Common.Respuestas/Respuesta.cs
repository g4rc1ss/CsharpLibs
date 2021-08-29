using System;
using Microsoft.Extensions.Logging;

namespace Garciss.Core.Common.Respuestas {
    /// <summary>
    /// Clase que contiene un objeto para gestionar resultados y excepciones
    /// principalemente en aplicaciones Web, hace que se devuelva los
    /// resultados en un formato parecido o igual
    /// </summary>
    public class Respuesta {
        /// <summary>
        /// Para comparar, OK es 0
        /// </summary>
        public const int OK = 0;
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
        /// Propiedad para agregar la excepcion pasada
        /// </summary>
        private Exception Excepcion { get; set; }

        /// <summary>
        /// Constructor devolviendo una respuesta con codigo 0
        /// Se le pasará un logger para ser registrado este evento
        /// </summary>
        /// <param name="logger">Interfaz generica de Microsoft.Extensions.Logging</param>
        public Respuesta(ILogger logger = null) {
            Resultado = 0;
            Mensaje = string.Empty;
            Funcionalidad = string.Empty;
            EjecutarLogger(logger, TipoLogger.Information);
        }

        /// <summary>
        /// Se inicializa la respuesta con un resultado, mensaje y quiza, funcionalidad
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="mensaje"></param>
        /// <param name="funcionalidad"></param>
        /// <param name="logger"></param>
        public Respuesta(int resultado, string mensaje, string funcionalidad = "", ILogger logger = null) {
            Resultado = resultado;
            Mensaje = mensaje;
            Funcionalidad = funcionalidad;
            EjecutarLogger(logger, TipoLogger.Error);
        }

        /// <summary>
        /// Se agrega una excepcion y se retorna
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="funcionalidad"></param>
        /// <param name="guardarLog"></param>
        /// <param name="logger"></param>
        public Respuesta(Exception ex, string funcionalidad = "", ILogger logger = null) {
            Excepcion = ex;
            Resultado = ex.HResult;
            Mensaje = ex.Message;
            Funcionalidad = funcionalidad;
            EjecutarLogger(logger, TipoLogger.Fatal);
        }

        private void EjecutarLogger(ILogger logger, TipoLogger tipoLogger) {
            if (logger is not null) {
                var logMessage = $"Resultado: {Resultado}; \n Funcionalidad: {Funcionalidad}; \n Mensaje: {Mensaje};";
                switch (tipoLogger) {
                    case TipoLogger.Information:
                        logger.LogInformation(logMessage);
                        break;
                    case TipoLogger.Error:
                        logger.LogError(logMessage);
                        break;
                    case TipoLogger.Fatal:
                        logger.LogCritical(Excepcion, logMessage);
                        break;
                }
            }
        }
    }
}
