using System.Net.Mail;
using BugFixer.Application.Services.Interfaces;
using BugFixer.Domain.Interfaces;


namespace BugFixer.Application.Services.Implementations
{
    public class EmailService : IEmailService
    {
        #region Ctor

        private ISiteSettingRepository _siteSettingRepository;

        public EmailService(ISiteSettingRepository siteSettingRepository)
        {
            _siteSettingRepository = siteSettingRepository;
        }

        #endregion

        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            try
            {
                var defaultSiteEmail = await _siteSettingRepository.GetDefaultEmail();

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient();

                mail.From = new MailAddress(defaultSiteEmail.From, defaultSiteEmail.DisplayName);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                if (defaultSiteEmail.Port != 0)
                {
                    smtpServer.Port = defaultSiteEmail.Port;
                    smtpServer.EnableSsl = defaultSiteEmail.EnableSSL;
                }

                smtpServer.Credentials =
                    new System.Net.NetworkCredential(defaultSiteEmail.From, defaultSiteEmail.Password);
                smtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
