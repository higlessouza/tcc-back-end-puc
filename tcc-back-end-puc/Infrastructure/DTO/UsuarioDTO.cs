using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc_back_end_puc.Infrastructure.DTO
{
    public class UsuarioDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public string  Senha { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public int CodigoPais { get; set; }
        public int CodigoCidade { get; set; }
        public int Telefone { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
    }
}
