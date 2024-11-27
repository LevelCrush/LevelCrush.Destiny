using Destiny.Converters;
using Destiny.Models.Enums;

namespace Destiny.Tests;

public class Tests
{
    private static DestinyConfig Config { get; set; }

    [SetUp]
    public void Setup()
    {
        //   var config = new ConfigurationBu
        Config = DestinyConfig.Load();
    }

    [Test]
    public void TestConvertFrom()
    {
        var input = "{membershipId}";
        var converter = new DestinyRouteParamConverter();
        var converted = converter.ConvertFrom(input);
        Assert.That(converted, Is.EqualTo(DestinyRouteParam.PlatformMembershipID));
    }

    [Test]
    public void TestConvertTo()
    {
        var input = DestinyRouteParam.PlatformMembershipID;
        var converter = new DestinyRouteParamConverter();
        var converted = converter.ConvertTo(input, typeof(string));
        Assert.That(converted, Is.EqualTo("{membershipId}"));
    }
}