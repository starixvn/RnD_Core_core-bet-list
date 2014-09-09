namespace Fanex.BetList.Core
{
    using System.Globalization;

    /// <summary>
    /// Class StringExtension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Uppercases the first.
        /// </summary>
        /// <param name="value">The string to uppercase.</param>
        /// <returns>Uppercase string.</returns>
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

        /// <summary>
        /// Counts the specified text.
        /// </summary>
        /// <param name="value">The string contains text to count.</param>
        /// <param name="countText">The text to count.</param>
        /// <returns>The number of text to count occurring in string.</returns>
        public static int Count(this string value, string countText)
        {
            return (value.Length - value.Replace(countText, string.Empty).Length) / countText.Length;
        }

        /// <summary>
        /// Gets the line break total.
        /// </summary>
        /// <param name="value">The string to count line-breaks.</param>
        /// <returns>Total line-breaks.</returns>
        public static int GetLineBreakTotal(this string value)
        {
            return value.Count("\n") + 1;
        }

        public static string ToInvariantString(this string value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }      
    }
}