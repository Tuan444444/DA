using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DA.Models; // Sửa lại theo namespace project bạn

namespace DA.Services // Đặt namespace cho phù hợp
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using var mail = new MailMessage
                {
                    From = new MailAddress(_settings.From, _settings.DisplayName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Cho phép nội dung HTML
                };

                mail.To.Add(toEmail);

                using var smtp = new SmtpClient(_settings.Host, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.From, _settings.Password),
                    EnableSsl = _settings.EnableSSL
                };

                await smtp.SendMailAsync(mail);
                Console.WriteLine("✅ Đã gửi email thành công đến " + toEmail);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine("❌ SMTP Error: " + smtpEx.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("❌ Lỗi gửi email: " + ex.Message);
            }

            return false;
        }
    }
}
