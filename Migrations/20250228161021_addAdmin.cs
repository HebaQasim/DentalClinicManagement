using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId" },
                values: new object[] { new Guid("ce93f771-e6b7-46b8-af59-66251bc1998f"), "hebaalqerem2003@gmial.com", "Admin", "Admin", "$2a$12$o5SJjcCzUWnZvg5aFo5AA.nQte7aQzhl.mhdjUrZTWuQ1eFa1Tu/O", "1234567890", new Guid("de4f6736-fa9a-48b3-b788-fc5506bedf08") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("ce93f771-e6b7-46b8-af59-66251bc1998f"));
        }
    }
}
