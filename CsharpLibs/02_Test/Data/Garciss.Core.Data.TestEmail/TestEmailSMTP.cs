using Garciss.Core.Data.Email.SMTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Core.Data.TestEmail {
    [TestClass]
    public class TestEmailSMTP {
        [TestMethod]
        public void SendEmail() {
            var email = new EmailSmtp("smtp.gmail.com", "emailtemporalparaenvio@gmail.com", "EmailTemporal", "Plantillas") {
                Remitente = "emailtemporalparaenvio@gmail.com",
                Destinatario = "emailtemporalparaenvio@gmail.com",
                NombrePlantilla = "EmailResponsiveTemplate.html",
                Asunto = "Es el asunto",
                BodyPersonalizado = new System.Collections.Generic.List<string>() {
                    "titulo",
                    "variables en texto",
                },
                ArchivosAdjuntos = new System.Collections.Generic.List<byte[]> {
                    File.ReadAllBytes(@"Plantillas\EmailResponsiveTemplate.html")
                },
                NombreArchivosAdjunto = new System.Collections.Generic.List<string> {
                    "EmailResponsiveTemplate.html"
                }
            };
            email.Enviar();
        }
    }
}
