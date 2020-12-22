using Microsoft.Extensions.Logging;
using System;

namespace Garciss.Core.Common.Respuestas {
    /// <summary>
    /// Clase respuesta, para generalizar la devolucion de objetos etc y tracear
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Respuesta<T> :Respuesta {

        /// <summary>
        /// propiedad que va a contener un objeto
        /// </summary>
        public T Datos { get; set; }

        /// <summary>
        /// Contructor sin nada
        /// </summary>
        public Respuesta(ILogger logger = null) : base(logger) { }

        /// <summary>
        /// Se inserta un objeto de cualquier tipop para retornar
        /// </summary>
        /// <param name="datos"></param>
        public Respuesta(T datos, ILogger logger = null) : base(logger) {
            Datos = datos;
        }

        /// <summary>
        /// Para ir traceando y retornar los resultados
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="mensaje"></param>
        /// <param name="funcionalidad"></param>
        public Respuesta(int resultado, string mensaje, string funcionalidad, ILogger logger = null)
            : base(resultado, mensaje, funcionalidad, logger) {
        }

        /// <summary>
        /// Se usara para tracear las excepciones y almacenarlas en logs
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="funcionalidad"></param>
        /// <param name="guardarLog"></param>
        public Respuesta(Exception ex, string funcionalidad = "", ILogger logger = null)
            : base(ex, funcionalidad, logger) {
        }
    }
}
