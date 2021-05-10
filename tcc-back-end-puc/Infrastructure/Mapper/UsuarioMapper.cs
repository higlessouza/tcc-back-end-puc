using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuarios;
using tcc_back_end_puc.Domain.Enum;
using tcc_back_end_puc.Infrastructure.DTO;

namespace tcc_back_end_puc.Infrastructure.Mapper
{
    public static class UsuarioMapper
    {
        public static UsuarioDTO ToUsuarioDTO(this Usuario usuario)
        {
            return new UsuarioDTO()
            {
                //Dados Padroes
                Identificador = usuario.Identificador,
                Email = usuario.Email,
                Tipo = (int)usuario.Tipo,
                Nome = usuario.Nome,
                Senha = usuario.GetSenha(),
                //Endereco
                Bairro = usuario.Endereco.Bairro,
                Cep = usuario.Endereco.Cep,
                Cidade = usuario.Endereco.Cidade,
                Estado = usuario.Endereco.Estado,
                Pais = usuario.Endereco.Pais,
                Rua = usuario.Endereco.Rua,
                //Contato
                CodigoCidade = usuario.Contato.Telefone.CodigoCidade,
                CodigoPais = usuario.Contato.Telefone.CodigoPais,
                Telefone = usuario.Contato.Telefone.Numero,
                Facebook = usuario.Contato.Facebook,
                Linkedin = usuario.Contato.Linkedin,
                Twitter = usuario.Contato.Twitter
            };
        }


        public static Usuario ToUsuario(this UsuarioDTO usuarioDTO)
        {
            return new Usuario()
            {
                //Dados Padroes
                Identificador = usuarioDTO.Identificador,
                Email = usuarioDTO.Email,
                Tipo = (TipoUsuario)usuarioDTO.Tipo,
                Nome = usuarioDTO.Nome,
                Senha = usuarioDTO.Senha,
                //Endereco
                Endereco = new Endereco()
                {
                    Bairro = usuarioDTO.Bairro,
                    Cep = usuarioDTO.Cep,
                    Cidade = usuarioDTO.Cidade,
                    Estado = usuarioDTO.Estado,
                    Pais = usuarioDTO.Pais,
                    Rua = usuarioDTO.Rua
                },
                //Contato
                Contato = new Contato()
                {
                    Telefone = new Telefone()
                    {
                        CodigoCidade = usuarioDTO.CodigoCidade,
                        CodigoPais = usuarioDTO.CodigoPais,
                        Numero = usuarioDTO.Telefone
                    },
                    Facebook = usuarioDTO.Facebook,
                    Linkedin = usuarioDTO.Linkedin,
                    Twitter = usuarioDTO.Twitter
                }

            };
        }

    }
}
