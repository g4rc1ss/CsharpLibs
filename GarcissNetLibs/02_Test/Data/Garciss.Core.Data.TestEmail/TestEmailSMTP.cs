using System.Collections.Generic;
using System.IO;
using Garciss.Core.Data.Email.SMTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Data.TestEmail {
    [TestClass]
    public class TestEmailSMTP {
        [TestMethod]
        public void SendEmail() {
            //var email = new EmailSmtp("smtp.gmail.com", "emailtemporalparaenvio@gmail.com", "EmailTemporal", "Plantillas") {
            //    Remitente = "emailtemporalparaenvio@gmail.com",
            //    Destinatarios = new List<string>() {
            //        "emailtemporalparaenvio@gmail.com",
            //        "emailtemporalparaenvio@gmail.com",
            //        "emailtemporalparaenvio@gmail.com",
            //        "emailtemporalparaenvio@gmail.com",
            //    },
            //    NombrePlantilla = "EmailResponsiveTemplate.html",
            //    Asunto = "Es el asunto",
            //    BodyPersonalizado = new List<string>() {
            //        "titulo",
            //        "variables en texto",
            //    },
            //    ArchivosAdjuntos = new List<byte[]> {
            //        File.ReadAllBytes(@"Plantillas\EmailResponsiveTemplate.html")
            //    },
            //    NombreArchivosAdjunto = new List<string> {
            //        "EmailResponsiveTemplate.html"
            //    }
            //};
            //email.Enviar();
        }
    }
}
