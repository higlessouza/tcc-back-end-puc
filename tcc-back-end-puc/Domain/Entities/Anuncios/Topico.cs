using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc_back_end_puc.Domain.Entities.Anuncios
{
    public class Topico
    {
        public int Identificador { get; set; }
        public int IdentificadorAnuncio { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
    }
}
