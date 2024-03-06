using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class flue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyPlanes",
                columns: table => new
                {
                    PlaneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaneCapacity = table.Column<int>(type: "int", nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PlaneType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyPlanes", x => x.PlaneId);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PasseportNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    PassFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PassLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TelNumber = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PasseportNumber);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    EffectiveArrival = table.Column<DateTime>(type: "Date", nullable: false),
                    Departure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaneFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_MyPlanes_PlaneFK",
                        column: x => x.PlaneFK,
                        principalTable: "MyPlanes",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    PasseportNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    EmploymentDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.PasseportNumber);
                    table.ForeignKey(
                        name: "FK_Staffs_Passengers_PasseportNumber",
                        column: x => x.PasseportNumber,
                        principalTable: "Passengers",
                        principalColumn: "PasseportNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travellers",
                columns: table => new
                {
                    PasseportNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    HealthInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travellers", x => x.PasseportNumber);
                    table.ForeignKey(
                        name: "FK_Travellers_Passengers_PasseportNumber",
                        column: x => x.PasseportNumber,
                        principalTable: "Passengers",
                        principalColumn: "PasseportNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateReservation = table.Column<DateTime>(type: "Date", nullable: false),
                    prix = table.Column<float>(type: "real", nullable: false),
                    PassengerPasseportNumber = table.Column<string>(type: "nvarchar(7)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationTicket_Passengers_PassengerPasseportNumber",
                        column: x => x.PassengerPasseportNumber,
                        principalTable: "Passengers",
                        principalColumn: "PasseportNumber");
                    table.ForeignKey(
                        name: "FK_ReservationTicket_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolsPassengers",
                columns: table => new
                {
                    FlightsFlightId = table.Column<int>(type: "int", nullable: false),
                    PassengersPasseportNumber = table.Column<string>(type: "nvarchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolsPassengers", x => new { x.FlightsFlightId, x.PassengersPasseportNumber });
                    table.ForeignKey(
                        name: "FK_VolsPassengers_Flights_FlightsFlightId",
                        column: x => x.FlightsFlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolsPassengers_Passengers_PassengersPasseportNumber",
                        column: x => x.PassengersPasseportNumber,
                        principalTable: "Passengers",
                        principalColumn: "PasseportNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneFK",
                table: "Flights",
                column: "PlaneFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTicket_PassengerPasseportNumber",
                table: "ReservationTicket",
                column: "PassengerPasseportNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTicket_TicketId",
                table: "ReservationTicket",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_VolsPassengers_PassengersPasseportNumber",
                table: "VolsPassengers",
                column: "PassengersPasseportNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationTicket");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Travellers");

            migrationBuilder.DropTable(
                name: "VolsPassengers");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "MyPlanes");
        }
    }
}
