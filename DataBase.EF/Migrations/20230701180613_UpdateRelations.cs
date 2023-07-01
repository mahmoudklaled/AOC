using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentVedios_QuestionCommentId",
                table: "QuestionCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentPhotos_QuestionCommentId",
                table: "QuestionCommentPhotos");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentVedios_PostCommentId",
                table: "PostCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentPhotos_PostCommentId",
                table: "PostCommentPhotos");

            migrationBuilder.AddColumn<string>(
                name: "RegistrationIP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentVedios_QuestionCommentId",
                table: "QuestionCommentVedios",
                column: "QuestionCommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentPhotos_QuestionCommentId",
                table: "QuestionCommentPhotos",
                column: "QuestionCommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentVedios_PostCommentId",
                table: "PostCommentVedios",
                column: "PostCommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentPhotos_PostCommentId",
                table: "PostCommentPhotos",
                column: "PostCommentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentVedios_QuestionCommentId",
                table: "QuestionCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentPhotos_QuestionCommentId",
                table: "QuestionCommentPhotos");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentVedios_PostCommentId",
                table: "PostCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentPhotos_PostCommentId",
                table: "PostCommentPhotos");

            migrationBuilder.DropColumn(
                name: "RegistrationIP",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentVedios_QuestionCommentId",
                table: "QuestionCommentVedios",
                column: "QuestionCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentPhotos_QuestionCommentId",
                table: "QuestionCommentPhotos",
                column: "QuestionCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentVedios_PostCommentId",
                table: "PostCommentVedios",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentPhotos_PostCommentId",
                table: "PostCommentPhotos",
                column: "PostCommentId");
        }
    }
}
