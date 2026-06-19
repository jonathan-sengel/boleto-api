using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoletoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeBanco = table.Column<string>(type: "text", nullable: false),
                    CodigoBanco = table.Column<string>(type: "text", nullable: false),
                    PercentualJuros = table.Column<decimal>(type: "numeric(3,4)", precision: 3, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomePagador = table.Column<string>(type: "text", nullable: false),
                    CpfCnpjPagador = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    NomeBeneficiario = table.Column<string>(type: "text", nullable: false),
                    CpfCnpjBeneficiario = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(13,2)", precision: 13, scale: 2, nullable: false),
                    DataVencimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    BancoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boletos_Bancos_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Bancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_BancoId",
                table: "Boletos",
                column: "BancoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletos");

            migrationBuilder.DropTable(
                name: "Bancos");
        }
    }
}
