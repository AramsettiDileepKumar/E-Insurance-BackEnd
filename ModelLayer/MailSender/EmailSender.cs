using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using System.Net.Mail;
using System.Net;

namespace ModelLayer.MailSender
{
    public class EmailSender
    {
        public static void sendMail(string Email,string password)
        {
            Console.WriteLine(Email);
            MailMessage mailMessage = new MailMessage("dileep20721@outlook.com", Email);
            try
            {
                mailMessage.Subject = "E-Insurance";
                mailMessage.Body = "You are Successfully Registered to E-Insurance Application, Here is Your login credentials."+"Email: "+Email+" Password: "+password;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
                smtpClient.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential("dileep20721@outlook.com", "ammadeepu@123");
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
