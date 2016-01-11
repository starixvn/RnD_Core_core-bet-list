namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Charge Commission.
    /// </summary>
    public class Choice500 : Choice1
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            BuildBetTeam(ticket, ticketHelper, ticketData);

            BuildBetTeamClassNameAndHandicap(ticket);

            BuildScore(ticket);

            BuildBetType(ticket, ticketHelper, ticketData);

            BuildMatch(ticket, ticketHelper);

            BuildSport(ticket, ticketHelper);

            BuildLeague(ticket, ticketHelper);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = string.Empty;

            Template.Handicap = null;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }
    }
}