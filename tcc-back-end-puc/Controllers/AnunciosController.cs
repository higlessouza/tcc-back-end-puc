using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;
using tcc_back_end_puc.Domain.Repositories;

namespace tcc_back_end_puc.Controllers
{
    [Route("anuncios")]
    [ApiController]
    public class AnunciosController : ControllerBase
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnunciosController(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        /// <summary>
        /// Obter anuncio pelo ID
        /// </summary>
        /// <param name="id">Identificador do anuncio</param>
        /// <returns>Usuário</returns>
        [HttpGet]
        public async Task<ActionResult> GetAsync(int identificador)
        {
            var anuncio = await _anuncioRepository.ObterPorIdentificador(identificador);
            return Ok(JsonConvert.SerializeObject(anuncio));
        }

        /// <summary>
        /// Obter lista de anuncios 
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        [HttpGet]
        [Route("ListarAnuncios")]
        public async Task<ActionResult> ListarAnunciosAsync()
        {
            var anuncios = await _anuncioRepository.ListarAnuncios();
            return Ok(JsonConvert.SerializeObject(anuncios));
        }

        /// <summary>
        /// Cria anuncio 
        /// </summary>
        /// <param name="anuncio">Usuário a ser criado</param>
        /// <returns>Usuário criado</returns>
        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult> CriarAsync(Anuncio anuncio)
        {
            var anuncioCriado = await _anuncioRepository.InserirAnuncio(anuncio);
            return Ok(JsonConvert.SerializeObject(anuncioCriado));
        }

        /// <summary>
        /// Atualiza informações dos anuncios
        /// </summary>
        /// <param name="anuncio"> Usuário com dados atualizados</param>
        /// <returns>anuncio atualizado</returns>
        [HttpPatch]
        [Route("atualizar")]
        public async Task<ActionResult> AtualizarAsync(Anuncio anuncio)
        {
            var anuncioAtualizado = await _anuncioRepository.AtualizarAnuncio(anuncio);
            return Ok(JsonConvert.SerializeObject(anuncioAtualizado));
        }

        /// <summary>
        /// Apaga um anuncio de acordo com o identificador
        /// </summary>
        /// <param name="identificador">Identificador do anuncio a ser apagado</param>
        /// <returns>Bool que informa se o anuncio foi apagado</returns>
        [HttpDelete]
        [Route("deletar")]
        public async Task<ActionResult> DeletarAsync(int identificador)
        {
            var anuncioCriado = await _anuncioRepository.DeletarAnuncio(identificador);
            return Ok(JsonConvert.SerializeObject(anuncioCriado));
        }
    }
}
