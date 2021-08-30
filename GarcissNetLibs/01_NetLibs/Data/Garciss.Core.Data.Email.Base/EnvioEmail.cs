using System;
using System.Collections.Generic;
using System.IO;
using Garciss.Core.Common.Respuestas;

namespace Garciss.Core.Data.Email.Base {
    public abstract class EnvioEmail {
        private const string REGEX_CUERPO_VARIABLE = @"¬V\d+";
        private readonly string rutaUbicacionPlantillasHtml;
        protected readonly string usuario;
        protected readonly string password;
        protected readonly string servidorEnvio;

        protected string Cuerpo { get; set; }
        public string Remitente { get; set; }
        public string Asunto { get; set; }
        public string NombrePlantilla { get; set; }
        public bool SSL { get; set; } = true;
        public List<string> Destinatarios { get; set; }
        public List<string> BodyPersonalizado { get; set; }
        public List<byte[]> ArchivosAdjuntos { get; set; }
        public List<string> NombreArchivosAdjunto { get; set; }

        public EnvioEmail(string servidorEnvio, string usuario, string password, string rutaUbicacionPlantillasHtml) {
            this.servidorEnvio = servidorEnvio;
            this.usuario = usuario;
            this.password = password;
            this.rutaUbicacionPlantillasHtml = rutaUbicacionPlantillasHtml;
        }

        public abstract Respuesta Enviar();

        protected void InicializarEnvioEmail() {
            ValidarCampos();
            SustituirTokens();
        }

        private void ValidarCampos() {
            if (string.IsNullOrEmpty(Remitente)) {
                throw new NullReferenceException($"Campo {nameof(Remitente)} vacio");
            }

            if (Destinatarios == null || Destinatarios.Count <= 0) {
                throw new NullReferenceException($"Campo {nameof(Destinatarios)} vacio");
            }

            if (string.IsNullOrEmpty(usuario)) {
                throw new NullReferenceException($"Campo {nameof(usuario)} vacio");
            }

            if (string.IsNullOrEmpty(password)) {
                throw new NullReferenceException($"Campo {nameof(password)} vacio");
            }

            if (string.IsNullOrEmpty(servidorEnvio)) {
                throw new NullReferenceException($"Campo {nameof(servidorEnvio)} vacio");
            }

            if (ArchivosAdjuntos?.Count != NombreArchivosAdjunto?.Count) {
                throw new NullReferenceException("Los archivos tienen que tener su nombre correspondiente");
            }

            if (string.IsNullOrEmpty(rutaUbicacionPlantillasHtml)) {
                throw new NullReferenceException("Es obligatorio el uso de plantillas Html para el envio del Mail");
            }
        }

        private void SustituirTokens() {
            var regex = new System.Text.RegularExpressions.Regex(REGEX_CUERPO_VARIABLE);
            var cuerpo = ObtenerTextoDesdeRepositorio();

            foreach (var param in BodyPersonalizado) {
                cuerpo = regex.Replace(cuerpo, param, 1);
            }
            Cuerpo = cuerpo;
        }

        private string ObtenerTextoDesdeRepositorio() {
            return File.ReadAllText(Path.Combine(rutaUbicacionPlantillasHtml, NombrePlantilla));
        }
    }
}
