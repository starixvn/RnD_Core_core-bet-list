namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    /// <summary>Playtech Live with Jackpot Contribution</summary>
    public class Choice15798 : Choice15799
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);
            var stake = string.Format(Formatter.DECIMALFORMAT40, ticket.OddsSpread);
            if (IsDetailTicket(ticket))
            {
                var rate = $"{string.Format(Formatter.DECIMALFORMAT40, ticket.Odds * 100)}%";
                Template.betTeam +=
                $"{LiveCasinoConstants.OpenTagJackpotContributionInfo}{CoreBetList.stake}: {stake} {ticket.Currency} @ {CoreBetList.rate} {rate}{LiveCasinoConstants.CloseTagDiv}";
            }
            else
            {
                Template.betTeam +=
                $"{LiveCasinoConstants.OpenTagJackpotContributionInfo}{CoreBetList.stake}: {stake} {ticket.Currency}{LiveCasinoConstants.CloseTagDiv}";
            }
        }
    }
}