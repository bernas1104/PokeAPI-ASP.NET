using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangePokemonEvolutionRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Pokemons_EvolutionId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_EvolutionId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "EvolutionId",
                table: "Pokemons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 583, DateTimeKind.Local).AddTicks(1715),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(2212));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 583, DateTimeKind.Local).AddTicks(1400),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(1881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 578, DateTimeKind.Local).AddTicks(9555),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 995, DateTimeKind.Local).AddTicks(9488));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 574, DateTimeKind.Local).AddTicks(8448),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 990, DateTimeKind.Local).AddTicks(635));

            migrationBuilder.AddColumn<bool>(
                name: "Captured",
                table: "Pokemons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Pokemons",
                maxLength: 255,
                nullable: false,
                defaultValue: "default.png");

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Pokemons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 584, DateTimeKind.Local).AddTicks(7988),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 584, DateTimeKind.Local).AddTicks(7690),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 581, DateTimeKind.Local).AddTicks(62),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(2056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 580, DateTimeKind.Local).AddTicks(9761),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(1763));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 8, 23, 14, 8, 23, 591, DateTimeKind.Local).AddTicks(1046), new DateTime(2020, 8, 23, 14, 8, 23, 591, DateTimeKind.Local).AddTicks(1395) });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PreEvolutionId",
                table: "Pokemons",
                column: "PreEvolutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Pokemons_PreEvolutionId",
                table: "Pokemons",
                column: "PreEvolutionId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Pokemons_PreEvolutionId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_PreEvolutionId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Captured",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Pokemons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(2212),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 583, DateTimeKind.Local).AddTicks(1715));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stats",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 0, DateTimeKind.Local).AddTicks(1881),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 583, DateTimeKind.Local).AddTicks(1400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 995, DateTimeKind.Local).AddTicks(9488),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 578, DateTimeKind.Local).AddTicks(9555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 990, DateTimeKind.Local).AddTicks(635),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 574, DateTimeKind.Local).AddTicks(8448));

            migrationBuilder.AddColumn<int>(
                name: "EvolutionId",
                table: "Pokemons",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7913),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 584, DateTimeKind.Local).AddTicks(7988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 46, 1, DateTimeKind.Local).AddTicks(7625),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 584, DateTimeKind.Local).AddTicks(7690));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(2056),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 581, DateTimeKind.Local).AddTicks(62));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 17, 44, 45, 998, DateTimeKind.Local).AddTicks(1763),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 23, 14, 8, 23, 580, DateTimeKind.Local).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 8, 16, 17, 44, 46, 5, DateTimeKind.Local).AddTicks(9721), new DateTime(2020, 8, 16, 17, 44, 46, 6, DateTimeKind.Local).AddTicks(25) });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_EvolutionId",
                table: "Pokemons",
                column: "EvolutionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Pokemons_EvolutionId",
                table: "Pokemons",
                column: "EvolutionId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
