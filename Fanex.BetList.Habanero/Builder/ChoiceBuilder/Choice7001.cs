namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice7001 : Choice1
    {
        public const string GameGroundRefId = "HB_RNG_GG";
        public const string GameTypeRefId = "HB_RNG_GT";

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc)}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {ticket.BetCheck}";
            }
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(GameGroundRefId, ticket.TransDesc)}</span>");

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildHabaneroDetailsLink(ticket));
            }

            bool IsSummaryTicket(string betCheck)
                => !IsDetailTicket(betCheck);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
           => Template.Match = null;

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            var sportTypeName = Template.League.sportTypeName;
            var gameGroupName = Template.League.LeagueName.ToString().StripHTML();

            if (!string.IsNullOrEmpty(sportTypeName) && !string.IsNullOrEmpty(gameGroupName))
            {
                rtfHelper.RTFRenderer.AddText($"\n{sportTypeName}{gameGroupName}");
            }
        }

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = Template.betTeam.StripHTML();        

        private static bool IsDetailTicket(string betCheck)
            => !string.IsNullOrEmpty(betCheck);

        private static string BuildHabaneroDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail habanero-detail\" onclick=\"showHabaneroBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}