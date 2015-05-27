namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice805 : Choice801
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betChoice = GetBetChoice(ticket);

            if (string.IsNullOrWhiteSpace(betChoice))
            {
                Template.betTeam = string.Empty;

                return;
            }

            var betChoices = betChoice.Split(',');

            var betTeamName = ticketHelper.GetResourceData(GetResourceId(), betChoices[1]);
            Template.betTeam = string.Format("{0} {1} {2}", betChoices[0], betTeamName, betChoices[2]);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.Visible = false;
        }
    }
}
