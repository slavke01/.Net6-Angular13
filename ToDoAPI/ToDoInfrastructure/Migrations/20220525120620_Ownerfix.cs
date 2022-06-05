using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoInfrastructure.Migrations
{
    public partial class Ownerfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "ToDoLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "ToDoLists");
        }
    }
}
