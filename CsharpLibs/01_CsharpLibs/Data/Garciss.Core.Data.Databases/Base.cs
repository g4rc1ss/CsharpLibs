using System;
using System.Text.RegularExpressions;

namespace Garciss.Core.Data.Databases {
    /// <summary>
    /// Clase base de la que hay que heredar para realizar validaciones de sentencias SQL etc.
    /// Se usara principalmente para prevenir SQL Injection
    /// </summary>
    public abstract class Base {
        /// <summary>
        /// Tipos de validaciones de expresiones sql
        /// </summary>
        public enum TiposSentenciaSql {
            /// <summary>
            /// 
            /// </summary>
            None = 0,
            /// <summary>
            /// 
            /// </summary>
            Procedure = 0,
            /// <summary>
            /// 
            /// </summary>
            Alter = 1,
            /// <summary>
            /// 
            /// </summary>
            Create = 2,
            /// <summary>
            /// 
            /// </summary>
            Delete = 4,
            /// <summary>
            /// 
            /// </summary>
            Drop = 8,
            /// <summary>
            /// 
            /// </summary>
            Execute = 16,
            /// <summary>
            /// 
            /// </summary>
            Insert = 32,
            /// <summary>
            /// 
            /// </summary>
            Select = 64,
            /// <summary>
            /// 
            /// </summary>
            Update = 128,
            /// <summary>
            /// 
            /// </summary>
            Union = 256,
            /// <summary>
            /// 
            /// </summary>
            Batch = 512,
            /// <summary>
            /// 
            /// </summary>
            Merge = 1024 | Delete | Insert | Select | Update,
            /// <summary>
            /// 
            /// </summary>
            Comment = 2048
        }

        /// <summary>
        /// Valida una expresión sql para evitar inyecciones sql
        /// </summary>
        /// <param name="sentencia">Sql o parte de una sql a validar</param>
        /// <param name="sentenciasAutorizadas">requisitos de la validación</param>
        /// <exception cref="UnauthorizedAccessException">Si la expresión sql no cumple los requisitos</exception>
        /// <example>https://larrysteinle.com/2011/02/20/use-regular-expressions-to-detect-sql-code-injection/</example>
        public static void ValidarSentencia(string sentencia, TiposSentenciaSql sentenciasAutorizadas) {
            //Construct Regular Expression To Find Text Blocks, Statement Breaks & SQL Statement Headers
            var regExText = "('(''|[^'])*')|(;)|(--)|(\\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\\b)";

            //Remove Authorized Options
            if ((sentenciasAutorizadas & TiposSentenciaSql.Batch) == TiposSentenciaSql.Batch)
                regExText = regExText.Replace("(;)", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Alter) == TiposSentenciaSql.Alter)
                regExText = regExText.Replace("ALTER", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Create) == TiposSentenciaSql.Create)
                regExText = regExText.Replace("CREATE", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Delete) == TiposSentenciaSql.Delete)
                regExText = regExText.Replace("DELETE", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Delete) == TiposSentenciaSql.Delete)
                regExText = regExText.Replace("DELETETREE", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Drop) == TiposSentenciaSql.Drop)
                regExText = regExText.Replace("DROP", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Execute) == TiposSentenciaSql.Execute)
                regExText = regExText.Replace("EXEC(UTE){0,1}", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Insert) == TiposSentenciaSql.Insert)
                regExText = regExText.Replace("INSERT( +INTO){0,1}", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Merge) == TiposSentenciaSql.Merge)
                regExText = regExText.Replace("MERGE", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Select) == TiposSentenciaSql.Select)
                regExText = regExText.Replace("SELECT", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Union) == TiposSentenciaSql.Union)
                regExText = regExText.Replace("UNION", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Update) == TiposSentenciaSql.Update)
                regExText = regExText.Replace("UPDATE", string.Empty);
            if ((sentenciasAutorizadas & TiposSentenciaSql.Comment) == TiposSentenciaSql.Comment)
                regExText = regExText.Replace("(--)", string.Empty);


            //Remove extra separators
            var regExOptions = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            regExText = Regex.Replace(regExText, "\\(\\|", "(", regExOptions);
            regExText = Regex.Replace(regExText, "\\|{2,}", "|", regExOptions);
            regExText = Regex.Replace(regExText, "\\|\\)", ")", regExOptions);

            //Check for errors
            if (string.IsNullOrEmpty(sentencia))
                return;
            var patternMatchList = Regex.Matches(sentencia, regExText, regExOptions);
            for (var patternIndex = patternMatchList.Count - 1; patternIndex >= 0; patternIndex += -1) {
                var value = patternMatchList[patternIndex].Value.Trim();
                if (string.IsNullOrWhiteSpace(value)) {
                    //Continue - Not an error.
                } else if (value.StartsWith("'") && value.EndsWith("'")) {
                    //Continue - Text Block
                } else if (value.Trim() == ";") throw new UnauthorizedAccessException("Batch statements not authorized:" + Environment.NewLine + sentencia);
                else throw new UnauthorizedAccessException(
                  string.Concat(value.Substring(0, 1).ToUpper(), value.Substring(1).ToLower(),
                                " statements not authorized:", Environment.NewLine, sentencia));
            }
        }

        /// <summary>
        /// Elimina caracteres no permitidos
        /// </summary>
        /// <param name="sentencia">Texto a limpiar</param>
        /// <returns>Parámetro sentencia sin los carecteres no permitidos</returns>
        public static string LimpiarParametrosSql(string sentencia) {
            var regExText = "\\W";

            if (string.IsNullOrEmpty(sentencia)) return string.Empty;
            return Regex.Replace(sentencia, regExText, string.Empty);
        }

        /// <summary>
        /// Funcion Que obtiene una query partiendo de un fichero y unos parametros
        /// </summary>
        /// <param name="file">Nombre del fichero, debe incluir la ruta partiendo desde C:\AccesoBaseDatos</param>
        /// <param name="parametros">Array de Parametros</param>
        /// <param name="server">Nombre del servidor (sin contrabarras al principio)</param>
        /// <returns>String con la query resultante de la sustitución de los parametros en el fichero</returns>
        /// <remarks></remarks>
        protected static string ObtenerQuery(string file, string[] parametros, string server = "") {
            string strSQL;
            int i;
            var sParametros = "";

            if (file.ToUpper().IndexOf("SELECT ") >= 0 & file.ToUpper().IndexOf("FROM") >= 0) strSQL = file;
            else {
                if (file.IndexOf(@"\\") < 0) {
                    var unidad = server == string.Empty ? @"C:" : string.Concat(@"\\", server);
                    file = string.Concat(unidad, @"\accesoBaseDatos", file);
                }

                using (var objFile = new System.IO.StreamReader(file, System.Text.Encoding.Default)) {
                    strSQL = objFile.ReadToEnd();
                    objFile.Close();
                }
            }

            if (!(parametros == null)) for (i = 0; i <= parametros.Length - 1; i++) {
                    sParametros += (sParametros.Length > 0 ? "," : "") + parametros[i];
                    strSQL = strSQL.Replace("@" + i.ToString() + "@", parametros[i]);
                }
            ValidarSentencia(sParametros, TiposSentenciaSql.None);
            return strSQL;
        }
    }
}
