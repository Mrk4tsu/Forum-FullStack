using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPropMod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(9743),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(5607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(8908),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(4590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(622),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(4580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(9629),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(3573));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(1046),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(4304));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(156),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(3194));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(2564),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(1553),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3012));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(2895),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(4881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(1980),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(3839));

            migrationBuilder.AddColumn<bool>(
                name: "is_private",
                table: "mods",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "21b1255e-a151-491e-86f7-dfda42d8a148", "AQAAAAIAAYagAAAAEEgVBtLYIhc1Wmz+YAk/jqAYNdkvLoFUaBbX4LdI8kv7tFfPPYuBhUL7y12ZwsMeqw==", new DateTime(2025, 4, 29, 15, 8, 26, 946, DateTimeKind.Local).AddTicks(7797), new DateTime(2025, 4, 29, 15, 8, 26, 946, DateTimeKind.Local).AddTicks(7797) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "37249530-2b62-4793-89fe-306a21e64dc8", "AQAAAAIAAYagAAAAEAl4oTP5WcY1aynxlCO0DFlBA8whX5yw/CcecwwJzC+h2fQ3ySDG2DZAJOSH37I5HA==", new DateTime(2025, 4, 29, 15, 8, 27, 12, DateTimeKind.Local).AddTicks(3473), new DateTime(2025, 4, 29, 15, 8, 27, 12, DateTimeKind.Local).AddTicks(3473) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "28c961ea-1a45-49c6-ba9f-c888b73d0bfa", "AQAAAAIAAYagAAAAEOL+tVhN0EL1TRA55NjxlSmTRL8fhUl/IbjGD+cZIA9qvq2YZtpWFvvGpa26QvbVvA==", new DateTime(2025, 4, 29, 15, 8, 27, 169, DateTimeKind.Local).AddTicks(9659), new DateTime(2025, 4, 29, 15, 8, 27, 169, DateTimeKind.Local).AddTicks(9681) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "d54c5582-0129-4afe-817c-449fffa29069", "AQAAAAIAAYagAAAAEG2mVMq7NUOMgw2kLkTv/GVrZuDLVlTmq+espm+M+w/U6CPgP5gHMPX+dVOOi2i6ag==", new DateTime(2025, 4, 29, 15, 8, 27, 251, DateTimeKind.Local).AddTicks(1253), new DateTime(2025, 4, 29, 15, 8, 27, 251, DateTimeKind.Local).AddTicks(1270) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "4e5f9d7e-266c-4339-80b0-71a240c0311d", "AQAAAAIAAYagAAAAENPMWeyU/Uxe9MRV7/zskRnRr2l8G9a4ObUeexTmFwCd+fH68Q5IK8AEx0HpSealTQ==", new DateTime(2025, 4, 29, 15, 8, 27, 324, DateTimeKind.Local).AddTicks(9482), new DateTime(2025, 4, 29, 15, 8, 27, 324, DateTimeKind.Local).AddTicks(9503) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "cfa01993-83e2-416b-9f1a-add8dd6ddd22", "AQAAAAIAAYagAAAAENaR/Vv8kE8usxCZtDMMFQCBLARG1c1vkxBEV0D76/Wfk3F1WwozoQYc7936LuQqRA==", new DateTime(2025, 4, 29, 15, 8, 27, 403, DateTimeKind.Local).AddTicks(9258), new DateTime(2025, 4, 29, 15, 8, 27, 403, DateTimeKind.Local).AddTicks(9292) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_private",
                table: "mods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(5607),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 935, DateTimeKind.Utc).AddTicks(4590),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(4580),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 934, DateTimeKind.Utc).AddTicks(3573),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(9629));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(4304),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(1046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 933, DateTimeKind.Utc).AddTicks(3194),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3978),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(2564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 932, DateTimeKind.Utc).AddTicks(3012),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(1553));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(4881),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(2895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 46, 52, 931, DateTimeKind.Utc).AddTicks(3839),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(1980));

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
        }
    }
}
