using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Anuncios;
using tcc_back_end_puc.Domain.Repositories;

namespace tcc_back_end_puc.Controllers
{
    [Route("api/anuncio")]
    [ApiController]
    
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioController(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
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
        /// Criar anuncio 
        /// </summary>
        /// <param name="anuncio">Usuário a ser criado</param>
        /// <returns>Usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult> CriarAsync(Anuncio anuncio)
        {
            var anuncioCriado = await _anuncioRepository.InserirAnuncio(anuncio);
            return Ok(JsonConvert.SerializeObject(anuncioCriado));
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
        /// Apaga um anuncio de acordo com o identificador
        /// </summary>
        /// <param name="id">Identificador do anuncio a ser apagado</param>
        /// <returns>Bool que informa se o anuncio foi apagado</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAsync(int id)
        {
            var anuncioCriado = await _anuncioRepository.DeletarAnuncio(id);
            return Ok(JsonConvert.SerializeObject(anuncioCriado));
        }
    }
}
