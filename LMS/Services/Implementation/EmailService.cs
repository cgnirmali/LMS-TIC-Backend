
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
            emailMessage.To.Add(new MailboxAddress("", mailRequest.Student?.UserEmail));
            emailMessage.Subject = mailRequest.Type.ToString();

            var bodyBuilder = new BodyBuilder { HtmlBody = MailBody(mailRequest) };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword); // Use the App Password here
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }


        public async Task SendEmailforUpdate(UpdateMailRequest updatemailRequest)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("LMS System", _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", updatemailRequest.UTEmail));
            emailMessage.Subject = "Update Password OTP";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Hi, your email {updatemailRequest.UTEmail} has requested a password update. Please use the OTP provided. OTP is {updatemailRequest.Otp}"
            };
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
        .info-box {{
            font-size: 16px;
            font-weight: bold;
            color: #007BFF;
            padding: 10px;
            border: 2px dashed #007BFF;
            display: inline-block;
            margin: 10px 0;
            background: #f8f9fa;
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
            <h1>Welcome to Unicom TIC</h1>
        </div>
        <div class='content'>
            <p>Dear {mailRequest.Student.FirstName}{mailRequest.Student.LastName} ,</p>
            <p>We are pleased to inform you that your student account has been successfully created at <strong>Unicom TIC</strong>.</p>
            
            <p><strong>Your account details:</strong></p>
            <p class='info-box'>UT Email: {mailRequest.User.UTEmail}</p>
            <p class='info-box'>UTEmail Password: {mailRequest.UTEmailPassword}</p>
            <p class='info-box'>UTloginPassword: {mailRequest.UTloginPassword}</p>

            <p>Please use these credentials to log in to your student portal.</p>

            <p>For security reasons, we recommend changing your password after logging in for the first time.</p>

            <p>If you have any questions or need further assistance, feel free to contact our support team.</p>
        </div>
        <div class='footer'>
            <p>&copy; 2025 Unicom TIC. All rights reserved.</p>
            <p>Need help? Contact us at <a href='mailto:unicomtic@gmail.com'>unicomtic@gmail.com</a></p>
        </div>
    </div>
</body>
</html>";


            return emailContent;


        }
        

     

    }
}
