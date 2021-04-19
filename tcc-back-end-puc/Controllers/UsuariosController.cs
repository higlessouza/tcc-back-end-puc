using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuario;
using tcc_back_end_puc.Domain.Repositories;

namespace tcc_back_end_puc.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Obter usuário pelo ID
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Usuário</returns>
        [HttpGet]
        public async Task<ActionResult> GetAsync(int identificador)
        {
            var usuario = await _usuarioRepository.ObterPorIdentificador(identificador);
            return Ok(JsonConvert.SerializeObject(usuario));
        }

        /// <summary>
        /// Obter lista de usuários 
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<ActionResult> ListarUsuáriosAsync()
        {
            var usuarios = await _usuarioRepository.ListarUsuarios();
            return Ok(JsonConvert.SerializeObject(usuarios));
        }

        /// <summary>
        /// Obter lista de usuários 
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string email, string senha)
        {
            var usuarios = await _usuarioRepository.RealizarLogin(email,senha);
            return Ok(JsonConvert.SerializeObject(usuarios));
        }

        /// <summary>
        /// Cria usuário 
        /// </summary>
        /// <param name="usuario">Usuário a ser criado</param>
        /// <returns>Usuário criado</returns>
        [HttpPost]
        [Route("criar")]
        public async Task<ActionResult> CriarAsync(Usuario usuario)
        {
            var usuarioCriado = await _usuarioRepository.InserirUsuario(usuario);
            return Ok(JsonConvert.SerializeObject(usuarioCriado));
        }

        /// <summary>
        /// Atualiza informações dos usuários
        /// </summary>
        /// <param name="usuario"> Usuário com dados atualizados</param>
        /// <returns>usuário atualizado</returns>
        [HttpPost]
        [Route("atualizar")]
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
        [HttpPost]
        [Route("deletar")]
        public async Task<ActionResult> DeletarAsync(int identificador)
        {
            var usuarioCriado = await _usuarioRepository.DeletarUsuario(identificador);
            return Ok(JsonConvert.SerializeObject(usuarioCriado));
        }
    }
}
