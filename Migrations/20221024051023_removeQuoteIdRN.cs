using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace applicant_api.Migrations
{
    public partial class removeQuoteIdRN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Quote_QuoteId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_QuoteId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Applicant");

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Quote",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Applicant_ApplicantId",
                table: "Quote");

            migrationBuilder.DropIndex(
                name: "IX_Quote_ApplicantId",
                table: "Quote");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Quote");

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_QuoteId",
                table: "Applicant",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Quote_QuoteId",
                table: "Applicant",
                column: "QuoteId",
                principalTable: "Quote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
