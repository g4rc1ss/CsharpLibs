using System;
using System.Collections.Generic;
using System.IO;
using Garciss.Core.Common.Respuestas;
using Garciss.Core.Data.Email.Base;
using Garciss.Core.Data.Email.MockSmtp;
using Garciss.Core.Data.Email.SMTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Core.Data.TestEmail {
    [TestClass]
    public class TestEmailSmtp {
        private static EnvioEmail envioEmailOk;

        private static Mock<EmailSmtp> MockEmailOk => new EmailSmtpMock().MockSmtpEmail;

        [ClassInitialize]
        public static void Inicializar(TestContext testContext) {
            if (testContext is null) {
                throw new ArgumentNullException(nameof(testContext));
            }
            envioEmailOk = MockEmailOk.Object;
        }

        [TestMethod]
        public void EnviarOk() {
            envioEmailOk.Remitente = "emailtemporalparaenvio@gmail.com";
            envioEmailOk.Destinatarios = new List<string>() {
                    "emailtemporalparaenvio@gmail.com",
                    "emailtemporalparaenvio@gmail.com",
                    "emailtemporalparaenvio@gmail.com",
                    "emailtemporalparaenvio@gmail.com",
            };
            envioEmailOk.NombrePlantilla = "EmailResponsiveTemplate.html";
            envioEmailOk.Asunto = "Es el asunto";
            envioEmailOk.BodyPersonalizado = new List<string>() {
                    "titulo",
                    "variables en texto",
            };
            envioEmailOk.ArchivosAdjuntos = new List<byte[]> {
                    File.ReadAllBytes(@"Plantillas\EmailResponsiveTemplate.html")
            };
            envioEmailOk.NombreArchivosAdjunto = new List<string> {
                    "EmailResponsiveTemplate.html"
            };
            
            var respuestaEnvio = envioEmailOk.Enviar();
            
            Assert.IsTrue(respuestaEnvio.Resultado == Respuesta.OK);
        }
    }
}
