using System;
using System.IO;
using NUnit.Framework;

namespace Sitecore.Rocks.Templates.Tests
{
    public class BaseTemplateTest
    {
        protected void AssertThatTemplatesMatch(string actual, string expected)
        {
            try
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            catch (Exception)
            {
                CreatOutput(actual, $"./failed-tests/{GetType().Name.ToLower()}-actual.txt");
                CreatOutput(expected, $"./failed-tests/{GetType().Name.ToLower()}-expected.txt");
                throw;
            }
        }

        private static void CreatOutput(string str, string fileName)
        {
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            using (var writer = File.CreateText(fileName))
            {
                writer.Write(str);
            }
        }
    }
}
