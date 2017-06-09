namespace Fanex.BetList.Core.Utils
{
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class RTFNumber.
    /// </summary>
    public class RTFNumber
    {
        private readonly IFont _posFontText;
        private readonly IFont _negFontText;
        private readonly RtfTextRender _renderContent;

        public RTFNumber(RtfTextRender render, IFont posFont, IFont negFont)
        {
            _renderContent = render;
            _posFontText = posFont;
            _negFontText = negFont;
        }

        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="text">The text value.</param>
        public void Render(string text)
        {
            var removeText = text.Replace(ConvertByBetType.FontOpenTag, string.Empty).Replace(ConvertByBetType.FontCloseTag, string.Empty);

            _renderContent.AddText(removeText, text.Length == removeText.Length ? _posFontText : _negFontText);
        }
    }
}