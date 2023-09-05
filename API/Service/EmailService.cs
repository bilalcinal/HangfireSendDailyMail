using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace API.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EMailSendModel emailSendModel)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("BilalCinal", "hbilalcinal@gmail.com")); 
                message.To.Add(new MailboxAddress("", emailSendModel.ToEmail));
                message.Subject = emailSendModel.Subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = emailSendModel.Body;
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("hbilalcinal@gmail.com", "tqktaustbvneybed");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}