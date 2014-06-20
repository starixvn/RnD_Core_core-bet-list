namespace Fanex.BetList.Core.Entities
{
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
    }
}