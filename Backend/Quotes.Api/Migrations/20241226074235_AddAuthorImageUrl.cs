using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AuthorDb",
                type: "varchar(2083)",
                maxLength: 2083,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AuthorDb");
        }
    }
}
