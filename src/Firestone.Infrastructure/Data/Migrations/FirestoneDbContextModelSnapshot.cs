﻿// <auto-generated />
using System;
using Firestone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Firestone.Infrastructure.Data.Migrations
{
    [DbContext(typeof(FirestoneDbContext))]
    partial class FirestoneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Firestone.Domain.Models.AssetHolder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ExpectedMonthlyIncome")
                        .HasColumnType("float");

                    b.Property<Guid>("FireTableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<double>("PlannedMonthlyContribution")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FireTableId");

                    b.ToTable("AssetHolders");
                });

            modelBuilder.Entity("Firestone.Domain.Models.Assets", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("AssetHolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LineItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AssetHolderId");

                    b.HasIndex("LineItemId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Firestone.Domain.Models.FireTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("RetirementTargetBeforeInflation")
                        .HasColumnType("float");

                    b.Property<double>("YearlyInflationRate")
                        .HasColumnType("float");

                    b.Property<double>("YearlyNominalReturnRate")
                        .HasColumnType("float");

                    b.Property<int>("YearsToRetirement")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FireTables");
                });

            modelBuilder.Entity("Firestone.Domain.Models.LineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FireTableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FireTableId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("Firestone.Domain.Models.AssetHolder", b =>
                {
                    b.HasOne("Firestone.Domain.Models.FireTable", "FireTable")
                        .WithMany("AssetHolders")
                        .HasForeignKey("FireTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FireTable");
                });

            modelBuilder.Entity("Firestone.Domain.Models.Assets", b =>
                {
                    b.HasOne("Firestone.Domain.Models.AssetHolder", "AssetHolder")
                        .WithMany("Assets")
                        .HasForeignKey("AssetHolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Firestone.Domain.Models.LineItem", "LineItem")
                        .WithMany("Assets")
                        .HasForeignKey("LineItemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AssetHolder");

                    b.Navigation("LineItem");
                });

            modelBuilder.Entity("Firestone.Domain.Models.LineItem", b =>
                {
                    b.HasOne("Firestone.Domain.Models.FireTable", "FireTable")
                        .WithMany("LineItems")
                        .HasForeignKey("FireTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FireTable");
                });

            modelBuilder.Entity("Firestone.Domain.Models.AssetHolder", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Firestone.Domain.Models.FireTable", b =>
                {
                    b.Navigation("AssetHolders");

                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("Firestone.Domain.Models.LineItem", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}
