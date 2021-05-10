using tcc_back_end_puc.Domain.Enum;

namespace tcc_back_end_puc.Domain.Entities.Usuarios
{
    public class Usuario
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
        public Contato Contato { get; set; }
        public Endereco Endereco { get; set; }

        private string senha;
        public string Senha
        {
            set { senha = value; }
        }

        public string GetSenha()
        {
            return senha;
        }
    }
}
