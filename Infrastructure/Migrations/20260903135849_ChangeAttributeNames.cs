using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttributeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatePublished",
                table: "JobPostings",
                newName: "DateOfPublishing");

            migrationBuilder.RenameColumn(
                name: "DateExpires",
                table: "JobPostings",
                newName: "DateOfExpiration");

            migrationBuilder.RenameColumn(
                name: "DateSubmitted",
                table: "JobApplications",
                newName: "DateOfSubmission");

            migrationBuilder.RenameColumn(
                name: "DateTimeScheduled",
                table: "Interviews",
                newName: "TimeScheduled");

            migrationBuilder.RenameColumn(
                name: "DateHired",
                table: "Employees",
                newName: "DateOfHire");

            migrationBuilder.RenameColumn(
                name: "DateBorn",
                table: "Employees",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfPublishing",
                table: "JobPostings",
                newName: "DatePublished");

            migrationBuilder.RenameColumn(
                name: "DateOfExpiration",
                table: "JobPostings",
                newName: "DateExpires");

            migrationBuilder.RenameColumn(
                name: "DateOfSubmission",
                table: "JobApplications",
                newName: "DateSubmitted");

            migrationBuilder.RenameColumn(
                name: "TimeScheduled",
                table: "Interviews",
                newName: "DateTimeScheduled");

            migrationBuilder.RenameColumn(
                name: "DateOfHire",
                table: "Employees",
                newName: "DateHired");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Employees",
                newName: "DateBorn");
        }
    }
}
