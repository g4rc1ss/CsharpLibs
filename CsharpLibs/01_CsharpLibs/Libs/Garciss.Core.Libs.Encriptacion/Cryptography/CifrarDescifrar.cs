using System;
using System.Collections.Generic;
using System.Text;

namespace Garciss.Core.Libs.Encriptacion.Cryptography {
    /// <summary>
    /// Enumerador para indicar si queremos cifrar o descifrar
    /// </summary>
    public enum CifrarDescifrar {
        /// <summary>
        /// indicador de que quieres cifrar algo, por ejemplo un documento
        /// </summary>
        /// <example>cifrarDescifrar.cifrar</example>
        cifrar = 0,
        /// <summary>
        /// indicador de que quieres descifrar algo, por ejemplo un archivo zip
        /// </summary>
        /// <example>cifrarDescifrar.descifrar</example>
        descifrar = 1
    }
}
