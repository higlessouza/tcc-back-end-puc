using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuarios;

namespace tcc_back_end_puc.Domain.Repositories
{
    /// <summary>
    /// Interface para repositorio de usuarios
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Obtem usuario pelo identificador 
        /// </summary>
        /// <param name="identificador">identificador do usuario</param>
        /// <returns></returns>
        public Task<Usuario> ObterPorIdentificador(int identificador);

        /// <summary>
        /// Insere um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Task<Usuario> InserirUsuario(Usuario usuario);

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Task<Usuario> AtualizarUsuario(Usuario usuario);

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public Task<IEnumerable<Usuario>> ListarUsuarios();

        /// <summary>
        /// Deleta usuário 
        /// </summary>
        /// <param name="identificador">Identificador usuário</param>
        /// <returns></returns>
        public Task<bool> DeletarUsuario(int identificador);

        /// <summary>
        /// Realiza login de acordo com as informações enviadas 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public Task<Usuario> RealizarLogin(string email,string senha);

    }
}
