using Garciss.Core.Common.Respuestas;

namespace Garciss.Core.Data.Email.Base {
    public interface IEnvioEmail {
        abstract Respuesta Enviar();
    }
}
