using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "QuotesDb",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "QuotesDb");
        }
    }
}
