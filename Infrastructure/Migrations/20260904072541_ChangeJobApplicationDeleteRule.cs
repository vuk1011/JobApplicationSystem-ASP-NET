using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeJobApplicationDeleteRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_JobApplications_JobApplicationId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_JobApplications_JobApplicationId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_JobApplications_JobApplicationId",
                table: "Interviews",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_JobApplications_JobApplicationId",
                table: "Offers",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_JobApplications_JobApplicationId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_JobApplications_JobApplicationId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_JobApplications_JobApplicationId",
                table: "Interviews",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_JobApplications_JobApplicationId",
                table: "Offers",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
