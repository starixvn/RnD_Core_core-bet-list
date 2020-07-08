namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Bet Type: Double Quarter Winner.
    /// </summary>
    public class Choice642 : Choice1
    {
        private readonly Dictionary<string, string> betTeamPatterns
                = new Dictionary<string, string>
                {
                    { "h12", CoreBetList.h12 },
                    { "h13", CoreBetList.h13 },
                    { "h14", CoreBetList.h14 },
                    { "h23", CoreBetList.h23 },
                    { "h24", CoreBetList.h24 },
                    { "h34", CoreBetList.h34 },
                    { "a12", CoreBetList.a12 },
                    { "a13", CoreBetList.a13 },
                    { "a14", CoreBetList.a14 },
                    { "a23", CoreBetList.a23 },
                    { "a24", CoreBetList.a24 },
                    { "a34", CoreBetList.a34 },
                    { "aos12", CoreBetList.aos12 },
                    { "aos13", CoreBetList.aos13 },
                    { "aos14", CoreBetList.aos14 },
                    { "aos23", CoreBetList.aos23 },
                    { "aos24", CoreBetList.aos24 },
                    { "aos34", CoreBetList.aos34 }
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