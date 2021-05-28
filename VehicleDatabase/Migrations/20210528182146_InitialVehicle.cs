using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleDatabase.Migrations
{
    public partial class InitialVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VIN = table.Column<string>(type: "TEXT", nullable: false),
                    Make = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<string>(type: "TEXT", nullable: true),
                    Miles = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    ImageURI = table.Column<string>(type: "TEXT", nullable: true),
                    sellingPrice = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VIN);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VIN", "Color", "ImageURI", "Make", "Miles", "Model", "Year", "sellingPrice" },
                values: new object[] { "4Y1SL65848Z411439", "Silver", "https://via.placeholder.com/150", "Toyota", 145000, "Corolla", "1997", 2000.0 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VIN", "Color", "ImageURI", "Make", "Miles", "Model", "Year", "sellingPrice" },
                values: new object[] { "5Z1SL65848A411439", "Black", "https://via.placeholder.com/150", "Honda", 145000, "Civic", "1997", 3000.0 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VIN", "Color", "ImageURI", "Make", "Miles", "Model", "Year", "sellingPrice" },
                values: new object[] { "7T1SL658486411439", "Blue", "https://via.placeholder.com/150", "Subaru", 175000, "Impreza", "2005", 4000.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
