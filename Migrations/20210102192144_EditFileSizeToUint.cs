using Microsoft.EntityFrameworkCore.Migrations;

namespace Worktop.Migrations
{
    public partial class EditFileSizeToUint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "Size",
                table: "Files",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Files",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint));
        }
    }
}
