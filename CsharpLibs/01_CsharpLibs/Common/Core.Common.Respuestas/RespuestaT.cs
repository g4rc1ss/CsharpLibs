using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Respuestas {
    public class Respuesta<T> :Respuesta {

        /// <summary>
        /// propiedad que va a contener un objeto
        /// </summary>
        public T Datos { get; private set; }

        public Respuesta() : base() {  }

        public Respuesta(T datos) : base() {
            Datos = datos;
        }

        public Respuesta(int resultado, string mensaje, string funcionalidad) 
            : base(resultado, mensaje, funcionalidad) {
        }

        public Respuesta(Exception ex, string funcionalidad = "", bool guardarLog = true, DondeGuardar donde = DondeGuardar.ArchivoTexto)
            :base(ex, funcionalidad, guardarLog, donde) {

        }
    }
}
