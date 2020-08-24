using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPokemonAbility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8601),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8037),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(313));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 988, DateTimeKind.Local).AddTicks(7571),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 35, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 982, DateTimeKind.Local).AddTicks(6698),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 29, DateTimeKind.Local).AddTicks(4898));

            migrationBuilder.AlterColumn<float>(
                name: "CatchRate",
                table: "Pokemons",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 996, DateTimeKind.Local).AddTicks(121),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 995, DateTimeKind.Local).AddTicks(9666),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(1062),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(944));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(696),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(645));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 8, 9, 12, 27, 32, 1, DateTimeKind.Local).AddTicks(4176), new DateTime(2020, 8, 9, 12, 27, 32, 1, DateTimeKind.Local).AddTicks(4519) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(625),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8601));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 39, DateTimeKind.Local).AddTicks(313),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 993, DateTimeKind.Local).AddTicks(8037));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 35, DateTimeKind.Local).AddTicks(1141),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 988, DateTimeKind.Local).AddTicks(7571));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 29, DateTimeKind.Local).AddTicks(4898),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 982, DateTimeKind.Local).AddTicks(6698));

            migrationBuilder.AlterColumn<int>(
                name: "CatchRate",
                table: "Pokemons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6720),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 996, DateTimeKind.Local).AddTicks(121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 40, DateTimeKind.Local).AddTicks(6412),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 995, DateTimeKind.Local).AddTicks(9666));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(944),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(1062));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 7, 13, 44, 49, 37, DateTimeKind.Local).AddTicks(645),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 9, 12, 27, 31, 991, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 8, 7, 13, 44, 49, 45, DateTimeKind.Local).AddTicks(528), new DateTime(2020, 8, 7, 13, 44, 49, 45, DateTimeKind.Local).AddTicks(847) });
        }
    }
}
