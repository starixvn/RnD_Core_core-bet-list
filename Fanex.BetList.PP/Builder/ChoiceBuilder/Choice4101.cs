namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice4101 : Choice1
    {
        private const string JackpotInfo = "jp=1";
        private const string JackpotTransIdInfo = "jpw=";
        public const string GameGroupRefId = "PP_RNG_GG";
        public const string GameTypeRefId = "PP_RNG_GT";

        private readonly Regex htmlTagMatching = new Regex("</?[^<>]*>");

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.BetTypeId.ToString())}</span>";

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam += " " + ticket.BetCheck;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(GameGroupRefId, ticket.BetTypeId.ToString())}</span>");

            var isSummaryTicket = string.IsNullOrEmpty(ticket.BetCheck);
            if (isSummaryTicket)
            {
                Template.League.Append(BuildPPDetailsLink(ticket));
            }

            var isDetailedTicket = !isSummaryTicket;
            if (isDetailedTicket && TicketHasJackpotInfo(ticket))
            {
                Template.League.Append(BuildShowJackpotWonDetails(ticket));
            }
        }

        private static bool TicketHasJackpotInfo(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.ExtraInfo) && ticket.ExtraInfo.ToLowerInvariant().Contains(JackpotInfo);

        private static string BuildShowJackpotWonDetails(ITicket ticket)
        {
            var indexOfJackpotTransId = ticket.ExtraInfo.LastIndexOf(JackpotTransIdInfo, StringComparison.InvariantCultureIgnoreCase) + 4;
            var transId = ticket.ExtraInfo.Substring(indexOfJackpotTransId, ticket.ExtraInfo.Length - indexOfJackpotTransId);
            var detailLinkMarkup =
                $"<div class=\"detail pp-detail\" onclick=\"showPPJackpotWonDetails('{transId}', '{ticket.CustId}', '{ticket.BetTypeId}', '{ticket.WinlostDate}');\">" +
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.JackpotWonDetails}</a>" +
                $"</div>";

            return detailLinkMarkup;
        }

        private static string BuildPPDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail pp-detail\" onclick=\"showPPDetails({parameterList});\">{detailLink}</div>";
        }

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = htmlTagMatching.Replace(Template.betTeam, string.Empty);

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            string sportTypeName = Template.League.sportTypeName;
            string gameGroupName = htmlTagMatching.Replace(Template.League.LeagueName.ToString(), string.Empty);

            if (!string.IsNullOrEmpty(sportTypeName) && !string.IsNullOrEmpty(gameGroupName))
            {
                rtfHelper.RTFRenderer.AddText($"\n{sportTypeName}{gameGroupName}");
            }
        }
    }
}