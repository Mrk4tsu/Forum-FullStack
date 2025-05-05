using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_HieuNgu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(1977),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(712),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(9042),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(7822),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(9629));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(5415),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(1046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(4109),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(2840),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(2564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(1411),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(1553));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(9373),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(2895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(8078),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "4704099e-05ec-4dd1-a3f3-3990a84e1e62", "AQAAAAIAAYagAAAAEH8+yp8aKzUu09WulKDOgPR9UIuj2cY5LLmv7w/AXLVbDYBC3dz3NlDL+ir/LM+vlg==", new DateTime(2025, 5, 1, 23, 21, 30, 511, DateTimeKind.Local).AddTicks(3538), new DateTime(2025, 5, 1, 23, 21, 30, 511, DateTimeKind.Local).AddTicks(3539) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "a6d22d16-f47b-48f8-8a6b-5655064e3511", "AQAAAAIAAYagAAAAEJY0sJXsd5ZrQNQOzZZMckv/mOhceKcrQvpXX/+/5b3PK/EKVZ6/jQWetXLbuf3qEQ==", new DateTime(2025, 5, 1, 23, 21, 30, 596, DateTimeKind.Local).AddTicks(9763), new DateTime(2025, 5, 1, 23, 21, 30, 596, DateTimeKind.Local).AddTicks(9764) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "68e2c868-df11-43ed-8b17-d608da4c6573", "AQAAAAIAAYagAAAAEBUHSaD+UzuSm5N2ZpioscoTjxhiWFJUg61/4pkb+6FE+OOTyjcYJXfJZr5/cJDN3g==", new DateTime(2025, 5, 1, 23, 21, 30, 762, DateTimeKind.Local).AddTicks(4702), new DateTime(2025, 5, 1, 23, 21, 30, 762, DateTimeKind.Local).AddTicks(4716) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "9973f087-e8d3-4d0d-b02b-d0608e41d0be", "AQAAAAIAAYagAAAAEDLxy7ngfPlyWTOIdF5ECwOR/bTDe+t61OdxOqMewJUqjKLRmSpWb6KlbZtSyL6zLA==", new DateTime(2025, 5, 1, 23, 21, 30, 848, DateTimeKind.Local).AddTicks(1313), new DateTime(2025, 5, 1, 23, 21, 30, 848, DateTimeKind.Local).AddTicks(1346) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "c3023fa2-75f7-4892-ad80-f62880b6cc48", "AQAAAAIAAYagAAAAEHJG0c85A85CAm+p7NOlwLp8wueVgqlos16Xlkg73fF74rSub+nZsQ/PlDkdPDy76g==", new DateTime(2025, 5, 1, 23, 21, 30, 934, DateTimeKind.Local).AddTicks(3773), new DateTime(2025, 5, 1, 23, 21, 30, 934, DateTimeKind.Local).AddTicks(3788) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "45ef2b56-603f-4110-b343-ce55df1d92a1", "AQAAAAIAAYagAAAAEEX+UQXBrBqN988cZ+tKdYIvtZywThdM0caGQmRP9dy3qrLNaso865kC8IOCMx8S+w==", new DateTime(2025, 5, 1, 23, 21, 31, 23, DateTimeKind.Local).AddTicks(6661), new DateTime(2025, 5, 1, 23, 21, 31, 23, DateTimeKind.Local).AddTicks(6674) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "concurrency_stamp", "email", "lockout_end", "normalized_email", "normalized_username", "password_hash", "time_created", "time_updated", "username" },
                values: new object[] { 13, "assets/images/avatars/vip/1360.png", "263da7d7-f262-4cb9-8693-bf0eb5c27aee", "tanhieuno4@mail.com", null, "TANHIEUNO4@EMAIL.COM", "TANHIEUNO4", "AQAAAAIAAYagAAAAEKvFX9FSESLiWXuYCxuT+mhJyIFitWQAIQeK+aI3cKIyiioO9hZEuAujgr9gjcVcOg==", new DateTime(2025, 5, 1, 23, 21, 31, 23, DateTimeKind.Local).AddTicks(6740), new DateTime(2025, 5, 1, 23, 21, 31, 23, DateTimeKind.Local).AddTicks(6741), "tanhieuno4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(9743),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(1977));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(8908),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(712));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 945, DateTimeKind.Utc).AddTicks(622),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(9042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(9629),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(7822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(1046),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 944, DateTimeKind.Utc).AddTicks(156),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(4109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(2564),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 943, DateTimeKind.Utc).AddTicks(1553),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(2895),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 8, 8, 26, 942, DateTimeKind.Utc).AddTicks(1980),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(8078));

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
    }
}
