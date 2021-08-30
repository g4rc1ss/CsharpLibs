using System.Net.Mail;
using Moq;

namespace Garciss.Core.Data.Email.MockSmtp {
    public class EmailSmtpMocks {

        public Mock<SmtpClient> MockSmtpEmail { get; set; }

        public EmailSmtpMocks() {
            MockSmtpEmail = new Mock<SmtpClient>();
            Initialize();
        }

        private void Initialize() {
            MockSmtpEmail.Setup(x => x.Send(It.IsAny<MailMessage>()));
        }
    }
}
