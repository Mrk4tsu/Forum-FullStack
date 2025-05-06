using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(5607),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 675, DateTimeKind.Utc).AddTicks(7205));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(4590),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 675, DateTimeKind.Utc).AddTicks(6411));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(4580),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(8608));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(3573),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(7716));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(4304),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(402));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(3194),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(9530));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3978),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(1444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3012),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(498));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(4881),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 671, DateTimeKind.Utc).AddTicks(3928));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(3839),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 671, DateTimeKind.Utc).AddTicks(3019));

            migrationBuilder.AddColumn<byte>(
                name: "category_id",
                table: "mods",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { (byte)1, "Chi tiết danh mục", "android" },
                    { (byte)2, "Chi tiết danh mục", "pc" },
                    { (byte)3, "Chi tiết danh mục", "java" },
                    { (byte)4, "Chi tiết danh mục", "ios" }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "7f176aa5-ce64-43af-b821-32dbc503360c", "AQAAAAIAAYagAAAAEAmVpK57lECa0wh+r2HMDlTF6KLOJlP9K5K4EPRyKQpa7ez4/FRWo9pRHMglOr+9yw==", new DateTime(2025, 4, 29, 14, 46, 52, 936, DateTimeKind.Local).AddTicks(4219), new DateTime(2025, 4, 29, 14, 46, 52, 936, DateTimeKind.Local).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "5dc0b7fb-5957-4c65-bd6c-cbf5b90cbabb", "AQAAAAIAAYagAAAAEIzWPw2IbRzkr3AOre1Eo5128Wa7cVURI0kMxdgwgEO3U2xtnc0UwzqesQAczOTr6A==", new DateTime(2025, 4, 29, 14, 46, 53, 25, DateTimeKind.Local).AddTicks(8579), new DateTime(2025, 4, 29, 14, 46, 53, 25, DateTimeKind.Local).AddTicks(8580) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "090debc8-c7f6-472e-acaa-5fc31d0115f5", "AQAAAAIAAYagAAAAEKy3b89+j7rkcv+WtYaFqtl+NNXGdeYXkSlhm0rWrQjLNvo9L9rdZtaTE/ep2W6VwQ==", new DateTime(2025, 4, 29, 14, 46, 53, 186, DateTimeKind.Local).AddTicks(1281), new DateTime(2025, 4, 29, 14, 46, 53, 186, DateTimeKind.Local).AddTicks(1299) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "df0cb9b3-54cf-4e73-b684-68f024be7730", "AQAAAAIAAYagAAAAEFGPsPBu4Ky0FRhrwWkwv99Lv4wbeijVpEQPlMclOQNsku4LvufIhFyHaD8Zq76uZg==", new DateTime(2025, 4, 29, 14, 46, 53, 265, DateTimeKind.Local).AddTicks(6920), new DateTime(2025, 4, 29, 14, 46, 53, 265, DateTimeKind.Local).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "a3d0ea85-e459-4872-a042-6db50d8a5289", "AQAAAAIAAYagAAAAEOWaxLkUz6a1F4he9nXVYMbG+W1Qm6fmPHz90vTOSz+0WKwlFEi1sfzvG9LvNdWTfA==", new DateTime(2025, 4, 29, 14, 46, 53, 345, DateTimeKind.Local).AddTicks(5861), new DateTime(2025, 4, 29, 14, 46, 53, 345, DateTimeKind.Local).AddTicks(5876) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "aa8e85bd-7a8f-4081-8230-bd2d67a828dd", "AQAAAAIAAYagAAAAEJpinvcYmRVsFoVHmg1kS3GKcuagSI7uFpaN9Uc8QYXog44p+pcQK0co9bbd+L2fIA==", new DateTime(2025, 4, 29, 14, 46, 53, 421, DateTimeKind.Local).AddTicks(3834), new DateTime(2025, 4, 29, 14, 46, 53, 421, DateTimeKind.Local).AddTicks(3851) });

            migrationBuilder.CreateIndex(
                name: "IX_mods_category_id",
                table: "mods",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mods_categories_category_id",
                table: "mods",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mods_categories_category_id",
                table: "mods");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropIndex(
                name: "IX_mods_category_id",
                table: "mods");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "mods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 675, DateTimeKind.Utc).AddTicks(7205),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(5607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 675, DateTimeKind.Utc).AddTicks(6411),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(4590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(8608),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(4580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(7716),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(3573));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(402),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(4304));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(9530),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(3194));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(1444),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(498),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3012));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 671, DateTimeKind.Utc).AddTicks(3928),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(4881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 671, DateTimeKind.Utc).AddTicks(3019),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(3839));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "8bd0d0dc-a6cd-419c-a878-0617c16b0768", "AQAAAAIAAYagAAAAEIwIpMnDwImAatUamj07WwysaCs30yQUXBGtqU8wwgyJK2UuMcoBWaXKEHmqxQREyQ==", new DateTime(2025, 4, 29, 14, 29, 29, 676, DateTimeKind.Local).AddTicks(4245), new DateTime(2025, 4, 29, 14, 29, 29, 676, DateTimeKind.Local).AddTicks(4245) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "61af5acb-191c-4771-9b43-22fe9e14bddd", "AQAAAAIAAYagAAAAEBnW2i8YkFlyr80bFTMtdSYikB1YBj1M6ooywbc1CVzkGIAjfvscO9XUl7xD4Z9KSg==", new DateTime(2025, 4, 29, 14, 29, 29, 746, DateTimeKind.Local).AddTicks(5874), new DateTime(2025, 4, 29, 14, 29, 29, 746, DateTimeKind.Local).AddTicks(5874) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "134a5a2c-93e3-435a-bd88-b4d0242b60f9", "AQAAAAIAAYagAAAAEAsS2wNw8nvF2B6GiJsMPt2H+vRonYiklh7GogPubITVrqr4H2Qen8crcXJ1/XOMNw==", new DateTime(2025, 4, 29, 14, 29, 29, 886, DateTimeKind.Local).AddTicks(8795), new DateTime(2025, 4, 29, 14, 29, 29, 886, DateTimeKind.Local).AddTicks(8811) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "82416f83-100e-402f-8c57-a61a9a08e327", "AQAAAAIAAYagAAAAELz3y46/mf7YcyPOnOgkomNZeOS92rY1cuzQMGAnNsIv81bgluXPgf8D4omtwc8UBQ==", new DateTime(2025, 4, 29, 14, 29, 29, 953, DateTimeKind.Local).AddTicks(7496), new DateTime(2025, 4, 29, 14, 29, 29, 953, DateTimeKind.Local).AddTicks(7512) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "49c7ae18-9626-4deb-b0f9-7acb35e3e639", "AQAAAAIAAYagAAAAELAC4ZT7a2YsyPIjlKSEsXgF9HC0p877a1idHZpJDp4PJJOV/bEEhAp9L23NlxaRIw==", new DateTime(2025, 4, 29, 14, 29, 30, 21, DateTimeKind.Local).AddTicks(9952), new DateTime(2025, 4, 29, 14, 29, 30, 21, DateTimeKind.Local).AddTicks(9968) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "b89b0d39-7628-4a00-8b41-2da90391af9b", "AQAAAAIAAYagAAAAEIZaCvIdWZRWEQgpWdU8H/0ec3vDuxugvmYPPcyjOnpVh6AnoNy/BbieexrrE0z0uA==", new DateTime(2025, 4, 29, 14, 29, 30, 89, DateTimeKind.Local).AddTicks(6547), new DateTime(2025, 4, 29, 14, 29, 30, 89, DateTimeKind.Local).AddTicks(6563) });
        }
    }
}
