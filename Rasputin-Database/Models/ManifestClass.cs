﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rasputin.Database.Models;

[Table("manifest_classes")]
[Index("Hash", IsUnique = true)]
[Index("Index", Name = "manifest_classes_index_index")]
[Index("Type", Name = "manifest_classes_type_index")]
public partial class ManifestClass
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("hash")]
    public uint Hash { get; set; }

    [Column("index")]
    public int Index { get; set; }

    [Column("type")]
    public int Type { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Column("created_at")]
    public long CreatedAt { get; set; }

    [Column("updated_at")]
    public long UpdatedAt { get; set; }

    [Column("deleted_at")]
    public long DeletedAt { get; set; }
}