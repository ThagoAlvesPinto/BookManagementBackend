using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagementBackend.Migrations
{
    /// <inheritdoc />
    public partial class IndicesERelacaoEntreUsuarioEDevolucao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksReturn_Books_BookId",
                table: "BooksReturn");

            migrationBuilder.DropColumn(
                name: "ReturnUserName",
                table: "BooksReturn");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<int>(
                name: "ReturnUserId",
                table: "BooksReturn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn13",
                table: "Books",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn10",
                table: "Books",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BooksReturn_ReturnUserId",
                table: "BooksReturn",
                column: "ReturnUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn10",
                table: "Books",
                column: "Isbn10");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn13",
                table: "Books",
                column: "Isbn13");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksReturn_Books_BookId",
                table: "BooksReturn",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BooksReturn_Users_ReturnUserId",
                table: "BooksReturn",
                column: "ReturnUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksReturn_Books_BookId",
                table: "BooksReturn");

            migrationBuilder.DropForeignKey(
                name: "FK_BooksReturn_Users_ReturnUserId",
                table: "BooksReturn");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_BooksReturn_ReturnUserId",
                table: "BooksReturn");

            migrationBuilder.DropIndex(
                name: "IX_Books_Isbn10",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Isbn13",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReturnUserId",
                table: "BooksReturn");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "ReturnUserName",
                table: "BooksReturn",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn13",
                table: "Books",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn10",
                table: "Books",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BooksReturn_Books_BookId",
                table: "BooksReturn",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
