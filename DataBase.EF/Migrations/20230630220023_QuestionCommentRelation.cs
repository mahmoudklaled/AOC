using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class QuestionCommentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentVedios_QuestionCommentId",
                table: "QuestionCommentVedios",
                column: "QuestionCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentReacts_QuestionCommentId",
                table: "QuestionCommentReacts",
                column: "QuestionCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentPhotos_QuestionCommentId",
                table: "QuestionCommentPhotos",
                column: "QuestionCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentPhotos_QuestionComments_QuestionCommentId",
                table: "QuestionCommentPhotos",
                column: "QuestionCommentId",
                principalTable: "QuestionComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_QuestionComments_QuestionCommentId",
                table: "QuestionCommentReacts",
                column: "QuestionCommentId",
                principalTable: "QuestionComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentVedios_QuestionComments_QuestionCommentId",
                table: "QuestionCommentVedios",
                column: "QuestionCommentId",
                principalTable: "QuestionComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentPhotos_QuestionComments_QuestionCommentId",
                table: "QuestionCommentPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_QuestionComments_QuestionCommentId",
                table: "QuestionCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentVedios_QuestionComments_QuestionCommentId",
                table: "QuestionCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentVedios_QuestionCommentId",
                table: "QuestionCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentReacts_QuestionCommentId",
                table: "QuestionCommentReacts");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCommentPhotos_QuestionCommentId",
                table: "QuestionCommentPhotos");
        }
    }
}
