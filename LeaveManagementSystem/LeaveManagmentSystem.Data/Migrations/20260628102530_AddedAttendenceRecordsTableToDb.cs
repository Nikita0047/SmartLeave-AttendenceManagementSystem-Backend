using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttendenceRecordsTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendenceRecord_Users_EmployeeId",
                table: "AttendenceRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendenceRecord",
                table: "AttendenceRecord");

            migrationBuilder.RenameTable(
                name: "AttendenceRecord",
                newName: "AttendanceRecords");

            migrationBuilder.RenameIndex(
                name: "IX_AttendenceRecord_EmployeeId",
                table: "AttendanceRecords",
                newName: "IX_AttendanceRecords_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceRecords",
                table: "AttendanceRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Users_EmployeeId",
                table: "AttendanceRecords",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Users_EmployeeId",
                table: "AttendanceRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceRecords",
                table: "AttendanceRecords");

            migrationBuilder.RenameTable(
                name: "AttendanceRecords",
                newName: "AttendenceRecord");

            migrationBuilder.RenameIndex(
                name: "IX_AttendanceRecords_EmployeeId",
                table: "AttendenceRecord",
                newName: "IX_AttendenceRecord_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendenceRecord",
                table: "AttendenceRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendenceRecord_Users_EmployeeId",
                table: "AttendenceRecord",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
