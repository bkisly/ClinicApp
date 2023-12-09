using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVisitStatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "VisitStatusId",
                table: "Visits",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "VisitsStatus",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitsStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitStatusId",
                table: "Visits",
                column: "VisitStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_VisitsStatus_VisitStatusId",
                table: "Visits",
                column: "VisitStatusId",
                principalTable: "VisitsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_VisitsStatus_VisitStatusId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "VisitsStatus");

            migrationBuilder.DropIndex(
                name: "IX_Visits_VisitStatusId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitStatusId",
                table: "Visits");
        }
    }
}
