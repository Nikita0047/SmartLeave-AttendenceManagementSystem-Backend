using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeaveBalancesTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveBalance_LeaveTypes_LeaveTypeId",
                table: "LeaveBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveBalance_Users_EmployeeId",
                table: "LeaveBalance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveBalance",
                table: "LeaveBalance");

            migrationBuilder.RenameTable(
                name: "LeaveBalance",
                newName: "LeaveBalances");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveBalance_LeaveTypeId",
                table: "LeaveBalances",
                newName: "IX_LeaveBalances_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveBalance_EmployeeId",
                table: "LeaveBalances",
                newName: "IX_LeaveBalances_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveBalances",
                table: "LeaveBalances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveBalances_LeaveTypes_LeaveTypeId",
                table: "LeaveBalances",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveBalances_Users_EmployeeId",
                table: "LeaveBalances",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveBalances_LeaveTypes_LeaveTypeId",
                table: "LeaveBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveBalances_Users_EmployeeId",
                table: "LeaveBalances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveBalances",
                table: "LeaveBalances");

            migrationBuilder.RenameTable(
                name: "LeaveBalances",
                newName: "LeaveBalance");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveBalances_LeaveTypeId",
                table: "LeaveBalance",
                newName: "IX_LeaveBalance_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveBalances_EmployeeId",
                table: "LeaveBalance",
                newName: "IX_LeaveBalance_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveBalance",
                table: "LeaveBalance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveBalance_LeaveTypes_LeaveTypeId",
                table: "LeaveBalance",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveBalance_Users_EmployeeId",
                table: "LeaveBalance",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
