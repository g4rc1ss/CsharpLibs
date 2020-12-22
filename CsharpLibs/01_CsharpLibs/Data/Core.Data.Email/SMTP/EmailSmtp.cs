using Core.Common.Respuestas;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Core.Data.Email.SMTP {
    public class EmailSmtp :Email {
        public EmailSmtp(string usuario, string password, string rutaUbicacionPlantilla = "") : base(usuario, password, rutaUbicacionPlantilla) {
        }

        public override Respuesta Enviar() {
            InicializarEnvioEmail();
            using (var cliente = new SmtpClient(ServidorEnvio)) {
                cliente.EnableSsl = true;
                cliente.Credentials = new NetworkCredential(Usuario, Password);
                var mensaje = new MailMessage(Remitente, Destinatario) {
                    IsBodyHtml = true,
                    Subject = Asunto,
                    Body = Cuerpo,
                };

                if (ArchivosAdjuntos == null || ArchivosAdjuntos.Count > 0)
                    using (var stream = new MemoryStream()) for (var x = 0; x < ArchivosAdjuntos.Count; x++)
                            mensaje.Attachments.Add(new Attachment(stream, NombreArchivosAdjunto[x]));

                cliente.Send(mensaje);
            }
            return new Respuesta();
        }
    }
}
