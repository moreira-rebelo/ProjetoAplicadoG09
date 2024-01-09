using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISI.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reservation_id",
                table: "room_history",
                newName: "reservation_code");

            migrationBuilder.RenameColumn(
                name: "room_id",
                table: "room_history",
                newName: "room_number");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "room",
                newName: "room_number_pk");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "reservations",
                newName: "reservation_code_pk");

            migrationBuilder.AlterColumn<string>(
                name: "reservation_code",
                table: "room_history",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "room_number",
                table: "room_history",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "room_number_pk",
                table: "room",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "reservations",
                type: "character varying(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "reservation_code_pk",
                table: "reservations",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reservation_code",
                table: "room_history",
                newName: "reservation_id");

            migrationBuilder.RenameColumn(
                name: "room_number",
                table: "room_history",
                newName: "room_id");

            migrationBuilder.RenameColumn(
                name: "room_number_pk",
                table: "room",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "reservation_code_pk",
                table: "reservations",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "reservation_id",
                table: "room_history",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "room_id",
                table: "room_history",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "room",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "reservations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "reservations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);
        }
    }
}
