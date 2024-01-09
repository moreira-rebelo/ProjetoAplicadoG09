using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Nomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "reservation_code_pk",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "reservations",
                newName: "fk_reservation_user");

            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "reservations",
                newName: "fK_reservation_room");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_UserId",
                table: "reservations",
                newName: "IX_reservations_fk_reservation_user");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_RoomNumber",
                table: "reservations",
                newName: "IX_reservations_fK_reservation_room");

            migrationBuilder.AddPrimaryKey(
                name: "reservation_code",
                table: "reservations",
                column: "reservation_code_pk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "reservation_code",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "fk_reservation_user",
                table: "reservations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "fK_reservation_room",
                table: "reservations",
                newName: "RoomNumber");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_fk_reservation_user",
                table: "reservations",
                newName: "IX_reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_fK_reservation_room",
                table: "reservations",
                newName: "IX_reservations_RoomNumber");

            migrationBuilder.AddPrimaryKey(
                name: "reservation_code_pk",
                table: "reservations",
                column: "reservation_code_pk");
        }
    }
}
