using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicimaxTurnike.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastEntryDetailsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LastEntryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastEntryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastEntryDetails_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LastEntryDetails_PersonId",
                table: "LastEntryDetails",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LastEntryDetails");
        }
    }
}
