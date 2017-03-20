namespace Fanex.BetList.Core.Entities
{
    public class RTFFormattingRun
    {
        public RTFFormattingRun(int beginIndex, int length, short fontIndex)
        {
            BeginIndex = beginIndex;
            Length = length;
            FontIndex = fontIndex;
        }

        public int BeginIndex { get; }

        public int Length { get; }

        public short FontIndex { get; }
    }
}