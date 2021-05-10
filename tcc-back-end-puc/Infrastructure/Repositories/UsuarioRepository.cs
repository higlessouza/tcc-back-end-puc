using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tcc_back_end_puc.Domain.Entities.Usuarios;
using tcc_back_end_puc.Domain.Repositories;
using tcc_back_end_puc.Infrastructure.DTO;
using tcc_back_end_puc.Infrastructure.Mapper;
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
                [Nome]
                ,[Senha]        
                ,[Email]        
                ,[Tipo]         
                ,[Facebook]     
                ,[Linkedin]     
                ,[Twitter]      
                ,[CodigoPais]   
                ,[CodigoCidade] 
                ,[Telefone]     
                ,[Rua]          
                ,[Bairro]       
                ,[Cidade]       
                ,[Estado]       
                ,[Pais]         
                ,[Cep]          
                )
            Values (
               @nome,
               @senha,
               @email,
               @tipo,
               @facebook,   
               @linkedin,
               @twitter,
               @codigoPais,
               @codigoCidade,
               @telefone,
               @rua,
               @bairro,
               @cidade,
               @estado,
               @pais,
               @cep
            );
            SELECT SCOPE_IDENTITY();";

        private const string SQL_LISTAR_USUARIOS = @"
            SELECT
                [Identificador]
                ,[Nome]
                ,[Senha]
                ,[Email]
                ,[Tipo]
                ,[Facebook]
                ,[Linkedin]
                ,[Twitter]
                ,[CodigoPais]
                ,[CodigoCidade]
                ,[Telefone]
                ,[Rua]
                ,[Bairro]
                ,[Cidade]
                ,[Estado]
                ,[Pais]
                ,[Cep]
            FROM
                Usuarios
            ";

        private const string SQL_OBTER_USUARIO = @"
            SELECT
                [Identificador]
                ,[Nome]
                ,[Senha]
                ,[Email]
                ,[Tipo]
                ,[Facebook]
                ,[Linkedin]
                ,[Twitter]
                ,[CodigoPais]
                ,[CodigoCidade]
                ,[Telefone]
                ,[Rua]
                ,[Bairro]
                ,[Cidade]
                ,[Estado]
                ,[Pais]
                ,[Cep]
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
                    [Tipo] = @tipo,
                    [Facebook] = @facebook,   
                    [Linkedin] = @linkedin,
                    [Twitter] = @twitter,
                    [CodigoPais] = @codigoPais,
                    [CodigoCidade] = @codigoCidade,
                    [Telefone] = @telefone,
                    [Rua] = @rua,
                    [Bairro] = @bairro,
                    [Cidade] = @cidade,
                    [Estado] = @estado,
                    [Pais] = @pais,
                    [Cep] = @cep
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
                BINARY_CHECKSUM([Senha]) = BINARY_CHECKSUM(@senha)
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
            var usuarioDTO = await UnitOfWork.Connection.QuerySingleAsync<UsuarioDTO>(SQL_OBTER_USUARIO, parametros);
            return usuarioDTO.ToUsuario();
        }

        /// <summary>
        /// Insere um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> InserirUsuario(Usuario usuario)
        {
            var usuarioDTO = usuario.ToUsuarioDTO();
            var parametros = CreateParameters
                .Add("@nome", usuarioDTO.Nome, DbType.String)
                .Add("@senha", usuarioDTO.Senha, DbType.String)
                .Add("@email", usuarioDTO.Email, DbType.String)
                .Add("@tipo", usuarioDTO.Tipo, DbType.Int32)
                .Add("@facebook", usuarioDTO.Facebook, DbType.String)
                .Add("@linkedin", usuarioDTO.Linkedin, DbType.String)
                .Add("@twitter", usuarioDTO.Twitter, DbType.String)
                .Add("@codigoPais", usuarioDTO.CodigoPais, DbType.Int32)
                .Add("@codigoCidade", usuarioDTO.CodigoCidade, DbType.Int32)
                .Add("@telefone", usuarioDTO.Telefone, DbType.Int32)
                .Add("@rua", usuarioDTO.Rua, DbType.String)
                .Add("@bairro", usuarioDTO.Bairro, DbType.String)
                .Add("@cidade", usuarioDTO.Cidade, DbType.String)
                .Add("@estado", usuarioDTO.Estado, DbType.String)
                .Add("@pais", usuarioDTO.Pais, DbType.String)
                .Add("@cep", usuarioDTO.Cep, DbType.String)
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
            var usuarioDTO = usuario.ToUsuarioDTO();
            var parametros = CreateParameters
                .Add("@nome", usuarioDTO.Nome, DbType.String)
                .Add("@senha", usuarioDTO.Senha, DbType.String)
                .Add("@email", usuarioDTO.Email, DbType.String)
                .Add("@tipo", usuarioDTO.Tipo, DbType.Int32)
                .Add("@facebook", usuarioDTO.Facebook, DbType.String)
                .Add("@linkedin", usuarioDTO.Linkedin, DbType.String)
                .Add("@twitter", usuarioDTO.Twitter, DbType.String)
                .Add("@codigoPais", usuarioDTO.CodigoPais, DbType.Int32)
                .Add("@codigoCidade", usuarioDTO.CodigoCidade, DbType.Int32)
                .Add("@telefone", usuarioDTO.Telefone, DbType.Int32)
                .Add("@rua", usuarioDTO.Rua, DbType.String)
                .Add("@bairro", usuarioDTO.Bairro, DbType.String)
                .Add("@cidade", usuarioDTO.Cidade, DbType.String)
                .Add("@estado", usuarioDTO.Estado, DbType.String)
                .Add("@pais", usuarioDTO.Pais, DbType.String)
                .Add("@cep", usuarioDTO.Cep, DbType.String)
                .Add("@identificador", usuario.Identificador, DbType.Int32)
                .GetParameters();
            parametros.RemoveUnused = true;
            await UnitOfWork.Connection.ExecuteAsync(SQL_ATUALIZAR_USUARIO, parametros);
            return usuario;
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            var usuariosDB = await UnitOfWork.Connection.ExecuteReaderAsync(SQL_LISTAR_USUARIOS);
            var usuariosDTO = usuariosDB.Parse<UsuarioDTO>();
            return usuariosDTO.Select(usuarioDTO => usuarioDTO.ToUsuario());
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
            return identificador.Parse<int>().Count<int>() == 1;
        }
    }
}
