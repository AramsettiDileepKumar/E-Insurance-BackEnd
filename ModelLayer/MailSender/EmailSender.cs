using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;

namespace ModelLayer.MailSender
{
    public class EmailSender
    {
        public static void sendMail(string Email,string password)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            try
            {
                mailMessage.From = new System.Net.Mail.MailAddress("dilip20721@outlook.com", "E-Insurance");
                mailMessage.To.Add(Email);
                mailMessage.Subject = "E-Insurance";
                mailMessage.Body = "You are Successfully Registered to E-Insurance Application, Here is Your login credentials."+"Email: "+Email+" Password: "+password;
                mailMessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                smtpClient.Port = 587; // Outlook SMTP port for TLS/STARTTLS

                // Enable SSL/TLS
                smtpClient.EnableSsl = true;

                string loginName = "dilip20721@outlook.com";
                string loginPassword = "Dileep@21700";

                System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(loginName, loginPassword);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
    }
}
