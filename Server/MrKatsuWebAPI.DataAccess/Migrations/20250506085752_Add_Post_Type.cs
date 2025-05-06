using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_Post_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 485, DateTimeKind.Utc).AddTicks(3440),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(1977));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 485, DateTimeKind.Utc).AddTicks(2508),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(712));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 484, DateTimeKind.Utc).AddTicks(1662),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(9042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 484, DateTimeKind.Utc).AddTicks(623),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(7822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 483, DateTimeKind.Utc).AddTicks(464),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 482, DateTimeKind.Utc).AddTicks(9481),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(4109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 481, DateTimeKind.Utc).AddTicks(8049),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 481, DateTimeKind.Utc).AddTicks(6910),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.AddColumn<bool>(
                name: "is_locked",
                table: "posts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "post_type",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 480, DateTimeKind.Utc).AddTicks(6817),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 480, DateTimeKind.Utc).AddTicks(5763),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "e32bf6a2-0e3e-4d69-9de2-31cee40dbc1b", "AQAAAAIAAYagAAAAEM5KyL9fciXu0n34iJ2gjbmswVmZq451R8o/VMy4QNQ3YUE9fsDLUGG5oHplbnjlbw==", new DateTime(2025, 5, 6, 15, 57, 51, 486, DateTimeKind.Local).AddTicks(3146), new DateTime(2025, 5, 6, 15, 57, 51, 486, DateTimeKind.Local).AddTicks(3147) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "2fecedcc-75b6-4f71-94b2-21d164a7bd90", "AQAAAAIAAYagAAAAEIxu1wbcau6VxfTEL5OBGEHFcYlMPvEYKs2TV0zQUFStE+R2xrBGeAgnh0Z0B4Yspg==", new DateTime(2025, 5, 6, 15, 57, 51, 567, DateTimeKind.Local).AddTicks(5198), new DateTime(2025, 5, 6, 15, 57, 51, 567, DateTimeKind.Local).AddTicks(5206) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "0d05eb02-72c5-4bb1-87f7-05384b177cec", "AQAAAAIAAYagAAAAEFO0V4FVnvU653UNkOc+f/7O+Sn3O3MFAcbyQsRFltyCClu3OGD9hVUltAk/nvqqFA==", new DateTime(2025, 5, 6, 15, 57, 51, 736, DateTimeKind.Local).AddTicks(4894), new DateTime(2025, 5, 6, 15, 57, 51, 736, DateTimeKind.Local).AddTicks(4917) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "9e366287-c6d1-46e5-8049-4c95f672061e", "AQAAAAIAAYagAAAAEKJA29qqJyn5pSx87mcPWwZjrVGhIbblu44zfgLxB9HkLlxQi8ZGZPNVTnkiUoTOcQ==", new DateTime(2025, 5, 6, 15, 57, 51, 818, DateTimeKind.Local).AddTicks(8282), new DateTime(2025, 5, 6, 15, 57, 51, 818, DateTimeKind.Local).AddTicks(8298) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "6ab4279b-45d9-4e15-8a34-39ef53f48be1", "AQAAAAIAAYagAAAAELOdHhWaeLTWQb8xdfnTkxgmZxgFIGWB6zIC/NDuw8GKtgud1T66nd9qP7ygAR2Zxw==", new DateTime(2025, 5, 6, 15, 57, 51, 902, DateTimeKind.Local).AddTicks(4463), new DateTime(2025, 5, 6, 15, 57, 51, 902, DateTimeKind.Local).AddTicks(4481) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "84aae0af-d06f-470f-b08b-662f7ae15665", "AQAAAAIAAYagAAAAEBkQNV3PzOpPJfFb7+0TguWMt6y6MUkY2oHu90iCtGJa+M6zvTpNUaw1GspYLEZNkw==", new DateTime(2025, 5, 6, 15, 57, 51, 989, DateTimeKind.Local).AddTicks(9039), new DateTime(2025, 5, 6, 15, 57, 51, 989, DateTimeKind.Local).AddTicks(9056) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "00fb23a0-9f9e-4e0b-ab9d-536f07602799", "AQAAAAIAAYagAAAAEM/RGihxO7p5YJncMuIH+c2Fm87XYdO3XPBOwvyzOXyAkVgkrUUY2YIWCX50uVwx2A==", new DateTime(2025, 5, 6, 15, 57, 51, 989, DateTimeKind.Local).AddTicks(9129), new DateTime(2025, 5, 6, 15, 57, 51, 989, DateTimeKind.Local).AddTicks(9129) });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_IsLocked",
                table: "posts",
                column: "is_locked");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_IsLocked",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "is_locked",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "post_type",
                table: "posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(1977),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 485, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "urls",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 510, DateTimeKind.Utc).AddTicks(712),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 485, DateTimeKind.Utc).AddTicks(2508));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(9042),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 484, DateTimeKind.Utc).AddTicks(1662));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 508, DateTimeKind.Utc).AddTicks(7822),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 484, DateTimeKind.Utc).AddTicks(623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(5415),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 483, DateTimeKind.Utc).AddTicks(464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "reactions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 507, DateTimeKind.Utc).AddTicks(4109),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 482, DateTimeKind.Utc).AddTicks(9481));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(2840),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 481, DateTimeKind.Utc).AddTicks(8049));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 506, DateTimeKind.Utc).AddTicks(1411),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 481, DateTimeKind.Utc).AddTicks(6910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(9373),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 480, DateTimeKind.Utc).AddTicks(6817));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "mods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 1, 16, 21, 30, 504, DateTimeKind.Utc).AddTicks(8078),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 5, 6, 8, 57, 51, 480, DateTimeKind.Utc).AddTicks(5763));

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

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "263da7d7-f262-4cb9-8693-bf0eb5c27aee", "AQAAAAIAAYagAAAAEKvFX9FSESLiWXuYCxuT+mhJyIFitWQAIQeK+aI3cKIyiioO9hZEuAujgr9gjcVcOg==", new DateTime(2025, 5, 1, 23, 21, 31, 23, DateTimeKind.Local).AddTicks(6740), new DateTime(2025, 5, 1, 23, 21, 31, 23, DateTimeKind.Local).AddTicks(6741) });
        }
    }
}
