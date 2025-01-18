using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasputin.Database.Migrations
{
    /// <inheritdoc />
    public partial class MemberCharacterTriumphAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "member_character_triumphs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    character_id = table.Column<long>(type: "bigint", nullable: false),
                    hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_character_triumphs", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_member_character_triumphs_membership_id_hash_character_id",
                table: "member_character_triumphs",
                columns: new[] { "membership_id", "hash", "character_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "member_character_triumphs_character_id_index",
                table: "member_character_triumphs",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "member_character_triumphs_hash_index",
                table: "member_character_triumphs",
                column: "hash");

            migrationBuilder.CreateIndex(
                name: "member_character_triumphs_membership_id_index",
                table: "member_character_triumphs",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "member_character_triumphs_state_index",
                table: "member_character_triumphs",
                column: "state");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_character_triumphs");
        }
    }
}
