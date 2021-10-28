using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Net_AhmedMohammedRabieAbdElwhab.Models
{
    public class SendEmailHelper
    {
        public void sendEmailConfirmation(string Email)
        {
            var apiKey = ConfigurationManager.AppSettings["RESALTY_SENDGRID_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(ConfigurationManager.AppSettings["emailService:Account"], "Ahmed Mohamed Rabie");
            var subject = "test";//message.Subject;
            var to = new EmailAddress(Email);
            var plainTextContent = "";
            var htmlContent = "<html> <head></head> <body>  <h1 style=\"font-size:30px;padding-right:30px;padding-left:30px\">Reset Password</h1><p style=\"font-size:17px;padding-right:30px;padding-left:30px\"> Please press the below button to Confirm your your demo Email .</p> <a href=\"" + "http://localhost:54816/api/User/ConfirmEmail" + "?Email=" + Email + "\" style=\"min-width: 196px; border-top: 13px solid;border-bottom: 13px solid;border-right: 24px solid;border-left: 24px solid;border-color: #0b80f9;border-radius: 4px;background-color: #0b80f9;color: #ffffff;font-size: 18px;line-height: 18px;word-break: break-word;display: inline-block; text-align: center;font-weight: 900;text-decoration: none!important;\">Confirm Email</a> </body> </html>";
            //var htmlContent = "<html>   <head></head>  <body>  <h1 style=\"font-size:30px;padding-right:30px;padding-left:30px\">  Confirm your email address</h1>      <p style=\"font-size:17px;padding-right:30px;padding-left:30px\">  Hello! We just need to verify that <strong><a href=\"#\"\">{0}</a></strong> is your email address, and then we’ll help you find your workspaces.\t</p>  <p><strong> Thanks to install your mobile app using the below link:</strong>\t</p> <p> <strong><a href=\"https://play.google.com/store/apps/details?id=org.nativescript.Field\"\">FieldCTRL App Link </a></strong>.\t</p>   <a   href=\"{1}\" style=\"min-width: 196px;   border-top: 13px solid;  border-bottom: 13px solid;   border-right: 24px solid;  border-left: 24px solid;    border-color: #0b80f9;    border-radius: 4px;  background-color: #0b80f9;    color: #ffffff;   font-size: 18px;    line-height: 18px;     word-break: break-word;    display: inline-block; text-align: center;  font-weight: 900;text-decoration: none!important;\"> Confirm Email Address   </a>  <p style=\"font-size:17px;padding-right:30px;padding-left:30px;margin-top:40px;margin-bottom:30px\">  <strong>Password</strong><br>  {2}</p>  </body></html>";
            //htmlContent.Replace("{0}", "http://localhost:54816/api/User/ConfirmEmail" + "?Email=" + to);
            //string.Format(htmlContent, "http://localhost:54816/api/User/ConfirmEmail" + "?Email=" + to);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(filePath));

            //msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //{
            //    new SendGrid.Helpers.Mail.Attachment
            //    {
            //        Content = Convert.ToBase64String(byteData),
            //        Filename = "Transcript.txt",
            //        Type = "txt/plain",
            //        Disposition = "attachment"
            //    }
            //};
            if (client != null)
            {
                client.SendEmailAsync(msg);
            }
            else
            {
                Trace.TraceError("Failed to Send Message");
                // Task.FromResult(0);
            }
        }
    }
}