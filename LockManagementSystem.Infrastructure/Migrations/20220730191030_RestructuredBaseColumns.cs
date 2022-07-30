using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManagementSystem.Infrastructure.Migrations
{
    public partial class RestructuredBaseColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Employees",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Employees",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "EmployeeDetails",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "EmployeeDetails",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Employees",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Employees",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "EmployeeDetails",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EmployeeDetails",
                newName: "UpdatedAt");
        }
    }
}
