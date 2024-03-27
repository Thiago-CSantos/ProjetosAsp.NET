using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoDev01.Migrations
{
    /// <inheritdoc />
    public partial class RecreateCadAtividadeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadAtividade");

            migrationBuilder.CreateTable(
                name: "CadastroAtividade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodAtividade = table.Column<string>(type: "text", nullable: false),
                    Grupo = table.Column<string>(type: "text", nullable: false),
                    CodigoFederal = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    AtivPrevistaExcecao = table.Column<string>(type: "text", nullable: false),
                    RetencaoObrigatoria = table.Column<string>(type: "text", nullable: false),
                    Aliquota = table.Column<float>(type: "real", nullable: false),
                    Recolhimento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroAtividade", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadastroAtividade");

            migrationBuilder.CreateTable(
                name: "CadAtividade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Aliquota = table.Column<float>(type: "real", nullable: false),
                    AtivPrevistaExcecao = table.Column<string>(type: "text", nullable: false),
                    CodAtividade = table.Column<string>(type: "text", nullable: false),
                    CodigoFederal = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Grupo = table.Column<string>(type: "text", nullable: false),
                    Recolhimento = table.Column<string>(type: "text", nullable: true),
                    RetencaoObrigatoria = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadAtividade", x => x.Id);
                });
        }
    }
}
