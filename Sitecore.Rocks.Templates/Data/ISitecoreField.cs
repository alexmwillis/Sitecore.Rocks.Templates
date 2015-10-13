namespace Sitecore.Rocks.Templates.Data
{
    public interface ISitecoreField
    {
        string Name { get; }

        string Value { get; }

        bool IsStandardField { get; }
    }
}
