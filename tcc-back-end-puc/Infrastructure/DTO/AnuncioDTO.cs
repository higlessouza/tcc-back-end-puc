using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;

namespace tcc_back_end_puc.Infrastructure.DTO
{
    public class AnuncioDTO
    {
        public int Identificador { get; set; }
        public int FkIdentificadorUsuario { get; set; }
        public string Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public double Preco { get; set; }
        public int Aprovado { get; set; }
    }
}
