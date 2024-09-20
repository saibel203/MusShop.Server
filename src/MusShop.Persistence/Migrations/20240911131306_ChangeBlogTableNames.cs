using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBlogTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "blog_posts");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "blog_categories");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CategoryId",
                table: "blog_posts",
                newName: "IX_blog_posts_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blog_posts",
                table: "blog_posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blog_categories",
                table: "blog_categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_blog_posts_blog_categories_CategoryId",
                table: "blog_posts",
                column: "CategoryId",
                principalTable: "blog_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            const string sqlSeedFileName = "SeedData.sql";
            string sqlFilePath = Path.Combine("/app/Seeds", sqlSeedFileName);
            string sqlFileText = File.ReadAllText(sqlFilePath);
            migrationBuilder.Sql(sqlFileText);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blog_posts_blog_categories_CategoryId",
                table: "blog_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blog_posts",
                table: "blog_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blog_categories",
                table: "blog_categories");

            migrationBuilder.RenameTable(
                name: "blog_posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "blog_categories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_blog_posts_CategoryId",
                table: "Posts",
                newName: "IX_Posts_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
