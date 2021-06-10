﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleDatabase.Data;

namespace VehicleWebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210610175010_changed vehicleimagedata to string")]
    partial class changedvehicleimagedatatostring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VehicleWebAPI.Models.InventoryVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Inventory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 6000.0,
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("VehicleWebAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Authorization")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("VehicleWebAPI.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Miles")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SellingPrice")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("VehicleWebAPI.Models.VehicleImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("VehicleWebAPI.Models.InventoryVehicle", b =>
                {
                    b.HasOne("VehicleWebAPI.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleWebAPI.Models.Vehicle", b =>
                {
                    b.HasOne("VehicleWebAPI.Models.User", "User")
                        .WithMany("Vehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VehicleWebAPI.Models.VehicleImage", b =>
                {
                    b.HasOne("VehicleWebAPI.Models.Vehicle", null)
                        .WithMany("VehicleImages")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleWebAPI.Models.User", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("VehicleWebAPI.Models.Vehicle", b =>
                {
                    b.Navigation("VehicleImages");
                });
#pragma warning restore 612, 618
        }
    }
}
