using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Web.Migrations
{
    /// <inheritdoc />
    public partial class usersRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e74caddb-36a7-4443-90ef-7b559be17134", "379224d0-1206-4e50-9c53-71708a99927d", "Admin", "admin" },
                    { "f41c7f0c-3ace-43bc-ac03-532f1ad420b1", "4812643f-8094-4dea-90ab-e7838125e116", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e74caddb-36a7-4443-90ef-7b559be17134");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f41c7f0c-3ace-43bc-ac03-532f1ad420b1");
        }
    }
}
