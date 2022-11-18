using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace urlshortner.Migrations
{
    public partial class ThirdCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatingSystem",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Browser",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "OperatingSystem",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Accesses");
        }
    }
}
