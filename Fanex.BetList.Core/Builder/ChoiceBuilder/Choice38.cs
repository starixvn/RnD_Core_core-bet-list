namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;

    /// <summary>
    /// Bet Type: Single Match Parlay - Bet Builder
    /// </summary>
    public class Choice38 : Choice1
    {
        private const string EventDateHtmlBeginTag = "<div class=\"event-date\">";
        private const string EventDateHtmlEndTag = "</div>";
        private const string NewLineExcel = "\n";

        #region BUILD HTML

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = BuildSingleMatchParlayDetailLink(ticket, ticketHelper);
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);
            var eventDate = EventDateHtmlBeginTag + Formatter.FormatDateTimeEn12ShortMonthNoSec(ticket.EventDate) + EventDateHtmlEndTag;

            Template.League.LeagueName.Append(eventDate);
        }

        private static string BuildSingleMatchParlayDetailLink(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            var detailLink =
                $"<div onclick=\"showSingleMatchParlayDetail('{ticket.TransId}', '{ticket.CustId}', {ticketHelper.Index})\">" +
                    $"<a href=\"javascript:void('');\">{betTypeName}</a>" +
                $"</div>";

            var detailBlock = $"<div id=\"divEvent_{ticket.TransId}{ticketHelper.Index}\" style=\"display: none\" ></div>";

            return detailLink + detailBlock;
        }

        #endregion BUILD HTML

        #region BUILD EXCEL

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var betTypeName = Template.BetType.betTypeName.StripHTML();

            rtfHelper.RTFRenderer.AddText(betTypeName);
        }

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            base.BuildMatchInfo(ref rtfHelper);

            rtfHelper.RTFRenderer.AddText(NewLineExcel + BuildExcelEventDate());
        }

        private string BuildExcelEventDate()
        {
            var eventDateBlockIndex = Template.League.LeagueName.ToString()
                    .LastIndexOf(EventDateHtmlBeginTag, StringComparison.InvariantCulture);
            var eventDateBlock = Template.League.LeagueName.ToString()
                    .Remove(0, eventDateBlockIndex);

            return eventDateBlock.StripHTML();
        }

        #endregion BUILD EXCEL
    }
}