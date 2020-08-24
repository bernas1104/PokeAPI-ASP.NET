using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPokemonSeenCapturedPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(2212),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8601));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(1881),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8037));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 995, DateTimeKind.Local).AddTicks(9488),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 988, DateTimeKind.Local).AddTicks(7571));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 990, DateTimeKind.Local).AddTicks(635),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 982, DateTimeKind.Local).AddTicks(6698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7913),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 996, DateTimeKind.Local).AddTicks(121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7625),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 995, DateTimeKind.Local).AddTicks(9666));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(2056),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(1062));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(1763),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 8, 16, 17, 44, 46, 5, DateTimeKind.Local).AddTicks(9721), new DateTime(2020, 8, 16, 17, 44, 46, 6, DateTimeKind.Local).AddTicks(25) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8601),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(2212));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8037),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(1881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 988, DateTimeKind.Local).AddTicks(7571),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 995, DateTimeKind.Local).AddTicks(9488));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 982, DateTimeKind.Local).AddTicks(6698),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 990, DateTimeKind.Local).AddTicks(635));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 996, DateTimeKind.Local).AddTicks(121),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 995, DateTimeKind.Local).AddTicks(9666),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(1062),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(2056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(696),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(1763));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 8, 9, 12, 27, 32, 1, DateTimeKind.Local).AddTicks(4176), new DateTime(2020, 8, 9, 12, 27, 32, 1, DateTimeKind.Local).AddTicks(4519) });
        }
    }
}
