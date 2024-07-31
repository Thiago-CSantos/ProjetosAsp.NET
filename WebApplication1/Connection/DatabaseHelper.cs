using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared;
using System.Data.Common;

namespace WebApplication1.Connection
{
    public class DatabaseHelper
    {
        /// <summary>
        /// Cria uma conexão com o provedor de banco de dados atual
        /// </summary>
        /// <returns></returns>
        public static DataBaseHelperAbs GetProvider()
        {
            // Obtem o arquivo de configuração do projeto que está
            // utilizando este projeto como biblioteca
            IConfigurationRoot conf = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = conf.GetConnectionString("DefaultConnection") ?? "";
            string providerStr = conf.GetConnectionString("DB_PROVIDER") ?? "";
            DB_PROVIDER provider = Enum.Parse<DB_PROVIDER>(providerStr);

            DbProviderFactory providerFactory = GetProviderFactory(connectionString, provider, out DbConnection connection);

            DataBaseHelperAbs connectionAbs = new DataBaseHelperAbs(connection, providerFactory);
            return connectionAbs;
        }


        internal static DbProviderFactory GetProviderFactory(string connectionString, DB_PROVIDER provider, out DbConnection connection)
        {
            switch (provider)
            {
                case DB_PROVIDER.SqlServer:
                    connection = new SqlConnection(connectionString);
                    DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                    return DbProviderFactories.GetFactory("System.Data.SqlClient");
                default:
                    throw new ArgumentException("Provedor de banco de dados inválido.");
            }

        }

    }
    /// <summary>
    /// Tipos de banco de dados que esta aplicação suporta
    /// </summary>
    internal enum DB_PROVIDER
    {
        SqlServer = 1,
    }
}
