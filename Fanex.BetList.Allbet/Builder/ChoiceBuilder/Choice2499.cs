namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using App_GlobalResources;

    public class Choice2499 : Choice2401
    {
        private const string OpenBetIdTag = "<span class='allbet-bet-id'>";
        private const string CloseBetIdTag = "</span>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!string.IsNullOrEmpty(ticket.MatchCode))
            {
                Template.BetType.betTypeName = $"{OpenBetIdTag}{CoreBetList.BetID}: {ticket.BetCheck}{CloseBetIdTag}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (string.IsNullOrEmpty(ticket.MatchCode))
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
