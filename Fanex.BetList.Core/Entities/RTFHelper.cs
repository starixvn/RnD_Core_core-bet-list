using BetListNPOI.SS.UserModel;
using Fanex.BetList.Core.Utils;
using System;
using System.Collections.Generic;

namespace Fanex.BetList.Core.Entities
{
    public struct RTFHelper : IEquatable<RTFHelper>
    {
        public IFont NormalFont { get; set; }

        public IFont PosFont { get; set; }

        public IFont NegFont { get; set; }

        public IFont NormalFontCrossed { get; set; }

        public IFont PosFontCrossed { get; set; }

        public IFont NegFontCrossed { get; set; }

        public RtfTextRender RTFRenderer { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RTFHelper other && Equals(other);
        }

        public bool Equals(RTFHelper other)
        {
            if (!ReferenceEquals(NormalFont, other.NormalFont))
            {
                return false;
            }

            if (!ReferenceEquals(PosFont, other.PosFont))
            {
                return false;
            }

            if (!ReferenceEquals(NegFont, other.NegFont))
            {
                return false;
            }

            if (!ReferenceEquals(NormalFontCrossed, other.NormalFontCrossed))
            {
                return false;
            }

            if (!ReferenceEquals(PosFontCrossed, other.PosFontCrossed))
            {
                return false;
            }

            return ReferenceEquals(NegFontCrossed, other.NegFontCrossed) && ReferenceEquals(RTFRenderer, other.RTFRenderer);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = CalculateHash(hash, NormalFont);
            hash = CalculateHash(hash, PosFont);
            hash = CalculateHash(hash, NegFont);
            hash = CalculateHash(hash, NormalFontCrossed);
            hash = CalculateHash(hash, PosFontCrossed);
            hash = CalculateHash(hash, NegFontCrossed);
            hash = CalculateHash(hash, RTFRenderer);
            return hash;
        }

        private static int CalculateHash(int currentHash, object obj)
        {
            return currentHash * 23 + (obj?.GetHashCode() ?? 0);
        }

        public static bool operator ==(RTFHelper left, RTFHelper right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RTFHelper left, RTFHelper right)
        {
            return !left.Equals(right);
        }

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