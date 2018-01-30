namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Jackpot Contribution
    /// </summary>
    public class Choice2198 : Choice2199
    {
        private const string OpenTagJackpotContributionInfo = "<div class=\"favorite\">";
        private const string CloseTagJackpotContribution = "</div>";
        private const string NewLine = "\n";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            var rate = $"{ConvertByBetType.CommValue(ticket.Odds)}%";
            Template.betTeam +=
                $"{OpenTagJackpotContributionInfo}{CoreBetList.stake}: {ticket.OddsSpread} {ticket.Currency} @ {CoreBetList.rate} {rate}{CloseTagJackpotContribution}";
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();

            Template.betTeam = Template.betTeam
                .Replace(OpenTagJackpotContributionInfo, NewLine)
                .Replace(CloseTagJackpotContribution, string.Empty);
        }
    }
}