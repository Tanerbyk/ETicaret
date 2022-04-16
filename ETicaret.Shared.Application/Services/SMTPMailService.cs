using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Services
{
    public class SMTPMailService : IEmailSender
    {
        public MailSettings _mailSettings { get; }

        public SMTPMailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendAsync(MailRequest request)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(_mailSettings.Host);
                mail.From = new MailAddress(_mailSettings.From);
            mail.To.Add(request.To);
            mail.Subject = request.Subject;
            mail.Body = request.Body;
            mail.IsBodyHtml = true;
            smtpServer.Port = _mailSettings.Port;
            smtpServer.Credentials = new System.Net.NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
            smtpServer.EnableSsl = true;
            smtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpServer.UseDefaultCredentials = false;

            await smtpServer.SendMailAsync(mail);
        }
    }

}
