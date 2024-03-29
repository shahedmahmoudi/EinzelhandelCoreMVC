﻿// <auto-generated />
using System;
using EinzelhandelCoreMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EinzelhandelCoreMVC.Migrations
{
    [DbContext(typeof(MVCEinzelhandelContext))]
    [Migration("20190927005513_Bon")]
    partial class Bon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Bon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Art");

                    b.Property<DateTime>("Datum");

                    b.Property<int?>("KundeID");

                    b.HasKey("ID");

                    b.HasIndex("KundeID");

                    b.ToTable("Bon");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Detail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BonID");

                    b.Property<decimal>("Ermäßigung");

                    b.Property<float>("Preis");

                    b.Property<int?>("ProduktID");

                    b.Property<int>("Zahl");

                    b.HasKey("ID");

                    b.HasIndex("BonID");

                    b.HasIndex("ProduktID");

                    b.ToTable("Detail");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Kunde", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresse")
                        .HasMaxLength(500);

                    b.Property<string>("Email");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Rufnummer")
                        .HasMaxLength(50);

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Kunde");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Produkt", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProduktartID");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Zahl");

                    b.HasKey("ID");

                    b.HasIndex("ProduktartID");

                    b.ToTable("Produkt");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Produktart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Produktart");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Bon", b =>
                {
                    b.HasOne("EinzelhandelCoreMVC.Models.Kunde", "Kunde")
                        .WithMany("Bon")
                        .HasForeignKey("KundeID");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Detail", b =>
                {
                    b.HasOne("EinzelhandelCoreMVC.Models.Bon", "Bon")
                        .WithMany("Detail")
                        .HasForeignKey("BonID");

                    b.HasOne("EinzelhandelCoreMVC.Models.Produkt", "Produkt")
                        .WithMany("Detail")
                        .HasForeignKey("ProduktID");
                });

            modelBuilder.Entity("EinzelhandelCoreMVC.Models.Produkt", b =>
                {
                    b.HasOne("EinzelhandelCoreMVC.Models.Produktart", "Produktart")
                        .WithMany("Produkts")
                        .HasForeignKey("ProduktartID");
                });
#pragma warning restore 612, 618
        }
    }
}
