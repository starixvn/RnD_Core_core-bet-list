namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Bet Type: HT/FT.
    /// </summary>
    public class Choice16 : Choice1
    {
        private readonly Dictionary<string, string> betTeamPatterns
                = new Dictionary<string, string>
                {
                    { "0:0", CoreBetList.dd },
                    { "0:1", CoreBetList.dh },
                    { "0:2", CoreBetList.da },
                    { "1:0", CoreBetList.hd },
                    { "1:1", CoreBetList.hh },
                    { "1:2", CoreBetList.ha },
                    { "2:0", CoreBetList.ad },
                    { "2:1", CoreBetList.ah },
                    { "2:2", CoreBetList.aa }
                };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam =
                (!string.IsNullOrWhiteSpace(ticket.BetTeam) && betTeamPatterns.ContainsKey(ticket.BetTeam))
                ? betTeamPatterns[ticket.BetTeam]
                : string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}