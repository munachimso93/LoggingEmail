﻿using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmailLog.Messaging
{
    public class SendGridEmailService
    {
        private readonly SendGridMessage msg;
        private readonly SendGridClient client;

        public SendGridEmailService(string apiKey,
            string senderEmail = "munachimsoemmanuel@gmail.com",
            string senderName = "Muna")
        {
            msg = new SendGridMessage();
            msg.From = new EmailAddress(senderEmail, senderName);
            client = new SendGridClient(apiKey);
        }

        public async Task<string> SendMail(EmailMessage message, Boolean isHtml)
        {
            msg.AddTo(message.Recipient);
            msg.Subject = message.Subject;
            if (isHtml)
            {
                msg.HtmlContent = message.Body;
            }
            else
            {
                msg.PlainTextContent = message.Body;
            }




            SendGrid.Response response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return "Message was sent successfully";
            else
                return string.Empty;

        }

        public void SendMail(EmailMessage message, Boolean isHtml, string fileName, Byte[] fileBytes)
        {
            msg.AddTo(message.Recipient);
            msg.Subject = message.Subject;

            if (!isHtml)
            {
                msg.PlainTextContent = message.Body;
            }
            else
            {
                msg.HtmlContent = message.Body;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                string fileContents = Convert.ToBase64String(fileBytes);
                msg.AddAttachment(fileName, fileContents);
            }

            client.SendEmailAsync(msg);
        }

    }
}