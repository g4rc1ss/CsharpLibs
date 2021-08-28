using Garciss.Core.Common.Helper.Converters;
using System;

namespace Garciss.Core.Common.Helper {
    public sealed class Importe {
        private string moneda = "EUR";
        public string Moneda {
            get { return moneda; }
            set {
                if (!string.IsNullOrEmpty(value) && value.Length != 3) throw new ArgumentOutOfRangeException(nameof(Moneda), "La abreviatura no cumple el estandar ISO 4217");
                moneda = value;
            }
        }
        public decimal? Cantidad { get; set; }

        /// <summary>
        /// Inicializa valores por defecto
        /// </summary>
        public Importe() {
            Cantidad = null;
        }

        public Importe(decimal? cantidad, string moneda = "") {
            Cantidad = cantidad;
            Moneda = moneda;
        }

        public Importe(int cantidad, string moneda = "") {
            Cantidad = ConvertHelper.ToDecimal(cantidad.ToString());
            Moneda = moneda;
        }

        public Importe(string cantidad, string moneda = "") {
            Cantidad = ConvertHelper.ToDecimal(cantidad);
            Moneda = moneda;
        }

        public override string ToString() {
            return $"{Cantidad} {Moneda}";
        }

        public override bool Equals(object importe) {
            return ((Importe)importe).Cantidad.Equals(Cantidad);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static Importe operator +(Importe a, Importe b) {
            return new Importe(a.Cantidad + b.Cantidad, a.Moneda);
        }

        public static Importe operator -(Importe a, Importe b) {
            return new Importe(a.Cantidad - b.Cantidad, a.Moneda);
        }
    }
}
