using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;

namespace tcc_back_end_puc.Domain.Repositories
{
    public interface IAnuncioRepository
    {
        /// <summary>
        /// Obtem um anuncio pelo identificador
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        public Task<Anuncio> ObterPorIdentificador(int identificador);

        /// <summary>
        /// Insere um anuncio
        /// </summary>
        /// <param name="anuncio"></param>
        /// <returns></returns>
        public Task<Anuncio> InserirAnuncio(Anuncio anuncio);

        /// <summary>
        /// Insere um Topico
        /// </summary>
        /// <param name="topico"></param>
        /// <returns></returns>
        public Task<Topico> InserirTopico(Topico topico);

        /// <summary>
        /// Insere um Topico
        /// </summary>
        /// <param name="imagem"></param>
        /// <returns></returns>
        public Task<Imagem> InserirImagem(Imagem imagem);

        /// <summary>
        /// Insere um Topico
        /// </summary>
        /// <param name="avaliacao"></param>
        /// <returns></returns>
        public Task<Avaliacao> InserirAvaliacao(Avaliacao avaliacao);

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        public Task<Anuncio> AtualizarAnuncio(Anuncio anuncio);
        /// <summary>
        /// Aprova um usuário
        /// </summary>
        public Task AprovarAnuncio(int identificadorAnuncio);

        /// <summary>
        /// Reprova um usuário
        /// </summary>
        public Task ReprovarAnuncio(int identificadorAnuncio);

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        public Task<IEnumerable<Anuncio>> ListarAnuncios();

        /// <summary>
        /// Deleta usuário 
        /// </summary>
        public Task<bool> DeletarAnuncio(int identificador);

        /// <summary>
        /// Deleta Avaliacao 
        /// </summary>
        public Task<bool> DeletarAvaliacao(int identificador);

        ///// <summary>
        ///// Lista todos os anuncios que ainda não foram aprovados/rejeitados
        ///// </summary>
        //public Task<IEnumerable<Anuncio>> ListarAnunciosParaSeremAprovados();

        ///// <summary>
        ///// aprova um anuncio
        ///// </summary>
        //public Task<Anuncio> AprovarAnuncio(Anuncio anuncio);

        ///// <summary>
        ///// Reprova um anuncio
        ///// </summary>
        ///// <returns></returns>
        //public Task<Anuncio> ReprovarAnuncio(Anuncio anuncio);
    }
}
