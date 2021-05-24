using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuarios;

namespace tcc_back_end_puc.Infrastructure.Repositories
{
    public class EmailRepository
    {
        public int EnviarEmail(string destinatario)
        {
            Random numAleatorio = new Random();
            int codigoRecuperacao = numAleatorio.Next(1000, 9999);

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("tccpucminassg@gmail.com", "tccpuc#2021")
            };

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("tccpucminassg@gmail.com"),
                //.Bcc.Add(new System.Net.Mail.MailAddress(textBoxCCo.Text));
                //.CC.Add(new System.Net.Mail.MailAddress("5544"));
                Subject = "Reuperação de senha - TCC Puc Minas",
                Body = $"{codigoRecuperacao}"
            };
            mail.To.Add(new MailAddress(destinatario));
            smtp.Send(mail);
            return codigoRecuperacao;
        }
    }
}
