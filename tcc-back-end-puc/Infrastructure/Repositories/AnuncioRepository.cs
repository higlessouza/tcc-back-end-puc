using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;
using tcc_back_end_puc.Domain.Enum;
using tcc_back_end_puc.Domain.Repositories;
using tcc_back_end_puc.Infrastructure.DTO;
using tcc_back_end_puc.Infrastructure.Mapper;
using tcc_back_end_puc.Infrastructure.Repositories.Base;

namespace tcc_back_end_puc.Infrastructure.Repositories
{
    public class AnuncioRepository : BaseDbRepository, IAnuncioRepository
    {
        public AnuncioRepository(IUnitOfWork unitOfWork)
        : base(unitOfWork) { }

        #region Consultas 
        private const string SQL_INSERIR_ANUNCIO = @"
            Insert into Anuncios (
                [Titulo]
                ,[DataPublicacao]
                ,[Preco]
                ,[Aprovado]
                ,[FkIdentificadorUsuario]
                )
            Values (
                @titulo
                ,@dataPublicacao
                ,@preco
                ,@aprovado 
                ,@fkIdentificadorUsuario
            );
            SELECT SCOPE_IDENTITY();";

        private const string SQL_LISTAR_ANUNCIOS = @"
            SELECT
	            [Identificador]
                ,[Titulo]
                ,[DataPublicacao]
                ,[Preco]
                ,[Aprovado]
                ,[FkIdentificadorUsuario]
            FROM
                Anuncios
            ";

        private const string SQL_OBTER_ANUNCIO = @"
            SELECT
	            [Identificador]
                ,[Titulo]
                ,[DataPublicacao]
                ,[Preco]
                ,[Aprovado]
                ,[FkIdentificadorUsuario]
            FROM
                Anuncios
            WHERE 
                [Identificador] = @identificador
            ";

        private const string SQL_ATUALIZAR_ANUNCIO = @"
            UPDATE Anuncios 
                SET 
                [Titulo] = @titulo
                ,[DataPublicacao] = @dataPublicacao
                ,[Preco] = @preco
                ,[Aprovado] = @aprovado   
                ,[FkIdentificadorUsuario] = @fkIdentificadorUsuario
            WHERE 
                [Identificador] = @identificador
            ;";

        private const string SQL_APROVAR_ANUNCIO = @"
            UPDATE Anuncios 
                SET 
                [Aprovado] = @aprovado   
            WHERE 
                [Identificador] = @identificador
            ;";

        private const string SQL_DELETA_ANUNCIO = @"
            DELETE FROM 
                Anuncios 
            WHERE 
                [Identificador] = @identificador
            ;";

        //Imagens
        private const string SQL_INSERIR_IMAGEM = @"
            Insert into Imagens (
                [ImagemBase64]
                ,[FkIdentificadorAnuncio]
                )
            Values (
                @imagemBase64
                ,@fkIdentificadorAnuncio
            );
            SELECT SCOPE_IDENTITY();";

        private const string SQL_LISTAR_IMAGENS = @"
            SELECT
	            [Identificador]
                ,[ImagemBase64]
                ,[FkIdentificadorAnuncio] AS IdentificadorAnuncio   
            FROM
                Imagens
            ";

        private const string SQL_OBTER_IMAGENS = @"
            SELECT
	            [Identificador]
                ,[ImagemBase64]
                ,[FkIdentificadorAnuncio] AS IdentificadorAnuncio   
            FROM
                Imagens
            WHERE 
                [FkIdentificadorAnuncio] = @identificadorAnuncio
            ";

        private const string SQL_APAGAR_IMAGENS_POR_ANUNCIO = @"
            DELETE FROM Imagens                
            WHERE 
                [FkIdentificadorAnuncio] = @identificadorAnuncio
            ;";

        private const string SQL_ATUALIZAR_IMAGEM = @"
            UPDATE Anuncios 
                SET [ImagemBase64] = @imagemBase64
                ,[FkIdentificadorAnuncio] = @FkIdentificadorAnuncio
            WHERE 
                [Identificador] = @identificador
            ;";

        //Topicos
        private const string SQL_INSERIR_TOPICO = @"
            Insert into Topicos (
                [Titulo]
                ,[Conteudo]
                ,[FkIdentificadorAnuncio]
                )
            Values (
                @titulo
                ,@conteudo
                ,@fkIdentificadorAnuncio
            );
            SELECT SCOPE_IDENTITY();";

        private const string SQL_LISTAR_TOPICOS = @"
            SELECT
	            [Identificador]
                ,[Titulo]
                ,[Conteudo]
                ,[FkIdentificadorAnuncio] AS IdentificadorAnuncio   
            FROM
                Topicos
            ";

        private const string SQL_OBTER_TOPICOS = @"
            SELECT
	            [Identificador]
                ,[Titulo]
                ,[Conteudo]
                ,[FkIdentificadorAnuncio] AS IdentificadorAnuncio   
            FROM
                Topicos
            WHERE 
                [FkIdentificadorAnuncio] = @identificadorAnuncio
            ";

        private const string SQL_APAGAR_TOPICOS_POR_ANUNCIO = @"
            DELETE FROM Topicos                
            WHERE 
                [FkIdentificadorAnuncio] = @identificadorAnuncio
            ;";

        //Avaliações
        private const string SQL_INSERIR_AVALIACAO = @"
            Insert into Avaliacoes (
                [Nome]
                ,[Comentario]
                ,[Nota]
                ,[FkIdentificadorAnuncio]
                )
            Values (
                @nome
                ,@comentario
                ,@nota
                ,@fkIdentificadorAnuncio
            );
            SELECT SCOPE_IDENTITY();";

        private const string SQL_LISTAR_AVALIACOES = @"
            SELECT
	            [Nome]
                ,[Comentario]
                ,[Nota]
                ,[FkIdentificadorAnuncio] AS IdentificadorAnuncio   
            FROM
                Avaliacoes
            ";

        private const string SQL_OBTER_AVALIACOES = @"
            SELECT
	            [Nome]
                ,[Comentario]
                ,[Nota]
                ,[FkIdentificadorAnuncio] AS IdentificadorAnuncio   
            FROM
                Avaliacoes
            WHERE 
                [FkIdentificadorAnuncio] = @identificadorAnuncio
            ";

        private const string SQL_APAGAR_AVALIACOES_POR_ANUNCIO = @"
            DELETE FROM Avaliacoes                
            WHERE 
                [FkIdentificadorAnuncio] = @identificadorAnuncio
            ;";

        private const string SQL_APAGAR_AVALIACOES_POR_IDENTIFICADOR = @"
            DELETE FROM Avaliacoes                
            WHERE 
                [Identificador] = @identificador
            ;";

        #endregion

        /// <summary>
        /// Obtem usuario pelo identificador 
        /// </summary>
        /// <param name="identificador">identificador do usuario</param>
        /// <returns></returns>
        public async Task<Anuncio> ObterPorIdentificador(int identificador)
        {
            try
            {
                var parametros = CreateParameters
                    .Add("@identificador", identificador, DbType.Int32)
                    .GetParameters();

                var anuncioDTO = await UnitOfWork.Connection.QuerySingleAsync<AnuncioDTO>(SQL_OBTER_ANUNCIO, parametros);
                var anuncio = anuncioDTO.ToAnuncio();

                var parametrosListas = CreateParameters
                    .Add("@identificadorAnuncio", identificador, DbType.Int32)
                    .GetParameters();
                var topicosDb = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_OBTER_TOPICOS, parametrosListas);
                var topicos = topicosDb.Parse<Topico>();

                var avaliacoesDb = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_OBTER_AVALIACOES, parametrosListas);
                var avaliacoes = avaliacoesDb.Parse<Avaliacao>();

                var imagensDb = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_OBTER_IMAGENS, parametrosListas);
                var imagens = imagensDb.Parse<Imagem>();


                anuncio.Avaliacoes = avaliacoes.Where(avaliacao => avaliacao.IdentificadorAnuncio == anuncio.Identificador);
                anuncio.Images = imagens.Where(imagem => imagem.IdentificadorAnuncio == anuncio.Identificador);
                anuncio.Topicos = topicos.Where(topico => topico.IdentificadorAnuncio == anuncio.Identificador);

                return anuncio;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Insere um anuncio
        /// </summary>
        /// <param name="anuncio"></param>
        /// <returns></returns>
        public async Task<Anuncio> InserirAnuncio(Anuncio anuncio)
        {
            var anuncioDTO = anuncio.ToAnuncioDTO();
            var parametros = CreateParameters
                .Add("@titulo", anuncioDTO.Titulo, DbType.String)
                .Add("@dataPublicacao", anuncioDTO.DataPublicacao, DbType.DateTime)
                .Add("@preco", anuncioDTO.Preco, DbType.Double) //Se der erro, pode ser aqui  banco e floar e db type é double
                .Add("@aprovado", anuncioDTO.Aprovado, DbType.Int16) //aqui deve dar ruim
                .Add("@fkIdentificadorUsuario", anuncioDTO.FkIdentificadorUsuario, DbType.Int16)
               .GetParameters();
            var identificadorAnuncio = await UnitOfWork.Connection.QuerySingleAsync<int>(SQL_INSERIR_ANUNCIO, parametros);
            anuncio.Identificador = identificadorAnuncio;

            return await InserirListasDoAnuncio(anuncio);
        }

        /// <summary>
        /// Insere um Topico
        /// </summary>
        /// <param name="topico"></param>
        /// <returns></returns>
        public async Task<Topico> InserirTopico(Topico topico)
        {
            var parametros = CreateParameters
                .Add("@titulo", topico.Titulo, DbType.String)
                .Add("@conteudo", topico.Conteudo, DbType.String)
                .Add("@fkIdentificadorAnuncio", topico.IdentificadorAnuncio, DbType.Int32)
               .GetParameters();
            var identificador = await UnitOfWork.Connection.QuerySingleAsync<int>(SQL_INSERIR_TOPICO, parametros);
            topico.Identificador = identificador;

            return topico;
        }

        /// <summary>
        /// Insere um Topico
        /// </summary>
        /// <param name="imagem"></param>
        /// <returns></returns>
        public async Task<Imagem> InserirImagem(Imagem imagem)
        {
            var parametros = CreateParameters
                .Add("@imagemBase64", imagem.ImagemBase64, DbType.String)
                .Add("@fkIdentificadorAnuncio", imagem.IdentificadorAnuncio, DbType.Int32)
               .GetParameters();
            var identificador = await UnitOfWork.Connection.QuerySingleAsync<int>(SQL_INSERIR_IMAGEM, parametros);
            imagem.Identificador = identificador;

            return imagem;
        }

        /// <summary>
        /// Insere um Topico
        /// </summary>
        /// <param name="avaliacao"></param>
        /// <returns></returns>
        public async Task<Avaliacao> InserirAvaliacao(Avaliacao avaliacao)
        {
            var parametros = CreateParameters
                .Add("@nome", avaliacao.Nome, DbType.String)
                .Add("@comentario", avaliacao.Comentario, DbType.String)
                .Add("@nota", avaliacao.Nota, DbType.Int16)
                .Add("@fkIdentificadorAnuncio", avaliacao.IdentificadorAnuncio, DbType.Int32)
               .GetParameters();

            var identificador = await UnitOfWork.Connection.QuerySingleAsync<int>(SQL_INSERIR_AVALIACAO, parametros);
            avaliacao.Identificador = identificador;

            return avaliacao;
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="anuncio"></param>
        /// <returns></returns>
        public async Task<Anuncio> AtualizarAnuncio(Anuncio anuncio)
        {
            var anuncioDTO = anuncio.ToAnuncioDTO();
            var parametros = CreateParameters
              .Add("@titulo", anuncioDTO.Titulo, DbType.String)
              .Add("@dataPublicacao", anuncioDTO.DataPublicacao, DbType.DateTime)
              .Add("@preco", anuncioDTO.Preco, DbType.Double) //Se der erro, pode ser aqui  banco e floar e db type é double
              .Add("@aprovado", anuncio.Aprovado, DbType.Boolean) //aqui deve dar ruim               
              .Add("@identificador", anuncio.Identificador, DbType.Int32)
              .GetParameters();
            parametros.RemoveUnused = true;
            await UnitOfWork.Connection.ExecuteAsync(SQL_ATUALIZAR_ANUNCIO, parametros);

            await ResetarListasDoAnuncio(anuncio.Identificador);
            anuncio = await InserirListasDoAnuncio(anuncio);
            return anuncio;
        }


        /// <summary>
        /// Aprova um usuário
        /// </summary>
        /// <param name="identificadorAnuncio"></param>
        /// <returns></returns>
        public async Task AprovarAnuncio(int identificadorAnuncio)
        {          
            var parametros = CreateParameters
              .Add("@aprovado", StatusAprovacaoAnuncio.Aprovado, DbType.Boolean) //aqui deve dar ruim               
              .Add("@identificador", identificadorAnuncio, DbType.Int32)
              .GetParameters();
            parametros.RemoveUnused = true;
            await UnitOfWork.Connection.ExecuteAsync(SQL_APROVAR_ANUNCIO, parametros);
        }

        /// <summary>
        /// Insere Topicos, Imagens e Avaliacoes do anuncio
        /// </summary>
        /// <param name="anuncio"></param>
        /// <returns></returns>
        private async Task<Anuncio> InserirListasDoAnuncio(Anuncio anuncio)
        {
            var identificadorAnuncio = anuncio.Identificador;
            //inserir topicos
            if (anuncio.Topicos != null)
            {
                foreach (var topico in anuncio.Topicos)
                {
                    topico.IdentificadorAnuncio = identificadorAnuncio;
                    var TopicoInserido = await InserirTopico(topico);
                    topico.Identificador = TopicoInserido.Identificador;
                }
            }

            if (anuncio.Images != null)
            {
                //inserir imagens
                foreach (var imagem in anuncio.Images)
                {
                    imagem.IdentificadorAnuncio = identificadorAnuncio;
                    var imagemInserida = await InserirImagem(imagem);
                    imagem.Identificador = imagemInserida.Identificador;
                }
            }

            if (anuncio.Avaliacoes != null)
            {
                //inserir avaliaca
                foreach (var avaliacao in anuncio.Avaliacoes)
                {
                    avaliacao.IdentificadorAnuncio = identificadorAnuncio;
                    var avaliacaoInserida = await InserirAvaliacao(avaliacao);
                    avaliacao.Identificador = avaliacaoInserida.Identificador;
                }
            }

            return anuncio;
        }

        /// <summary>
        /// Apaga Topicos, Imagens e Avaliacoes do anuncio
        /// </summary>
        /// <param name="idAnuncio"></param>
        /// <returns></returns>
        private async Task ResetarListasDoAnuncio(int idAnuncio)
        {
            var parametros = CreateParameters
               .Add("@identificadorAnuncio", idAnuncio, DbType.Int32)
               .GetParameters();

            await UnitOfWork.Connection.ExecuteAsync(SQL_APAGAR_TOPICOS_POR_ANUNCIO, parametros);

            await UnitOfWork.Connection.ExecuteAsync(SQL_APAGAR_IMAGENS_POR_ANUNCIO, parametros);
            await UnitOfWork.Connection.ExecuteAsync(SQL_APAGAR_AVALIACOES_POR_ANUNCIO, parametros);
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public async Task<IEnumerable<Anuncio>> ListarAnuncios()
        {
            var anunciosDB = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_LISTAR_ANUNCIOS);
            var anunciosDTO = anunciosDB.Parse<AnuncioDTO>().ToList();

            var topicosDb = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_LISTAR_TOPICOS);
            var topicos = topicosDb.Parse<Topico>().ToList();

            var avaliacoesDb = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_LISTAR_AVALIACOES);
            var avaliacoes = avaliacoesDb.Parse<Avaliacao>().ToList();

            var imagensDb = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_LISTAR_IMAGENS);
            var imagens = imagensDb.Parse<Imagem>().ToList();


            var teste = avaliacoes.Where(x => x.IdentificadorAnuncio == 3);
            IEnumerable<Anuncio> anuncios = new List<Anuncio>();
            foreach (var anuncioDTO in anunciosDTO)
            {
                var anuncio = anuncioDTO.ToAnuncio();
                anuncio.Avaliacoes = avaliacoes.Where(avaliacao => avaliacao.IdentificadorAnuncio == anuncio.Identificador);
                anuncio.Images = imagens.Where(imagem => imagem.IdentificadorAnuncio == anuncio.Identificador);
                anuncio.Topicos = topicos.Where(topico => topico.IdentificadorAnuncio == anuncio.Identificador);
                anuncios = anuncios.Append(anuncio);
            }

            return anuncios;
        }

        /// <summary>
        /// Deleta usuário 
        /// </summary>
        /// <param name="identificador">Identificador usuário</param>
        /// <returns></returns>
        public async Task<bool> DeletarAnuncio(int identificador)
        {
            var parametros = CreateParameters
               .Add("@identificador", identificador, DbType.Int32)
               .GetParameters();
            var identificadorApagado = await UnitOfWork.Connection.ExecuteAsync(SQL_DELETA_ANUNCIO, parametros);

            return (identificadorApagado == 1);
        }


        /// <summary>
        /// Deleta usuário 
        /// </summary>
        /// <param name="identificador">Identificador usuário</param>
        /// <returns></returns>
        public async Task<bool> DeletarAvaliacao(int identificador)
        {
            var parametros = CreateParameters
               .Add("@identificador", identificador, DbType.Int32)
               .GetParameters();
            var identificadorApagado = await UnitOfWork.Connection.ExecuteAsync(SQL_APAGAR_AVALIACOES_POR_IDENTIFICADOR, parametros);

            return (identificadorApagado == 1);
        }
    }
}
