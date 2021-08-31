using System;

namespace Garciss.Core.Common.MockRespuesta {
    public class DatosFake {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public double Salario { get; set; }
    }

    // Me creo una estructura de errores paraa pasarlos
    // por resultado, por convencion seria bueno crear esto, puesto
    // que un proyecto solitario es normal que tenga sus propios errores internos
    // y hay que reflegarlos
    public struct Errores {
        public const int SINERROR = 0;
        public const int GENERAL = 9999;
        public const int ZERODIVISION = 1;
    }
}
