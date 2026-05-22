using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SeederSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Email", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "Ianos Rares", "rares@gmail.com", "AQAAAAIAAYagAAAAEI79EbWOzQywErjFyKDhccMMR2nVFmg0swag51rfxTNI+ktXlcki1sgfi7Q6t986AQ==", 1 },
                    { 2, "Niculai Ilie", "ilie@gmail.com", "AQAAAAIAAYagAAAAEI79EbWOzQywErjFyKDhccMMR2nVFmg0swag51rfxTNI+ktXlcki1sgfi7Q6t986AQ==", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
