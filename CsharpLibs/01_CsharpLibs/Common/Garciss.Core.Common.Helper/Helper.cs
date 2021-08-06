using System.Collections.Generic;

namespace Garciss.Core.Common.Helper {
    public static class Helper {
        /// <summary>
        /// Comprueba las variables de entorno creadas en el proyecto de Visual
        /// "ASPNETCORE_ENVIRONMENT",
        /// "CONSOLE_ENVIRONMENT",
        /// "WPF_ENVIRONMENT"
        /// </summary>
        public static bool IsDevelopment {
            get {
                var variablesEntorno = new List<string> {
                    "ASPNETCORE_ENVIRONMENT",
                    "CONSOLE_ENVIRONMENT",
                    "WPF_ENVIRONMENT"
                };
                foreach (var variableEntorno in variablesEntorno) {
                    var env = System.Environment.GetEnvironmentVariable(variableEntorno);
                    if (env == "Development") {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
