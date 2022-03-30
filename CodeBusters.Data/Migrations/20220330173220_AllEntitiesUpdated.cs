using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeBusters.Data.Migrations
{
    public partial class AllEntitiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentId",
                table: "Responses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Responses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Assessments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Assessments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Assessments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TicketEntityId",
                table: "Assessments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeRequired",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_AssessmentId",
                table: "Responses",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_TicketEntityId",
                table: "Assessments",
                column: "TicketEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Tickets_TicketEntityId",
                table: "Assessments",
                column: "TicketEntityId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Assessments_AssessmentId",
                table: "Responses",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Tickets_TicketEntityId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Assessments_AssessmentId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_AssessmentId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_TicketEntityId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "AssessmentId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "TicketEntityId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "TimeRequired",
                table: "Assessments");
        }
    }
}
