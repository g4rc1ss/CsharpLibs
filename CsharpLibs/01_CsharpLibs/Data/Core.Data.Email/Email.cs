using Core.Common.Respuestas;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Data.Email {
    public abstract class Email {
        private readonly string regexCuerpoVariables = "¬V\\d+";
        private readonly string rutaUbicacionPlantillasHtml = @"C:\PlantillasEmail\";

        public string ServidorEnvio { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string NombrePlantilla { get; set; }
        public string Cuerpo { get; set; }
        public List<string> BodyPersonalizado { get; set; }
        public List<byte[]> ArchivosAdjuntos { get; set; }
        public List<string> NombreArchivosAdjunto { get; set; }

        public string Usuario { get; }
        public string Password { get; }

        public Email(string usuario, string password, string rutaUbicacionPlantilla = "") {
            Usuario = usuario;
            Password = password;
            if (!string.IsNullOrEmpty(rutaUbicacionPlantilla))
                rutaUbicacionPlantillasHtml = rutaUbicacionPlantilla;
        }

        public abstract Respuesta Enviar();

        internal void InicializarEnvioEmail() {
            ValidarCampos();
            SustituirTokens();
        }

        private void ValidarCampos() {
            if (string.IsNullOrEmpty(Remitente))
                throw new Exception($"Campo {nameof(Remitente)} vacio");
            if (string.IsNullOrEmpty(Destinatario))
                throw new Exception($"Campo {nameof(Destinatario)} vacio");
            if (string.IsNullOrEmpty(Usuario))
                throw new Exception($"Campo {nameof(Usuario)} vacio");
            if (string.IsNullOrEmpty(Password))
                throw new Exception($"Campo {nameof(Password)} vacio");
            if (string.IsNullOrEmpty(ServidorEnvio))
                throw new Exception($"Campo {nameof(ServidorEnvio)} vacio");
            if (ArchivosAdjuntos?.Count != NombreArchivosAdjunto?.Count)
                throw new Exception("Los archivos tienen que tener su nombre correspondiente");
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
