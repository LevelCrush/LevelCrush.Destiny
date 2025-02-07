
// load api configuration information

using Destiny;
using Microsoft.EntityFrameworkCore;
using Rasputin_Redis;
using Rasputin_Server.Model;
using Rasputin.Database;

var destinyApiConfig = DestinyConfig.Load();
BungieClient.ApiKey = destinyApiConfig.ApiKey;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/member/{bungieName}/titles", async (string bungieName) =>
{

    var targetMember = $"{bungieName}";
    var membershipId = $"{bungieName}";

    await using (var db = await RasputinDatabase.Connect())
    {
        var res = await db.Database.SqlQuery<DestinyMemberTitleResponse>(@$"
            WITH
            target_members AS (
                SELECT
                    members.*
                FROM clans
                INNER JOIN clan_members ON clans.group_id = clan_members.group_id
                INNER JOIN members ON clan_members.membership_id = members.membership_id
                WHERE clans.is_network = 1
                AND (members.membership_id = {membershipId}  OR members.display_name_global LIKE {targetMember})
            ),
            triumph_titles AS (
                SELECT
                    triumphs.*
                FROM manifest_triumphs AS triumphs
                WHERE triumphs.is_title = 1
            ),
            titles_earned_account AS (
                SELECT
                    # GROUP_CONCAT(DISTINCT triumph_titles.hash SEPARATOR ' | ') AS hashes,
                    triumph_titles.title,
                    COALESCE(SUM(member_triumphs.state & 64 = 64), 0) AS amount
                FROM target_members
                INNER JOIN member_triumphs ON target_members.membership_id = member_triumphs.membership_id
                INNER JOIN triumph_titles ON member_triumphs.hash = triumph_titles.hash
                GROUP BY triumph_titles.title
            ),
            titles_earned_character AS (
                SELECT
                    # GROUP_CONCAT(DISTINCT triumph_titles.hash SEPARATOR ' | ') AS hashes,
                    member_character_triumphs.membership_id,
                    COUNT(DISTINCT member_character_triumphs.character_id) AS characters,
                    triumph_titles.title,
                    COALESCE(SUM(member_character_triumphs.state & 64 = 64), 0) AS amount
                FROM target_members
                INNER JOIN member_character_triumphs ON target_members.membership_id = member_character_triumphs.membership_id
                INNER JOIN triumph_titles ON member_character_triumphs.hash = triumph_titles.hash
                GROUP BY triumph_titles.title, member_character_triumphs.membership_id
            ),
            titles_earned_character_member AS (
                SELECT
                    titles_earned_character.membership_id,
                    titles_earned_character.title,
                    CEIL(titles_earned_character.amount / titles_earned_character.characters) AS amount
                FROM target_members
                INNER JOIN titles_earned_character ON target_members.membership_id = titles_earned_character.membership_id
                GROUP BY titles_earned_character.title, titles_earned_character.membership_id
            ),
            titles_earned_character_merged AS (
                 SELECT
                    titles_earned_character_member.title,
                    COALESCE(SUM(titles_earned_character_member.amount), 0) AS amount
                FROM titles_earned_character_member
                GROUP BY titles_earned_character_member.title
            ),
            titles_earned AS (
                SELECT
                   titles_earned_account.title,
                   titles_earned_account.amount
                FROM titles_earned_account
                UNION
                SELECT
                    titles_earned_character_merged.title,
                    titles_earned_character_merged.amount
                FROM titles_earned_character_merged
            )
            SELECT * FROM titles_earned
            WHERE amount > 0
            ORDER BY title ASC
            ").ToArrayAsync();

        return res;
    }
});



await app.RunAsync();
