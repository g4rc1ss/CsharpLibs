using System;
using System.Globalization;

namespace Garciss.Core.Common.Helper.Converters {
    public partial class ConvertHelper {
        /// <summary>
        /// Convertimos a decimal
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string valor) {
            var esCulture = new CultureInfo("es-ES");
            var usCulture = new CultureInfo("en-US");

            decimal number;
            try {
                number = Convert.ToDecimal(valor, esCulture);
                try {
                    var numberUS = Convert.ToDecimal(valor, usCulture);
                    if (numberUS < number) {
                        number = numberUS;
                    }
                } catch (Exception) { }
            } catch (Exception) {
                number = Convert.ToDecimal(valor, usCulture);
            }
            return number;
        }
    }
}
