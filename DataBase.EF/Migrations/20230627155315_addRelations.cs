using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.EF.Migrations
{
    /// <inheritdoc />
    public partial class addRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostCommentPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostCommentVedios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VedioPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentVedios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCommentPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCommentPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCommentVedios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VedioPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCommentVedios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoverPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoverPhotos_ProfileAccounts_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCommentReacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reacts = table.Column<int>(type: "int", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentReacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCommentReacts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePhotos_ProfileAccounts_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCommentReacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reacts = table.Column<int>(type: "int", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCommentReacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionCommentReacts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionPosts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPhotos_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reacts = table.Column<int>(type: "int", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReacts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReacts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostVedios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VedioPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVedios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostVedios_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionComments_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionComments_QuestionPosts_QuestionPostId",
                        column: x => x.QuestionPostId,
                        principalTable: "QuestionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionPhotos_QuestionPosts_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionReacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reacts = table.Column<int>(type: "int", nullable: false),
                    ProfileAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionReacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionReacts_ProfileAccounts_ProfileAccountId",
                        column: x => x.ProfileAccountId,
                        principalTable: "ProfileAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionReacts_QuestionPosts_QuestionPostId",
                        column: x => x.QuestionPostId,
                        principalTable: "QuestionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionVedios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VedioPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionVedios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionVedios_QuestionPosts_QuestionPostId",
                        column: x => x.QuestionPostId,
                        principalTable: "QuestionPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoverPhotos_ProfileId",
                table: "CoverPhotos",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentReacts_ProfileAccountId",
                table: "PostCommentReacts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_ProfileAccountId",
                table: "PostComments",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPhotos_PostId",
                table: "PostPhotos",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReacts_PostId",
                table: "PostReacts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReacts_ProfileAccountId",
                table: "PostReacts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProfileAccountId",
                table: "Posts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVedios_PostId",
                table: "PostVedios",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCommentReacts_ProfileAccountId",
                table: "QuestionCommentReacts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionComments_ProfileAccountId",
                table: "QuestionComments",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionComments_QuestionPostId",
                table: "QuestionComments",
                column: "QuestionPostId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPhotos_QuestionId",
                table: "QuestionPhotos",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPosts_ProfileAccountId",
                table: "QuestionPosts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionReacts_ProfileAccountId",
                table: "QuestionReacts",
                column: "ProfileAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionReacts_QuestionPostId",
                table: "QuestionReacts",
                column: "QuestionPostId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVedios_QuestionPostId",
                table: "QuestionVedios",
                column: "QuestionPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverPhotos");

            migrationBuilder.DropTable(
                name: "PostCommentPhotos");

            migrationBuilder.DropTable(
                name: "PostCommentReacts");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "PostCommentVedios");

            migrationBuilder.DropTable(
                name: "PostPhotos");

            migrationBuilder.DropTable(
                name: "PostReacts");

            migrationBuilder.DropTable(
                name: "PostVedios");

            migrationBuilder.DropTable(
                name: "ProfilePhotos");

            migrationBuilder.DropTable(
                name: "QuestionCommentPhotos");

            migrationBuilder.DropTable(
                name: "QuestionCommentReacts");

            migrationBuilder.DropTable(
                name: "QuestionComments");

            migrationBuilder.DropTable(
                name: "QuestionCommentVedios");

            migrationBuilder.DropTable(
                name: "QuestionPhotos");

            migrationBuilder.DropTable(
                name: "QuestionReacts");

            migrationBuilder.DropTable(
                name: "QuestionVedios");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "QuestionPosts");

            migrationBuilder.DropTable(
                name: "ProfileAccounts");
        }
    }
}
