using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManagementSystem.Infrastructure.Migrations
{
    public partial class UpdateDeprecatedAtColumnToNullableType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Offices",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Offices",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Offices",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Locks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Locks",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Locks",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "EventLogs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "EmployeeRoles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "EmployeeDetails",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "Address", "Country", "CreatedAt", "CreatedBy", "DateOfBirth", "DeprecatedAt", "Email", "EmployeeId", "EmploymentDate", "FirstName", "Gender", "IsDeprecated", "LastName", "MiddleName", "Nationality", "OfficeId", "PhoneNumber", "State" },
                values: new object[] { new Guid("d89c582e-7744-4511-98fc-d7c1c4c9a0b1"), "No 12, Palace road", "Nigeria", new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3870), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "default.user@clay.com", null, new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3880), "default", "Male", false, "user", null, "Nigerian", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), "23411111111", "Lagos" });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "Country", "CreatedAt", "CreatedBy", "DeprecatedAt", "Description", "IsDeprecated", "Name", "NumberOfDoors", "NumberOfLocks", "State", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), "No 20, Johnson avenue", "Nigeria", new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3700), new Guid("00000000-0000-0000-0000-000000000000"), null, "Locks and Security", false, "Clay Locks", 24, 12, null, new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3700), null });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateInstalled", "DeprecatedAt", "IsDeprecated", "Location", "Model", "OfficeId", "SerialNo", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("ce1dc3c6-2ac1-4ddb-95d0-76cc90c0d126"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3910), null, false, "Storage Entrance, Ground floor", "Clay Lock 2.0", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), "40478872", new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900), null },
                    { new Guid("d3572785-6459-4388-8311-e2d034ecd2f3"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900), null, false, "Main Entrance, Ground floor", "Clay Lock 2.0", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), "123454fd", new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900), null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeprecatedAt", "Description", "IsDeprecated", "Name", "OfficeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00998faa-2c73-43f2-a8c4-f97516d696f4"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3940), new Guid("00000000-0000-0000-0000-000000000000"), null, "Director role", false, "OfficeManager", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3940), null },
                    { new Guid("4191f7bc-05ab-4963-8750-4d5107a57e10"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930), new Guid("00000000-0000-0000-0000-000000000000"), null, "Director role", false, "Director", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3940), null },
                    { new Guid("75bd15c8-01e5-4a2d-adb6-98c48be67f8a"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930), new Guid("00000000-0000-0000-0000-000000000000"), null, "Employee role", false, "Employee", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930), null },
                    { new Guid("aa697c3c-1276-4e5c-9fe4-94b673a20562"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930), new Guid("00000000-0000-0000-0000-000000000000"), null, "Administrator role", false, "Administrator", new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"), new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930), null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Offices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Offices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Offices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Locks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Locks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Locks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "EventLogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "EmployeeRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeprecatedAt",
                table: "EmployeeDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

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
        }
    }
}
