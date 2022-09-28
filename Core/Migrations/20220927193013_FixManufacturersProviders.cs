using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class FixManufacturersProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Manufacturers_ManufacturerId",
                table: "Providers");

            migrationBuilder.DropTable(
                name: "ManufacturerProvider");

            migrationBuilder.DropIndex(
                name: "IX_Providers_ManufacturerId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Providers");

            migrationBuilder.AddColumn<string>(
                name: "ProviderId",
                table: "Manufacturers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_ProviderId",
                table: "Manufacturers",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Providers_ProviderId",
                table: "Manufacturers",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Providers_ProviderId",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_ProviderId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Manufacturers");

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerId",
                table: "Providers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ManufacturerProvider",
                columns: table => new
                {
                    ManufacturerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerProvider", x => new { x.ManufacturerId, x.ProviderId });
                    table.ForeignKey(
                        name: "FK_ManufacturerProvider_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManufacturerProvider_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_ManufacturerId",
                table: "Providers",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerProvider_ProviderId",
                table: "ManufacturerProvider",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Manufacturers_ManufacturerId",
                table: "Providers",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
