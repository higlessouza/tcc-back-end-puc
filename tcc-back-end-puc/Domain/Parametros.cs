using System;

namespace tcc_back_end_puc.Domain
{
    public class Parametros
    {
        /// <summary>
        /// Parâmetros do Sql Server
        /// </summary>
        public static class SqlServer
        {
            /// <summary>
            /// string de conexão do banco de dados
            /// </summary>
            public static string Banco => Environment.GetEnvironmentVariable("DB_TCC");
            /// <summary>
            /// Usuário do banco de dados
            /// </summary>
            public static string Usuario => Environment.GetEnvironmentVariable("DB_TCC_USER");
            /// <summary>
            /// Senha do banco de dados
            /// </summary>
            public static string Senha => Environment.GetEnvironmentVariable("DB_TCC_PASSWORD");
        }
    }
}
