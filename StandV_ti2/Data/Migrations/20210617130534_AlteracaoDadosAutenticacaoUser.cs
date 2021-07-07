using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StandV_ti2.Data.Migrations
{
    public partial class AlteracaoDadosAutenticacaoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionariosReparacoes_Funcionarios_FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                table: "FuncionariosReparacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionariosReparacoes_Reparacoes_ListaReparacoesIdReparacao",
                table: "FuncionariosReparacoes");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegisto",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionariosReparacoes_Funcionarios_FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                table: "FuncionariosReparacoes",
                column: "FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                principalTable: "Funcionarios",
                principalColumn: "IdFuncionario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionariosReparacoes_Reparacoes_ListaReparacoesIdReparacao",
                table: "FuncionariosReparacoes",
                column: "ListaReparacoesIdReparacao",
                principalTable: "Reparacoes",
                principalColumn: "IdReparacao",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionariosReparacoes_Funcionarios_FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                table: "FuncionariosReparacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionariosReparacoes_Reparacoes_ListaReparacoesIdReparacao",
                table: "FuncionariosReparacoes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataRegisto",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionariosReparacoes_Funcionarios_FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                table: "FuncionariosReparacoes",
                column: "FuncionariosEnvolvidosNaReparacaoIdFuncionario",
                principalTable: "Funcionarios",
                principalColumn: "IdFuncionario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionariosReparacoes_Reparacoes_ListaReparacoesIdReparacao",
                table: "FuncionariosReparacoes",
                column: "ListaReparacoesIdReparacao",
                principalTable: "Reparacoes",
                principalColumn: "IdReparacao",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
