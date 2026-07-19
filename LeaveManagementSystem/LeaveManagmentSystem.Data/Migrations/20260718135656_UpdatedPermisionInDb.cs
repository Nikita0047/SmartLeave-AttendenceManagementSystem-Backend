using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPermisionInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "PasswordHash",
                value: "$2a$11$flY5M6QZfoX/JyYKafJR6uzkmsCsQALsiA1zWxhlL3i7mHeCxOvmC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "PasswordHash",
                value: "$2a$11$iKmdmIxwr5k4MvBbWXgt9usmrld42/jG6/ayulDM5MkgwXFjb.xJa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "PasswordHash",
                value: "$2a$11$unNNQj2Fqc8cK2jsmK43Te49CQfjAMsH1BW7vm8oZ/Li5t4RmEWLO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "PasswordHash",
                value: "$2a$11$Xb28.8k4vcXEn4T07b12W.T5iLCaGkMQVSVJIgmeFNDPMaTFWX8My");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "PasswordHash",
                value: "$2a$11$.LpJmzbmxrTPvEbEFbQRn.hRSQsMfr3HFaWosTJNiOond1EeSDGjC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "PasswordHash",
                value: "$2a$11$6sGnr3PlZo3yVl7wP8Y1hu41G0dVZRGnWiz4dhSlQ4oj1dgHkJQ.G");
        }
    }
}
