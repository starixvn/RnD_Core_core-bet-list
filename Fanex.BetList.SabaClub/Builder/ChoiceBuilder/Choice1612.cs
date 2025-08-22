namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice1612 : Choice1
    {
        private const string JackpotTransIdInfo = "jpw=";
        private const string JackpotInfo = "jp=1";
        private readonly Regex htmlTagMatching = new Regex("</?[^<>]*>");

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var bettypeSection = $"<span class=\"handicap\">{ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}</span>";
            Template.betTeam = IsSummaryTicket(ticket) ? bettypeSection : $"{bettypeSection} {ticket.BetCheck}";
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var isSummary = IsSummaryTicket(ticket);
            if (isSummary)
            {
                Template.League.Append(BuildSabaClubeDetails(ticket));
            }

            if (!isSummary && TicketHasJackpotInfo(ticket))
            {
                Template.League.Append(BuildShowJackpotWonDetails(ticket));
            }

            Template.League.LeagueName = null;
        }

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = htmlTagMatching.Replace(Template.betTeam, string.Empty);

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        private static bool TicketHasJackpotInfo(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.JackpotInfo) && ticket.JackpotInfo.ToLowerInvariant().Contains(JackpotInfo);

        private static bool IsSummaryTicket(ITicket ticket) => string.IsNullOrWhiteSpace(ticket.BetCheck);

        private static string BuildShowJackpotWonDetails(ITicket ticket)
        {
            var indexOfJackpotTransId = ticket.JackpotInfo.LastIndexOf(JackpotTransIdInfo, StringComparison.InvariantCultureIgnoreCase) + 4;
            var transId = ticket.JackpotInfo.Substring(indexOfJackpotTransId, ticket.JackpotInfo.Length - indexOfJackpotTransId);
            var parames = new List<object>
            {
                $"'{transId}'",
                $"'{ticket.CustId}'",
                $"'{ticket.BetTypeId}'",
                $"'{ticket.WinlostDate.ToString(CultureInfo.InvariantCulture)}'"
            };
            var parameterList = string.Join(", ", parames);
            var detailLinkMarkup =
                $"<div class=\"detail sabaclub-detail\" onclick=\"showSabaClubJackpotWonDetails({parameterList});\">" +
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.JackpotWonDetails}</a>" +
                $"</div>";

            return detailLinkMarkup;
        }

        private static Choice_League_Block BuildSabaClubeDetails(ITicket ticket)
        {
            string[] detailsFunctionDivCompositions =
            {
                "<div class=\"detail saba-club-detail\" onclick=\"showSabaClubDetails('",
                ticket.TransId.ToString(CultureInfo.InvariantCulture),
                "', ",
                "'",
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "'",
                ", ",
                ticket.CustId.ToString(CultureInfo.InvariantCulture),
                ", ",
                ticket.GetTransactionType(),
                ");\">",
                "<a class=\"detail-link\" href=\"javascript:void('');\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };
            string detailLinkMarkup = string.Join(null, detailsFunctionDivCompositions);
            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }
    }
}