using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedtheprimarykeyofUserCourseSubscriptionEntitytoacompositeprimarykey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourseSubscriptions",
                table: "UserCourseSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserCourseSubscriptions_UserId",
                table: "UserCourseSubscriptions");

            // Drop the 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserCourseSubscriptions");

            // Add the 'Id' column back without the 'IDENTITY' property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserCourseSubscriptions",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourseSubscriptions",
                table: "UserCourseSubscriptions",
                columns: new[] { "UserId", "CourseId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourseSubscriptions",
                table: "UserCourseSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_UserCourseSubscriptions_UserId",
                table: "UserCourseSubscriptions");

            // Drop the 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserCourseSubscriptions");

            // Add the 'Id' column back with the 'IDENTITY' property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserCourseSubscriptions",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add the primary key back
            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourseSubscriptions",
                table: "UserCourseSubscriptions",
                column: "Id");

        }
    }
}
