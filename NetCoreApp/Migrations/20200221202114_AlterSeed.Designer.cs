﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCoreApp.Models;

namespace NetCoreApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200221202114_AlterSeed")]
    partial class AlterSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreApp.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Department = 2,
                            Email = "ahmet@protonmail.com",
                            Name = "Ahmet Tachmuradov"
                        },
                        new
                        {
                            Id = 1,
                            Department = 2,
                            Email = "murat@protonmail.com",
                            Name = "Murat Tachmuradov"
                        },
                        new
                        {
                            Id = 3,
                            Department = 2,
                            Email = "leyli@protonmail.com",
                            Name = "Leyli Tachmuradova"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
