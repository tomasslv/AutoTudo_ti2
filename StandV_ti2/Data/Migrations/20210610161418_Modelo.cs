using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StandV_ti2.Data.Migrations
{
    public partial class Modelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodPostal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NIF = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    DataNasc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    IdFuncionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodPostal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "Gestores",
                columns: table => new
                {
                    IdGestor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodPostal = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestores", x => x.IdGestor);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    IdVeiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoVeiculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Combustivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Potencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cilindrada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Km = table.Column<int>(type: "int", nullable: false),
                    TipoConducao = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.IdVeiculo);
                    table.ForeignKey(
                        name: "FK_Veiculos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reparacoes",
                columns: table => new
                {
                    IdReparacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVeiculo = table.Column<int>(type: "int", nullable: false),
                    IdGestor = table.Column<int>(type: "int", nullable: false),
                    TipoAvaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRepar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparacoes", x => x.IdReparacao);
                    table.ForeignKey(
                        name: "FK_Reparacoes_Gestores_IdGestor",
                        column: x => x.IdGestor,
                        principalTable: "Gestores",
                        principalColumn: "IdGestor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reparacoes_Veiculos_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculos",
                        principalColumn: "IdVeiculo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuncionariosReparacoes",
                columns: table => new
                {
                    FuncionariosEnvolvidosNaReparacaoIdFuncionario = table.Column<int>(type: "int", nullable: false),
                    ListaReparacoesIdReparacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionariosReparacoes", x => new { x.FuncionariosEnvolvidosNaReparacaoIdFuncionario, x.ListaReparacoesIdReparacao });
                    table.ForeignKey(
                        name: "FK_FuncionariosReparacoes_Funcionarios_FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                        column: x => x.FuncionariosEnvolvidosNaReparacaoIdFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuncionariosReparacoes_Reparacoes_ListaReparacoesIdReparacao",
                        column: x => x.ListaReparacoesIdReparacao,
                        principalTable: "Reparacoes",
                        principalColumn: "IdReparacao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "IdFuncionario", "Cargo", "CodPostal", "Email", "Fotografia", "Morada", "Nome", "Telemovel" },
                values: new object[] { 1, "Mecânico", "2300-675", "joaopedro@gmail.com", "func02.jpg", "Rua Capelo e Ivens, Santarém", "João Pedro Silva", "915453329" });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "IdFuncionario", "Cargo", "CodPostal", "Email", "Fotografia", "Morada", "Nome", "Telemovel" },
                values: new object[] { 2, "Pintor", "2070-116", "andrecos@gmail.com", "func03.png", "Rua Serpa Pinto, Cartaxo", "André Costa", "917549939" });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "IdFuncionario", "Cargo", "CodPostal", "Email", "Fotografia", "Morada", "Nome", "Telemovel" },
                values: new object[] { 3, "Bate-chapas", "2543-322", "albertosantos@gmail.com", "func01.png", "Rua João César Monteiro, Lisboa", "Alberto Santos", "925142348" });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosReparacoes_ListaReparacoesIdReparacao",
                table: "FuncionariosReparacoes",
                column: "ListaReparacoesIdReparacao");

            migrationBuilder.CreateIndex(
                name: "IX_Reparacoes_IdGestor",
                table: "Reparacoes",
                column: "IdGestor");

            migrationBuilder.CreateIndex(
                name: "IX_Reparacoes_IdVeiculo",
                table: "Reparacoes",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_IdCliente",
                table: "Veiculos",
                column: "IdCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionariosReparacoes");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Reparacoes");

            migrationBuilder.DropTable(
                name: "Gestores");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
