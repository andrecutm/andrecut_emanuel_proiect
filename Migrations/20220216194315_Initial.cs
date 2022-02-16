using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndrecutEmanuelMedii.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TrainStation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainStation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatingCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainStationID = table.Column<int>(type: "int", nullable: false),
                    DestinationTrainStationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Train_TrainStation_DestinationTrainStationID",
                        column: x => x.DestinationTrainStationID,
                        principalTable: "TrainStation",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Train_TrainStation_TrainStationID",
                        column: x => x.TrainStationID,
                        principalTable: "TrainStation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wagon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSeats = table.Column<decimal>(type: "decimal(3,0)", nullable: false),
                    FreeSeats = table.Column<decimal>(type: "decimal(3,0)", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wagon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wagon_Train_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityWagon",
                columns: table => new
                {
                    FacilitiesID = table.Column<int>(type: "int", nullable: false),
                    WagonsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityWagon", x => new { x.FacilitiesID, x.WagonsID });
                    table.ForeignKey(
                        name: "FK_FacilityWagon_Facility_FacilitiesID",
                        column: x => x.FacilitiesID,
                        principalTable: "Facility",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityWagon_Wagon_WagonsID",
                        column: x => x.WagonsID,
                        principalTable: "Wagon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityWagon_WagonsID",
                table: "FacilityWagon",
                column: "WagonsID");

            migrationBuilder.CreateIndex(
                name: "IX_Train_DestinationTrainStationID",
                table: "Train",
                column: "DestinationTrainStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Train_TrainStationID",
                table: "Train",
                column: "TrainStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Wagon_TrainId",
                table: "Wagon",
                column: "TrainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityWagon");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "Wagon");

            migrationBuilder.DropTable(
                name: "Train");

            migrationBuilder.DropTable(
                name: "TrainStation");
        }
    }
}
