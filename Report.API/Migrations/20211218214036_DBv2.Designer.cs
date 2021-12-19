﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Report.API.Entities.Context;

#nullable disable

namespace Report.API.Migrations
{
    [DbContext(typeof(ReportContext))]
    [Migration("20211218214036_DBv2")]
    partial class DBv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Report.API.Entities.Report", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ReportStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UUID");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("Report.API.Entities.ReportDetail", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int>("PersonCount")
                        .HasColumnType("integer");

                    b.Property<int>("PhoneNumberCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("ReportId");

                    b.ToTable("report_details");
                });

            modelBuilder.Entity("Report.API.Entities.ReportDetail", b =>
                {
                    b.HasOne("Report.API.Entities.Report", null)
                        .WithMany("ReportDetails")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Report.API.Entities.Report", b =>
                {
                    b.Navigation("ReportDetails");
                });
#pragma warning restore 612, 618
        }
    }
}