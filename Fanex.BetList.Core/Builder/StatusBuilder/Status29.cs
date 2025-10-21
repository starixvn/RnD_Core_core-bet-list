namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;
    using Fanex.BetList.Core.Constants;

    public class Status29 : Status1
    {
        private const int OddsSpreadForBetBuilderPlus = 3;

        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.refNo_MixParlay = ticket.RefNo;
            if(ticket.BetTeam == "1")
            {
                if(ticket.OddsSpread == OddsSpreadForBetBuilderPlus)
                {
                    Template.StatusResult.betId += $", '{BetTypes.MixParlay}-{OddsSpreadForBetBuilderPlus.ToString()}'";
                }
                else
                {
                    Template.StatusResult.betId += $", '{BetTypes.MixParlay}'";
                }
            }
        }

        protected override string GetHalfWonLost(string statusText, ITicket ticket)
        => statusText;
    }
}