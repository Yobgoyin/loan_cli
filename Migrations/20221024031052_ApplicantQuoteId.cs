using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace applicant_api.Migrations
{
    public partial class ApplicantQuoteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Quote_QuoteId",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Quote_QuoteId",
                table: "Applicant",
                column: "QuoteId",
                principalTable: "Quote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Quote_QuoteId",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Applicant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Quote_QuoteId",
                table: "Applicant",
                column: "QuoteId",
                principalTable: "Quote",
                principalColumn: "Id");
        }
    }
}
