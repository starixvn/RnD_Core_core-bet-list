namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Live Roulette.
    /// </summary>
    public class Choice1102 : Choice1101
    {
        protected override string GetBetTeamName(string betTeam, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetRouletteBetItemNameById(betTeam);
        }
    }
}