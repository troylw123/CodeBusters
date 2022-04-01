using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeBusters.Data.Migrations
{
    public partial class AddReviewEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Assessments_AssessmentId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_AssessmentId",
                table: "Responses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Responses_AssessmentId",
                table: "Responses",
                column: "AssessmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Assessments_AssessmentId",
                table: "Responses",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
