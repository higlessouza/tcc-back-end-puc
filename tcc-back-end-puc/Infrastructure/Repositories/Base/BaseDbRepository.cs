using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using tcc_back_end_puc.Domain.Repositories;


namespace tcc_back_end_puc.Infrastructure.Repositories.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseDbRepository { 
        protected readonly IUnitOfWork UnitOfWork;

        public static DynamicParameterBuilder CreateParameters => DynamicParameterBuilder.Create();

        public BaseDbRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public sealed class DynamicParameterBuilder
        {
            #region [props]
            private readonly DynamicParameters _parameters;
            #endregion [props]
            #region [ctor]
            private DynamicParameterBuilder()
            {
                _parameters = new DynamicParameters();
            }
            #endregion [ctor]
            public DynamicParameterBuilder Add(string parameterName, object value, DbType parameterType, int? tamanho = null)
            {
                _parameters.Add(parameterName, value, parameterType, null, tamanho);
                return this;
            }
            public DynamicParameters GetParameters()
            {
                return _parameters;
            }
            public static DynamicParameterBuilder Create()
            {
                var parameters = new DynamicParameterBuilder();
                return parameters;
            }
        }
    }
}
