namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Resources;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Choice7901 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = string.Format(Formatter.DECIMALFORMAT, ticket.Handicap1);
            Template.betTeamClassName = IsOverTicket(ticket.BetTeam) ? Favorite : Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = IsOverTicket(ticket.BetTeam) ? CoreBetList.over : CoreBetList.under;

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var matchNo = $"<div class='handicap'>{string.Format(BitcoinLang.Number, ticket.MatchId)}</div>";
            var expireDate = $"<div class='handicap'>{ticket.EventDate.Value.ToString("M/dd/yyyy h:mm tt")}</div>";
            Template.Match.homeTeam = string.Empty;
            Template.Match.VS = null;
            Template.Match.awayTeam = string.Empty;
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            Template.Match.Append(matchNo);
            Template.Match.Append(expireDate);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            var rows = Template.Match.ToString().SplitHTMLIntoRows();

            foreach (var row in rows)
            {
                rtfHelper.RTFRenderer.AddText("\n" + row);
            }
        }

        private static bool IsOverTicket(string betTeam)
            => betTeam.ToLowerInvariant() == BetTeamValue.O;
    }
}