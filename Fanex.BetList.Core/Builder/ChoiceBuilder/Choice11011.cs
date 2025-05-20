namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Choice11011 : Choice1
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.awayTeam = string.Empty;
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            Template.Match.VS = null;

            var actionName = string.Empty;
            if (ticket.ExtraInfo.HasAttributeInXmlTag("p81", "a"))
            {
                var actionId = ticket.ExtraInfo.GetXmlTagAttributeValue("p81", "a");
                actionName = ticketHelper.GetResourceData("SBC_Credit", actionId);
            }
            Template.Match.homeTeam = actionName;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
                    => Template.betTeam = string.Empty;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
            => Template.Handicap.Hide();

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName = null;


        protected override void BuildEventKickoffDateTime(ITicket ticket)
            => Template.EventDate = null;
    }
}