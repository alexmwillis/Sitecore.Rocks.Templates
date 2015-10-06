using System.IO;

namespace Sitecore.Rocks.Templates.Formatting
{
    public static class TemplateManager
    {
        private const string ResourcePath = "..//..//..//Resources//{0}.txt";

        public static string GetTemplate(string name)
        {
            var itemPath = string.Format(ResourcePath, name);

            if (File.Exists(itemPath))
            {
                return File.ReadAllText(itemPath);
            }
            return null;
        }
    }
}
