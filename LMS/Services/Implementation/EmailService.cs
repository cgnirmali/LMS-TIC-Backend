
﻿using LMS.DB.Entities.Email;
using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Security.Cryptography;
using MailKit.Net.Smtp;
﻿using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace LMS.Services.Implementation
{
    public class EmailService
    {

        private readonly EmailSettings _emailSettings;
        //private readonly IUserService _userService;
        private readonly IConfiguration _configuration;



        public EmailService(IOptions<EmailSettings> emailSettings, IConfiguration configuration)
        {
            _emailSettings = emailSettings.Value;
            _configuration = configuration;

        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("LMS System", _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", mailRequest.User?.Email));
            emailMessage.Subject = mailRequest.Type.ToString();

            var bodyBuilder = new BodyBuilder { HtmlBody = MailBody(mailRequest) };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword); // Use the App Password here
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }


        public async Task SendEmailtoLoginAsync(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("LMS System", _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword); // Use the App Password here
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }

    


        public string MailBody(MailRequest mailRequest)
        {
            
            string emailContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            background: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #007BFF;
            color: #ffffff;
            padding: 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 20px;
            color: #333333;
        }}
        .content p {{
            margin: 10px 0;
            line-height: 1.6;
        }}
        .otp-box {{
            font-size: 20px;
            font-weight: bold;
            color: #007BFF;
            padding: 10px;
            border: 2px dashed #007BFF;
            display: inline-block;
            margin: 20px 0;
        }}
        .footer {{
            background-color: #f4f4f4;
            text-align: center;
            padding: 10px;
            font-size: 12px;
            color: #666666;
        }}
        .footer a {{
            color: #007BFF;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>OTP Verification - Unicom TIC</h1>
        </div>
        <div class='content'>
            <p>Dear {mailRequest.User.Email},</p>
            <p>Your One-Time Password (OTP) for verification is:</p>
            <p class='otp-box'>{mailRequest.Otp}</p>
            <p>This OTP is valid for the next <strong>5 minutes</strong>. Please do not share this OTP with anyone.</p>
            <p>If you did not request this, please ignore this email.</p>
        </div>
        <div class='footer'>
            <p>&copy; 2025 Unicom TIC. All rights reserved.</p>
            <p>Need help? Contact us at <a href='mailto:ut03304tic2024@gmail.com'>ut03304tic2024@gmail.com</a></p>
        </div>
    </div>
</body>
</html>";

            return emailContent;


        }
        

        //private readonly IConfiguration _configuration;

        //public EmailService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public async Task SendEmailAsync(string toEmail, string subject, string body)
        //{
        //    try
        //    {
        //        var emailMessage = new MimeMessage();
        //        emailMessage.From.Add(new MailboxAddress("Unicom TIC", _configuration["EmailSettings:FromAddress"]));
        //        emailMessage.To.Add(new MailboxAddress("", toEmail));  // Recipient email
        //        emailMessage.Subject = subject;

        //        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        //        emailMessage.Body = bodyBuilder.ToMessageBody();

        //        using (var smtpClient = new SmtpClient())
        //        {
        //            // Connect with Gmail SMTP settings (465 for SSL or 587 for TLS)
        //            await smtpClient.ConnectAsync(_configuration["EmailSettings:SmtpServer"], Convert.ToInt32(_configuration["EmailSettings:Port"]), SecureSocketOptions.SslOnConnect);

        //            // Authenticate using the provided username and password
        //            await smtpClient.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
        //            await smtpClient.SendAsync(emailMessage);
        //            await smtpClient.DisconnectAsync(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error (you can use a logging framework like Serilog or NLog)
        //        Console.WriteLine($"Error sending email: {ex.Message}");
        //        throw;  // Re-throw the exception after logging it
        //    }
        //}

    }
}
