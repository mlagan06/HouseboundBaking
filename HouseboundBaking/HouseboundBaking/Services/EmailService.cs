using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HouseboundBaking.Services
{
    class EmailService
    {
        int attemptsToSendEmailCount = 0;
        public EmailService()
        {

        }

        public bool SendEmailReceipt(int OrderId, string priceOfOrder, string userEmail)
        {
            bool emailSent = true;

            string body = "Thank you for your recent order totalling to £" + priceOfOrder + "\nYour order number is: " + OrderId + "\nPlease pay for your order upon collection." + "\n\nEnjoy your home baking products.\n\nThank You\nHouseboundBaking";
            //await Xamarin.Essentials.Email.ComposeAsync("HouseboundBaking - Order " + OrderId, body, userEmail);

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("HomeBaker365email@gmail.com", "NewPassword123!");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("HomeBaker365email@gmail.com");
                mail.To.Add(userEmail);
                mail.Subject = "HouseboundBaking - Order " + OrderId;
                mail.Body = body;

                ServicePointManager.ServerCertificateValidationCallback = delegate (object senders, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslp)
                { return true; };

                //send user email as receipt
                SmtpServer.Send(mail);

                // throw new Exception();

                //send copy of receipt to home email
                MailMessage mail2 = new MailMessage();
                mail2.From = new MailAddress("HomeBaker365email@gmail.com");
                mail2.To.Add("HomeBaker365email@gmail.com");
                mail2.Subject = "Copy - Receipt - Order " + OrderId;
                mail2.Body = body + "\n\n (Above email has been sent to: " + userEmail + ")";

                SmtpServer.Send(mail2);

                return emailSent;
            }
            catch (Exception ex)
            {
                //Catch Error
                //DisplayAlert("Faild", ex.Message, "OK");
                string error = ex.ToString();

                Crashes.TrackError(ex);

                emailSent = false;
                return emailSent;
            }
        }

        public bool SendResetPasswordEmail(string userEmail)
        {
            bool emailSent = true;

            string body = "Sorry to hear about your issues. Use this temporary password to login and reset your password:\n";
            //await Xamarin.Essentials.Email.ComposeAsync("HouseboundBaking - Order " + OrderId, body, userEmail);

            try
            {
                //todo make call to create temp password and update password on system to this temp
                int tempPassword = 12345;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("HouseboundBakingemail@gmail.com", "NewPassword123!");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("HouseboundBakingemail@gmail.com");
                mail.To.Add(userEmail);
                mail.Subject = "HouseboundBaking - Reset Password";
                mail.Body = body + tempPassword.ToString() + "\n\nThank You\nHouseboundBakingper365";

                ServicePointManager.ServerCertificateValidationCallback = delegate (object senders, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslp)
                { return true; };

                //send user email as receipt
                SmtpServer.Send(mail);

                // throw new Exception();

                //send copy of receipt to home email
                MailMessage mail2 = new MailMessage();
                mail2.From = new MailAddress("HouseboundBakingemail@gmail.com");
                mail2.To.Add("HouseboundBakingemail@gmail.com");
                mail2.Subject = "Copy - HouseboundBaking - Reset Password";
                mail2.Body = body + "\n\n (Above email has been sent to: " + userEmail + ")";

                SmtpServer.Send(mail2);

                return emailSent;
            }
            catch (Exception ex)
            {
                //Catch Error
                //DisplayAlert("Faild", ex.Message, "OK");
                string error = ex.ToString();

                Crashes.TrackError(ex);

                emailSent = false;
                return emailSent;
            }
        }

        public void SendErrorFoundEmail(string PageWithError, int OrderId, string adminEmail)
        {
            bool emailSent = true;

            string body = "Error found on " + PageWithError + " with Order: " + OrderId + "\n\n Date/Time: " + DateTime.UtcNow;

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("HouseboundBakingemail@gmail.com", "NewPassword123!");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("HouseboundBakingemail@gmail.com");
                mail.To.Add(adminEmail);
                mail.Subject = "HouseboundBaking - Order " + OrderId;
                mail.Body = body;

                ServicePointManager.ServerCertificateValidationCallback = delegate (object senders, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslp)
                { return true; };

                attemptsToSendEmailCount++;

                if (attemptsToSendEmailCount == 3)
                {
                    attemptsToSendEmailCount = 0;
                    Exception ex = new Exception("EmailService -> SendErrorFoundEmail() -> Error: Stuck in loop");
                    Crashes.TrackError(ex);
                    return;
                }

                //send user email as receipt
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                Crashes.TrackError(ex);

                //below gets stuck in loop if email not sent, so added count
                SendErrorFoundEmail(PageWithError, OrderId, adminEmail);
            }
        }
    }
}
