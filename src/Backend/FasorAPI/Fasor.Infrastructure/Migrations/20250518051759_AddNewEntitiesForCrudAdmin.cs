using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fasor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntitiesForCrudAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_RideOptions_CompanyServices_CompanyServiceId",
                table: "RideOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_RideOptions_RideQuotes_RideQuoteId",
                table: "RideOptions");

            migrationBuilder.DropTable(
                name: "CompanyServices");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "RideQuoteId",
                table: "RideOptions",
                newName: "CompanyRideId");

            migrationBuilder.RenameColumn(
                name: "CompanyServiceId",
                table: "RideOptions",
                newName: "AppServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RideOptions_RideQuoteId",
                table: "RideOptions",
                newName: "IX_RideOptions_CompanyRideId");

            migrationBuilder.RenameIndex(
                name: "IX_RideOptions_CompanyServiceId",
                table: "RideOptions",
                newName: "IX_RideOptions_AppServiceId");

            migrationBuilder.RenameColumn(
                name: "TradeName",
                table: "Companies",
                newName: "NameService");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CompanyRides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TradeName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyRideId = table.Column<Guid>(type: "uuid", nullable: false),
                    NameService = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppServices_CompanyRides_CompanyRideId",
                        column: x => x.CompanyRideId,
                        principalTable: "CompanyRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCompanyRide",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyRideId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCompanyRide", x => new { x.CompanyId, x.CompanyRideId });
                    table.ForeignKey(
                        name: "FK_CompanyCompanyRide_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCompanyRide_CompanyRides_CompanyRideId",
                        column: x => x.CompanyRideId,
                        principalTable: "CompanyRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RideOptions_QuoteId",
                table: "RideOptions",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_AppServices_CompanyRideId",
                table: "AppServices",
                column: "CompanyRideId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompanyRide_CompanyRideId",
                table: "CompanyCompanyRide",
                column: "CompanyRideId");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOptions_AppServices_AppServiceId",
                table: "RideOptions",
                column: "AppServiceId",
                principalTable: "AppServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideOptions_CompanyRides_CompanyRideId",
                table: "RideOptions",
                column: "CompanyRideId",
                principalTable: "CompanyRides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideOptions_RideQuotes_QuoteId",
                table: "RideOptions",
                column: "QuoteId",
                principalTable: "RideQuotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOptions_AppServices_AppServiceId",
                table: "RideOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_RideOptions_CompanyRides_CompanyRideId",
                table: "RideOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_RideOptions_RideQuotes_QuoteId",
                table: "RideOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AppServices");

            migrationBuilder.DropTable(
                name: "CompanyCompanyRide");

            migrationBuilder.DropTable(
                name: "CompanyRides");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_RideOptions_QuoteId",
                table: "RideOptions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CompanyRideId",
                table: "RideOptions",
                newName: "RideQuoteId");

            migrationBuilder.RenameColumn(
                name: "AppServiceId",
                table: "RideOptions",
                newName: "CompanyServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RideOptions_CompanyRideId",
                table: "RideOptions",
                newName: "IX_RideOptions_RideQuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_RideOptions_AppServiceId",
                table: "RideOptions",
                newName: "IX_RideOptions_CompanyServiceId");

            migrationBuilder.RenameColumn(
                name: "NameService",
                table: "Companies",
                newName: "TradeName");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCompany = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    NameService = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyServices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyServices_CompanyId",
                table: "CompanyServices",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOptions_CompanyServices_CompanyServiceId",
                table: "RideOptions",
                column: "CompanyServiceId",
                principalTable: "CompanyServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideOptions_RideQuotes_RideQuoteId",
                table: "RideOptions",
                column: "RideQuoteId",
                principalTable: "RideQuotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
