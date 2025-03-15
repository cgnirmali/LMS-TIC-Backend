using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace LMS.Services.Implementation
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Unicom TIC", _configuration["EmailSettings:FromAddress"]));
                emailMessage.To.Add(new MailboxAddress("", toEmail));  // Recipient email
                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var smtpClient = new SmtpClient())
                {
                    // Connect with Gmail SMTP settings (465 for SSL or 587 for TLS)
                    await smtpClient.ConnectAsync(_configuration["EmailSettings:SmtpServer"], Convert.ToInt32(_configuration["EmailSettings:Port"]), SecureSocketOptions.SslOnConnect);

                    // Authenticate using the provided username and password
                    await smtpClient.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                    await smtpClient.SendAsync(emailMessage);
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logging framework like Serilog or NLog)
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;  // Re-throw the exception after logging it
            }
        }
    }
}
