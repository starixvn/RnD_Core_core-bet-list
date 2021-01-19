namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

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
            => !string.IsNullOrEmpty(ticket.ExtraInfo) && ticket.ExtraInfo.ToLowerInvariant().Contains(JackpotInfo);

        private static bool IsSummaryTicket(ITicket ticket) => string.IsNullOrWhiteSpace(ticket.BetCheck);

        private static string BuildShowJackpotWonDetails(ITicket ticket)
        {
            var indexOfJackpotTransId = ticket.ExtraInfo.LastIndexOf(JackpotTransIdInfo, StringComparison.InvariantCultureIgnoreCase) + 4;
            var transId = ticket.ExtraInfo.Substring(indexOfJackpotTransId, ticket.ExtraInfo.Length - indexOfJackpotTransId);
            var detailLinkMarkup =
                $"<div class=\"detail sabaclub-detail\" onclick=\"showSabaClubJackpotWonDetails('{transId}', '{ticket.CustId}', '{ticket.BetTypeId}');\">" +
                $"<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{CoreBetList.JackpotWonDetails}</a>" +
                $"</div>";

            return detailLinkMarkup;
        }

        private static Choice_League_Block BuildSabaClubeDetails(ITicket ticket)
        {
            string[] detailsFunctionDivCompositions =
            {
                "<div class=\"detail saba-club-detail\" onclick=\"showSabaClubDetails(",
                ticket.TransId.ToString(CultureInfo.InvariantCulture),
                ", ",
                "'",
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "'",
                ", ",
                ticket.CustId.ToString(CultureInfo.InvariantCulture),
                ", ",
                GetTransactionType(ticket),
                ");\">",
                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };
            string detailLinkMarkup = string.Join(null, detailsFunctionDivCompositions);
            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }

        private static string GetTransactionType(ITicket ticket)
        {
            // @TransType
            // + 0 : All (DRAW/WON/LOSE/Reject/Void/Refund/Running/Waiting)
            // + 1 : DRAW/WON/LOSE/Refund
            // + 2 : Reject/Void
            // + 3 : Running/Waiting
            // Use this because web server and db are in the same timezone
            if (ticket.WinlostDate.Date >= DateTime.Today.AddDays(-1))
            {
                switch (ticket.Status.ToLowerInvariant())
                {
                    case BetStatus.Won:
                    case BetStatus.Lose:
                    case BetStatus.Draw:
                    case BetStatus.Refund:
                        return "1";

                    case BetStatus.Reject:
                    case BetStatus.Void:
                        return "2";

                    case BetStatus.Running:
                    case BetStatus.Waiting:
                        return "3";

                    default:
                        return "0";
                }
            }

            return "0"; // Previous days Ticket
        }
    }
}