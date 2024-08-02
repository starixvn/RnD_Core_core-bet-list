namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System;
    using System.Collections.Generic;

    public class Choice10450 : ChoiceRNG
    {
        public const string GameGroundRefId = "NS_RNG_GG";
        public const string GameTypeRefId = "NS_RNG_GT";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.BetTypeId.ToString())}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {GetTransId3rd(ticket.BetCheck)}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(GameGroundRefId, ticket.BetTypeId.ToString())}</span>");

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildNextspinDetailsLink(ticket));
            }
        }

        private static string BuildNextspinDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail nextspin-detail\" onclick=\"showNextspinBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}