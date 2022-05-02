using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCData.Migrations
{
    public partial class tattiMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_OwnedById",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubForums_OwnedById",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubForums_Forums_BelongsToId",
                table: "SubForums");

            migrationBuilder.DropIndex(
                name: "IX_Comments_OwnedById",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "OwnedById",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "BelongsToId",
                table: "SubForums",
                newName: "ForumId");

            migrationBuilder.RenameIndex(
                name: "IX_SubForums_BelongsToId",
                table: "SubForums",
                newName: "IX_SubForums_ForumId");

            migrationBuilder.RenameColumn(
                name: "OwnedById",
                table: "Posts",
                newName: "SubForumId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_OwnedById",
                table: "Posts",
                newName: "IX_Posts_SubForumId");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Comments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SubForums_SubForumId",
                table: "Posts",
                column: "SubForumId",
                principalTable: "SubForums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubForums_Forums_ForumId",
                table: "SubForums",
                column: "ForumId",
                principalTable: "Forums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubForums_SubForumId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubForums_Forums_ForumId",
                table: "SubForums");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ForumId",
                table: "SubForums",
                newName: "BelongsToId");

            migrationBuilder.RenameIndex(
                name: "IX_SubForums_ForumId",
                table: "SubForums",
                newName: "IX_SubForums_BelongsToId");

            migrationBuilder.RenameColumn(
                name: "SubForumId",
                table: "Posts",
                newName: "OwnedById");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_SubForumId",
                table: "Posts",
                newName: "IX_Posts_OwnedById");

            migrationBuilder.AddColumn<int>(
                name: "OwnedById",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OwnedById",
                table: "Comments",
                column: "OwnedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_OwnedById",
                table: "Comments",
                column: "OwnedById",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SubForums_OwnedById",
                table: "Posts",
                column: "OwnedById",
                principalTable: "SubForums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubForums_Forums_BelongsToId",
                table: "SubForums",
                column: "BelongsToId",
                principalTable: "Forums",
                principalColumn: "Id");
        }
    }
}
