using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedcoursespeficicationstableaswellasusercoursesubscriptionstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSpecificationEntity_Courses_CourseId",
                table: "CourseSpecificationEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourseSubscriptionEntity_AspNetUsers_UserId",
                table: "UserCourseSubscriptionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourseSubscriptionEntity_Courses_CourseId",
                table: "UserCourseSubscriptionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourseSubscriptionEntity",
                table: "UserCourseSubscriptionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSpecificationEntity",
                table: "CourseSpecificationEntity");

            migrationBuilder.RenameTable(
                name: "UserCourseSubscriptionEntity",
                newName: "UserCourseSubscriptions");

            migrationBuilder.RenameTable(
                name: "CourseSpecificationEntity",
                newName: "CourseSpecifications");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourseSubscriptionEntity_UserId",
                table: "UserCourseSubscriptions",
                newName: "IX_UserCourseSubscriptions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourseSubscriptionEntity_CourseId",
                table: "UserCourseSubscriptions",
                newName: "IX_UserCourseSubscriptions_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSpecificationEntity_CourseId",
                table: "CourseSpecifications",
                newName: "IX_CourseSpecifications_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourseSubscriptions",
                table: "UserCourseSubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSpecifications",
                table: "CourseSpecifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSpecifications_Courses_CourseId",
                table: "CourseSpecifications",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourseSubscriptions_AspNetUsers_UserId",
                table: "UserCourseSubscriptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourseSubscriptions_Courses_CourseId",
                table: "UserCourseSubscriptions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSpecifications_Courses_CourseId",
                table: "CourseSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourseSubscriptions_AspNetUsers_UserId",
                table: "UserCourseSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourseSubscriptions_Courses_CourseId",
                table: "UserCourseSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourseSubscriptions",
                table: "UserCourseSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSpecifications",
                table: "CourseSpecifications");

            migrationBuilder.RenameTable(
                name: "UserCourseSubscriptions",
                newName: "UserCourseSubscriptionEntity");

            migrationBuilder.RenameTable(
                name: "CourseSpecifications",
                newName: "CourseSpecificationEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourseSubscriptions_UserId",
                table: "UserCourseSubscriptionEntity",
                newName: "IX_UserCourseSubscriptionEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourseSubscriptions_CourseId",
                table: "UserCourseSubscriptionEntity",
                newName: "IX_UserCourseSubscriptionEntity_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSpecifications_CourseId",
                table: "CourseSpecificationEntity",
                newName: "IX_CourseSpecificationEntity_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourseSubscriptionEntity",
                table: "UserCourseSubscriptionEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSpecificationEntity",
                table: "CourseSpecificationEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSpecificationEntity_Courses_CourseId",
                table: "CourseSpecificationEntity",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourseSubscriptionEntity_AspNetUsers_UserId",
                table: "UserCourseSubscriptionEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourseSubscriptionEntity_Courses_CourseId",
                table: "UserCourseSubscriptionEntity",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
