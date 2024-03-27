using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDev01.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoCadastroAtividadeUpdate02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descrição",
                table: "CadAtividade",
                newName: "Descricao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "CadAtividade",
                newName: "Descrição");
        }
    }
}
