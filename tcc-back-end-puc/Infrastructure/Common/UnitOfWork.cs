using System.Data;
using tcc_back_end_puc.Domain.Repositories;

namespace tcc_back_end_puc.Infrastructure.Common
{

    public sealed class UnitOfWork : IUnitOfWork
    {
        #region [prop]
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        #endregion [prop]
        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _transaction = null;
        }
        public IDbConnection Connection
        {
            get { return _connection; }
        }
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }
        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
