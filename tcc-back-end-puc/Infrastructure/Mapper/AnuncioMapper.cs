using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;
using tcc_back_end_puc.Domain.Enum;
using tcc_back_end_puc.Infrastructure.DTO;

namespace tcc_back_end_puc.Infrastructure.Mapper
{
    public static class AnuncioMapper
    {
        public static AnuncioDTO ToAnuncioDTO(this Anuncio anuncio)
        {
            return new AnuncioDTO()
            {
                Titulo = anuncio.Titulo,
                Identificador = anuncio.Identificador,
                DataPublicacao = anuncio.DataPublicacao,
                Preco = anuncio.Preco,
                Aprovado = (int)anuncio.Aprovado
            };
        }
        public static Anuncio ToAnuncio(this AnuncioDTO anuncioDTO, IEnumerable<Imagem> imagens, IEnumerable<Topico> topicos, IEnumerable<Avaliacao> avaliacoes)
        {
            var anuncio = new Anuncio()
            {
                Titulo = anuncioDTO.Titulo,
                Identificador = anuncioDTO.Identificador,
                DataPublicacao = anuncioDTO.DataPublicacao,
                Preco = anuncioDTO.Preco,
                Avaliacoes = avaliacoes,
                Images = imagens,
                Topicos = topicos,
                Aprovado = (StatusAprovacaoAnuncio)anuncioDTO.Aprovado
            };

            return anuncio;
        }

        public static Anuncio ToAnuncio(this AnuncioDTO anuncioDTO)
        {
            var anuncio = new Anuncio()
            {
                Titulo = anuncioDTO.Titulo,
                Identificador = anuncioDTO.Identificador,
                DataPublicacao = anuncioDTO.DataPublicacao,
                Preco = anuncioDTO.Preco,
                Aprovado = (StatusAprovacaoAnuncio)anuncioDTO.Aprovado
            };

            return anuncio;
        }

    }
}
