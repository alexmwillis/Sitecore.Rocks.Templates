namespace Sitecore.Rocks.Templates.Data.Template
{
    public interface ISitecoreTemplateField
    {
        string Id { get; }

        string Name { get; }

        string Type { get; }

        string SortOrder { get; }
    }
}
