using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_Name_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "users",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "users",
                newName: "two_factor_enabled");

            migrationBuilder.RenameColumn(
                name: "TimeUpdated",
                table: "users",
                newName: "time_updated");

            migrationBuilder.RenameColumn(
                name: "TimeCreated",
                table: "users",
                newName: "time_created");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "users",
                newName: "security_stamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "users",
                newName: "phone_number_confirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "users",
                newName: "normalized_username");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "users",
                newName: "normalized_email");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "users",
                newName: "lockout_end");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "users",
                newName: "lockout_enabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "users",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "users",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "users",
                newName: "access_failed_count");

            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "users",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "two_factor_enabled",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "security_stamp",
                table: "users",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "phone_number_confirmed",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "users",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "lockout_enabled",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "email_confirmed",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "concurrency_stamp",
                table: "users",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "access_failed_count",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 728, DateTimeKind.Utc).AddTicks(8060),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 502, DateTimeKind.Utc).AddTicks(2449));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 728, DateTimeKind.Utc).AddTicks(7313),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 502, DateTimeKind.Utc).AddTicks(1664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 728, DateTimeKind.Utc).AddTicks(27),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 501, DateTimeKind.Utc).AddTicks(4979));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 727, DateTimeKind.Utc).AddTicks(9356),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 501, DateTimeKind.Utc).AddTicks(4352));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "users",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "two_factor_enabled",
                table: "users",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "time_updated",
                table: "users",
                newName: "TimeUpdated");

            migrationBuilder.RenameColumn(
                name: "time_created",
                table: "users",
                newName: "TimeCreated");

            migrationBuilder.RenameColumn(
                name: "security_stamp",
                table: "users",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phone_number_confirmed",
                table: "users",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "normalized_username",
                table: "users",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                table: "users",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockout_end",
                table: "users",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockout_enabled",
                table: "users",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                table: "users",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                table: "users",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                table: "users",
                newName: "AccessFailedCount");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "TwoFactorEnabled",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "SecurityStamp",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "LockoutEnabled",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "AccessFailedCount",
                table: "users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 502, DateTimeKind.Utc).AddTicks(2449),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 728, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 502, DateTimeKind.Utc).AddTicks(1664),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 728, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 501, DateTimeKind.Utc).AddTicks(4979),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 728, DateTimeKind.Utc).AddTicks(27));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 0, 38, 501, DateTimeKind.Utc).AddTicks(4352),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 4, 44, 727, DateTimeKind.Utc).AddTicks(9356));
        }
    }
}
