using Core.Data.Email.SMTP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Data.TestEmail {
    [TestClass]
    public class TestEmailSMTP {
        [TestMethod]
        public void SendEmail() {
            new EmailSmtp("", "", "") {

            };
        }
    }
}
