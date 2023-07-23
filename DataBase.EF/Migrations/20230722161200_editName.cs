using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class editName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_ProfileAccounts_ProfileId",
                table: "CoverPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_ProfileAccounts_FirstUserId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_ProfileAccounts_SecondUserId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_ProfileAccounts_ReceiverId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_ProfileAccounts_RequestorId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_ProfileAccounts_ProfileAccountId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReacts_ProfileAccounts_ProfileAccountId",
                table: "PostReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_ProfileAccounts_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComments_ProfileAccounts_ProfileAccountId",
                table: "QuestionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionReacts");

            migrationBuilder.DropTable(
                name: "ProfileAccounts");

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_UserAccounts_ProfileId",
                table: "CoverPhotos",
                column: "ProfileId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_UserAccounts_FirstUserId",
                table: "Friend",
                column: "FirstUserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_UserAccounts_SecondUserId",
                table: "Friend",
                column: "SecondUserId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_UserAccounts_ReceiverId",
                table: "FriendRequest",
                column: "ReceiverId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_UserAccounts_RequestorId",
                table: "FriendRequest",
                column: "RequestorId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_UserAccounts_ProfileAccountId",
                table: "PostCommentReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_UserAccounts_ProfileAccountId",
                table: "PostComments",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReacts_UserAccounts_ProfileAccountId",
                table: "PostReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserAccounts_ProfileAccountId",
                table: "Posts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_UserAccounts_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_UserAccounts_ProfileAccountId",
                table: "QuestionCommentReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComments_UserAccounts_ProfileAccountId",
                table: "QuestionComments",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_UserAccounts_ProfileAccountId",
                table: "QuestionPosts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionReacts_UserAccounts_ProfileAccountId",
                table: "QuestionReacts",
                column: "ProfileAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_UserAccounts_ProfileId",
                table: "CoverPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_UserAccounts_FirstUserId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_UserAccounts_SecondUserId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_UserAccounts_ReceiverId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_UserAccounts_RequestorId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReacts_UserAccounts_ProfileAccountId",
                table: "PostCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_UserAccounts_ProfileAccountId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReacts_UserAccounts_ProfileAccountId",
                table: "PostReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserAccounts_ProfileAccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_UserAccounts_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCommentReacts_UserAccounts_ProfileAccountId",
                table: "QuestionCommentReacts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComments_UserAccounts_ProfileAccountId",
                table: "QuestionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_UserAccounts_ProfileAccountId",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionReacts_UserAccounts_ProfileAccountId",
                table: "QuestionReacts");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.CreateTable(
                name: "ProfileAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAccounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_ProfileAccounts_ProfileId",
                table: "CoverPhotos",
                column: "ProfileId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_ProfileAccounts_FirstUserId",
                table: "Friend",
                column: "FirstUserId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_ProfileAccounts_SecondUserId",
                table: "Friend",
                column: "SecondUserId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_ProfileAccounts_ReceiverId",
                table: "FriendRequest",
                column: "ReceiverId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_ProfileAccounts_RequestorId",
                table: "FriendRequest",
                column: "RequestorId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "PostCommentReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_ProfileAccounts_ProfileAccountId",
                table: "PostComments",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReacts_ProfileAccounts_ProfileAccountId",
                table: "PostReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                table: "Posts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_ProfileAccounts_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionCommentReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComments_ProfileAccounts_ProfileAccountId",
                table: "QuestionComments",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                table: "QuestionPosts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionReacts_ProfileAccounts_ProfileAccountId",
                table: "QuestionReacts",
                column: "ProfileAccountId",
                principalTable: "ProfileAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
