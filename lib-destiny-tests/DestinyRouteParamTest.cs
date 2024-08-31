using LevelCrush.Destiny.Converters;
using LevelCrush.Destiny.Models.Enums;

namespace LevelCrush.Destiny.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestConvertFrom()
    {
        var input = "{membershipId}";
        var converter = new DestinyRouteParamConverter();
        var converted = converter.ConvertFrom(input);
        Assert.AreEqual(converted, DestinyRouteParam.PlatformMembershipID);
    }

    [Test]
    public void TestConvertTo()
    {
        var input = DestinyRouteParam.PlatformMembershipID;
        var converter = new DestinyRouteParamConverter();
        var converted = converter.ConvertTo(input, typeof(string));
        Assert.AreEqual(converted, "{membershipId}");
    }
}