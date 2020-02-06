﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Migrations
{
    [DbContext(typeof(TechnicalAdvisorContext))]
    partial class TechnicalAdvisorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechnicalAdvisor.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Dealer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Dealer");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Name");

                    b.Property<string>("PublicationCode");

                    b.Property<double>("PublicationVersion");

                    b.Property<string>("TypeOfProduct");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DealerId");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DealerId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Dealer", b =>
                {
                    b.HasOne("TechnicalAdvisor.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Product", b =>
                {
                    b.HasOne("TechnicalAdvisor.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.User", b =>
                {
                    b.HasOne("TechnicalAdvisor.Models.Dealer", "Dealer")
                        .WithMany()
                        .HasForeignKey("DealerId");
                });
#pragma warning restore 612, 618
        }
    }
}
