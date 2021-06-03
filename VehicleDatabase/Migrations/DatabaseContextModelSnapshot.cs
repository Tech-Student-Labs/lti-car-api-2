﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleDatabase.Data;

namespace VehicleDatabase.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Models.InventoryVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Inventory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 6000.0,
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("VehicleDatabase.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Authorization")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Authorization = 1,
                            Name = "Robert Muehler"
                        });
                });

            modelBuilder.Entity("VehicleDatabase.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Make")
                        .HasColumnType("TEXT");

                    b.Property<int>("Miles")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<double>("SellingPrice")
                        .HasColumnType("REAL");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VIN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Silver",
                            Make = "Toyota",
                            Miles = 145000,
                            Model = "Corolla",
                            SellingPrice = 2000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "4Y1SL65848Z411439",
                            Year = "1997"
                        },
                        new
                        {
                            Id = 2,
                            Color = "Black",
                            Make = "Honda",
                            Miles = 145000,
                            Model = "Civic",
                            SellingPrice = 3000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "5Z1SL39746U411411",
                            Year = "1997"
                        },
                        new
                        {
                            Id = 3,
                            Color = "Blue",
                            Make = "Subaru",
                            Miles = 175000,
                            Model = "Impreza",
                            SellingPrice = 4000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "7T1SL646726411440",
                            Year = "2005"
                        },
                        new
                        {
                            Id = 4,
                            Color = "Red",
                            Make = "Mazda",
                            Miles = 200000,
                            Model = "3",
                            SellingPrice = 2000.0,
                            Status = 0,
                            UserId = 1,
                            VIN = "9P1SL658486268352",
                            Year = "2007"
                        },
                        new
                        {
                            Id = 5,
                            Color = "Purple",
                            Make = "Mitsubishi",
                            Miles = 75000,
                            Model = "Eclipse",
                            SellingPrice = 6000.0,
                            Status = 2,
                            UserId = 1,
                            VIN = "YU1SL658486123463",
                            Year = "2005"
                        });
                });

            modelBuilder.Entity("VehicleDatabase.Models.VehicleImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("BLOB");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleImages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("VehicleDatabase.Models.Vehicle", b =>
                {
                    b.HasOne("VehicleDatabase.Models.User", "User")
                        .WithMany("Vehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VehicleDatabase.Models.VehicleImage", b =>
                {
                    b.HasOne("VehicleDatabase.Models.Vehicle", null)
                        .WithMany("VehicleImages")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleDatabase.Models.User", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("VehicleDatabase.Models.Vehicle", b =>
                {
                    b.Navigation("VehicleImages");
                });
#pragma warning restore 612, 618
        }
    }
}
