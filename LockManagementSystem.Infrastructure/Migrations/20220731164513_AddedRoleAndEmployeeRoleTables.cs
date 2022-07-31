using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManagementSystem.Infrastructure.Migrations
{
    public partial class AddedRoleAndEmployeeRoleTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventLog",
                table: "EventLog");

            migrationBuilder.RenameTable(
                name: "EventLog",
                newName: "EventLogs");

            migrationBuilder.RenameIndex(
                name: "IX_EventLog_CreatedAt",
                table: "EventLogs",
                newName: "IX_EventLogs_CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventLogs",
                table: "EventLogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Address", "Country", "CreatedAt", "CreatedBy", "DateOfBirth", "DeprecatedAt", "Email", "EmployeeId", "EmploymentDate", "FirstName", "Gender", "IsDeprecated", "LastName", "MiddleName", "Nationality", "OfficeId", "PhoneNumber", "State" },
                values: new object[] { new Guid("e41b9758-1a38-4c3b-8d7e-74e640f30edd"), "No 12, Palace road", "Nigeria", new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3200), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "default.user@clay.com", null, new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3210), "default", "Male", false, "user", null, "Nigerian", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), "23411111111", "Lagos" });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "Country", "CreatedAt", "CreatedBy", "DeprecatedAt", "Description", "IsDeprecated", "Name", "NumberOfDoors", "NumberOfLocks", "State", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), "No 20, Johnson avenue", "Nigeria", new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3090), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Locks and Security", false, "Clay Locks", 24, 12, null, new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3100), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateInstalled", "DeprecatedAt", "IsDeprecated", "Location", "Model", "OfficeId", "SerialNo", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1af0235f-ad8c-4d3b-beef-3540993722af"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3270), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3270), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Main Entrance, Ground floor", "Clay Lock 2.0", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), "123454fd", new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3270), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("fee7d619-35d8-4c0d-8840-03b9afd650cf"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3270), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3280), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Storage Entrance, Ground floor", "Clay Lock 2.0", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), "40478872", new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3270), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeprecatedAt", "Description", "IsDeprecated", "Name", "OfficeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("356dea14-79d9-427a-9cd2-779a266bf764"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director role", false, "Director", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b17e919b-a4f9-4d08-b909-3f675edbf31d"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee role", false, "Employee", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d5de738a-e297-424d-a84a-6eb09766a3ea"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator role", false, "Administrator", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d7161619-d3b9-436f-953a-26fe25e7f697"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director role", false, "OfficeManager", new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"), new DateTime(2022, 7, 31, 16, 45, 13, 324, DateTimeKind.Utc).AddTicks(3300), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_EmployeeId_RoleId",
                table: "EmployeeRoles",
                columns: new[] { "EmployeeId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_OfficeId",
                table: "Roles",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventLogs",
                table: "EventLogs");

            migrationBuilder.DeleteData(
                table: "EmployeeDetails",
                keyColumn: "Id",
                keyValue: new Guid("e41b9758-1a38-4c3b-8d7e-74e640f30edd"));

            migrationBuilder.DeleteData(
                table: "Locks",
                keyColumn: "Id",
                keyValue: new Guid("1af0235f-ad8c-4d3b-beef-3540993722af"));

            migrationBuilder.DeleteData(
                table: "Locks",
                keyColumn: "Id",
                keyValue: new Guid("fee7d619-35d8-4c0d-8840-03b9afd650cf"));

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: new Guid("0cad73e3-b734-4234-80ed-c95b95389e47"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EventLogs",
                newName: "EventLog");

            migrationBuilder.RenameIndex(
                name: "IX_EventLogs_CreatedAt",
                table: "EventLog",
                newName: "IX_EventLog_CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventLog",
                table: "EventLog",
                column: "Id");
        }
    }
}
