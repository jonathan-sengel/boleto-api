using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoletoAPI.Migrations
{
    /// <inheritdoc />
    public partial class DecimalPrecisionAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Boletos",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)",
                oldPrecision: 13,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentualJuros",
                table: "Bancos",
                type: "numeric(6,4)",
                precision: 6,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,4)",
                oldPrecision: 3,
                oldScale: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Boletos",
                type: "numeric(13,2)",
                precision: 13,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentualJuros",
                table: "Bancos",
                type: "numeric(3,4)",
                precision: 3,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,4)",
                oldPrecision: 6,
                oldScale: 4);
        }
    }
}
