﻿// <auto-generated />
using System;
using LockManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LockManagementSystem.Infrastructure.Migrations
{
    [DbContext(typeof(LockManagementWriteContext))]
    [Migration("20220731180840_UpdateDeprecatedAtColumnToNullableType")]
    partial class UpdateDeprecatedAtColumnToNullableType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EmployeeDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EmploymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Nationality")
                        .HasColumnType("text");

                    b.Property<Guid>("OfficeId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("EmployeeDetails");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d89c582e-7744-4511-98fc-d7c1c4c9a0b1"),
                            Address = "No 12, Palace road",
                            Country = "Nigeria",
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3870),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "default.user@clay.com",
                            EmploymentDate = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3880),
                            FirstName = "default",
                            Gender = "Male",
                            IsDeprecated = false,
                            LastName = "user",
                            Nationality = "Nigerian",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            PhoneNumber = "23411111111",
                            State = "Lagos"
                        });
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EmployeeRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("EmployeeId", "RoleId");

                    b.ToTable("EmployeeRoles");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EventLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OfficeId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.ToTable("EventLogs");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.LockEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateInstalled")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<Guid>("OfficeId")
                        .HasColumnType("uuid");

                    b.Property<string>("SerialNo")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Locks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d3572785-6459-4388-8311-e2d034ecd2f3"),
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DateInstalled = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900),
                            IsDeprecated = false,
                            Location = "Main Entrance, Ground floor",
                            Model = "Clay Lock 2.0",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            SerialNo = "123454fd",
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900)
                        },
                        new
                        {
                            Id = new Guid("ce1dc3c6-2ac1-4ddb-95d0-76cc90c0d126"),
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DateInstalled = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3910),
                            IsDeprecated = false,
                            Location = "Storage Entrance, Ground floor",
                            Model = "Clay Lock 2.0",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            SerialNo = "40478872",
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3900)
                        });
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.OfficeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfLocks")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            Id = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            Address = "No 20, Johnson avenue",
                            Country = "Nigeria",
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3700),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Locks and Security",
                            IsDeprecated = false,
                            Name = "Clay Locks",
                            NumberOfDoors = 24,
                            NumberOfLocks = 12,
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3700)
                        });
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeprecatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OfficeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OfficeId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa697c3c-1276-4e5c-9fe4-94b673a20562"),
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Administrator role",
                            IsDeprecated = false,
                            Name = "Administrator",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930)
                        },
                        new
                        {
                            Id = new Guid("75bd15c8-01e5-4a2d-adb6-98c48be67f8a"),
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Employee role",
                            IsDeprecated = false,
                            Name = "Employee",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930)
                        },
                        new
                        {
                            Id = new Guid("4191f7bc-05ab-4963-8750-4d5107a57e10"),
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3930),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Director role",
                            IsDeprecated = false,
                            Name = "Director",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3940)
                        },
                        new
                        {
                            Id = new Guid("00998faa-2c73-43f2-a8c4-f97516d696f4"),
                            CreatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3940),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Director role",
                            IsDeprecated = false,
                            Name = "OfficeManager",
                            OfficeId = new Guid("05e27dcc-3917-49c0-8498-b957e82fbabc"),
                            UpdatedAt = new DateTime(2022, 7, 31, 18, 8, 39, 231, DateTimeKind.Utc).AddTicks(3940)
                        });
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EmployeeDetailEntity", b =>
                {
                    b.HasOne("LockManagementSystem.Domain.Entities.EmployeeEntity", "Employee")
                        .WithOne("Detail")
                        .HasForeignKey("LockManagementSystem.Domain.Entities.EmployeeDetailEntity", "EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EmployeeRoleEntity", b =>
                {
                    b.HasOne("LockManagementSystem.Domain.Entities.EmployeeEntity", "Employee")
                        .WithMany("EmployeeRoles")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LockManagementSystem.Domain.Entities.RoleEntity", "Role")
                        .WithMany("EmployeeRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.LockEntity", b =>
                {
                    b.HasOne("LockManagementSystem.Domain.Entities.OfficeEntity", "Office")
                        .WithMany("Locks")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.RoleEntity", b =>
                {
                    b.HasOne("LockManagementSystem.Domain.Entities.OfficeEntity", "Office")
                        .WithMany("Roles")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("Detail");

                    b.Navigation("EmployeeRoles");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.OfficeEntity", b =>
                {
                    b.Navigation("Locks");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("LockManagementSystem.Domain.Entities.RoleEntity", b =>
                {
                    b.Navigation("EmployeeRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
