using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Check1.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EName",
                table: "Employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DName",
                table: "Departments",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "EName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Departments",
                newName: "DName");
        }
    }
}
