using System;

namespace Core.Data.Respuestas {
    /// <summary>
    /// Clase que contiene un objeto para gestionar resultados y excepciones
    /// principalemente en aplicaciones Web, hace que se devuelva los
    /// resultados en un formato parecido o igual
    /// </summary>
    public class Respuesta {
        /// <summary>
        /// propiedad que va a contener un objeto
        /// </summary>
        public object Datos { get; private set; } = null;
        /// <summary>
        /// Propiedad para enseñar un mensaje de error, advertencia, etc
        /// </summary>
        public string Mensaje { get; private set; } = string.Empty;
        /// <summary>
        /// Propiedad que contiene el codigo de un error
        /// </summary>
        public int Resultado { get; private set; } = 0;

        /// <summary>
        /// Se usa para no retornar datos
        /// </summary>
        public Respuesta() {
            Mensaje = string.Empty;
            Resultado = 0;
        }
        /// <summary>
        /// Tipo de salida cuando "se supone" que hay datos
        /// </summary>
        /// <param name="datos">va a contener un objeto con los datos que queremos retornar al controller</param>
        /// <param name="mensaje">va a contener un mensaje escrito por el usuario tipo informativo, sino no va a contener nada</param>
        /// <param name="resultado">va a contener el resultado de la aplicacion, si todo va bien sera 0</param>
        public Respuesta(object datos, string mensaje = "", int resultado = 0) {
            Datos = datos;
            Mensaje = mensaje;
            Resultado = resultado;
        }
        /// <summary>
        /// usado para retornar una excepcion
        /// </summary>
        /// <param name="ex">contendra la excepcion</param>
        /// <param name="resultado">contendra un codigo encontrado en la estructura Errores</param>
        public Respuesta(Exception ex, int resultado) {
            Mensaje = ex.Message;
            Resultado = resultado;
        }
    }
}
