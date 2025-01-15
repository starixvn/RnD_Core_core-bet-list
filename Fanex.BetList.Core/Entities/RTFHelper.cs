namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;
    using BetListNPOI.SS.UserModel;
    using Utils;

    public struct RTFHelper
    {
        public IFont NormalFont { get; set; }

        public IFont PosFont { get; set; }

        public IFont NegFont { get; set; }

        public IFont NormalFontCrossed { get; set; }

        public IFont PosFontCrossed { get; set; }

        public IFont NegFontCrossed { get; set; }

        public IRtfTextRender RTFRenderer { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>RTFHelper object.</returns>
        public RTFHelper Clone()
        {
            RTFHelper tmp = new RTFHelper();

            tmp.PosFont = PosFont;
            tmp.NegFont = NegFont;
            tmp.NormalFont = NormalFont;

            tmp.NormalFontCrossed = NormalFontCrossed;
            tmp.PosFontCrossed = PosFontCrossed;
            tmp.NegFontCrossed = NegFontCrossed;

            tmp.RTFRenderer = RTFRenderer;

            return tmp;
        }

        public void ApplyFormattingRuns(IRichTextString text, List<RTFFormattingRun> formattingRuns, int beginIndex = 0)
        {
            foreach (var format in formattingRuns)
            {
                var begin = format.BeginIndex + beginIndex;
                var end = begin + format.Length;

                text.ApplyFont(begin, end, format.FontIndex);
            }
        }
    }
}