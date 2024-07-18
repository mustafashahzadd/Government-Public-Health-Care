﻿// <auto-generated />
using Governement_Public_Health_Care.DB_Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Governement_Public_Health_Care.Migrations
{
    [DbContext(typeof(HealthCareContext))]
    [Migration("20231117103234_InitalCreate#")]
    partial class InitalCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Governement_Public_Health_Care.Models.DiseaseRegistry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Disease")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HospitalPatientID")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPeopleAffected")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DiseaseRegistries");
                });
#pragma warning restore 612, 618
        }
    }
}
