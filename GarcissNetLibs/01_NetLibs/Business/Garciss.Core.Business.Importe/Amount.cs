using System;
using System.Globalization;

namespace Garciss.Core.Business.Importe {
    public sealed class Amount {
        public decimal? Cantidad { get; private set; }
        public string Moneda { get; private set; }

        public Amount(object cantidad, string moneda = "EUR") {
            if (!string.IsNullOrEmpty(moneda) && moneda.Length != 3) {
                throw new ArgumentOutOfRangeException(nameof(Moneda), "La abreviatura no cumple el estandar ISO 4217");
            }
            Moneda = moneda;

            if (cantidad is not null) {
                var cantidadEs = Convert.ToDecimal(cantidad, new CultureInfo("es-ES"));
                var cantidadUS = Convert.ToDecimal(cantidad, new CultureInfo("en-US"));
                Cantidad = cantidadUS < cantidadEs ? cantidadUS : cantidadEs;
            }
        }

        public override string ToString() {
            return $"{Cantidad} {Moneda}";
        }

        public override bool Equals(object importe) {
            return (importe as Amount).Cantidad.Equals(Cantidad);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static Amount operator +(Amount a, Amount b) {
            return new Amount(a.Cantidad + b.Cantidad, a.Moneda);
        }

        public static Amount operator -(Amount a, Amount b) {
            return new Amount(a.Cantidad - b.Cantidad, a.Moneda);
        }

        public static Amount operator *(Amount a, Amount b) {
            return new Amount(a.Cantidad * b.Cantidad, a.Moneda);
        }

        public static Amount operator /(Amount a, Amount b) {
            return new Amount(a.Cantidad / b.Cantidad, a.Moneda);
        }
    }
}
