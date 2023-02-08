using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuLote.Migrations
{
    public partial class SeagregacolumnTipoaLote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos");

            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "Lotes");

            migrationBuilder.AlterColumn<string>(
                name: "Orientacion",
                table: "Lotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Lotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Lotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Lotes");

            migrationBuilder.AlterColumn<int>(
                name: "Orientacion",
                table: "Lotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Etapa",
                table: "Lotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lote_Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tipos_Lotes_Lote_Id",
                        column: x => x.Lote_Id,
                        principalTable: "Lotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Lote_Id",
                table: "Tipos",
                column: "Lote_Id");
        }
    }
}
