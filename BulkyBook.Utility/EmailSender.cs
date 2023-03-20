﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("hello@dotnetmastery.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage};

            //send email
            using (var emailClaint = new SmtpClient())
            {
                emailClaint.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClaint.Authenticate("dotnet1master@gmail.com", "c#master1");
                emailClaint.Send(emailToSend);
                emailClaint.Disconnect(true);
            }
            return Task.CompletedTask;
        }

    }
}
