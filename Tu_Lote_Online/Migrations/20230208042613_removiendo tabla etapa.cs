using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuLote.Migrations
{
    public partial class removiendotablaetapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Lotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Lotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
