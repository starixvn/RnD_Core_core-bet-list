namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Live Roulette.
    /// </summary>
    public class Choice1102 : Choice1101
    {
        protected override string GetLeagueName()
        {
            return CoreLang.PartyRoulette;
        }

        protected override string GetBetTypeName(string betTeam, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetRouletteBetItemNameById(betTeam);
        }
    }
}