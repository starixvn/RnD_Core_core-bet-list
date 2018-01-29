namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// Jackpot Game
    /// </summary>
    public class Choice2179 : Choice2101
    {
        private const string JackpotInfo = "jp=1";
        private const string JackpotTransIdInfo = "jpw=";

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            var isDetailedTicket = !string.IsNullOrEmpty(ticket.BetCheck);

            if (isDetailedTicket && TicketHasJackpotInfo(ticket))
            {
                Template.League.Append(BuildShowProgressiveJackpotWonDetails(ticket));
            }
        }

        private static bool TicketHasJackpotInfo(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.ExtraInfo) && ticket.ExtraInfo.ToLowerInvariant().Contains(JackpotInfo);

        private static Choice_League_Block BuildShowProgressiveJackpotWonDetails(ITicket ticket)
        {
            var indexOfJackpotTransId = ticket.ExtraInfo.LastIndexOf(JackpotTransIdInfo, StringComparison.InvariantCultureIgnoreCase) + 4;
            var transId = ticket.ExtraInfo.Substring(indexOfJackpotTransId, ticket.ExtraInfo.Length - indexOfJackpotTransId);
            var detailLinkMarkup = $"<div class=\"detail rng-casino-detail\" onclick=\"showProgressiveJackpotWonDetails('{transId}');\">" +
                $"<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{CoreBetList.ProgressiveJackpotWonDetails}</a></div>";

            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }
    }
}