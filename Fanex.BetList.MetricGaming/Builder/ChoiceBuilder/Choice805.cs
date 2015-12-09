namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice805 : Choice804
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var bettypeTime = GetBetTypeTime(ticket);

            if (string.IsNullOrWhiteSpace(bettypeTime))
            {
                Template.betTeam = string.Empty;

                return;
            }

            var betTeamName = ticketHelper.GetResourceData(GetResourceId(), GetBetChoice(ticket));
            var bettypeTimes = bettypeTime.Split(',');
            var teamName = ticketHelper.GetTeamNameById(bettypeTimes[0]);

            Template.betTeam = string.Format("{0} {1} {2}%", teamName, betTeamName, bettypeTimes[1]);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.Visible = false;
        }
    }
}