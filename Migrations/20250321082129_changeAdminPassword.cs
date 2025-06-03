using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class changeAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("ce93f771-e6b7-46b8-af59-66251bc1998f"),
                column: "Password",
                value: "$2a$12$rnWo0frYDFCHMYdhEfzUf.NaNLMnAp3om7XE8DWLkbJqG.ATIzg8i");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("ce93f771-e6b7-46b8-af59-66251bc1998f"),
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBUw0I4lTSGxDbeOl5dKGrDzf6rdHkGf4deUAiwvwOXIlSrQHyidITXkAH92YI2qIg==");
        }
    }
}
