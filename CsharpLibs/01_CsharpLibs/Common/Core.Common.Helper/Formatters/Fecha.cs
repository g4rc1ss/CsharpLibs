using System;

namespace Core.Common.Helper.Formatters {
    /// <summary>
    /// Clase para formartear objetos
    /// </summary>
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
        /// Convierte una fecha a formato Juliana
        /// </summary>
        /// <param name="fecha">fecha a convertir</param>
        /// <returns>Fecha en Juliana yydd (año, dia del año)</returns>
        public static string FormatToFechaJuliana(DateTime fecha) {
            return $"{Convert.ToString(fecha.Year).Substring(3)}{fecha.DayOfYear.ToString().PadLeft(3, '0')}";
        }

        /// <summary>
        /// Funcion que calcula la diferencia en milisegundos entre dos fechas
        /// </summary>
        /// <param name="fechaDesde"></param>
        /// <returns></returns>
        public static double DateDiffMilliSecond(DateTime fechaDesde) {
            try {
                var milliseconds = default(double);
                milliseconds = Math.Round((DateTime.Now - fechaDesde).TotalMilliseconds, 0);
                return milliseconds < 0 ? 0 : milliseconds;
            } catch (Exception) {
                return 99999;
            }
        }

        /// <summary>
        /// Funcion que calcula la diferencia en milisegundos entre dos fechas
        /// </summary>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        public static double DateDiffMilliSecond(DateTime fechaDesde, DateTime fechaHasta) {
            try {
                var milliseconds = default(double);
                milliseconds = Math.Round((fechaHasta - fechaDesde).TotalMilliseconds, 0);
                return milliseconds < 0 ? 0 : milliseconds;
            } catch (Exception) {
                return 99999;
            }
        }
    }
}
