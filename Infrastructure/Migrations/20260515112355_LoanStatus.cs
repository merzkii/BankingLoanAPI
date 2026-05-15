using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class LoanStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "LoanStatusHistories",
           columns: table => new
           {
               Id = table.Column<int>(type: "int", nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
               LoanId = table.Column<int>(type: "int", nullable: false),
               FromStatus = table.Column<int>(type: "int", nullable: false),
               ToStatus = table.Column<int>(type: "int", nullable: false),
               Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
               ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
               ChangedByAdminId = table.Column<int>(type: "int", nullable: false)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_LoanStatusHistories", x => x.Id);
               table.ForeignKey(
                   name: "FK_LoanStatusHistories_AdminUsers_ChangedByAdminId",
                   column: x => x.ChangedByAdminId,
                   principalTable: "AdminUsers",
                   principalColumn: "Id",
                   onDelete: ReferentialAction.Restrict);
               table.ForeignKey(
                   name: "FK_LoanStatusHistories_Loans_LoanId",
                   column: x => x.LoanId,
                   principalTable: "Loans",
                   principalColumn: "LoanId",
                   onDelete: ReferentialAction.Cascade);
           });

            migrationBuilder.CreateIndex(
                name: "IX_LoanStatusHistories_ChangedByAdminId",
                table: "LoanStatusHistories",
                column: "ChangedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanStatusHistories_LoanId",
                table: "LoanStatusHistories",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LoanStatusHistories");
        }
    }
}
