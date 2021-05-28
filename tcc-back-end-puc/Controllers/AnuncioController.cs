﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;
using tcc_back_end_puc.Domain.Entities.Usuarios;
using tcc_back_end_puc.Domain.Enum;
using tcc_back_end_puc.Domain.Repositories;
using tcc_back_end_puc.Infrastructure.Repositories;

namespace tcc_back_end_puc.Controllers
{
    [Route("api/anuncio")]
    [ApiController]

    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AnuncioController(IAnuncioRepository anuncioRepository, IUsuarioRepository usuarioRepository)
        {
            _anuncioRepository = anuncioRepository;
            _usuarioRepository = usuarioRepository;
        }


        /// <summary>
        /// Busca um anuncio pelo Id
        /// </summary>
        /// <param name="id">Identificador do anuncio</param>
        /// <returns>Anuncio</returns>        
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var anuncio = await _anuncioRepository.ObterPorIdentificador(id);
            return Ok(JsonConvert.SerializeObject(anuncio));
        }

        /// <summary>
        /// Listar anuncios 
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        [HttpGet]
        public async Task<ActionResult> ListarAnunciosAsync()
        {
            var anuncios = await _anuncioRepository.ListarAnuncios();
            anuncios = anuncios.Where(anun => anun.Aprovado == StatusAprovacaoAnuncio.Aprovado);
            return Ok(JsonConvert.SerializeObject(anuncios));
        }

        /// <summary>
        /// Listar anuncios pendentes moderação 
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        [HttpGet("listar-pendente-moderar/")]
        public async Task<ActionResult> ListarAnunciosPendenteModerarAsync()
        {
            var anuncios = await _anuncioRepository.ListarAnuncios();
            anuncios = anuncios.Where(anun => anun.Aprovado == StatusAprovacaoAnuncio.Pendente);
            return Ok(JsonConvert.SerializeObject(anuncios));
        }

        /// <summary>
        /// Listar anuncios por usuário
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        [HttpGet("listar-por-usuario/{id}")]
        public async Task<ActionResult> ListarAnunciosPorUsuarioAsync(int id)
        {
            var anuncios = (await _anuncioRepository.ListarAnuncios()).Where(anun => anun.IdentificadorUsuario == id);
            return Ok(JsonConvert.SerializeObject(anuncios));
        }

        /// <summary>
        ///  Inserir avaliação em Anuncio
        /// </summary>
        /// <param name="avaliacao"></param>
        /// <returns></returns>
        [HttpPost("inserir-avaliacao/")]
        public async Task<ActionResult> InserirAvaliacaoAsync(Avaliacao avaliacao)
        {
            var avaliacaoInserida = await _anuncioRepository.InserirAvaliacao(avaliacao);
            
            var servicoEmail = new EmailRepository();
            var usuario = _usuarioRepository.ObterPorIdentificador(avaliacao.Identificador);
            _ = servicoEmail.EnviarEmailNovaAvaliacao(avaliacao, usuario);

            return Ok(avaliacaoInserida);
        }

        /// <summary>
        /// Apaga uma avaliacao de acordo com o identificador
        /// </summary>
        /// <param name="id">Identificador do anuncio a ser apagado</param>
        /// <returns>Bool que informa se o anuncio foi apagado</returns>
        [HttpDelete("apagar-avaliacao/{id}")]
        public async Task<ActionResult> DeletarAvaliacaoAsync(int id)
        {
            var apagado = await _anuncioRepository.DeletarAvaliacao(id);
            return Ok(JsonConvert.SerializeObject(apagado));
        }

        /// <summary>
        /// Criar anuncio 
        /// </summary>
        /// <param name="anuncio">Usuário a ser criado</param>
        /// <returns>Usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult> CriarAsync(Anuncio anuncio)
        {
            var anuncioCriado = await _anuncioRepository.InserirAnuncio(anuncio);

            var servicoEmail = new EmailRepository();
            var usuario =  _usuarioRepository.ObterPorIdentificador(anuncio.Identificador);
            _ = servicoEmail.EnviarEmailNovoAnuncio(anuncio, usuario);

            return Ok(JsonConvert.SerializeObject(anuncio));
        }

        /// <summary>
        /// Atualizar informações do anuncio
        /// </summary>
        /// <param name="anuncio"> Anuncio com dados atualizados</param>
        /// <returns>anuncio atualizado</returns>
        [HttpPatch]
        public async Task<ActionResult> AtualizarAsync(Anuncio anuncio)
        {
            var anuncioAtualizado = await _anuncioRepository.AtualizarAnuncio(anuncio);
            return Ok(JsonConvert.SerializeObject(anuncioAtualizado));
        }


        /// <summary>
        ///Aprovar Anuncio
        /// </summary>
        /// <param name="id"> Identificador do anuncio a ser aprovado</param>
        /// <returns>anuncio aprovado</returns>
        [HttpPatch("aprovar-anuncio/{id}")]
        public async Task<ActionResult> AprovarAsync(int id)
        {
            await _anuncioRepository.AprovarAnuncio(id);
            return Ok(true);
        }  
        
        /// <summary>
        ///Adiciona visita em um anuncio
        /// </summary>
        /// <param name="id"> Identificador do anuncio a ser aprovado</param>
        /// <returns>anuncio aprovado</returns>
        [HttpPatch("adicionar-visita-anuncio/{id}")]
        public async Task<ActionResult> AdicionarVisitaAsync(int id)
        {
            await _anuncioRepository.AdicionarVisita(id);
            return Ok(true);
        }

        /// <summary>
        ///Reprovar Anuncio
        /// </summary>
        /// <param name="id"> Identificador do anuncio a ser aprovado</param>
        /// <returns>anuncio aprovado</returns>
        [HttpPatch("reprovar-anuncio/{id}")]
        public async Task<ActionResult> ReprovarAsync(int id)
        {
            await _anuncioRepository.ReprovarAnuncio(id);
            return Ok(true);
        }

        /// <summary>
        /// Apaga um anuncio de acordo com o identificador
        /// </summary>
        /// <param name="id">Identificador do anuncio a ser apagado</param>
        /// <returns>Bool que informa se o anuncio foi apagado</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAsync(int id)
        {
            var apagado = await _anuncioRepository.DeletarAnuncio(id);
            return Ok(JsonConvert.SerializeObject(apagado));
        }
    }
}
