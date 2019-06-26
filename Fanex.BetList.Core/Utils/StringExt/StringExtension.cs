namespace Fanex.BetList.Core.Utils.StringExt
{
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public static class StringExtension
    {
        public static string UppercaseFirst(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            char[] a = value.ToCharArray();
            a[0] = char.ToUpper(a[0], CultureInfo.CurrentCulture);
            return new string(a);
        }

        public static int Count(this string value, string countText)
        {
            return (value.Length - value.Replace(countText, string.Empty).Length) / countText.Length;
        }

        public static int GetLineBreakTotal(this string value)
        {
            return value.Count("\n") + 1;
        }

        public static bool HasXmlTag(this string value, string xmlTag)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var xmlDoc = value.ParseXml();

                return xmlDoc.Descendants(xmlTag)?.FirstOrDefault() != null;
            }

            return false;
        }

        public static bool HasAttributeInXmlTag(this string value, string xmlTag, string attributeName)
        {
            if (value.HasXmlTag(xmlTag))
            {
                var xmlDoc = value.ParseXml();

                return xmlDoc.Descendants(xmlTag).FirstOrDefault().Attribute(attributeName) != null;
            }

            return false;
        }

        public static string GetXmlTagAttributeValue(this string value, string xmlTag, string attributeName)
        {
            if (value.HasXmlTag(xmlTag))
            {
                var xmlDoc = value.ParseXml();

                return xmlDoc.Descendants(xmlTag).FirstOrDefault().Attribute(attributeName)?.Value;
            }

            return string.Empty;
        }

        public static XDocument ParseXml(this string value) => XDocument.Parse($"<root>{value}</root>");

        public static string StripHTML(this string value)
        {
            return Regex.Replace(value, "<.*?>", string.Empty);
        }

        public static string[] SplitHTMLIntoRows(this string html)
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