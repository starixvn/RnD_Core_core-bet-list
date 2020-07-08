namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Bet Type: Highest Scoring Quarter.
    /// </summary>
    public class Choice643 : Choice1
    {
        private readonly Dictionary<string, string> betTeamPatterns
                = new Dictionary<string, string>
                {
                    { "1q", CoreBetList._1q },
                    { "2q", CoreBetList._2q },
                    { "3q", CoreBetList._3q },
                    { "4q", CoreBetList._4q },
                    { "tie", CoreBetList.Tie }
                };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam =
                (!string.IsNullOrWhiteSpace(ticket.BetTeam) && betTeamPatterns.ContainsKey(ticket.BetTeam.ToLowerInvariant()))
                ? betTeamPatterns[ticket.BetTeam.ToLowerInvariant()]
                : string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}