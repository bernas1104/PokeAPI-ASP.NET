using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AdminEmailAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Admins");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 310, DateTimeKind.Local).AddTicks(171),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 687, DateTimeKind.Local).AddTicks(4224));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 309, DateTimeKind.Local).AddTicks(9829),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 687, DateTimeKind.Local).AddTicks(3808));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 305, DateTimeKind.Local).AddTicks(9767),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 683, DateTimeKind.Local).AddTicks(3118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 300, DateTimeKind.Local).AddTicks(3732),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 677, DateTimeKind.Local).AddTicks(4787));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6611),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 689, DateTimeKind.Local).AddTicks(343));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6282),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 689, DateTimeKind.Local).AddTicks(42));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Admins",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 308, DateTimeKind.Local).AddTicks(315),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 685, DateTimeKind.Local).AddTicks(4169));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 307, DateTimeKind.Local).AddTicks(9990),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 685, DateTimeKind.Local).AddTicks(3853));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Admins");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 687, DateTimeKind.Local).AddTicks(4224),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 310, DateTimeKind.Local).AddTicks(171));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 687, DateTimeKind.Local).AddTicks(3808),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 309, DateTimeKind.Local).AddTicks(9829));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 683, DateTimeKind.Local).AddTicks(3118),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 305, DateTimeKind.Local).AddTicks(9767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 677, DateTimeKind.Local).AddTicks(4787),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 300, DateTimeKind.Local).AddTicks(3732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 689, DateTimeKind.Local).AddTicks(343),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 689, DateTimeKind.Local).AddTicks(42),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 311, DateTimeKind.Local).AddTicks(6282));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Admins",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 685, DateTimeKind.Local).AddTicks(4169),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 308, DateTimeKind.Local).AddTicks(315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 4, 19, 3, 13, 685, DateTimeKind.Local).AddTicks(3853),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 5, 16, 9, 20, 307, DateTimeKind.Local).AddTicks(9990));
        }
    }
}
