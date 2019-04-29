using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.Infrastructure;
using Dapper;
using Npgsql;

namespace ControlBot.DAL.Concrety
{
    public class Session : ISession
    {
        private IDbConnection _connection;

        private IDbTransaction _dbTransaction;

        //----------------------------------------------------------------//

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        //----------------------------------------------------------------//

        public IDbTransaction Transaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        //----------------------------------------------------------------//

        public Session(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _connection = new NpgsqlConnection(DALConfigurationFactory.MainDbConnectionString);
            _connection.Open();
            _dbTransaction = _connection.BeginTransaction();
        }

        //----------------------------------------------------------------//

        public void Dispose()
        {
            try
            {
                _dbTransaction?.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
            }
            finally
            {
                _connection?.Close();
                _connection?.Dispose();
            }
        }

        //----------------------------------------------------------------//

    }
}
