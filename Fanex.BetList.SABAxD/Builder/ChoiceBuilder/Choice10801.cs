namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice10801 : ChoiceRNG
    {
        private string SportName = string.Empty;
        private bool HideSabaText;
        
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            HideSabaText = ticketHelper.HideSabaText;
            SportName = HideSabaText
                ? ticketHelper.GetResourceData("Sports_Saba", ticket.SportTypeId.ToString())
                : ticketHelper.GetSportNameById(ticket.SportTypeId);

            base.BuildBetTeam(ticket, ticketHelper, ticketData);
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null)
            {
                return;
            }

            Template.League.sportTypeName = SportName;
        }

        protected override string BuildDetailsLink(ITicket ticket)
        {
            var detailLink =
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList =
                $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}, '{SportName}', '{HideSabaText}'";

            return $"<div class=\"detail sabaxd-detail\" onclick=\"showSabaxDDetails({parameterList});\">{detailLink}</div>";
        }
    }
}
