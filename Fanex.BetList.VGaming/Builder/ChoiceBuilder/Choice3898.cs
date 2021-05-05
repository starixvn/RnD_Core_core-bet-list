namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Jackpot Contribution
    /// </summary>
    public class Choice3898: Choice3899
    {
        private const string OpenTagJackpotContributionInfo = "<div class=\"favorite\">";
        private const string CloseTagJackpotContribution = "</div>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            var rate = $"{string.Format(Formatter.DECIMALFORMAT40, ticket.Odds * 100)}%";
            var stake = string.Format(Formatter.DECIMALFORMAT40, ticket.OddsSpread); 

            Template.betTeam +=
                $"{OpenTagJackpotContributionInfo}{CoreBetList.stake}: {stake} {ticket.Currency} @ {CoreBetList.rate} {rate}{CloseTagJackpotContribution}";
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam
                .Replace(OpenTagJackpotContributionInfo, "\n")
                .Replace(CloseTagJackpotContribution, string.Empty);

            base.AdjustBetTeamToRTF();
        }
    }
}
