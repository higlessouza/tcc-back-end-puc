using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc_back_end_puc.Domain.Entities.Anuncios
{
    public class Imagem
    {
        public int Identificador { get; set; }
        public int IdentificadorAnuncio { get; set; }
        public string ImagemBase64 { get; set; }
    }
}
