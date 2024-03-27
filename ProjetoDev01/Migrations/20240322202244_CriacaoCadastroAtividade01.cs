using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoDev01.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoCadastroAtividade01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CadAtividade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodAtividade = table.Column<string>(type: "text", nullable: false),
                    Grupo = table.Column<string>(type: "text", nullable: false),
                    CodigoFederal = table.Column<string>(type: "text", nullable: false),
                    Descrição = table.Column<string>(type: "text", nullable: false),
                    AtivPrevistaExcecao = table.Column<string>(type: "text", nullable: false),
                    RetencaoObrigatoria = table.Column<string>(type: "text", nullable: false),
                    Aliquota = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadAtividade", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadAtividade");
        }
    }
}
