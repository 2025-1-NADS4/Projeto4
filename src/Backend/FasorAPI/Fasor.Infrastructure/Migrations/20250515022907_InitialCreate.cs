using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fasor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    DateBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TradeName = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RideQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginAddress = table.Column<string>(type: "text", nullable: false),
                    DestinationAddress = table.Column<string>(type: "text", nullable: false),
                    LongitudeOrigin = table.Column<float>(type: "real", nullable: false),
                    LatitudeOrigin = table.Column<float>(type: "real", nullable: false),
                    LongitudeDestination = table.Column<float>(type: "real", nullable: false),
                    LatitudeDestination = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RideQuotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCompany = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    NameService = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RideOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    QuoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    RideQuoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    TimeRide = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstimatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UrlRedirect = table.Column<string>(type: "text", nullable: false),
                    CompanyServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RideOptions_CompanyServices_CompanyServiceId",
                        column: x => x.CompanyServiceId,
                        principalTable: "CompanyServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RideOptions_RideQuotes_RideQuoteId",
                        column: x => x.RideQuoteId,
                        principalTable: "RideQuotes",
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

            migrationBuilder.CreateIndex(
                name: "IX_RideOptions_CompanyServiceId",
                table: "RideOptions",
                column: "CompanyServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RideOptions_RideQuoteId",
                table: "RideOptions",
                column: "RideQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_RideQuotes_UserId",
                table: "RideQuotes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideOptions");

            migrationBuilder.DropTable(
                name: "CompanyServices");

            migrationBuilder.DropTable(
                name: "RideQuotes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
