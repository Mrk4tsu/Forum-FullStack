using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 944, DateTimeKind.Utc).AddTicks(65),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 843, DateTimeKind.Utc).AddTicks(4729));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 943, DateTimeKind.Utc).AddTicks(9129),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 843, DateTimeKind.Utc).AddTicks(4095));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(4437),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 842, DateTimeKind.Utc).AddTicks(7869));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(3589),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 842, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "concurrency_stamp", "email", "lockout_end", "normalized_email", "normalized_username", "password_hash", "time_created", "time_updated", "username" },
                values: new object[,]
                {
                    { 1, "assets/images/avatars/564.png", "9379c5b6-25fb-4b33-8c83-38cd87e09ba3", "admin@mail.com", null, "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEEVixtYh934ge2rJ8HUlnCo9yoCzYRESXaeGtq0g5RmbXS/CAkfCrHAM8ZPv1KjFPA==", new DateTime(2025, 4, 29, 13, 58, 56, 945, DateTimeKind.Local).AddTicks(1717), new DateTime(2025, 4, 29, 13, 58, 56, 945, DateTimeKind.Local).AddTicks(1718), "admin" },
                    { 2, "assets/images/avatars/vip/1475.png", "401c667e-1f8c-41a3-b115-8f24a3955dde", "hunghero@mail.com", null, "HUNGHERO@MAIL.COM", "HUNGHERO", "AQAAAAIAAYagAAAAEMyHlGlrk3YZ84VB6uXjyNeZnLOGwZnOjNFKFcsST5bRHoKdZ2FnXOmjqw1ps9QGAw==", new DateTime(2025, 4, 29, 13, 58, 57, 32, DateTimeKind.Local).AddTicks(2469), new DateTime(2025, 4, 29, 13, 58, 57, 32, DateTimeKind.Local).AddTicks(2473), "hunghero" },
                    { 3, "assets/images/avatars/vip/1360.png", "5e37fcb8-0383-44b2-a9e6-adf09a9c11ee", "katsu@mail.com", null, "KATSU@MAIL.COM", "KATSU", "AQAAAAIAAYagAAAAECdTj6Kx+c1alSoZQjn6c+OchC9TEvqkmMw3M+GgFjSFKrBnE5UK+YPrq6emLopczw==", new DateTime(2025, 4, 29, 13, 58, 57, 201, DateTimeKind.Local).AddTicks(6263), new DateTime(2025, 4, 29, 13, 58, 57, 201, DateTimeKind.Local).AddTicks(6279), "katsu" },
                    { 4, "assets/images/avatars/vip/1361.png", "8756e2c7-dd62-42a2-b5cd-ef766cc47bcb", "gatapchoi@email.com", null, "GATAPCHOI@EMAIL.COM", "GATAPCHOI", "AQAAAAIAAYagAAAAEGoV8MB13ZJ+ZBo25qjYUr7VVIY851fp677z7AfS6+mvXHiW0mkqYyH46X3eyQL59w==", new DateTime(2025, 4, 29, 13, 58, 57, 290, DateTimeKind.Local).AddTicks(5877), new DateTime(2025, 4, 29, 13, 58, 57, 290, DateTimeKind.Local).AddTicks(5900), "gatapchoi" },
                    { 5, "assets/images/avatars/vip/1474.png", "cc6e3352-c109-433a-9e8a-d00440ab0f55", "manhhdc@email.com", null, "MANHHDC@EMAIL.COM", "MANHHDC", "AQAAAAIAAYagAAAAEJZFzwNQL1u1f+aUQ1bVD7UCFy7VD8YTmrfBSIfR2xb4Nv75sTiNiDOu0jxpN/i4ig==", new DateTime(2025, 4, 29, 13, 58, 57, 374, DateTimeKind.Local).AddTicks(7652), new DateTime(2025, 4, 29, 13, 58, 57, 374, DateTimeKind.Local).AddTicks(7684), "manhhdc" },
                    { 6, "assets/images/avatars/1209.png", "5f51214c-4eca-4606-9038-0197ca2bfead", "ehvn@email.com", null, "ehvn@email.com", "EHVN", "AQAAAAIAAYagAAAAEAvGMgWp0YK7u/MS1EDGLmS5TfGn6qAwzengORhAvMjTdLgBocZRlY4cAltRZPbOmA==", new DateTime(2025, 4, 29, 13, 58, 57, 460, DateTimeKind.Local).AddTicks(7167), new DateTime(2025, 4, 29, 13, 58, 57, 460, DateTimeKind.Local).AddTicks(7187), "ehvn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 843, DateTimeKind.Utc).AddTicks(4729),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 944, DateTimeKind.Utc).AddTicks(65));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 843, DateTimeKind.Utc).AddTicks(4095),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 943, DateTimeKind.Utc).AddTicks(9129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 842, DateTimeKind.Utc).AddTicks(7869),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(4437));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 11, 29, 27, 842, DateTimeKind.Utc).AddTicks(7285),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(3589));
        }
    }
}
