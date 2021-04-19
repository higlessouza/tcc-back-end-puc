using static tcc_back_end_puc.Domain.Enum.TipoUsuario;

namespace tcc_back_end_puc.Domain.Entities.Usuario
{
    public class Usuario
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public StatusCobranca Tipo { get; set; }

    }
}
