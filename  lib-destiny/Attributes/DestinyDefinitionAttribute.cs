namespace Destiny.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DestinyDefinitionAttribute : Attribute
{
    protected string definitionName;

    public DestinyDefinitionAttribute(string definitionName)
    {
        this.definitionName = definitionName;

    }

    public string DefinitionName
    {
        get { return definitionName; }
    }
}