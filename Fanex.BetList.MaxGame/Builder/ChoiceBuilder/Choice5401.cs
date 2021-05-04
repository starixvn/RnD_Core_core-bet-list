namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice5401 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}</span>";

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam += $" {ticket.BetCheck}";
            }
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName = null;

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.League.Append(BuildPGSoftDetailsLink(ticket));
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
           => Template.Match = null;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;


        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = Template.betTeam.StripHTML();

        private static string BuildPGSoftDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"{ticket.TransId}, '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}