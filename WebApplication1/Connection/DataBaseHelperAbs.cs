using System.Data;
using System.Data.Common;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static NuGet.Protocol.Core.Types.Repository;

namespace WebApplication1.Connection
{
    /// <summary>
    /// Classe que executa comandos no banco de dados automatizando a gerência de alguns processos como abertura e fechamento da conexão
    /// </summary>
    public class DataBaseHelperAbs : IDisposable
    {

        public int commandTimeout;
        public DbConnection connection;
        private DbCommand command;
        private readonly DbProviderFactory providerFactory;

        public DataBaseHelperAbs(DbConnection connection, DbProviderFactory providerFactory)
        {
            this.connection = connection;
            this.providerFactory = providerFactory;
            this.command = connection.CreateCommand();
        }


        #region Connection
        /// <summary>
        /// Abre a conexão com o banco de dados
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Erro se o objeto de conexão for nulo</exception>
        public DbConnection Open()
        {
            if (connection == null)
                throw new NullReferenceException("Conexão não instanciada.");

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return connection;
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados e reseta o objeto de conexão<br>
        /// Se tiver alguma transaction aberta, será executado o commit nela
        /// </summary>
        public void Close()
        {
            if (command.Transaction != null)
            {
                command.Transaction?.Commit();
            }
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados e reseta o objeto de conexão depois do using
        /// </summary>
        public void Dispose() => Close();
        #endregion



        #region Command
        /// <summary>
        /// Cria um comando do banco de dados associado à conexão atual
        /// </summary>
        /// <param name="sql">String contendo o comando sql</param>
        /// <param name="commandType">Tipo de comando a ser executado</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Erro se sql for vazio ou nulo</exception>
        private void CreateCommand(String sql, CommandType commandType)
        {
            if (String.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }
            if (this.connection.State == ConnectionState.Closed)
            {
                Open();
            }
            this.command.CommandText = sql;
            this.command.CommandType = commandType;
            this.command.CommandTimeout = this.commandTimeout;
        }
        #endregion

        #region Parameters
        public void ClearParameters()
        {
            command.Parameters.Clear();
        }

        public DbParameter CreateParameter(string name)
        {
            DbParameter p = command.CreateParameter();
            p.ParameterName = name;
            return p;
        }

        public DbParameter CreateParameter()
        {
            return CreateParameter("");
        }

        public IEnumerable<int> AddParameter(DbParameterCollection parameters)
        {
            List<int> indexes = new List<int>();

            foreach (var p in parameters)
            {
                indexes.Add(AddParameter((DbParameter)p));
            }

            return indexes;
        }

        public int AddParameter(DbParameter parameter)
        {
            return command.Parameters.Add(parameter);
        }

        public int AddParameter(string name, object? value)
        {
            DbParameter p = CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            return AddParameter(p);
        }

        public int AddParameter(string name, DbType type, object? value)
        {
            DbParameter p = CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            p.DbType = type;
            return AddParameter(p);
        }

        public IEnumerable<DbParameter> ParameterCollection()
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (var p in command.Parameters)
            {
                parameters.Add((DbParameter)p);
            }

            return parameters;
        }
        #endregion

        #region Transaction

        /// <summary>
        /// Inicia uma transação dentro da conexão
        /// </summary>
        public void BeginTransaction()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.Transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// Aplica as alterações feitas por comandos executados dentro da conexão com uma transação aberta
        /// </summary>
        public void CommitTransaction()
        {
            command.Transaction?.Commit();
            connection.Close();
        }

        /// <summary>
        /// Desfaz alterções de todos os comandos executados dentro da conexão desde quando a transação foi aberta
        /// </summary>
        public void RollbackTransaction()
        {
            if (command.Transaction != null)
                command.Transaction.Rollback();

            connection.Close();
        }

        /// <summary>
        /// Se existe uma transação
        /// </summary>
        /// <returns></returns>
        public bool IsInTranssaction()
        {
            return command.Transaction != null;
        }

        #endregion

        #region Executes
        public object? ExecuteScalar(StringBuilder sql, ConnectionState connectionState = ConnectionState.Closed)
            => ExecuteScalar(sql.ToString(), connectionState);
        public object? ExecuteScalar(string sql, ConnectionState connectionState = ConnectionState.Closed)
        {
            CreateCommand(sql, CommandType.Text);

            try
            {
                return command.ExecuteScalar()!;
            }
            catch (Exception ex)
            {
                GerenciarExecao(ex);
                throw;
            }
            finally
            {
                ClearParameters();
                if (connectionState == ConnectionState.Closed)
                    connection.Close();
            }
        }


        public DbDataReader ExecuteReader(StringBuilder sql, ConnectionState connectionState = ConnectionState.Closed)
            => ExecuteReader(sql.ToString(), connectionState);
        public DbDataReader ExecuteReader(string sql, ConnectionState connectionState = ConnectionState.Closed)
        {
            CreateCommand(sql, CommandType.Text);
            DbDataReader r;

            try
            {
                r = command.ExecuteReader(connectionState == ConnectionState.Closed ? CommandBehavior.CloseConnection : CommandBehavior.Default);
                return r;
            }
            catch (Exception ex)
            {
                GerenciarExecao(ex);
                throw;
            }
            finally
            {
                ClearParameters();
                //if (connectionState == ConnectionState.Closed)
                //    Connection.Close();
            }
        }


        public int ExecuteNonQuery(StringBuilder sql, ConnectionState connectionState = ConnectionState.Closed)
            => ExecuteNonQuery(sql.ToString(), connectionState);
        public int ExecuteNonQuery(string sql, ConnectionState connectionState = ConnectionState.Closed)
        {
            CreateCommand(sql, CommandType.Text);

            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                GerenciarExecao(ex);
                throw;
            }
            finally
            {
                ClearParameters();
                if (connectionState == ConnectionState.Closed)
                    connection.Close();
            }
        }


        public object? ExecuteProcedure(StringBuilder sql, ConnectionState connectionState = ConnectionState.Closed)
            => ExecuteProcedure(sql.ToString(), connectionState);
        public object? ExecuteProcedure(string sql, ConnectionState connectionState = ConnectionState.Closed)
        {
            CreateCommand(sql, CommandType.StoredProcedure);
            object? r = null;
            try
            {
                r = command.ExecuteScalar();
                return r;
            }
            catch (Exception ex)
            {
                GerenciarExecao(ex);
                throw;
            }
            finally
            {
                ClearParameters();
                if (connectionState == ConnectionState.Closed)
                    connection.Close();
            }
        }


        public DataSet ExecuteDataSet(StringBuilder sql, ConnectionState connectionState = ConnectionState.Closed)
            => ExecuteDataSet(sql.ToString(), connectionState);
        public DataSet ExecuteDataSet(string sql, ConnectionState connectionState = ConnectionState.Closed)
        {
            CreateCommand(sql, CommandType.Text);
            DataSet r = new DataSet();

            try
            {
                if (providerFactory.CanCreateDataAdapter)
                {
                    DbDataAdapter? dbDataAdapter = providerFactory.CreateDataAdapter();
                    if (dbDataAdapter != null)
                    {
                        dbDataAdapter.SelectCommand = command;
                        dbDataAdapter.Fill(r);
                    }
                }
                return r;
            }
            catch (Exception ex)
            {
                GerenciarExecao(ex);
                return r;
            }
            finally
            {
                ClearParameters();
                if (connectionState == ConnectionState.Closed)
                    connection.Close();
            }
        }

        #endregion
        private void GerenciarExecao(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
