using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;
using tcc_back_end_puc.Domain.Entities.Usuarios;

namespace tcc_back_end_puc.Infrastructure.Repositories
{
    public class EmailRepository
    {
        private SmtpClient smtp;

        public EmailRepository()
        {
            smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("tccpucminassg@gmail.com", "mqsigzallosybqdk")//"tccpuc#2021")
            };
        }
        public Task<int> EnviarEmailRecuperacaoSenha(string destinatario)
        {
            Random numAleatorio = new Random();
            int codigoRecuperacao = numAleatorio.Next(1000, 9999);

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("tccpucminassg@gmail.com"),
                Subject = "Reuperação de senha - Marketing Go!",
                IsBodyHtml = true,
                Body = ObterCorpoEmailRecuperacaoSenha(codigoRecuperacao)
            };
            mail.To.Add(new MailAddress(destinatario));
            smtp.Send(mail);
            return Task.FromResult(codigoRecuperacao);
        }

        public async Task EnviarEmailNovaAvaliacao(Avaliacao avaliacao, Task<Usuario> usuario)
        {
            var usuarioEmail = await usuario;

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("tccpucminassg@gmail.com"),
                Subject = "Nova avaliação- Marketing Go!",
                IsBodyHtml = true,
                Body = ObterCorpoEmailNovaAvaliacao(avaliacao)
            };
            mail.To.Add(new MailAddress(usuarioEmail.Email));
            smtp.Send(mail);
        }

        public async Task EnviarEmailNovoAnuncio(Anuncio anuncio, Task<Usuario> usuario)
        {
            var usuarioEmail = await usuario;

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("tccpucminassg@gmail.com"),
                Subject = "Anúncio aprovado - Marketing Go!",
                IsBodyHtml = true,
                Body = ObterCorpoEmailAnuncioAprovado(anuncio)
            };
            mail.To.Add(new MailAddress(usuarioEmail.Email));
            smtp.Send(mail);
        }

        private string ObterCorpoEmailAnuncioAprovado(Anuncio anuncio)
        {
            return @"<!DOCTYPE html><html><head><style>
         body {background-color: white;} h1 {color: black; text-align: left;}                  
         p, h2, h1 {font-family: verdana;font-size:20px;margin-left:1%}                    
         .iconStar{float:left}         		
		</ style ></style></head><body><div style='max-width:800px'><img src='https://www.imagemhost.com.br/images/2021/05/27/marketingGo.png' style='display: block; margin-left: auto; margin-right: auto; width:130px'/><h1> Oba, seu anúncio foi aprovado! </h1><table class='cardPuc' style='pading:2%'><tr><td style='background-color: #0070a1' rowspan='4' ><img style='margin:5px' src='https://www.imagemhost.com.br/images/2021/05/28/megafone.png' width = '50px' /></td></tr><tr><th style='background-color: #0070a1; color:white; width:90%; text-align: left'><h2>" + anuncio.Titulo + @"</h2></th></tr><tr><th rowspan='2' style='background-color: #f5f5f5; color:black;text-align: left;width:90%'><p>" + anuncio.Descricao + @"</p></th></tr></table><p> Atenciosamente, </p><p> Marketing Go! </p ><img src = 'https://uxyja.stripocdn.email/content/guids/CABINET_1ce849b9d6fc2f13978e163ad3c663df/images/3991592481152831.png' style = 'display: block; margin-left: auto; margin-right: auto' /></ div ></body>
";
        }

        private string ObterCorpoEmailRecuperacaoSenha(int codigo)
        {
            return @"<!DOCTYPE html><html><head><style>
                    body {background-color: white;} h1 {color: black; text-align: left;}                  
                    p {font-family: verdana;font-size:20px;}
                    h2, h1{font-family: verdana}    
                    </style></head><body><div style='max-width:800px'><img src='https://www.imagemhost.com.br/images/2021/05/27/marketingGo.png' style='display: block; margin-left: auto; margin-right: auto; width:130px' /><h1> Redefinição de senha</h1><p> Olá, </p><p>Recebemos uma solicitação para restaurar sua senha de acesso em nosso site. </p><p>Por favor, utilize o código <b>" + codigo + @"</b> para concluir a redefinição.</p><p> Atenciosamente, </p><p> Marketing Go! </p><img src ='https://uxyja.stripocdn.email/content/guids/CABINET_1ce849b9d6fc2f13978e163ad3c663df/images/3991592481152831.png'style='display: block; margin-left: auto; margin-right: auto;' /></div></body></html>
          ";
        }

        private string ObterCorpoEmailNovaAvaliacao(Avaliacao avaliacao)
        {
            return @"<!DOCTYPE html><html><head><style>
         body {background-color: white;} h1 {color: black; text-align: left;}                  
         p, h2, h1 {font-family: verdana;font-size:20px;margin-left:1%}                    
         .iconStar{float:left}
         
		</ style ></style></head><body><div style='max-width:800px'><img src='https://www.imagemhost.com.br/images/2021/05/27/marketingGo.png' style='display: block; margin-left: auto; margin-right: auto; width:130px'/><h1> Oba, tem um novo comentário no seu anúncio! </h1><table class='cardPuc' style='pading:2%'><tr><th style='background-color: #0070a1; color:white; width:1%; text-align: left'><h2>" + avaliacao.Nome + "</h2></th></tr><tr><th style='background-color: #f5f5f5; color:black;text-align: left;'><p>" + avaliacao.Comentario + @"</p></th></tr><tr><th style='background-color: #f5f5f5; color:black;text-align: left;'>

            " + ObterEstrelasEmailAvaliacao(avaliacao.Nota) + @"
            </th></tr></table> <p> Atenciosamente, </p><p> Marketing Go! </p ><img src = 'https://uxyja.stripocdn.email/content/guids/CABINET_1ce849b9d6fc2f13978e163ad3c663df/images/3991592481152831.png' style = 'display: block; margin-left: auto; margin-right: auto' /></ div ></ body >
                 ";
        }

        private string ObterEstrelasEmailAvaliacao(int nota)
        {
            string estrelas = "";
            for (int i = 0; i < nota; i++)
            {
                estrelas += "<span class='iconStar'><img src = 'https://cdn.iconscout.com/icon/free/png-256/star-bookmark-favorite-shape-rank-16-28621.png' width = '25px'/></span>";
            }

            for (int i = 0; i < (5 - nota); i++)
            {
                estrelas += "<span class='iconStar'><img src = 'https://iconape.com/wp-content/png_logo_vector/star-2.png' width = '25px'/></span>";
            }
            return estrelas;
        }
    }
}
