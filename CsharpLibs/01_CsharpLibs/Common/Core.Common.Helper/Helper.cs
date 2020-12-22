using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Helper {
    public static class Helper {
        public static bool IsDevelopment {
            get {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == null || env.Equals("Production"))
                    return false;
                return true;
            }
        }
    }
}
