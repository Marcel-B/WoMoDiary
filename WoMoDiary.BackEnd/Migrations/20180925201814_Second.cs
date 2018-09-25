using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WoMoDiary.BackEnd.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Location_LocationId",
                table: "Places");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Places_LocationId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "IsGood",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "Trip",
                table: "Places",
                newName: "TripFk");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastEdit",
                table: "Places",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Places",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Altitude",
                table: "Places",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Places",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Places",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<short>(
                name: "Rating",
                table: "Places",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Places",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "TripFk",
                table: "Places",
                newName: "Trip");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastEdit",
                table: "Places",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Places",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddColumn<bool>(
                name: "IsGood",
                table: "Places",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Places",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Altitude = table.Column<double>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastEdit = table.Column<DateTimeOffset>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_LocationId",
                table: "Places",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Location_LocationId",
                table: "Places",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
