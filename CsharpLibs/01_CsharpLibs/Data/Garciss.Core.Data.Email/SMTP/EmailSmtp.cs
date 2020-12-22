using Core.Common.Respuestas;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Core.Data.Email.SMTP {
    public class EmailSmtp :Email {
        public EmailSmtp(string servidor, string usuario, string password, string rutaUbicacionPlantillasHtml)
            : base(servidor, usuario, password, rutaUbicacionPlantillasHtml) {
        }

        public override Respuesta Enviar() {
            InicializarEnvioEmail();
            using (var cliente = new SmtpClient(servidorEnvio)) {
                using (var stream = new MemoryStream()) {
                    cliente.EnableSsl = true;
                    cliente.Credentials = new NetworkCredential(usuario, password);
                    var mensaje = new MailMessage(Remitente, Destinatario) {
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
            return new Respuesta();
        }
    }
}
