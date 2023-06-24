using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Migrations
{
    /// <inheritdoc />
    public partial class AddReacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    reacts = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reacts_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reacts_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reacts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reacts_QuestionPosts_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QuestionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_CommentID",
                table: "Reacts",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_PostID",
                table: "Reacts",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_ProfileAccountId",
                table: "Reacts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Reacts_QuestionID",
                table: "Reacts",
                column: "QuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reacts");
        }
    }
}
