using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicimaxTurnike.Data.Migrations
{
    /// <inheritdoc />
    public partial class TypeColumnAddedToDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "LastEntryDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "LastEntryDetails");
        }
    }
}
