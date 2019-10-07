using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OutlookCO2
{
    /// <summary>
    /// Replaces content in files.
    /// </summary>
    public class FileContentReplacer
    {
        /// <summary>
        /// Replaces the tokens on all .htm files in the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="data">The data.</param>
        public void Replace(string folder, Dictionary<string, string> data)
        {
            string[] files = Directory.GetFiles(folder, "*.htm", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                Console.WriteLine($"Parsing file {file}.");
                try
                {
                    string content = File.ReadAllText(file, CodePagesEncodingProvider.Instance.GetEncoding(1252));
                    bool found = false;
                    foreach (var item in data)
                    {
                        if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value)) continue;

                        var exp = @$"(?s)({{{item.Key}}})|(<span[\s\n]+id='?{item.Key}'?>.*?<\/\s?span>)";

                        // "[$1]" for capture replacements
                        var newContent = RegExReplace(content, exp, @$"<span id={item.Key}>{item.Value}</span>");
                        if (newContent != null)
                        {
                            Console.WriteLine($"  Setting {item.Key} as {item.Value}.");
                            found = true;
                            content = newContent;
                        }
                    }
                    if (found)
                    {
                        // Make files writable
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.WriteAllText(file, content, Encoding.UTF8);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string RegExReplace(string content, string regexExpression, string newContent)
        {
            Regex regex = new Regex(regexExpression);
            if (regex.IsMatch(content))
                return regex.Replace(content, newContent);
            else
                return null;
        }
    }
}
