using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class addApplicationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationCategoryId",
                table: "Actions",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Actions",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplicaionCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicaionCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Applicaions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationCategoryId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicaions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicaions_ApplicaionCategories_ApplicationCategoryId",
                        column: x => x.ApplicationCategoryId,
                        principalTable: "ApplicaionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ApplicationCategoryId",
                table: "Actions",
                column: "ApplicationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ApplicationId",
                table: "Actions",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicaions_ApplicationCategoryId",
                table: "Applicaions",
                column: "ApplicationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_ApplicaionCategories_ApplicationCategoryId",
                table: "Actions",
                column: "ApplicationCategoryId",
                principalTable: "ApplicaionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Applicaions_ApplicationId",
                table: "Actions",
                column: "ApplicationId",
                principalTable: "Applicaions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_ApplicaionCategories_ApplicationCategoryId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Applicaions_ApplicationId",
                table: "Actions");

            migrationBuilder.DropTable(
                name: "Applicaions");

            migrationBuilder.DropTable(
                name: "ApplicaionCategories");

            migrationBuilder.DropIndex(
                name: "IX_Actions_ApplicationCategoryId",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_ApplicationId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "ApplicationCategoryId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Actions");
        }
    }
}
