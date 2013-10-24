// ---------------------------------------------------------------------------------
// <copyright file="RTFNumber.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.12.10">Create</change>
// </history>
// ---------------------------------------------------------------------------------

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

        /// <summary>
        /// Initializes a new instance of the <see cref="RTFNumber" /> class.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="posFont">The positive font.</param>
        /// <param name="negFont">The negative font.</param>
        public RTFNumber(RtfTextRender render, IFont posFont, IFont negFont)
        {
            this._renderContent = render;
            this._posFontText = posFont;
            this._negFontText = negFont;
        }

        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="text">The text to render.</param>
        public void Render(string text)
        {
            string removeText = text.Replace(ConvertByBetType.FontOpenTag, string.Empty).Replace(ConvertByBetType.FontCloseTag, string.Empty);
            if (text.Length == removeText.Length)
            {
                this._renderContent.AddText(removeText, this._posFontText);
            }
            else
            {
                this._renderContent.AddText(removeText, this._negFontText);
            }
        }
    }    
}