using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasputin.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clan_members",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    group_id = table.Column<long>(type: "bigint", nullable: false),
                    group_role = table.Column<int>(type: "int", nullable: false),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    platform = table.Column<int>(type: "int", nullable: false),
                    joined_at = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clan_members", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clans",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    group_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    slug = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_network = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    motto = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    about = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    call_sign = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clans", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "instance_members",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    instance_id = table.Column<long>(type: "bigint", nullable: false),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    platform = table.Column<int>(type: "int", nullable: false),
                    character_id = table.Column<long>(type: "bigint", nullable: false),
                    class_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    class_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    emblem_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    light_level = table.Column<int>(type: "int", nullable: false),
                    clan_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_tag = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    completion_reason = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instance_members", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "instances",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    instance_id = table.Column<long>(type: "bigint", nullable: false),
                    occurred_at = table.Column<long>(type: "bigint", nullable: false),
                    starting_phase_index = table.Column<int>(type: "int", nullable: false),
                    started_from_beginning = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    activity_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    activity_director_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    is_private = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    completion_reasons = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instances", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manifest_activities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    activity_type = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    index = table.Column<int>(type: "int", nullable: false),
                    is_pvp = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    image_url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    matchmaking_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    fireteam_min_size = table.Column<int>(type: "int", nullable: false),
                    fireteam_max_size = table.Column<int>(type: "int", nullable: false),
                    max_players = table.Column<int>(type: "int", nullable: false),
                    requires_guardian_oath = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manifest_activities", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manifest_activity_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hash = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon_url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false),
                    index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manifest_activity_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manifest_classes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    index = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manifest_classes", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manifest_seasons",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pass_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    starts_at = table.Column<long>(type: "bigint", nullable: false),
                    ends_at = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manifest_seasons", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manifest_triumphs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_title = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    gilded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manifest_triumphs", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "member_activities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    character_id = table.Column<long>(type: "bigint", nullable: false),
                    platform_played = table.Column<int>(type: "int", nullable: false),
                    activity_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    activity_hash_director = table.Column<uint>(type: "int unsigned", nullable: false),
                    instance_id = table.Column<long>(type: "bigint", nullable: false),
                    mode = table.Column<int>(type: "int", nullable: false),
                    modes = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    @private = table.Column<bool>(name: "private", type: "tinyint(1)", nullable: false),
                    occurred_at = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_activities", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "member_activity_stats",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    character_id = table.Column<long>(type: "bigint", nullable: false),
                    instance_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    value_display = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_activity_stats", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "member_characters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    platform = table.Column<int>(type: "int", nullable: false),
                    character_id = table.Column<long>(type: "bigint", nullable: false),
                    class_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    light = table.Column<int>(type: "int", nullable: false),
                    last_played_at = table.Column<long>(type: "bigint", nullable: false),
                    minutes_played_session = table.Column<int>(type: "int", nullable: false),
                    minutes_played_lifetime = table.Column<int>(type: "int", nullable: false),
                    emblem_hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    emblem_url = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    emblem_background_url = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_characters", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "member_triumphs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    hash = table.Column<uint>(type: "int unsigned", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    times_completed = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_triumphs", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membership_id = table.Column<long>(type: "bigint", nullable: false),
                    platform = table.Column<int>(type: "int", nullable: false),
                    display_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_name_global = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guardian_rank_current = table.Column<int>(type: "int", nullable: false),
                    guardian_rank_lifetime = table.Column<int>(type: "int", nullable: false),
                    last_played_at = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<long>(type: "bigint", nullable: false),
                    deleted_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "clan_members_group_id_group_role_index",
                table: "clan_members",
                columns: new[] { "group_id", "group_role" });

            migrationBuilder.CreateIndex(
                name: "clan_members_group_id_index",
                table: "clan_members",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "clan_members_group_role_index",
                table: "clan_members",
                column: "group_role");

            migrationBuilder.CreateIndex(
                name: "clan_members_joined_at_index",
                table: "clan_members",
                column: "joined_at");

            migrationBuilder.CreateIndex(
                name: "clan_members_membership_id_index",
                table: "clan_members",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "IX_clan_members_group_id_group_role_membership_id",
                table: "clan_members",
                columns: new[] { "group_id", "group_role", "membership_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clans_group_id_index",
                table: "clans",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "clans_is_network_index",
                table: "clans",
                column: "is_network");

            migrationBuilder.CreateIndex(
                name: "clans_name_index",
                table: "clans",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "clans_slug_index",
                table: "clans",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "IX_clans_group_id",
                table: "clans",
                column: "group_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "instance_members_character_id_completed_index",
                table: "instance_members",
                columns: new[] { "character_id", "completed" });

            migrationBuilder.CreateIndex(
                name: "instance_members_character_id_index",
                table: "instance_members",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "instance_members_class_hash_index",
                table: "instance_members",
                column: "class_hash");

            migrationBuilder.CreateIndex(
                name: "instance_members_class_name_completed_index",
                table: "instance_members",
                columns: new[] { "class_name", "completed" });

            migrationBuilder.CreateIndex(
                name: "instance_members_class_name_index",
                table: "instance_members",
                column: "class_name");

            migrationBuilder.CreateIndex(
                name: "instance_members_completed_index",
                table: "instance_members",
                column: "completed");

            migrationBuilder.CreateIndex(
                name: "instance_members_completion_reason_index",
                table: "instance_members",
                column: "completion_reason");

            migrationBuilder.CreateIndex(
                name: "instance_members_emblem_hash_index",
                table: "instance_members",
                column: "emblem_hash");

            migrationBuilder.CreateIndex(
                name: "instance_members_instance_id_completed_index",
                table: "instance_members",
                columns: new[] { "instance_id", "completed" });

            migrationBuilder.CreateIndex(
                name: "instance_members_instance_id_completed_membership_id_index",
                table: "instance_members",
                columns: new[] { "instance_id", "completed", "membership_id" });

            migrationBuilder.CreateIndex(
                name: "instance_members_instance_id_completed_membership_id_index2",
                table: "instance_members",
                columns: new[] { "instance_id", "completed", "membership_id" });

            migrationBuilder.CreateIndex(
                name: "instance_members_instance_id_index",
                table: "instance_members",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "instance_members_membership_id_completed_index",
                table: "instance_members",
                columns: new[] { "membership_id", "completed" });

            migrationBuilder.CreateIndex(
                name: "instance_members_membership_id_index",
                table: "instance_members",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "instance_members_platform_index",
                table: "instance_members",
                column: "platform");

            migrationBuilder.CreateIndex(
                name: "IX_instance_members_membership_id_instance_id_character_id",
                table: "instance_members",
                columns: new[] { "membership_id", "instance_id", "character_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "instances_activity_director_hash_index",
                table: "instances",
                column: "activity_director_hash");

            migrationBuilder.CreateIndex(
                name: "instances_activity_hash_index",
                table: "instances",
                column: "activity_hash");

            migrationBuilder.CreateIndex(
                name: "instances_competed_index",
                table: "instances",
                column: "completed");

            migrationBuilder.CreateIndex(
                name: "instances_completion_reasons_index",
                table: "instances",
                column: "completion_reasons");

            migrationBuilder.CreateIndex(
                name: "instances_completion_reasons_instance_id_index",
                table: "instances",
                columns: new[] { "completion_reasons", "instance_id" });

            migrationBuilder.CreateIndex(
                name: "instances_instance_id_competed_index",
                table: "instances",
                columns: new[] { "instance_id", "completed" });

            migrationBuilder.CreateIndex(
                name: "instances_instance_id_index",
                table: "instances",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "instances_instance_id_started_from_beginning_index",
                table: "instances",
                columns: new[] { "instance_id", "started_from_beginning" });

            migrationBuilder.CreateIndex(
                name: "instances_is_private_index",
                table: "instances",
                column: "is_private");

            migrationBuilder.CreateIndex(
                name: "instances_occurred_at_index",
                table: "instances",
                column: "occurred_at");

            migrationBuilder.CreateIndex(
                name: "instances_started_from_beginning_index",
                table: "instances",
                column: "started_from_beginning");

            migrationBuilder.CreateIndex(
                name: "instances_starting_phase_index_index",
                table: "instances",
                column: "starting_phase_index");

            migrationBuilder.CreateIndex(
                name: "IX_instances_instance_id",
                table: "instances",
                column: "instance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_manifest_activities_hash",
                table: "manifest_activities",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "manifest_activities_activity_type_index",
                table: "manifest_activities",
                column: "activity_type");

            migrationBuilder.CreateIndex(
                name: "manifest_activities_hash_index",
                table: "manifest_activities",
                column: "hash");

            migrationBuilder.CreateIndex(
                name: "manifest_activities_index_index",
                table: "manifest_activities",
                column: "index");

            migrationBuilder.CreateIndex(
                name: "manifest_activities_is_pvp_index",
                table: "manifest_activities",
                column: "is_pvp");

            migrationBuilder.CreateIndex(
                name: "manifest_activities_matchmaking_enabled_index",
                table: "manifest_activities",
                column: "matchmaking_enabled");

            migrationBuilder.CreateIndex(
                name: "manifest_activities_name_index",
                table: "manifest_activities",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_manifest_activity_types_hash",
                table: "manifest_activity_types",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_manifest_classes_hash",
                table: "manifest_classes",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "manifest_classes_index_index",
                table: "manifest_classes",
                column: "index");

            migrationBuilder.CreateIndex(
                name: "manifest_classes_type_index",
                table: "manifest_classes",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_manifest_seasons_hash",
                table: "manifest_seasons",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "manifest_seasons_hash_index",
                table: "manifest_seasons",
                column: "hash");

            migrationBuilder.CreateIndex(
                name: "manifest_seasons_name_index",
                table: "manifest_seasons",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "manifest_seasons_number_index",
                table: "manifest_seasons",
                column: "number");

            migrationBuilder.CreateIndex(
                name: "IX_manifest_triumphs_hash",
                table: "manifest_triumphs",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "manifest_triumphs_gilded_index",
                table: "manifest_triumphs",
                column: "gilded");

            migrationBuilder.CreateIndex(
                name: "manifest_triumphs_hash_index",
                table: "manifest_triumphs",
                column: "hash");

            migrationBuilder.CreateIndex(
                name: "manifest_triumphs_is_title_index",
                table: "manifest_triumphs",
                column: "is_title");

            migrationBuilder.CreateIndex(
                name: "manifest_triumphs_name_index",
                table: "manifest_triumphs",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "manifest_triumphs_title_index",
                table: "manifest_triumphs",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_member_activities_instance_id_membership_id_character_id",
                table: "member_activities",
                columns: new[] { "instance_id", "membership_id", "character_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "member_activities_activity_hash_director_index",
                table: "member_activities",
                column: "activity_hash_director");

            migrationBuilder.CreateIndex(
                name: "member_activities_activity_hash_index",
                table: "member_activities",
                column: "activity_hash");

            migrationBuilder.CreateIndex(
                name: "member_activities_character_id_index",
                table: "member_activities",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "member_activities_instance_id_index",
                table: "member_activities",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "member_activities_membership_id_index",
                table: "member_activities",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "member_activities_membership_id_instance_id_index",
                table: "member_activities",
                columns: new[] { "membership_id", "instance_id" });

            migrationBuilder.CreateIndex(
                name: "member_activities_mode_index",
                table: "member_activities",
                column: "mode");

            migrationBuilder.CreateIndex(
                name: "member_activities_modes_index",
                table: "member_activities",
                column: "modes");

            migrationBuilder.CreateIndex(
                name: "member_activities_occurred_at_index",
                table: "member_activities",
                column: "occurred_at");

            migrationBuilder.CreateIndex(
                name: "member_activities_platform_played_index",
                table: "member_activities",
                column: "platform_played");

            migrationBuilder.CreateIndex(
                name: "member_activities_private_index",
                table: "member_activities",
                column: "private");

            migrationBuilder.CreateIndex(
                name: "IX_member_activity_stats_membership_id_character_id_instance_id~",
                table: "member_activity_stats",
                columns: new[] { "membership_id", "character_id", "instance_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_character_id_index",
                table: "member_activity_stats",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_instance_id_index",
                table: "member_activity_stats",
                column: "instance_id");

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_membership_id_index",
                table: "member_activity_stats",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_membership_id_instance_id_index",
                table: "member_activity_stats",
                columns: new[] { "membership_id", "instance_id" });

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_membership_id_instance_id_name_index",
                table: "member_activity_stats",
                columns: new[] { "membership_id", "instance_id", "name" });

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_name_index",
                table: "member_activity_stats",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_value_display_index",
                table: "member_activity_stats",
                column: "value_display");

            migrationBuilder.CreateIndex(
                name: "member_activity_stats_value_index",
                table: "member_activity_stats",
                column: "value");

            migrationBuilder.CreateIndex(
                name: "IX_member_characters_character_id",
                table: "member_characters",
                column: "character_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_member_characters_platform_membership_id_character_id",
                table: "member_characters",
                columns: new[] { "platform", "membership_id", "character_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "member_characters_character_id_index",
                table: "member_characters",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "member_characters_class_hash_index",
                table: "member_characters",
                column: "class_hash");

            migrationBuilder.CreateIndex(
                name: "member_characters_emblem_background_url_index",
                table: "member_characters",
                column: "emblem_background_url");

            migrationBuilder.CreateIndex(
                name: "member_characters_emblem_hash_index",
                table: "member_characters",
                column: "emblem_hash");

            migrationBuilder.CreateIndex(
                name: "member_characters_emblem_url_index",
                table: "member_characters",
                column: "emblem_url");

            migrationBuilder.CreateIndex(
                name: "member_characters_membership_id_index",
                table: "member_characters",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "member_characters_platform_index",
                table: "member_characters",
                column: "platform");

            migrationBuilder.CreateIndex(
                name: "IX_member_triumphs_membership_id_hash",
                table: "member_triumphs",
                columns: new[] { "membership_id", "hash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "member_triumphs_hash_index",
                table: "member_triumphs",
                column: "hash");

            migrationBuilder.CreateIndex(
                name: "member_triumphs_membership_id_index",
                table: "member_triumphs",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "member_triumphs_state_index",
                table: "member_triumphs",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "member_triumphs_times_completed_index",
                table: "member_triumphs",
                column: "times_completed");

            migrationBuilder.CreateIndex(
                name: "IX_members_membership_id",
                table: "members",
                column: "membership_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "members_display_name_global_index",
                table: "members",
                column: "display_name_global");

            migrationBuilder.CreateIndex(
                name: "members_display_name_index",
                table: "members",
                column: "display_name");

            migrationBuilder.CreateIndex(
                name: "members_last_played_at_index",
                table: "members",
                column: "last_played_at");

            migrationBuilder.CreateIndex(
                name: "members_platform_index",
                table: "members",
                column: "platform");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clan_members");

            migrationBuilder.DropTable(
                name: "clans");

            migrationBuilder.DropTable(
                name: "instance_members");

            migrationBuilder.DropTable(
                name: "instances");

            migrationBuilder.DropTable(
                name: "manifest_activities");

            migrationBuilder.DropTable(
                name: "manifest_activity_types");

            migrationBuilder.DropTable(
                name: "manifest_classes");

            migrationBuilder.DropTable(
                name: "manifest_seasons");

            migrationBuilder.DropTable(
                name: "manifest_triumphs");

            migrationBuilder.DropTable(
                name: "member_activities");

            migrationBuilder.DropTable(
                name: "member_activity_stats");

            migrationBuilder.DropTable(
                name: "member_characters");

            migrationBuilder.DropTable(
                name: "member_triumphs");

            migrationBuilder.DropTable(
                name: "members");
        }
    }
}
