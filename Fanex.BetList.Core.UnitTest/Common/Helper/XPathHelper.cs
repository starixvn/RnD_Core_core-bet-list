// --------------------------------------------------------------------------
// <copyright file="XPathHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.6.4">Created</change>
// </history>

namespace Fanex.BetList.Core.UnitTest.Common.Helper
{
    using System.Linq;
    using HtmlAgilityPack;

    /// <summary>
    /// The extension class for get HTML from XPath.
    /// </summary>
    public static class XPathHelper
    {
        /// <summary>
        /// Gets the outer HTML.
        /// </summary>
        /// <param name="htmlDocument">The HTML document.
        /// <![CDATA[
        ///     <div>
        ///         <span class="favorite">
        ///             Milan Reds
        ///             <span class="handicap"> -1</span>
        ///         </span>
        ///     </div>
        /// ]]></param>
        /// <param name="xmlPath">The xml path value.<![CDATA[/div/span[1]/span[1]]]></param>
        /// <returns>The HTML string.<![CDATA[<span class="handicap"> -1</span>]]> </returns>
        public static string GetOuterHtml(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            return htmlNode.OuterHtml;
        }

        /// <summary>
        /// Gets the inner HTML.
        /// </summary>
        /// <param name="htmlDocument">The HTML document.
        /// <![CDATA[
        ///     <div>
        ///         <span class="favorite">
        ///             Milan Reds
        ///             <span class="handicap"> -1</span>
        ///         </span>
        ///     </div>
        /// ]]></param>
        /// <param name="xmlPath">The xml path value.<![CDATA[/div/span[1]/span[1]]]></param>
        /// <returns>The HTML string.<![CDATA[ -1]]> </returns>
        public static string GetInnerHtml(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            return htmlNode.InnerHtml;
        }

        public static string GetPreviousSiblingOuterHtml(this HtmlDocument htmlDocument, string xmlPath, int length = 1)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            for (int i = 0; i < length; i++)
            {
                htmlNode = htmlNode.PreviousSibling;
            }

            return htmlNode.OuterHtml;
        }

        /// <summary>
        /// Gets the inner HTML.
        /// </summary>
        /// <param name="htmlDocument">The HTML document.
        /// <![CDATA[
        ///     <div>
        ///         <span class="favorite">
        ///             Milan Reds
        ///             <span class="handicap"> -1</span>
        ///         </span>
        ///     </div>
        /// ]]></param>
        /// <param name="xmlPath">The xml path value.<![CDATA[/div/span[1]/span[1]]]></param>
        /// <returns>The HTML string.<![CDATA[ -1]]> </returns>
        public static string GetFirstChildInnerHtml(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            return htmlNode.FirstChild.InnerHtml;
        }

        public static string GetFirstChildOuterHtml(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            return htmlNode.FirstChild.OuterHtml;
        }

        public static string GetFirstChildNextSiblingOuterHtml(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            return htmlNode.FirstChild.NextSibling.OuterHtml;
        }

        public static string GetHtmlClassValue(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath).FirstOrDefault();

            return htmlNode.Attributes["class"].Value;
        }

        public static bool IsExist(this HtmlDocument htmlDocument, string xmlPath)
        {
            HtmlNodeCollection htmlNode = htmlDocument.DocumentNode.SelectNodes(xmlPath);

            return !ReferenceEquals(htmlNode, null);
        }
    }
}
