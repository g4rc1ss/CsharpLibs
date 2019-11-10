using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Common.Helper.Extensions {
    public static class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(string value, int length) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            return value.Length <= length 
                ? value 
                : value.Substring(value.Length - length);
        }
    }
}
