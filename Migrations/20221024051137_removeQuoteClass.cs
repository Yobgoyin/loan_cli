using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace applicant_api.Migrations
{
    public partial class removeQuoteClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Applicant_ApplicantId",
                table: "Quote");

            migrationBuilder.DropIndex(
                name: "IX_Quote_ApplicantId",
                table: "Quote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Quote_ApplicantId",
                table: "Quote",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Applicant_ApplicantId",
                table: "Quote",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
