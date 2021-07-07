using Microsoft.EntityFrameworkCore.Migrations;

namespace StandV_ti2.Data.Migrations
{
    public partial class RemoveGestor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reparacoes_Gestores_IdGestor",
                table: "Reparacoes");

            migrationBuilder.DropIndex(
                name: "IX_Reparacoes_IdGestor",
                table: "Reparacoes");

            migrationBuilder.DropColumn(
                name: "IdGestor",
                table: "Reparacoes");

            migrationBuilder.DropTable(
                name: "Gestores");

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Clientes",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdGestor",
                table: "Reparacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Clientes",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.CreateTable(
                name: "Gestores",
                columns: table => new
                {
                    IdGestor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodPostal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestores", x => x.IdGestor);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reparacoes_IdGestor",
                table: "Reparacoes",
                column: "IdGestor");

            migrationBuilder.AddForeignKey(
                name: "FK_Reparacoes_Gestores_IdGestor",
                table: "Reparacoes",
                column: "IdGestor",
                principalTable: "Gestores",
                principalColumn: "IdGestor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
