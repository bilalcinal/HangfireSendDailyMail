using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_emailDatas",
                table: "emailDatas");

            migrationBuilder.RenameTable(
                name: "emailDatas",
                newName: "EmailDatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailDatas",
                table: "EmailDatas",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailDatas",
                table: "EmailDatas");

            migrationBuilder.RenameTable(
                name: "EmailDatas",
                newName: "emailDatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_emailDatas",
                table: "emailDatas",
                column: "Id");
        }
    }
}
