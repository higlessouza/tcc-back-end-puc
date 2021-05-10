using System;
using System.Collections.Generic;
using tcc_back_end_puc.Domain.Enum;

namespace tcc_back_end_puc.Domain.Entities.Anuncios
{
    public class Anuncio
    {
        public int Identificador { get; set; }
        public string Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }

        public double Preco { get; set; }
        public IEnumerable<Imagem> Images { get; set; }
        public IEnumerable<Topico> Topicos { get; set; }
        public IEnumerable<Avaliacao> Avaliacoes { get; set; }
        public StatusAprovacaoAnuncio Aprovado { get; set; }
    }
}
