using Garciss.Core.Common.Respuestas;
using System;
using System.Collections.Generic;
using System.IO;

namespace Garciss.Core.Data.Email {
    public abstract class Email {
        private readonly string regexCuerpoVariables = "¬V\\d+";
        private readonly string rutaUbicacionPlantillasHtml;
        internal readonly string usuario;
        internal readonly string password;
        internal readonly string servidorEnvio;
        internal string Cuerpo { get; set; }

        public string Remitente { get; set; }
        public string Asunto { get; set; }
        public string NombrePlantilla { get; set; }
        public bool SSL { get; set; } = true;
        public List<string> Destinatarios { get; set; }
        public List<string> BodyPersonalizado { get; set; }
        public List<byte[]> ArchivosAdjuntos { get; set; }
        public List<string> NombreArchivosAdjunto { get; set; }

        public Email(string servidorEnvio, string usuario, string password, string rutaUbicacionPlantillasHtml) {
            this.servidorEnvio = servidorEnvio;
            this.usuario = usuario;
            this.password = password;
            this.rutaUbicacionPlantillasHtml = rutaUbicacionPlantillasHtml;
        }

        public abstract Respuesta Enviar();

        internal void InicializarEnvioEmail() {
            ValidarCampos();
            SustituirTokens();
        }

        private void ValidarCampos() {
            if (string.IsNullOrEmpty(Remitente))
                throw new Exception($"Campo {nameof(Remitente)} vacio");
            if (Destinatarios == null || Destinatarios.Count <= 0)
                throw new Exception($"Campo {nameof(Destinatarios)} vacio");
            if (string.IsNullOrEmpty(usuario))
                throw new Exception($"Campo {nameof(usuario)} vacio");
            if (string.IsNullOrEmpty(password))
                throw new Exception($"Campo {nameof(password)} vacio");
            if (string.IsNullOrEmpty(servidorEnvio))
                throw new Exception($"Campo {nameof(servidorEnvio)} vacio");
            if (ArchivosAdjuntos?.Count != NombreArchivosAdjunto?.Count)
                throw new Exception("Los archivos tienen que tener su nombre correspondiente");
            if (string.IsNullOrEmpty(rutaUbicacionPlantillasHtml))
                throw new Exception("Es obligatorio el uso de plantillas Html para el envio del Mail");
        }

        private void SustituirTokens() {
            var regex = new System.Text.RegularExpressions.Regex(regexCuerpoVariables);
            var cuerpo = ObtenerTextoDesdeRepositorio();

            foreach (var param in BodyPersonalizado) cuerpo = regex.Replace(cuerpo, param, 1);
            Cuerpo = cuerpo;
        }

        private string ObtenerTextoDesdeRepositorio() {
            return File.ReadAllText(Path.Combine(rutaUbicacionPlantillasHtml, NombrePlantilla));
        }
    }
}
