namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice2499 : Choice2401
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName += " " + BuildGameId(ticket);
            }
        }
    }
}
