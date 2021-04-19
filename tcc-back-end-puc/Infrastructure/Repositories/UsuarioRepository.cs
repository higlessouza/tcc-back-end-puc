using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuario;
using tcc_back_end_puc.Domain.Repositories;
using tcc_back_end_puc.Infrastructure.Repositories.Base;

namespace tcc_back_end_puc.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseDbRepository, IUsuarioRepository
    {
        public UsuarioRepository(IUnitOfWork unitOfWork)
        : base(unitOfWork) { }

        #region Consultas 
        private const string SQL_INSERIR_USUARIO = @"
            Insert into Usuarios (
                [Nome],
                [Senha],
                [Email],
                [Tipo]
                )
            Values (
                @nome,
                @senha,
                @email,
                @tipo
            );
            SELECT SCOPE_IDENTITY();";

        private const string SQL_LISTAR_USUARIOS = @"
            SELECT
                [Identificador],
                [Nome],
                [Senha],
                [Email],
                [Tipo]
            FROM
                Usuarios
            ";

        private const string SQL_OBTER_USUARIO = @"
            SELECT
                [Identificador],
                [Nome],
                [Senha],
                [Email],
                [Tipo]
            FROM
                Usuarios
            WHERE 
                [Identificador] = @identificador
            ";

        private const string SQL_ATUALIZAR_USUARIO = @"
            UPDATE Usuarios 
                SET [Nome] = @nome,
                    [Senha] = @senha,
                    [Email] = @email,
                    [Tipo] = @tipo
            WHERE 
                [Identificador] = @identificador
            ;";

        private const string SQL_DELETA_USUARIO = @"
            DELETE FROM 
                Usuarios 
            WHERE 
                [Identificador] = @identificador
            ;";

        private const string SQL_REALIZA_LOGIN = @"
            SELECT 
                [Identificador]
            FROM 
                Usuarios 
            WHERE 
                [Email] = @email and
                [Senha] = @senha
            ";
        #endregion

        /// <summary>
        /// Obtem usuario pelo identificador 
        /// </summary>
        /// <param name="identificador">identificador do usuario</param>
        /// <returns></returns>
        public async Task<Usuario> ObterPorIdentificador(int identificador)
        {
            var parametros = CreateParameters
                .Add("@identificador", identificador, DbType.Int32)
                .GetParameters();
            var usuario = await UnitOfWork.Connection.QuerySingleAsync<Usuario>(SQL_OBTER_USUARIO, parametros);
            return usuario;
        }

        /// <summary>
        /// Insere um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> InserirUsuario(Usuario usuario)
        {
            var parametros = CreateParameters
               .Add("@nome", usuario.Nome, DbType.String)
               .Add("@email", usuario.Email, DbType.String)
               .Add("@senha", usuario.Senha, DbType.String)
               .Add("@tipo", usuario.Tipo, DbType.Int32)
               .GetParameters();

            var identificador = await UnitOfWork.Connection.QuerySingleAsync<int>(SQL_INSERIR_USUARIO, parametros);
            usuario.Identificador = identificador;
            return usuario;
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            var parametros = CreateParameters
                .Add("@nome", usuario.Nome, DbType.String)
                 .Add("@senha", usuario.Senha, DbType.String)
                .Add("@email", usuario.Email, DbType.String)             
                .Add("@tipo", usuario.Tipo, DbType.Int32)
                .Add("@identificador", usuario.Identificador, DbType.Int32)
                .GetParameters();

            await UnitOfWork.Connection.ExecuteAsync(SQL_ATUALIZAR_USUARIO, parametros);
            return usuario;
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
           
            var usuarios = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_LISTAR_USUARIOS);
            return usuarios.Parse<Usuario>();
        }

        /// <summary>
        /// Deleta usuário 
        /// </summary>
        /// <param name="identificador">Identificador usuário</param>
        /// <returns></returns>
        public async Task<bool> DeletarUsuario(int identificador)
        {
            var parametros = CreateParameters
               .Add("@identificador", identificador, DbType.Int32)
               .GetParameters();
            var identificadorApagado = await UnitOfWork.Connection.ExecuteAsync(SQL_DELETA_USUARIO, parametros);

            return (identificadorApagado == 1);
        }

        /// <summary>
        /// Realiza login de acordo com as informações enviadas 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public async Task<bool> RealizarLogin(string email, string senha)
        {
            var parametros = CreateParameters 
              .Add("@email", email, DbType.String)
              .Add("@senha", senha, DbType.String)     
              .GetParameters();

            var identificador = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_REALIZA_LOGIN, parametros);
            //TODO: Melhorar metodo
            return identificador.Parse<int>().Count<int>()==1;
        }
    }
}
