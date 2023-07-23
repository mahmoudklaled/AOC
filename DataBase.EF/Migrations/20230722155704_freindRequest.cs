using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class freindRequest : Migration
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

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecondUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_ProfileAccounts_FirstUserId",
                        column: x => x.FirstUserId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friend_ProfileAccounts_SecondUserId",
                        column: x => x.SecondUserId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequest_ProfileAccounts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendRequest_ProfileAccounts_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FirstUserId",
                table: "Friend",
                column: "FirstUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_SecondUserId",
                table: "Friend",
                column: "SecondUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_ReceiverId",
                table: "FriendRequest",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_RequestorId",
                table: "FriendRequest",
                column: "RequestorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "FriendRequest");

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
