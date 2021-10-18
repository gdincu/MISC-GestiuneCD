﻿// <auto-generated />
using GestiuneCD.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestiuneCD.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211018120030_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.2.21480.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GestiuneCD.Domain.CD", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("dimensiuneMB")
                        .HasColumnType("int");

                    b.Property<int>("nrDeSesiuni")
                        .HasColumnType("int");

                    b.Property<string>("nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("spatiuOcupat")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("tip")
                        .HasColumnType("int");

                    b.Property<string>("tipSesiune")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("vitezaDeInscriptionare")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("CDs");
                });
#pragma warning restore 612, 618
        }
    }
}
