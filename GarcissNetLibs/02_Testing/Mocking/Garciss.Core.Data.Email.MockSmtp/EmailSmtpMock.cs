using Garciss.Core.Data.Email.SMTP;
using Moq;

namespace Garciss.Core.Data.Email.MockSmtp {
    public class EmailSmtpMock {

        public Mock<EmailSmtp> MockSmtpEmail { get; set; }

        public EmailSmtpMock() {
            MockSmtpEmail = new Mock<EmailSmtp>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Initialize();
        }

        private void Initialize() {
            MockSmtpEmail.Setup(x => x.Enviar()).Returns(new Common.Respuestas.Respuesta());
        }
    }
}
