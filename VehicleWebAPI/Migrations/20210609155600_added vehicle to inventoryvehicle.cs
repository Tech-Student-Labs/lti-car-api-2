using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleWebAPI.Migrations
{
    public partial class addedvehicletoinventoryvehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VehicleId",
                table: "Inventory",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Vehicles_VehicleId",
                table: "Inventory",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Vehicles_VehicleId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_VehicleId",
                table: "Inventory");
        }
    }
}
