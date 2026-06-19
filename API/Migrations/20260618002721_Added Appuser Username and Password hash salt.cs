using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppuserUsernameandPasswordhashsalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "AppUsers",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AppUsers",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "AppUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE AppUsers SET Username = 'User' || CAST(Id AS VARCHAR)");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Username",
                table: "AppUsers",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppUsers_Username",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "AppUsers");
        }
    }
}
