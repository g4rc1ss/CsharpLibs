using System;

namespace Core.Common.Helper.Formatters {
    public partial class Format {

        /// <summary>
        /// Transforma una fecha a un formato especifico en string
        /// </summary>
        /// <param name="valor">Fecha a formatear</param>
        /// <param name="formato">Formato de la fecha enviada a Host, por defecto dia mes año (ddMMyyyy)</param>
        /// <returns>Texto con la fecha válida para Host. Si el valor de entrada es nulo se devuelven ceros</returns>
        public static string FormatearFecha(DateTime? valor, string formato = "dd/MM/yyyy") {
            if (valor == null)
                return "00000000";
            return valor.Value.ToString(formato);
        }

        /// <summary>
        /// Cambia el formato de la fecha DateTime introducida
        /// </summary>
        /// <param name="dateToChange"></param>
        /// <param name="formato"></param>
        /// <returns></returns>
        public static DateTime? CambiarFormatoFecha(ref DateTime dateToChange, string formato = "dd/MM/yyy") {

            return null;
        }
    }
}
