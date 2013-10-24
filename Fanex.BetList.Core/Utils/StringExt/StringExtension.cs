// ---------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.12.10">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Utils.StringExt
{
    /// <summary>
    /// Class StringExtension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Uppercases the first.
        /// </summary>
        /// <param name="s">The string to uppercase.</param>
        /// <returns>Uppercase string.</returns>
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        /// <summary>
        /// Counts the specified text.
        /// </summary>
        /// <param name="s">The string contains text to count.</param>
        /// <param name="countText">The text to count.</param>
        /// <returns>The number of text to count occurring in string.</returns>
        public static int Count(this string s, string countText)
        {
            return (s.Length - s.Replace(countText, string.Empty).Length) / countText.Length;
        }

        /// <summary>
        /// Gets the line break total.
        /// </summary>
        /// <param name="s">The string to count line-breaks.</param>
        /// <returns>Total line-breaks.</returns>
        public static int GetLineBreakTotal(this string s)
        {
            return s.Count("\n") + 1;
        }
    }
}