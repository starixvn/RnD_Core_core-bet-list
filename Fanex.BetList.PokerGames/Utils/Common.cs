namespace Fanex.BetList.Core.Utils
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Common
    {
        public static string[] SplitHtmlIntoRows(string html)
        {
            const string splitter = "</div>";
            var htmlTagMatching = new Regex("</?[^<>]*>");

            return Regex
                .Split(html, splitter)
                .Select(row => htmlTagMatching.Replace(row, string.Empty))
                .Where(row => row != string.Empty)
                .ToArray();
        }
    }
}