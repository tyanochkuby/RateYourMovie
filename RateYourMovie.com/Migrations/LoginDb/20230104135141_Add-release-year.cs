using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateYourMovie.Migrations.LoginDb
{
    /// <inheritdoc />
    public partial class Addreleaseyear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Movie");
        }
    }
}
