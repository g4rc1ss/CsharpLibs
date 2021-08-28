using Garciss.Core.Common.Respuestas;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Garciss.Core.Data.Email.SMTP {
    public sealed class EmailSmtp :Email {
        public EmailSmtp(string servidor, string usuario, string password, string rutaUbicacionPlantillasHtml)
            : base(servidor, usuario, password, rutaUbicacionPlantillasHtml) {
        }

        /// <summary>
        /// Despues de inicializar los datos se podra usar el metodo Enviar que preparará el envio del email
        /// </summary>
        /// <returns>Devuelve un objeto Respuesta para validar si la operacion es correcta</returns>
        /// <example>
        /// <code>
        /// var email = new EmailSmtp("servidor", "usuario", "contraseña", "carpeta de los html") {
        ///    Remitente = "remitente@remitente.com",
        ///    Destinatario = "desinatario@destinatario.com",
        ///    NombrePlantilla = "plantilla.html",
        ///    Asunto = "Es el asunto",
        ///    BodyPersonalizado = new System.Collections.Generic.List<string>() {
        ///           "titulo",
        ///           "variables en texto",
        ///       },
        ///    ArchivosAdjuntos = new System.Collections.Generic.List<byte[]> {
        ///            File.ReadAllBytes(@"Plantillas\EmailResponsiveTemplate.html")
        ///        },
        ///    NombreArchivosAdjunto = new System.Collections.Generic.List<string> {
        ///           "EmailResponsiveTemplate.html"
        ///       }
        /// };
        /// email.Enviar();
        /// </code>
        /// </example>
        public override Respuesta Enviar() {
            InicializarEnvioEmail();

            if (Destinatarios.Count == 1)
                EnviarMensaje(Destinatarios[0]);
            else
                Parallel.ForEach(Destinatarios, destinatario => {
                    EnviarMensaje(destinatario);
                });

            return new Respuesta();
        }

        private void EnviarMensaje(string destinatario) {
            using (var cliente = new SmtpClient(servidorEnvio)) {
                using (var stream = new MemoryStream()) {
                    cliente.EnableSsl = SSL;
                    cliente.Credentials = new NetworkCredential(usuario, password);

                    var mensaje = new MailMessage(Remitente, destinatario) {
                        IsBodyHtml = true,
                        Subject = Asunto,
                        Body = Cuerpo,
                    };

                    if (ArchivosAdjuntos != null && ArchivosAdjuntos?.Count > 0)
                        for (var x = 0; x < ArchivosAdjuntos.Count; x++) {
                            mensaje.Attachments.Add(new Attachment(stream, NombreArchivosAdjunto[x]));
                        }
                    cliente.Send(mensaje);
                }
            }
        }
    }
}
