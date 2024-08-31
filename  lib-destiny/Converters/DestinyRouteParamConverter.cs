using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using LevelCrush.Destiny.Extensions;
using LevelCrush.Destiny.Models.Enums;

namespace LevelCrush.Destiny.Converters;

public class DestinyRouteParamConverter : EnumConverter
{
    public DestinyRouteParamConverter() : base(typeof(DestinyRouteParam))
    {
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        var input = (string)value; // we only accept converting from a string 
        return Enum.GetValues<DestinyRouteParam>()
            .FirstOrDefault(x => x.GetAttribute<DisplayAttribute>().Name == input);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
        Type destinationType)
    {
        return ((DestinyRouteParam)value).GetAttribute<DisplayAttribute>().Name;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string);
    }
}