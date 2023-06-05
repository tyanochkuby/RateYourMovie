using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateYourMovie.Migrations.LoginDb
{
    /// <inheritdoc />
    public partial class addposteranddescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "Movie");
        }
    }
}
