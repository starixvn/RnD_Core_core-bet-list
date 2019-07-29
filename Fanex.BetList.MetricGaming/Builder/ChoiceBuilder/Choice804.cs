namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Every Single Minute
    /// </summary>
    public class Choice804 : Choice801
    {
        protected override string GetBetTypeTime(ITicket ticket)
        {
            return GetTransDescElementByName(ticket, "bettypetime");
        }
    }
}