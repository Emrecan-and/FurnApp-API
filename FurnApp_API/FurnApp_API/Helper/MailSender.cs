using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace FurnApp_API.Helper
{
    public class MailSender
    {
        //BU videoyu izle ="https://www.youtube.com/watch?v=lk5dhDzfzsU"
        private static MailSender instance;
        private MailSender() { }

        public static MailSender GetInstance()
        {
            if (instance == null)
            {
                instance = new MailSender();
            }
            return instance;
        }

        public void LoginSender(string toAddress)
        {
            // E-posta konusu ve içeriği
            string subject = "FurnApp'e Hoşgeldiniz!";
            string body = "Mobilya alışverişinizde size eşsiz deneyim sunmaktan keyif alacağımızı bilmenizi isteriz.Uygulama kullanımında karşılaştığınız" +
                "herhangi bir sorunda bu mail adresinden destek ekibimize ulaşabilirsiniz.";

            // Gönderen e-posta adresi ve şifresi
            string fromAddress = "alp792746@gmail.com";
            string password = "123berkay123";

            // SMTP sunucusu ve portu
            string smtpServer = "smtp.example.com";
            int smtpPort = 587;
            try
            {
                // MailMessage oluşturma
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;

                // SMTP istemcisi oluşturma ve gönderme işlemi
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                smtpClient.Port = smtpPort;
                smtpClient.Credentials = new NetworkCredential(fromAddress, password);
                smtpClient.EnableSsl = true; // TLS kullanılıyor
                smtpClient.Send(mail);

                Console.WriteLine("Doğrulama emaili başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.Message);
            }
        }
    }
}
