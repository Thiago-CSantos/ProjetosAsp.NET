using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoDev01.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableCadAtividade01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recolhimento",
                table: "CadAtividade",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recolhimento",
                table: "CadAtividade");
        }
    }
}
