namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.BetList.Core.Entities;

    public class Choice9486 : Choice9484
    {
        private const string BetTeamGreater = "7";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.Handicap1.ToString(CultureInfo.InvariantCulture);
            if (ticket.BetTeam == BetTeamGreater)
            {
                Template.betTeam = "> " + Template.betTeam;
            }
        }
    }
}