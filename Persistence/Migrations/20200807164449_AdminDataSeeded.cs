using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AdminDataSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(625),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 310, DateTimeKind.Local).AddTicks(171));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(313),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 309, DateTimeKind.Local).AddTicks(9829));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 35, DateTimeKind.Local).AddTicks(1141),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 305, DateTimeKind.Local).AddTicks(9767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 29, DateTimeKind.Local).AddTicks(4898),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 300, DateTimeKind.Local).AddTicks(3732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6720),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6412),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6282));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(944),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 308, DateTimeKind.Local).AddTicks(315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(645),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 307, DateTimeKind.Local).AddTicks(9990));

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2020, 8, 7, 13, 44, 49, 45, DateTimeKind.Local).AddTicks(528), "bernardoc1104@gmail.com", "AQAAAAEAACcQAAAAEBwF+KTzyPtq0ReO+PjXxZKOg4WzIB1OD7RC+s8DYkVYeOgumfgoq3K6a6LQ3Th4AQ==", new DateTime(2020, 8, 7, 13, 44, 49, 45, DateTimeKind.Local).AddTicks(847) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 310, DateTimeKind.Local).AddTicks(171),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 309, DateTimeKind.Local).AddTicks(9829),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(313));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 305, DateTimeKind.Local).AddTicks(9767),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 35, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 300, DateTimeKind.Local).AddTicks(3732),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 29, DateTimeKind.Local).AddTicks(4898));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6611),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6282),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 308, DateTimeKind.Local).AddTicks(315),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(944));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 307, DateTimeKind.Local).AddTicks(9990),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(645));
        }
    }
}
