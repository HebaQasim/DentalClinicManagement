using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class addRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // حذف جدول Users بالكامل لأننا بحاجة لتغيير نوع الـ Id
            migrationBuilder.DropTable(name: "Users");

            // إعادة إنشاء جدول Users مع التعديلات المطلوبة
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),  // تغيير Id إلى Guid
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),  // عمود Password بدلًا من Name
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),  // إضافة FirstName
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),  // إضافة LastName
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)  // جعل RoleId nullable مؤقتًا
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);  // تعيين Id كمفتاح أساسي
                });

            // إنشاء جدول Roles
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            // إضافة علاقة بين جدول Users وجدول Roles
            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            // إنشاء المفتاح الأجنبي بين Users و Roles
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // حذف المفتاح الأجنبي بين Users و Roles
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            // حذف جدول Users
            migrationBuilder.DropTable(name: "Users");

            // حذف جدول Roles
            migrationBuilder.DropTable(name: "Roles");

            // إعادة إنشاء جدول Users بالخصائص القديمة
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false) // إعادة النوع إلى int
                        .Annotation("SqlServer:Identity", "1, 1"), // إضافة خاصية Identity من جديد
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false) // إعادة عمود Name
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id); // تعيين Id كمفتاح أساسي
                });
        }

    }
}
