using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Destiny.Converters;

namespace Destiny.Models.Enums;

[TypeConverter(typeof(DestinyDefinitionConverter))]
public enum DestinyDefinition
{
    [Display(Name="DestinyActivityDefinition")]
    Activity,
    
    [Display(Name="DestinyActivityTypeDefinition")]
    ActivityType,
    
    [Display(Name="DestinyClassDefinition")]
    Class,
    
    [Display(Name="DestinySeasonDefinition")]
    Seasons,
    
    [Display(Name="DestinyRecordDefinition")]
    Records
}