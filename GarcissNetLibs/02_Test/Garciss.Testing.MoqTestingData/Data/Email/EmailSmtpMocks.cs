using Garciss.Testing.MoqTestingData.Data.Email.FakeData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Garciss.Testing.MoqTestingData.Data.Email {
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
