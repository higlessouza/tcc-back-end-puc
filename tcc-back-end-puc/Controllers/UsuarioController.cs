using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuarios;
using tcc_back_end_puc.Domain.Repositories;

namespace tcc_back_end_puc.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Obter usuário pelo ID
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Usuário</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var usuario = await _usuarioRepository.ObterPorIdentificador(id);
            return Ok(JsonConvert.SerializeObject(usuario));
        }

        /// <summary>
        /// Listar usuários 
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        public async Task<ActionResult> ListarUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.ListarUsuarios();
            return Ok(JsonConvert.SerializeObject(usuarios));
        }

        /// <summary>
        /// Realiza login de um usuário
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string email, string senha)
        {
            var usuarioLogado = await _usuarioRepository.RealizarLogin(email,senha);
            return Ok(JsonConvert.SerializeObject(usuarioLogado));
        }

        /// <summary>
        /// Criar usuário 
        /// </summary>
        /// <param name="usuario">Usuário a ser criado</param>
        /// <returns>Usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult> CriarAsync(Usuario usuario)
        {
            var usuarioCriado = await _usuarioRepository.InserirUsuario(usuario);
            return Ok(JsonConvert.SerializeObject(usuarioCriado));
        }

        /// <summary>
        /// Atualizar informações do usuário
        /// </summary>
        /// <param name="usuario"> Usuário com dados atualizados</param>
        /// <returns>usuário atualizado</returns>
        [HttpPatch]
        public async Task<ActionResult> AtualizarAsync(Usuario usuario)
        {
            var usuarioAtualizado = await _usuarioRepository.AtualizarUsuario(usuario);
            return Ok(JsonConvert.SerializeObject(usuarioAtualizado));
        }

        /// <summary>
        /// Apaga um usuário de acordo com o identificador
        /// </summary>
        /// <param name="identificador">Identificador do usuário a ser apagado</param>
        /// <returns>Bool que informa se o usuário foi apagado</returns>
        [HttpDelete]
        public async Task<ActionResult> DeletarAsync(int identificador)
        {
            var usuarioCriado = await _usuarioRepository.DeletarUsuario(identificador);
            return Ok(JsonConvert.SerializeObject(usuarioCriado));
        }
    }
}
