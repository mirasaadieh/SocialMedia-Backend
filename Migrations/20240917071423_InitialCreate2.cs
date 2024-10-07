using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsComments_Posts_PostId",
                table: "PostsComments");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsComments_Posts_PostId",
                table: "PostsComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsComments_Posts_PostId",
                table: "PostsComments");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsComments_Posts_PostId",
                table: "PostsComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
