using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrKatsuWebAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddMod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(8608),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 944, DateTimeKind.Utc).AddTicks(65));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(7716),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 943, DateTimeKind.Utc).AddTicks(9129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(1444),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(4437));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(498),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(3589));

            migrationBuilder.CreateTable(
                name: "mods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 671, DateTimeKind.Utc).AddTicks(3019)),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 671, DateTimeKind.Utc).AddTicks(3928)),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mods", x => x.id);
                    table.ForeignKey(
                        name: "FK_mods_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(9530)),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(402)),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    mod_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_reactions_mods_mod_id",
                        column: x => x.mod_id,
                        principalTable: "mods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "urls",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    url_string = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 675, DateTimeKind.Utc).AddTicks(6411)),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 675, DateTimeKind.Utc).AddTicks(7205)),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    mod_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_urls", x => x.id);
                    table.ForeignKey(
                        name: "FK_urls_mods_mod_id",
                        column: x => x.mod_id,
                        principalTable: "mods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_mods_user_id",
                table: "mods",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_reactions_mod_id",
                table: "reactions",
                column: "mod_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UpdatedAt",
                table: "reactions",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                table: "reactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_urls_mod_id",
                table: "urls",
                column: "mod_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reactions");

            migrationBuilder.DropTable(
                name: "urls");

            migrationBuilder.DropTable(
                name: "mods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 944, DateTimeKind.Utc).AddTicks(65),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(8608));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "replies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 943, DateTimeKind.Utc).AddTicks(9129),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 674, DateTimeKind.Utc).AddTicks(7716));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(4437),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(1444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "posts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 29, 6, 58, 56, 942, DateTimeKind.Utc).AddTicks(3589),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 29, 7, 29, 29, 673, DateTimeKind.Utc).AddTicks(498));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "9379c5b6-25fb-4b33-8c83-38cd87e09ba3", "AQAAAAIAAYagAAAAEEVixtYh934ge2rJ8HUlnCo9yoCzYRESXaeGtq0g5RmbXS/CAkfCrHAM8ZPv1KjFPA==", new DateTime(2025, 4, 29, 13, 58, 56, 945, DateTimeKind.Local).AddTicks(1717), new DateTime(2025, 4, 29, 13, 58, 56, 945, DateTimeKind.Local).AddTicks(1718) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "401c667e-1f8c-41a3-b115-8f24a3955dde", "AQAAAAIAAYagAAAAEMyHlGlrk3YZ84VB6uXjyNeZnLOGwZnOjNFKFcsST5bRHoKdZ2FnXOmjqw1ps9QGAw==", new DateTime(2025, 4, 29, 13, 58, 57, 32, DateTimeKind.Local).AddTicks(2469), new DateTime(2025, 4, 29, 13, 58, 57, 32, DateTimeKind.Local).AddTicks(2473) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "5e37fcb8-0383-44b2-a9e6-adf09a9c11ee", "AQAAAAIAAYagAAAAECdTj6Kx+c1alSoZQjn6c+OchC9TEvqkmMw3M+GgFjSFKrBnE5UK+YPrq6emLopczw==", new DateTime(2025, 4, 29, 13, 58, 57, 201, DateTimeKind.Local).AddTicks(6263), new DateTime(2025, 4, 29, 13, 58, 57, 201, DateTimeKind.Local).AddTicks(6279) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "8756e2c7-dd62-42a2-b5cd-ef766cc47bcb", "AQAAAAIAAYagAAAAEGoV8MB13ZJ+ZBo25qjYUr7VVIY851fp677z7AfS6+mvXHiW0mkqYyH46X3eyQL59w==", new DateTime(2025, 4, 29, 13, 58, 57, 290, DateTimeKind.Local).AddTicks(5877), new DateTime(2025, 4, 29, 13, 58, 57, 290, DateTimeKind.Local).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "cc6e3352-c109-433a-9e8a-d00440ab0f55", "AQAAAAIAAYagAAAAEJZFzwNQL1u1f+aUQ1bVD7UCFy7VD8YTmrfBSIfR2xb4Nv75sTiNiDOu0jxpN/i4ig==", new DateTime(2025, 4, 29, 13, 58, 57, 374, DateTimeKind.Local).AddTicks(7652), new DateTime(2025, 4, 29, 13, 58, 57, 374, DateTimeKind.Local).AddTicks(7684) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "concurrency_stamp", "password_hash", "time_created", "time_updated" },
                values: new object[] { "5f51214c-4eca-4606-9038-0197ca2bfead", "AQAAAAIAAYagAAAAEAvGMgWp0YK7u/MS1EDGLmS5TfGn6qAwzengORhAvMjTdLgBocZRlY4cAltRZPbOmA==", new DateTime(2025, 4, 29, 13, 58, 57, 460, DateTimeKind.Local).AddTicks(7167), new DateTime(2025, 4, 29, 13, 58, 57, 460, DateTimeKind.Local).AddTicks(7187) });
        }
    }
}
