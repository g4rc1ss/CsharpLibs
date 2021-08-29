using System;

namespace Garciss.Core.Business.Importe {
    public sealed class Importe {
        private string moneda;

        public decimal? Cantidad { get; set; }
        public string Moneda {
            get => moneda;
            set {
                if (!string.IsNullOrEmpty(value) && value.Length != 3) {
                    throw new ArgumentOutOfRangeException(nameof(Moneda), "La abreviatura no cumple el estandar ISO 4217");
                }
                moneda = value;
            }
        }

        public Importe() {
            Cantidad = null;
        }

        public Importe(decimal? cantidad, string moneda = "") {
            Cantidad = cantidad;
            Moneda = moneda;
        }

        public Importe(int cantidad, string moneda = "") {
            Cantidad = Convert.ToDecimal(cantidad.ToString());
            Moneda = moneda;
        }

        public Importe(string cantidad, string moneda = "") {
            Cantidad = Convert.ToDecimal(cantidad);
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
