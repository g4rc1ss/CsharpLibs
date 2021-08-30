using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Garciss.Core.Common.TestConverter.Fake {
    public class ClaseParaConvertirToDictionary {
        public string Nombre { get; set; }
        public decimal Salario { get; set; }
        public int Edad { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, string> Diccionario { get; set; }
        public string[] ArrayString { get; set; }
        public NameValueCollection NameValue { get; set; }
        public List<int> ListaValoresInt { get; set; }
        public List<string> ListaValoresString { get; set; }
        public List<OtherClass> ListaValoresOtroObj { get; set; }
    }
}
