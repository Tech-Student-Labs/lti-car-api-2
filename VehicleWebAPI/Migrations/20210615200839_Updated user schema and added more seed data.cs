using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleWebAPI.Migrations
{
    public partial class Updateduserschemaandaddedmoreseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "Id", "Price", "VehicleId" },
                values: new object[,]
                {
                    { 2, 21600.0, 2 },
                    { 3, 35400.0, 3 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Color", "Make", "Miles", "Model", "SellingPrice", "Status", "UserId", "VIN", "Year" },
                values: new object[,]
                {
                    { 28, "White", "Dodge", 345531, "Ram Pickup 1500", 16512.0, 1, 1, "1D7HU18D54S747050", 2004 },
                    { 27, "Black", "Hyundai", 46333, "Santa Fe Sport", 54631.0, 1, 1, "5XYZUDLB7DG006717", 2013 },
                    { 26, "Blue", "Pontiac", 96841, "Sunfire", 36545.0, 1, 1, "1G2JB12F047226515", 2004 },
                    { 25, "Purple", "Toyota", 98461, "Camry", 54166.0, 2, 1, "4T1BF3EK5BU638805", 2011 },
                    { 24, "Red", "Mazda", 222000, "323", 6511.0, 0, 1, "JM1BF2325G0V37585", 1986 },
                    { 23, "Blue", "GMC", 984132, "Safari", 4549.0, 1, 1, "1GKEL19WXRB546238", 1994 },
                    { 22, "Black", "Acura", 16514, "NSX", 23186.0, 1, 1, "JH4NA1150PT000087", 1993 },
                    { 21, "Silver", "Mini", 145000, "Cooper", 16000.0, 1, 1, "WMWZN3C51BT133317", 2011 },
                    { 20, "White", "Jeep", 42000, "Cherokee", 52000.0, 1, 1, "1J4FT68SXXL633294", 1999 },
                    { 19, "Beige", "Infiniti", 98700, "Fx35", 35000.0, 1, 1, "JN8AS1MU0CM120061", 2012 },
                    { 18, "White", "Hyundai", 62335, "Elantra", 15000.0, 1, 1, "KMHD25LE1DU042025", 2013 },
                    { 17, "Black", "Honda", 49000, "Odyssey", 11200.0, 1, 1, "5FNRL38739B001353", 2009 },
                    { 16, "Blue", "Ford", 700000, "Ranger", 1500.0, 1, 1, "1FTCR15T4GPB29162", 1986 },
                    { 15, "Purple", "Acura", 84000, "Legend", 3200.0, 2, 1, "JH4KA4540KC031984", 1989 },
                    { 14, "Red", "Honda", 22000, "CRV", 6500.0, 1, 1, "5J6RE4H48BL023237", 2011 },
                    { 13, "Blue", "Mercedes Benz", 25000, "C240", 67000.0, 1, 1, "WDBAB23A6DB369209", 1983 },
                    { 12, "Black", "Toyota", 10000, "Corolla", 15550.0, 1, 1, "1NXBB02E9TZ393131", 1996 },
                    { 11, "Silver", "Infiniti", 21500, "QX56", 15999.0, 1, 1, "5N3ZA0NE6AN906847", 2010 },
                    { 10, "White", "Infiniti", 42000, "G37", 14200.0, 1, 1, "JNKCV64E78M131002", 2008 },
                    { 9, "Beige", "Honda", 98000, "CRV", 7800.0, 1, 1, "5J6RM4H75CL059384", 2012 },
                    { 8, "White", "Acura", 352000, "Integra", 2000.0, 1, 1, "JH4DA3341JS014654", 1988 },
                    { 7, "Black", "Ford", 154000, "F250", 7400.0, 1, 1, "2FTHF25H6LCB36173", 1990 },
                    { 6, "Blue", "Volkswagen", 15600, "Rabbit", 35000.0, 1, 1, "WVWAA71K08W201030", 2015 },
                    { 29, "Beige", "Jaguar", 15000, "S-Type", 2000000.0, 1, 1, "SAJDA41GXDPA26883", 1983 },
                    { 30, "White", "Ferrari", 5600, "F430", 500000.0, 1, 1, "ZFFEW59A190165924", 2009 }
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "Id", "Price", "VehicleId" },
                values: new object[,]
                {
                    { 4, 35000.0, 6 },
                    { 23, 16512.0, 28 },
                    { 22, 54631.0, 27 },
                    { 21, 36545.0, 26 },
                    { 20, 4549.0, 23 },
                    { 19, 23186.0, 22 },
                    { 18, 16000.0, 21 },
                    { 17, 52000.0, 20 },
                    { 16, 35000.0, 19 },
                    { 15, 15000.0, 18 },
                    { 14, 11200.0, 17 },
                    { 13, 1500.0, 16 },
                    { 12, 6500.0, 14 },
                    { 11, 67000.0, 13 },
                    { 10, 15550.0, 12 },
                    { 9, 15999.0, 11 },
                    { 8, 14200.0, 10 },
                    { 7, 7800.0, 9 },
                    { 6, 2000.0, 8 },
                    { 5, 7400.0, 7 },
                    { 24, 2000000.0, 29 },
                    { 25, 500000.0, 30 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
