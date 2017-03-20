namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    /// Live Roulette.
    /// </summary>
    public class Choice1102 : Choice1101
    {
        protected override string GetBetTeamName(ITicketData ticketData, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetRouletteBetItemNameById(ticketData.BetTeam);
        }
    }
}