﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesShop.Models;

namespace MoviesShop.Migrations
{
    [DbContext(typeof(MoviesShopContext))]
    [Migration("20191224075421_FixTitleCountry")]
    partial class FixTitleCountry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoviesShop.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDay");

                    b.Property<int?>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("MoviesShop.Models.Countrys", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameOfTheCountry");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MoviesShop.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountrysId");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("CountrysId");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("MoviesShop.Models.FilmActor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActorId");

                    b.Property<int>("FilmId");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("FilmId");

                    b.ToTable("FilmActor");
                });

            modelBuilder.Entity("MoviesShop.Models.FilmGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilmId");

                    b.Property<int>("GenreId");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("GenreId");

                    b.ToTable("FilmGenre");
                });

            modelBuilder.Entity("MoviesShop.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MoviesShop.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("Loggin");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MoviesShop.Models.UserFilm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilmId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFilm");
                });

            modelBuilder.Entity("MoviesShop.Models.Actor", b =>
                {
                    b.HasOne("MoviesShop.Models.Countrys", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("MoviesShop.Models.Film", b =>
                {
                    b.HasOne("MoviesShop.Models.Countrys", "Countrys")
                        .WithMany("Film")
                        .HasForeignKey("CountrysId");
                });

            modelBuilder.Entity("MoviesShop.Models.FilmActor", b =>
                {
                    b.HasOne("MoviesShop.Models.Actor", "Actor")
                        .WithMany("FilmActor")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoviesShop.Models.Film", "Film")
                        .WithMany("FilmActor")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MoviesShop.Models.FilmGenre", b =>
                {
                    b.HasOne("MoviesShop.Models.Film", "Film")
                        .WithMany("FilmGenre")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoviesShop.Models.Genre", "Genre")
                        .WithMany("FilmGenre")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MoviesShop.Models.UserFilm", b =>
                {
                    b.HasOne("MoviesShop.Models.Film", "Film")
                        .WithMany("UserFilm")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoviesShop.Models.User", "User")
                        .WithMany("UserFilm")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
