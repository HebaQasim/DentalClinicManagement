using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddChequeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Patients_PatientsId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_CustomerServices_CustomerServiceId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Patients_PatientId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Patient");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_CustomerServiceId",
                table: "Patient",
                newName: "IX_Patient_CustomerServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient",
                table: "Patient",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cheques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheques_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_PaymentId",
                table: "Cheques",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patient_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Patient_PatientsId",
                table: "DoctorPatient",
                column: "PatientsId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_CustomerServices_CustomerServiceId",
                table: "Patient",
                column: "CustomerServiceId",
                principalTable: "CustomerServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Patient_PatientId",
                table: "Payments",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patient_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Patient_PatientsId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_CustomerServices_CustomerServiceId",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Patient_PatientId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Cheques");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient",
                table: "Patient");

            migrationBuilder.RenameTable(
                name: "Patient",
                newName: "Patients");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_CustomerServiceId",
                table: "Patients",
                newName: "IX_Patients_CustomerServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Patients_PatientsId",
                table: "DoctorPatient",
                column: "PatientsId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_CustomerServices_CustomerServiceId",
                table: "Patients",
                column: "CustomerServiceId",
                principalTable: "CustomerServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Patients_PatientId",
                table: "Payments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
