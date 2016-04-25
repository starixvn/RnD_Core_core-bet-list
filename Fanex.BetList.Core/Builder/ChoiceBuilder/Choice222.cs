namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    public class Choice222 : Choice221
    {
        protected override string GenerateMatchMinute(decimal handicap1)
        {
            return string.Format("({0}:00-{1}:59)", FormatMatchMinute(handicap1), FormatMatchMinute(handicap1 + 4));
        }
    }
}