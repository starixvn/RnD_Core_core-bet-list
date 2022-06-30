namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;
    using NPOI.SS.UserModel;
    using Utils;

    public struct RTFHelper
    {
        public IFont NormalFont { get; set; }

        public IFont PosFont { get; set; }

        public IFont NegFont { get; set; }

        public IFont NormalFontCrossed { get; set; }

        public IFont PosFontCrossed { get; set; }

        public IFont NegFontCrossed { get; set; }

        public RtfTextRender RTFRenderer { get; set; }

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

            tmp.RTFRenderer = new RtfTextRender();

            return tmp;
        }
/*
        public List<RTFFormattingRun> GetFormattingRuns(IRichTextString text)
        {
            var formattingRuns = new List<RTFFormattingRun>();

            for (int formatIndex = 0; formatIndex < text.NumFormattingRuns; formatIndex++)
            {
                formattingRuns.Add(GetFormatingRun(text, formatIndex));
            }

            return formattingRuns;
        }
*/
        public void ApplyFormattingRuns(IRichTextString text, List<RTFFormattingRun> formattingRuns, int beginIndex = 0)
        {
            foreach (var format in formattingRuns)
            {
                var begin = format.BeginIndex + beginIndex;
                var end = begin + format.Length;

                text.ApplyFont(begin, end, format.FontIndex);
            }
        }
/*
        private static RTFFormattingRun GetFormatingRun(IRichTextString text, int formatIndex)
        {
            
            var beginIndex = text.GetIndexOfFormattingRun(formatIndex);
            short fontIndex = text.GetFontAtIndex(formatIndex);
            var length = 0;

            for (var i = beginIndex; i < text.Length; i++)
            {
                var currentFontIndex = text.GetFontAtIndex(i);

                if (currentFontIndex == fontIndex)
                {
                    length++;
                }
                else
                {
                    break;
                }
            }

            return new RTFFormattingRun(beginIndex, length, fontIndex);
        }*/
    }
}