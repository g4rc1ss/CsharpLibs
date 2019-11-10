using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Core.Common.TestHelper.Converters {
    public class ClaseParaConvertirToDictionary {
        public string Nombre { get; set; }
        public decimal Salario { get; set; }
        public int Edad { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, string> Diccionario { get; set; }
        public string[] ArrayString { get; set; }
        public NameValueCollection NameValue { get; set; }
    }
}
