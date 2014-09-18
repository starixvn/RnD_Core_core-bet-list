namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Bet Type: HT/FT.
    /// </summary>
    public class Choice16 : Choice1
    {
        private static readonly Dictionary<string, string> BetTeamPatterns
                = new Dictionary<string, string>
                {
                    { "0:0", "DD" },
                    { "0:1", "DH" },
                    { "0:2", "DA" },
                    { "1:0", "HD" },
                    { "1:1", "HH" },
                    { "1:2", "HA" },
                    { "2:0", "AD" },
                    { "2:1", "AH" },
                    { "2:2", "AA" }
                };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam =
                (!string.IsNullOrWhiteSpace(ticket.BetTeam) && BetTeamPatterns.ContainsKey(ticket.BetTeam))
                ? BetTeamPatterns[ticket.BetTeam]
                : string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}