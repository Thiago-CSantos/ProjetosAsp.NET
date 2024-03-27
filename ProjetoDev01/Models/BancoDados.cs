using Microsoft.EntityFrameworkCore;
using ProjetoDev01.Models;

namespace ProjetoDev1.Models
{
    public class BancoDados : DbContext
    {

        public BancoDados(DbContextOptions<BancoDados> options) : base(options) { }

        // Essa parte faz ele entender qual tabela quero 
        // que gere na minha Migration quando
        // comando: Add-Migration Criação_Inicial -Context BancoDados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CadastroAtividade> CadastroAtividades { get; set; }

    }
}
