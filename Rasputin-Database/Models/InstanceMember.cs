﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rasputin.Database.Models;

[Table("instance_members")]
[Index("MembershipId", "InstanceId", "CharacterId", IsUnique = true)]
[Index("CharacterId", "Completed", Name = "instance_members_character_id_completed_index")]
[Index("CharacterId", Name = "instance_members_character_id_index")]
[Index("ClassHash", Name = "instance_members_class_hash_index")]
[Index("ClassName", "Completed", Name = "instance_members_class_name_completed_index")]
[Index("ClassName", Name = "instance_members_class_name_index")]
[Index("Completed", Name = "instance_members_completed_index")]
[Index("CompletionReason", Name = "instance_members_completion_reason_index")]
[Index("EmblemHash", Name = "instance_members_emblem_hash_index")]
[Index("InstanceId", "Completed", Name = "instance_members_instance_id_completed_index")]
[Index("InstanceId", "Completed", "MembershipId", Name = "instance_members_instance_id_completed_membership_id_index")]
[Index("InstanceId", "Completed", "MembershipId", Name = "instance_members_instance_id_completed_membership_id_index2")]
[Index("InstanceId", Name = "instance_members_instance_id_index")]
[Index("MembershipId", "Completed", Name = "instance_members_membership_id_completed_index")]
[Index("MembershipId", Name = "instance_members_membership_id_index")]
[Index("Platform", Name = "instance_members_platform_index")]
public partial class InstanceMember
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("instance_id")]
    public long InstanceId { get; set; }

    [Column("membership_id")]
    public long MembershipId { get; set; }

    [Column("platform")]
    public int Platform { get; set; }

    [Column("character_id")]
    public long CharacterId { get; set; }

    [Column("class_hash")]
    public uint ClassHash { get; set; }

    [Required]
    [Column("class_name")]
    public string ClassName { get; set; }

    [Column("emblem_hash")]
    public uint EmblemHash { get; set; }

    [Column("light_level")]
    public int LightLevel { get; set; }

    [Required]
    [Column("clan_name")]
    public string ClanName { get; set; }

    [Required]
    [Column("clan_tag")]
    public string ClanTag { get; set; }

    [Column("completed")]
    public bool Completed { get; set; }

    [Required]
    [Column("completion_reason")]
    public string CompletionReason { get; set; }

    [Column("created_at")]
    public long CreatedAt { get; set; }

    [Column("updated_at")]
    public long UpdatedAt { get; set; }

    [Column("deleted_at")]
    public long DeletedAt { get; set; }
}