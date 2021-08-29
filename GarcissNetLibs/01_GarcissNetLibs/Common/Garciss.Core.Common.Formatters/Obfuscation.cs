using System;
using Garciss.Core.Common.Extensions;

namespace Garciss.Core.Common.Formatters {
    public class Obfuscation {
        /// <summary>
        /// Oculta la parte del correo electronico
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public static string OfuscarCorreo(string correo) {
            var dividirCorreo = correo.Split('@');
            if (dividirCorreo.Length < 2) {
                return string.Empty;
            }

            var direccionCorreo = dividirCorreo[0];
            var longitudNombre = direccionCorreo.Length;
            if (longitudNombre < 3) {
                return string.Concat("***", correo);
            } else {
                // Se escriben tantos * como la longitud del nombre del correo menos 2, Right devuelve los ultimos 2 caracteres y ya se escribe lo demas
                return string.Concat(new string('*', longitudNombre - 2), direccionCorreo.Right(2), "@", dividirCorreo[1]);
            }
        }

        /// <summary>
        /// Oculta los 5 digitos centrales del telefono movil
        /// </summary>
        /// <param name="telefono"></param>
        /// <param name="caracterParaOfuscar"></param>
        /// <returns></returns>
        public static string OfuscarMovil(string telefono, char caracterParaOfuscar = '*') {
            if (telefono.Length < 9) {
                throw new ArgumentOutOfRangeException(nameof(telefono), telefono, "La longitud del numero de telefono no es correcta");
            }
            return string.Concat(telefono[0..3], new string(caracterParaOfuscar, 5), telefono[8..]);
        }
    }
}
