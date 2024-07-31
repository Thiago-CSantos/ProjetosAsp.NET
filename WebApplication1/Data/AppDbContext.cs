namespace WebApplication1.Data
{
    // Classe criada para representar nosso Contexto de dados do banco, feito ser usado Direto com SqlServer sem uso de Entity
    public class AppDbContext 
    {
        private readonly String _connectionString;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection"); // precisa ser o mesmo nome definido no appsettings.json
        }


    }
}
