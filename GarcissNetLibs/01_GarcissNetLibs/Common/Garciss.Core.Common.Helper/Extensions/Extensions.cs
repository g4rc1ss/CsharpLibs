namespace Garciss.Core.Common.Helper.Extensions {
    /// <summary>
    /// Clase estatica para almacenar extensiones de metodos
    /// </summary>
    public static class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string value, int length) {
            if (string.IsNullOrEmpty(value)) {
                return string.Empty;
            }

            return value.Length <= length
                ? value
                : value[^length..];
        }
    }
}
