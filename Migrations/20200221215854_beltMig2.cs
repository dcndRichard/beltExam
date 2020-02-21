using Microsoft.EntityFrameworkCore.Migrations;

namespace beltExam.Migrations
{
    public partial class beltMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "FunThings",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "FunThings",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
