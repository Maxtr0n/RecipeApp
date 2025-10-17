using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Recipes",
                newName: "UpdatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Recipes",
                newName: "CreatedAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAtUtc",
                table: "Recipes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Recipes",
                newName: "CreatedAt");
        }
    }
}
