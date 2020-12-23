using System;

namespace Garciss.Core.Common.Helper.EnumHelper {
    /// <summary>
    /// Metodos extensores para enumeraciones
    /// </summary>
    public static class Extensions {
        /// <summary>
        /// HashFlag para enumeraciones con Flag y valores negativos
        /// </summary>
        /// <param name="enumeracion"></param>
        /// <param name="flag"></param>
        /// <returns>
        ///     Devuelve el true o false si tiene o no el valor respectivamente
        /// </returns>
        public static bool HasFlagWithNegative(this Enum enumeracion, Enum flag) {
            if (!enumeracion.GetType().IsEquivalentTo(flag.GetType()))
                throw new ArgumentException("Argument_EnumtypeDoesNotMatch", $"{enumeracion.GetType().ToString()} /" +
                    $" {flag.GetType().ToString()}");

            return Convert.ToInt64(enumeracion) < 0
                ? Convert.ToInt64(enumeracion) == Convert.ToInt64(flag)
                : enumeracion.HasFlag(flag);
        }
    }
}
