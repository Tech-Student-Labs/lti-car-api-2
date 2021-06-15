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
    [Migration("20210615150736_MoreVehiclesToInventory")]
    partial class MoreVehiclesToInventory
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
                        },
                        new
                        {
                            Id = 2,
                            Price = 21600.0,
                            VehicleId = 2
                        },
                        new
                        {
                            Id = 3,
                            Price = 35400.0,
                            VehicleId = 3
                        },
                        new
                        {
                            Id = 4,
                            Price = 35000.0,
                            VehicleId = 6
                        },
                        new
                        {
                            Id = 5,
                            Price = 7400.0,
                            VehicleId = 7
                        },
                        new
                        {
                            Id = 6,
                            Price = 2000.0,
                            VehicleId = 8
                        },
                        new
                        {
                            Id = 7,
                            Price = 7800.0,
                            VehicleId = 9
                        },
                        new
                        {
                            Id = 8,
                            Price = 14200.0,
                            VehicleId = 10
                        },
                        new
                        {
                            Id = 9,
                            Price = 15999.0,
                            VehicleId = 11
                        },
                        new
                        {
                            Id = 10,
                            Price = 15550.0,
                            VehicleId = 12
                        },
                        new
                        {
                            Id = 11,
                            Price = 67000.0,
                            VehicleId = 13
                        },
                        new
                        {
                            Id = 12,
                            Price = 6500.0,
                            VehicleId = 14
                        },
                        new
                        {
                            Id = 13,
                            Price = 1500.0,
                            VehicleId = 16
                        },
                        new
                        {
                            Id = 14,
                            Price = 11200.0,
                            VehicleId = 17
                        },
                        new
                        {
                            Id = 15,
                            Price = 15000.0,
                            VehicleId = 18
                        },
                        new
                        {
                            Id = 16,
                            Price = 35000.0,
                            VehicleId = 19
                        },
                        new
                        {
                            Id = 17,
                            Price = 52000.0,
                            VehicleId = 20
                        },
                        new
                        {
                            Id = 18,
                            Price = 16000.0,
                            VehicleId = 21
                        },
                        new
                        {
                            Id = 19,
                            Price = 23186.0,
                            VehicleId = 22
                        },
                        new
                        {
                            Id = 20,
                            Price = 4549.0,
                            VehicleId = 23
                        },
                        new
                        {
                            Id = 21,
                            Price = 36545.0,
                            VehicleId = 26
                        },
                        new
                        {
                            Id = 22,
                            Price = 54631.0,
                            VehicleId = 27
                        },
                        new
                        {
                            Id = 23,
                            Price = 16512.0,
                            VehicleId = 28
                        },
                        new
                        {
                            Id = 24,
                            Price = 2000000.0,
                            VehicleId = 29
                        },
                        new
                        {
                            Id = 25,
                            Price = 500000.0,
                            VehicleId = 30
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

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
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

                    b.Property<int>("Year")
                        .HasColumnType("int");

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
                            Year = 1997
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
                            Year = 1997
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
                            Year = 2005
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
                            Year = 2007
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
                            Year = 2005
                        },
                        new
                        {
                            Id = 6,
                            Color = "Blue",
                            Make = "Volkswagen",
                            Miles = 15600,
                            Model = "Rabbit",
                            SellingPrice = 35000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "WVWAA71K08W201030",
                            Year = 2015
                        },
                        new
                        {
                            Id = 7,
                            Color = "Black",
                            Make = "Ford",
                            Miles = 154000,
                            Model = "F250",
                            SellingPrice = 7400.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "2FTHF25H6LCB36173",
                            Year = 1990
                        },
                        new
                        {
                            Id = 8,
                            Color = "White",
                            Make = "Acura",
                            Miles = 352000,
                            Model = "Integra",
                            SellingPrice = 2000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "JH4DA3341JS014654",
                            Year = 1988
                        },
                        new
                        {
                            Id = 9,
                            Color = "Beige",
                            Make = "Honda",
                            Miles = 98000,
                            Model = "CRV",
                            SellingPrice = 7800.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "5J6RM4H75CL059384",
                            Year = 2012
                        },
                        new
                        {
                            Id = 10,
                            Color = "White",
                            Make = "Infiniti",
                            Miles = 42000,
                            Model = "G37",
                            SellingPrice = 14200.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "JNKCV64E78M131002",
                            Year = 2008
                        },
                        new
                        {
                            Id = 11,
                            Color = "Silver",
                            Make = "Infiniti",
                            Miles = 21500,
                            Model = "QX56",
                            SellingPrice = 15999.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "5N3ZA0NE6AN906847",
                            Year = 2010
                        },
                        new
                        {
                            Id = 12,
                            Color = "Black",
                            Make = "Toyota",
                            Miles = 10000,
                            Model = "Corolla",
                            SellingPrice = 15550.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "1NXBB02E9TZ393131",
                            Year = 1996
                        },
                        new
                        {
                            Id = 13,
                            Color = "Blue",
                            Make = "Mercedes Benz",
                            Miles = 25000,
                            Model = "C240",
                            SellingPrice = 67000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "WDBAB23A6DB369209",
                            Year = 1983
                        },
                        new
                        {
                            Id = 14,
                            Color = "Red",
                            Make = "Honda",
                            Miles = 22000,
                            Model = "CRV",
                            SellingPrice = 6500.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "5J6RE4H48BL023237",
                            Year = 2011
                        },
                        new
                        {
                            Id = 15,
                            Color = "Purple",
                            Make = "Acura",
                            Miles = 84000,
                            Model = "Legend",
                            SellingPrice = 3200.0,
                            Status = 2,
                            UserId = 1,
                            VIN = "JH4KA4540KC031984",
                            Year = 1989
                        },
                        new
                        {
                            Id = 16,
                            Color = "Blue",
                            Make = "Ford",
                            Miles = 700000,
                            Model = "Ranger",
                            SellingPrice = 1500.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "1FTCR15T4GPB29162",
                            Year = 1986
                        },
                        new
                        {
                            Id = 17,
                            Color = "Black",
                            Make = "Honda",
                            Miles = 49000,
                            Model = "Odyssey",
                            SellingPrice = 11200.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "5FNRL38739B001353",
                            Year = 2009
                        },
                        new
                        {
                            Id = 18,
                            Color = "White",
                            Make = "Hyundai",
                            Miles = 62335,
                            Model = "Elantra",
                            SellingPrice = 15000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "KMHD25LE1DU042025",
                            Year = 2013
                        },
                        new
                        {
                            Id = 19,
                            Color = "Beige",
                            Make = "Infiniti",
                            Miles = 98700,
                            Model = "Fx35",
                            SellingPrice = 35000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "JN8AS1MU0CM120061",
                            Year = 2012
                        },
                        new
                        {
                            Id = 20,
                            Color = "White",
                            Make = "Jeep",
                            Miles = 42000,
                            Model = "Cherokee",
                            SellingPrice = 52000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "1J4FT68SXXL633294",
                            Year = 1999
                        },
                        new
                        {
                            Id = 21,
                            Color = "Silver",
                            Make = "Mini",
                            Miles = 145000,
                            Model = "Cooper",
                            SellingPrice = 16000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "WMWZN3C51BT133317",
                            Year = 2011
                        },
                        new
                        {
                            Id = 22,
                            Color = "Black",
                            Make = "Acura",
                            Miles = 16514,
                            Model = "NSX",
                            SellingPrice = 23186.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "JH4NA1150PT000087",
                            Year = 1993
                        },
                        new
                        {
                            Id = 23,
                            Color = "Blue",
                            Make = "GMC",
                            Miles = 984132,
                            Model = "Safari",
                            SellingPrice = 4549.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "1GKEL19WXRB546238",
                            Year = 1994
                        },
                        new
                        {
                            Id = 24,
                            Color = "Red",
                            Make = "Mazda",
                            Miles = 222000,
                            Model = "323",
                            SellingPrice = 6511.0,
                            Status = 0,
                            UserId = 1,
                            VIN = "JM1BF2325G0V37585",
                            Year = 1986
                        },
                        new
                        {
                            Id = 25,
                            Color = "Purple",
                            Make = "Toyota",
                            Miles = 98461,
                            Model = "Camry",
                            SellingPrice = 54166.0,
                            Status = 2,
                            UserId = 1,
                            VIN = "4T1BF3EK5BU638805",
                            Year = 2011
                        },
                        new
                        {
                            Id = 26,
                            Color = "Blue",
                            Make = "Pontiac",
                            Miles = 96841,
                            Model = "Sunfire",
                            SellingPrice = 36545.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "1G2JB12F047226515",
                            Year = 2004
                        },
                        new
                        {
                            Id = 27,
                            Color = "Black",
                            Make = "Hyundai",
                            Miles = 46333,
                            Model = "Santa Fe Sport",
                            SellingPrice = 54631.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "5XYZUDLB7DG006717",
                            Year = 2013
                        },
                        new
                        {
                            Id = 28,
                            Color = "White",
                            Make = "Dodge",
                            Miles = 345531,
                            Model = "Ram Pickup 1500",
                            SellingPrice = 16512.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "1D7HU18D54S747050",
                            Year = 2004
                        },
                        new
                        {
                            Id = 29,
                            Color = "Beige",
                            Make = "Jaguar",
                            Miles = 15000,
                            Model = "S-Type",
                            SellingPrice = 2000000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "SAJDA41GXDPA26883",
                            Year = 1983
                        },
                        new
                        {
                            Id = 30,
                            Color = "White",
                            Make = "Ferrari",
                            Miles = 5600,
                            Model = "F430",
                            SellingPrice = 500000.0,
                            Status = 1,
                            UserId = 1,
                            VIN = "ZFFEW59A190165924",
                            Year = 2009
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