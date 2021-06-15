using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleWebAPI.Migrations
{
    public partial class Addedmoreseedvehiclestodatabase : Migration
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
                    { 6, "Blue", "Volkswagen", 15600, "Rabbit", 35000.0, 1, 1, "WVWAA71K08W201030", 2015 },
                    { 7, "Black", "Ford", 154000, "F250", 7400.0, 1, 1, "2FTHF25H6LCB36173", 1990 },
                    { 8, "White", "Acura", 352000, "Integra", 2000.0, 1, 1, "JH4DA3341JS014654", 1988 },
                    { 9, "Beige", "Honda", 98000, "CRV", 7800.0, 1, 1, "5J6RM4H75CL059384", 2012 },
                    { 10, "White", "Infiniti", 42000, "G37", 14200.0, 1, 1, "JNKCV64E78M131002", 2008 }
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "Id", "Price", "VehicleId" },
                values: new object[,]
                {
                    { 4, 35000.0, 6 },
                    { 5, 7400.0, 7 },
                    { 6, 2000.0, 8 },
                    { 7, 7800.0, 9 },
                    { 8, 14200.0, 10 }
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
        }
    }
}
