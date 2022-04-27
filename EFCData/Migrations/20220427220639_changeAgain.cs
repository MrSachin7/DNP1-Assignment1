using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCData.Migrations
{
    public partial class changeAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubForums_OwnedById",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubForums_Forums_BelongsToId",
                table: "SubForums");

            migrationBuilder.AlterColumn<int>(
                name: "BelongsToId",
                table: "SubForums",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "OwnedById",
                table: "Posts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubForums_OwnedById",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubForums_Forums_BelongsToId",
                table: "SubForums");

            migrationBuilder.AlterColumn<int>(
                name: "BelongsToId",
                table: "SubForums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnedById",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SubForums_OwnedById",
                table: "Posts",
                column: "OwnedById",
                principalTable: "SubForums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubForums_Forums_BelongsToId",
                table: "SubForums",
                column: "BelongsToId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
