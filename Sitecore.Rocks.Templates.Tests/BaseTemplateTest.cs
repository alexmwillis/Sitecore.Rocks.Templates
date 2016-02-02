using System;
using System.IO;
using NUnit.Framework;

namespace Sitecore.Rocks.Templates.Tests
{
    public class BaseTemplateTest
    {
        protected void ClearFailedTestLogs()
        {
            foreach (var file in new DirectoryInfo("./failed-tests").GetFiles())
            {
                file.Delete();
            }
        }

        protected void AssertThatTemplatesMatch(string actual, string expected)
        {
            var fileNameActual = $"./failed-tests/{GetType().Name.ToLower()}-actual.txt";
            var fileNameExpected = $"./failed-tests/{GetType().Name.ToLower()}-expected.txt";
            File.Delete(fileNameActual);
            File.Delete(fileNameExpected);
            try
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
            catch (Exception)
            {
                CreatOutput(actual, fileNameActual);
                CreatOutput(expected, fileNameExpected);
                throw;
            }
        }

        private static void CreatOutput(string str, string fileName)
        {
            if (!Directory.Exists(fileName))
            {
                var dir = Path.GetDirectoryName(fileName);
                if (dir == null)
                {
                    throw new ArgumentException("Invalid file name");
                }
                Directory.CreateDirectory(dir);
            }
            using (var writer = File.CreateText(fileName))
            {
                writer.Write(str);
            }
        }
    }
}
