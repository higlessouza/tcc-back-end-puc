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
        public Task<int> EnviarEmail(string destinatario)
        {
            Random numAleatorio = new Random();
            int codigoRecuperacao = numAleatorio.Next(1000, 9999);

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("tccpucminassg@gmail.com", "tccpuc#2021")
            };

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("tccpucminassg@gmail.com"),
                //.Bcc.Add(new System.Net.Mail.MailAddress(textBoxCCo.Text));
                //.CC.Add(new System.Net.Mail.MailAddress("5544"));
                Subject = "Reuperação de senha - TCC Puc Minas",
                IsBodyHtml = true,
                Body = ObterCorpoEmail(codigoRecuperacao)
            };
            mail.To.Add(new MailAddress(destinatario));
            smtp.Send(mail);
            return Task.FromResult(codigoRecuperacao);
        }

        public string ObterCorpoEmail( int codigo)
        {
            return @" <!DOCTYPE html>
                    <html>
                    <head>
                    <style>
                    body {background-color: white;} h1 {color: black; text-align: left;}                  
                    p {font-family: verdana;font-size:20px;}                    
                    img {display: block; margin-left: auto; margin-right: auto}
                    </style>
                    </head>
                    <body>
                    <img src='https://ipuc.pucminas.br/green/documentos/green/logo_pucminas.png' width = '130px'/>
                    <h1> Redefinição de senha</h1>
                    <p> Olá, </p>
                    <p>Recebemos uma solicitação para restaurar sua senha de acesso em nosso site. </p> 
                    <p>Por favor, utilize o código <b>" + codigo+@"</b> para concluir a redefinição.</p>
                    <p> Atenciosamente, </p>
                    <p> Marketing Go. </p>
                    <img src ='https://uxyja.stripocdn.email/content/guids/CABINET_1ce849b9d6fc2f13978e163ad3c663df/images/3991592481152831.png'/>
                     </body>
                     </html>
          ";
        }
    }
}
