namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Choice803 : Choice801
    {
        private const string AfterText = "After";

        protected override string GetBetTypeTime(ITicket ticket)
        {
            var defaultBetTypeTime = GetTransDescElementByName(ticket, "bettypetime");

            return string.Format("{0} {1}", AfterText, defaultBetTypeTime);
        }
    }
}
