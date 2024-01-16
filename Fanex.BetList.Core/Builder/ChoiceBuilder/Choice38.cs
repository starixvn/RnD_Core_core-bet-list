namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.StringExt;

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

        protected override void BuildScore(ITicket ticket)
        => Template.Score = null;
        
        public override string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
            => ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

        private string BuildSingleMatchParlayDetailLink(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            var detailLink =
                "<div class=\"combinationLink\">" +
                    $"<span onclick=\"showSingleMatchParlayDetail('{ticket.TransId}', '{ticket.CustId}', '{ticket.Status}', {ticketHelper.Index})\">" +
                        $"<a href=\"javascript:void('');\">{betTypeName}</a>" +
                    "</span>" + BuildLiveScore(ticket) +
                "</div>";

            var detailBlock = $"<div id=\"divEvent_{ticket.TransId}{ticketHelper.Index}\" style=\"display: none\" ></div>";

            return detailLink + detailBlock;
        }

        private string BuildLiveScore(ITicket ticket)
        {
            var score = new Choice_Score_Block();
            if (ticket.SportTypeId == SportsbookSportTypes.Soccer)
            {
                score.homeScore = ticket.LiveHomeScore.ToString();
                score.awayScore = ticket.LiveAwayScore.ToString();
            }
            else
            {                
                score.SetValue($"<span class=\"{Favorite}\"> [{CoreBetList.Live}]</span>");
            }

            score.Visible = ticket.IsLive;

            return score.ToString();
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