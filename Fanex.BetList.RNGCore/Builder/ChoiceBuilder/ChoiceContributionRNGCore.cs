namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class ChoiceContributionRNGCore : ChoiceJackpotRNGCore
    {
        private const string OpenTagJackpotContributionInfo = "<div class=\"favorite\">";
        private const string CloseTagJackpotContribution = "</div>";
        private const string NewLine = "\n";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);
            
            var stake = FormatDecimal(ticket.OddsSpread);
            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.betTeam +=
                $"{OpenTagJackpotContributionInfo}{CoreBetList.stake}: {stake} {ticket.Currency}{CloseTagJackpotContribution}";
            }
            else
            {
                var rate = $"{FormatDecimal(ticket.Odds * 100)}%";
                Template.betTeam +=
                $"{OpenTagJackpotContributionInfo}{CoreBetList.stake}: {stake} {ticket.Currency} @ {CoreBetList.rate} {rate}{CloseTagJackpotContribution}";
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam
                .Replace(OpenTagJackpotContributionInfo, NewLine)
                .Replace(CloseTagJackpotContribution, string.Empty);

            base.AdjustBetTeamToRTF();
        }

        private static string FormatDecimal(decimal number)
            => string.Format(Formatter.DECIMALFORMAT40, number);
    }
}