namespace Fanex.BetList.Core.Utils
{
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class RTFNumber.
    /// </summary>
    public class RTFNumber
    {
        private IFont _posFontText;
        private IFont _negFontText;
        private RtfTextRender _renderContent;

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
            string removeText = text.Replace(ConvertByBetType.FontOpenTag, string.Empty).Replace(ConvertByBetType.FontCloseTag, string.Empty);
            if (text.Length == removeText.Length)
            {
                _renderContent.AddText(removeText, _posFontText);
            }
            else
            {
                _renderContent.AddText(removeText, _negFontText);
            }
        }
    }
}