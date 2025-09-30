using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "Users",
                newName: "UserType");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RejectedById",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ApprovedById",
                table: "Loans",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_RejectedById",
                table: "Loans",
                column: "RejectedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AdminUsers_ApprovedById",
                table: "Loans",
                column: "ApprovedById",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AdminUsers_RejectedById",
                table: "Loans",
                column: "RejectedById",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AdminUsers_ApprovedById",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AdminUsers_RejectedById",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_Loans_ApprovedById",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_RejectedById",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RejectedById",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "UserRole");
        }
    }
}
