using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManagementSystem.Infrastructure.Migrations
{
    public partial class AddMigrationAndSeedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LockId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    NumberOfDoors = table.Column<int>(type: "integer", nullable: false),
                    NumberOfLocks = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    OfficeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    EmploymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    SerialNo = table.Column<string>(type: "text", nullable: true),
                    DateInstalled = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locks_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "LockRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LockId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeprecated = table.Column<bool>(type: "boolean", nullable: false),
                    DeprecatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LockRoles_Locks_LockId",
                        column: x => x.LockId,
                        principalTable: "Locks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LockRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Address", "Country", "CreatedAt", "CreatedBy", "DateOfBirth", "DeprecatedAt", "Email", "EmployeeId", "EmploymentDate", "FirstName", "Gender", "IsDeprecated", "LastName", "MiddleName", "Nationality", "OfficeId", "PhoneNumber", "State" },
                values: new object[] { new Guid("7935586b-78b7-4f2d-97ba-c05e66f89aa3"), "No 12, Palace road", "Nigeria", new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(20), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "default.user@clay.com", null, new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(20), "default", "Male", false, "user", null, "Nigerian", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), "23411111111", "Lagos" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeprecatedAt", "Email", "IsDeprecated", "PasswordHash" },
                values: new object[] { new Guid("7935586b-78b7-4f2d-97ba-c05e66f89aa3"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(40), new Guid("00000000-0000-0000-0000-000000000000"), null, "default.user@clay.com", false, "#CLAY#2000$rFAUch4AxrxF+3gvCxY3IjdZHpwNUMNUh84rPQ39QOmNEPyl" });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "Country", "CreatedAt", "CreatedBy", "DeprecatedAt", "Description", "IsDeprecated", "Name", "NumberOfDoors", "NumberOfLocks", "State", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), "No 20, Johnson avenue", "Nigeria", new DateTime(2022, 8, 1, 20, 21, 16, 948, DateTimeKind.Utc).AddTicks(9810), new Guid("00000000-0000-0000-0000-000000000000"), null, "Locks and Security", false, "Clay Locks", 24, 12, null, new DateTime(2022, 8, 1, 20, 21, 16, 948, DateTimeKind.Utc).AddTicks(9810), null });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateInstalled", "DeprecatedAt", "IsDeprecated", "Location", "Model", "OfficeId", "SerialNo", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2de68316-75ac-4eed-b055-88dc4d1f2d2b"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(90), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(90), null, false, "Main Entrance, Ground floor", "Clay Lock 2.0", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), "123454fd", new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(90), null },
                    { new Guid("cf1c460d-f74d-48b5-b358-e5649145009e"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(90), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(100), null, false, "Storage Entrance, Ground floor", "Clay Lock 2.0", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), "40478872", new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(90), null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeprecatedAt", "Description", "IsDeprecated", "Name", "OfficeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("73877ecb-b7a5-4204-8dde-5ac7efa8a6b4"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(120), new Guid("00000000-0000-0000-0000-000000000000"), null, "Administrator role", false, "Administrator", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(120), null },
                    { new Guid("7897165a-9025-474a-be4d-1e5434e546cb"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(130), new Guid("00000000-0000-0000-0000-000000000000"), null, "Director role", false, "Director", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(130), null },
                    { new Guid("a6df5647-d432-474f-9f2c-e8a8a1c3c966"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(130), new Guid("00000000-0000-0000-0000-000000000000"), null, "Director role", false, "OfficeManager", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(130), null },
                    { new Guid("c842fdb8-3a25-4419-b704-66bc6f8524df"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(130), new Guid("00000000-0000-0000-0000-000000000000"), null, "Employee role", false, "Employee", new Guid("a76662b3-dfc5-43e9-a5af-74e9c0cb3a87"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(130), null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeprecatedAt", "EmployeeId", "IsDeprecated", "RoleId" },
                values: new object[,]
                {
                    { new Guid("24bf6c52-8f55-4118-90b4-85bee8fd831f"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(160), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("7935586b-78b7-4f2d-97ba-c05e66f89aa3"), false, new Guid("a6df5647-d432-474f-9f2c-e8a8a1c3c966") },
                    { new Guid("baea5a2d-ca2a-4118-ac40-f25cf7a7dda8"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(150), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("7935586b-78b7-4f2d-97ba-c05e66f89aa3"), false, new Guid("c842fdb8-3a25-4419-b704-66bc6f8524df") },
                    { new Guid("e7136117-2f93-4f86-b99a-7c300b340625"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(150), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("7935586b-78b7-4f2d-97ba-c05e66f89aa3"), false, new Guid("73877ecb-b7a5-4204-8dde-5ac7efa8a6b4") },
                    { new Guid("f3fd95b2-b3c9-4fee-89cb-49ded8e3a3f6"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(150), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("7935586b-78b7-4f2d-97ba-c05e66f89aa3"), false, new Guid("7897165a-9025-474a-be4d-1e5434e546cb") }
                });

            migrationBuilder.InsertData(
                table: "LockRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeprecatedAt", "IsDeprecated", "LockId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("29d85de4-311e-42e7-9fb4-bc10c0dfaa45"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(170), new Guid("00000000-0000-0000-0000-000000000000"), null, false, new Guid("2de68316-75ac-4eed-b055-88dc4d1f2d2b"), new Guid("c842fdb8-3a25-4419-b704-66bc6f8524df") },
                    { new Guid("6753a823-fb49-4e9c-9694-1037c60d0b5d"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(180), new Guid("00000000-0000-0000-0000-000000000000"), null, false, new Guid("cf1c460d-f74d-48b5-b358-e5649145009e"), new Guid("7897165a-9025-474a-be4d-1e5434e546cb") },
                    { new Guid("6da0949f-b038-4bd5-ae98-a9967c019a14"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(170), new Guid("00000000-0000-0000-0000-000000000000"), null, false, new Guid("cf1c460d-f74d-48b5-b358-e5649145009e"), new Guid("73877ecb-b7a5-4204-8dde-5ac7efa8a6b4") },
                    { new Guid("f1dc7c34-17d9-45ed-bbbb-05b4659d0a11"), new DateTime(2022, 8, 1, 20, 21, 16, 949, DateTimeKind.Utc).AddTicks(180), new Guid("00000000-0000-0000-0000-000000000000"), null, false, new Guid("cf1c460d-f74d-48b5-b358-e5649145009e"), new Guid("a6df5647-d432-474f-9f2c-e8a8a1c3c966") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_Email",
                table: "EmployeeDetails",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_EmployeeId_RoleId",
                table: "EmployeeRoles",
                columns: new[] { "EmployeeId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_CreatedAt",
                table: "EventLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_LockRoles_LockId",
                table: "LockRoles",
                column: "LockId");

            migrationBuilder.CreateIndex(
                name: "IX_LockRoles_RoleId",
                table: "LockRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Locks_OfficeId",
                table: "Locks",
                column: "OfficeId");

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
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "LockRoles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Locks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
