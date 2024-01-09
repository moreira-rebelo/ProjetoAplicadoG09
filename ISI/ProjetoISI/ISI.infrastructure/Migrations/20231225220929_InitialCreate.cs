using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "controller",
                columns: table => new
                {
                    controller_address = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lock_code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controller", x => x.controller_address);
                });

            migrationBuilder.CreateTable(
                name: "room_history",
                columns: table => new
                {
                    access_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    room_id = table.Column<string>(type: "text", nullable: false),
                    reservation_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_history", x => new { x.room_id, x.reservation_id, x.access_time });
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    access_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ControllerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("room_number_pk", x => x.Id);
                    table.ForeignKey(
                        name: "fk_room_controller",
                        column: x => x.ControllerId,
                        principalTable: "controller",
                        principalColumn: "controller_address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    checkin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    checkout = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reservation_password = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    RoomNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reservation_code_pk", x => x.Id);
                    table.ForeignKey(
                        name: "fK_reservation_room",
                        column: x => x.RoomNumber,
                        principalTable: "room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reservation_user",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_RoomNumber",
                table: "reservations",
                column: "RoomNumber");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_UserId",
                table: "reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_room_ControllerId",
                table: "room",
                column: "ControllerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "room_history");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "controller");
        }
    }
}
