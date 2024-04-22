namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Collections.Generic;

    public class Choice8900 : ChoiceRNG
    {
        public const string GameGroundRefId = "SEAL_GG";
        public const string GameTypeRefId = "SEAL_GT";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc)}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {GetTransId3rd(ticket.BetCheck)}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(GameGroundRefId, ticket.TransDesc)}</span>");

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildSealEntertainmentDetailsLink(ticket));
            }
        }

        private static string BuildSealEntertainmentDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail seal-detail\" onclick=\"showSealEntertainmentBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}
