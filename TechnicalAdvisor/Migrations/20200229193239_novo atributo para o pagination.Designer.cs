﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Migrations
{
    [DbContext(typeof(TechnicalAdvisorContext))]
    [Migration("20200229193239_novo atributo para o pagination")]
    partial class novoatributoparaopagination
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("CompanyID");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.ToTable("Dealer");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Pagination", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentPage");

                    b.Property<int>("ProductId");

                    b.Property<int>("TotalPages");

                    b.HasKey("id");

                    b.ToTable("Pagination");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Json");

                    b.Property<string>("Name");

                    b.Property<string>("PictureName");

                    b.Property<string>("PublicationCode");

                    b.Property<double>("PublicationVersion");

                    b.Property<string>("TypeOfProduct");

                    b.Property<int>("XmlProductId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DealerID");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DealerID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.XmlProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("InfosDiversas");

                    b.Property<string>("LinkDaImagem");

                    b.Property<string>("MaisInfos");

                    b.Property<int>("ProductId");

                    b.Property<string>("TituloDoBloco");

                    b.HasKey("Id");

                    b.ToTable("XmlProduct");
                });

            modelBuilder.Entity("TechnicalAdvisor.Models.Dealer", b =>
                {
                    b.HasOne("TechnicalAdvisor.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .HasForeignKey("DealerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}