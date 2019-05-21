namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using System.Collections.Generic;

    /// <summary>
    /// Bet Type: Single Match Parlay - Bet Builder
    /// </summary>
    public class Choice38 : Choice1
    {
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

        public override string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
            => ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

        private static string BuildSingleMatchParlayDetailLink(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            var detailLink =
                "<div class=\"combinationLink\">" +
                    $"<div onclick=\"showSingleMatchParlayDetail('{ticket.TransId}', '{ticket.CustId}', '{ticket.Status}', {ticketHelper.Index})\">" +
                        $"<a href=\"javascript:void('');\">{betTypeName}</a>" +
                    "</div>" +
                "</div>";

            var detailBlock = $"<div id=\"divEvent_{ticket.TransId}{ticketHelper.Index}\" style=\"display: none\" ></div>";

            return detailLink + detailBlock;
        }

        #endregion BUILD HTML

        #region BUILD EXCEL

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var betTypeName = Template.BetType.betTypeName.StripHTML();

            rtfHelper.RTFRenderer.AddText(betTypeName, rtfHelper.PosFont);
        }

        #endregion BUILD EXCEL
    }
}