using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoInfrastructure.Migrations
{
    public partial class Fluent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoListItems_ToDoLists_ToDoListID",
                table: "ToDoListItems");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ToDoLists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ToDoListID",
                table: "ToDoListItems",
                newName: "ToDoListId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ToDoListItems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoListItems_ToDoListID",
                table: "ToDoListItems",
                newName: "IX_ToDoListItems_ToDoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoListItems_ToDoLists_ToDoListId",
                table: "ToDoListItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoListItems_ToDoLists_ToDoListId",
                table: "ToDoListItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDoLists",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ToDoListId",
                table: "ToDoListItems",
                newName: "ToDoListID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDoListItems",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoListItems_ToDoListId",
                table: "ToDoListItems",
                newName: "IX_ToDoListItems_ToDoListID");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoListItems_ToDoLists_ToDoListID",
                table: "ToDoListItems",
                column: "ToDoListID",
                principalTable: "ToDoLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
