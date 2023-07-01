using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class postCommentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PostCommentVedios_PostCommentId",
                table: "PostCommentVedios",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentReacts_PostCommentId",
                table: "PostCommentReacts",
                column: "PostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentPhotos_PostCommentId",
                table: "PostCommentPhotos",
                column: "PostCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentPhotos_PostComments_PostCommentId",
                table: "PostCommentPhotos",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_PostComments_PostCommentId",
                table: "PostCommentReacts",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentVedios_PostComments_PostCommentId",
                table: "PostCommentVedios",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentPhotos_PostComments_PostCommentId",
                table: "PostCommentPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_PostComments_PostCommentId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentVedios_PostComments_PostCommentId",
                table: "PostCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentVedios_PostCommentId",
                table: "PostCommentVedios");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentReacts_PostCommentId",
                table: "PostCommentReacts");

            migrationBuilder.DropIndex(
                name: "IX_PostCommentPhotos_PostCommentId",
                table: "PostCommentPhotos");
        }
    }
}
